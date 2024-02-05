using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkolohaz_arpas
{
    internal class Emelet
    {
        public int Szint { get; set; }
        public string SzintNev { get; set; }
        public List<int> Szektorok { get; set; }

        public Emelet(string sor, int index)
        {
            var d = sor.Split("; ");
            Szint = index+1;
            SzintNev = d[0];
            Szektorok = new List<int>();
            for (int i = 1; i < d.Length; i++)
            {
                Szektorok.Add(int.Parse(d[i]));
            }
        }

        public override string ToString()
        {
            return $"{SzintNev,11} | {string.Join(", ", Szektorok)}";
        }
    }
}
