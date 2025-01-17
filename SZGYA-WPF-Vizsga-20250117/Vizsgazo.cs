using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZGYA_WPF_Vizsga_20250117
{
    internal class Vizsgazo
    {
        public static string[] OsztalyzatTipusok =
        {
            "IT és hálózatok írásbeli",
            "Programozás írásbeli",
            "Hálózatok A modul",
            "Hálózatok B modul",
            "Hálózatok C modul",
            "Hálózatok D modul",
            "Szóbeli angol",
            "Szóbeli IT",
        };
        public string Nev { get; set; }
        public Dictionary<string, int> Osztalyzatok { get; set; }
        public int Vegeredmeny => Convert.ToInt32(this.Osztalyzatok.Average(o => o.Value));
        public string Erdemjegy
        {
            get
            {
                foreach (var item in Osztalyzatok)
                {
                    if (item.Value < 51) return "elégtelen";
                }
                if (this.Vegeredmeny >= 81) return "jeles";
                else if (this.Vegeredmeny >= 71) return "jó";
                else if (this.Vegeredmeny >= 61) return "közepes";
                else return "elégséges";
            }
        }

        public Vizsgazo(string sor)
        {
            string[] adatok = sor.Split(";");
            this.Nev = adatok[0];
            this.Osztalyzatok = new();
            for (int i = 1; i < adatok.Length; i++)
            {
                Osztalyzatok.Add(OsztalyzatTipusok[i - 1], Convert.ToInt32(double.Parse(adatok[i]) * 100));
            }
        }

        public override string ToString()
        {
            return $"{this.Nev}\t{this.Vegeredmeny}\t{this.Erdemjegy}";
        }
    }
}
