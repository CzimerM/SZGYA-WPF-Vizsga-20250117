using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SZGYA_WPF_Vizsga_20250117
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Vizsgazo> vizsgazok;
        public MainWindow()
        {
            InitializeComponent();
            vizsgazok = new List<Vizsgazo>();
            var sr = new StreamReader("../../../src/adatok.txt");
            while (!sr.EndOfStream)
            {
                vizsgazok.Add(new Vizsgazo(sr.ReadLine()));
            }
            lblDbVizsgazo.Content = $"{vizsgazok.Count} vizsgázó adatait beolvastuk";
            lblSikeresVizsga.Content = "";
            lstbxVizsgazok.ItemsSource = vizsgazok.Select(v => v.Nev);

            lblLeggyengebbEredmeny.Content = "";
            lblLegjobbEredmeny.Content = "";
            lblVizsgaSikeres.Content = "";
        }

        private void btnSikeresVizsga_Click(object sender, RoutedEventArgs e)
        {
            lblSikeresVizsga.Content = $"{vizsgazok.Count(v => v.Erdemjegy != "elégtelen")} fő";
        }

        private void btnEredmenyIras_Click(object sender, RoutedEventArgs e)
        {
            using var sw = new StreamWriter("../../../src/vizsgaeredmenyek.txt",false,Encoding.UTF8);
            foreach (var vizsgazo in vizsgazok.Where(v => v.Erdemjegy != "elégtelen"))
            {
                sw.WriteLine(vizsgazo);
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txb = sender as TextBox;
            var talalat = vizsgazok.FirstOrDefault(v => v.Nev.ToLower().Contains(txb.Text.ToLower()));
            if (talalat == null)
            {
                MessageBox.Show($"Nincs ilyen tanuló a listában: {txb.Text}");
                return;
            }
            lblLegjobbEredmeny.Content = $"Legjobb eredménye: {talalat.Osztalyzatok.Max(l => l.Value)}%";
            lblLeggyengebbEredmeny.Content = $"Leggyengébb eredménye:  {talalat.Osztalyzatok.Min(l => l.Value)}%";
            lblVizsgaSikeres.Content = $"{talalat.Vegeredmeny} vizsgát tett";
        }
    }
}