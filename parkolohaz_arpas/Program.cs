using System.Linq;
using System.Text;
using System.Text.Unicode;

namespace parkolohaz_arpas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Emelet> emeletek = new();
            StreamReader sr = new(@"..\..\..\src\parkolohaz.txt");

            int index = 0;

            while (!sr.EndOfStream)
            {
                emeletek.Add(new(sr.ReadLine(), index));
                index++;
            }

            Console.WriteLine("7. feladat:\n");

            Console.WriteLine("Szint neve: | 1. szektor | 2. szektor | 3. szektor | 4. szektor | 5. szektor | 6. szektor");
            foreach (var item in emeletek)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"\n8. feladat: A(z) {emeletenkevesebbAuto(emeletek).Szint}. emeleten van a legkevesebb autó.");

            Console.WriteLine("\n9. feladat:");
            foreach (var (emelet, szektor) in nincsAutoSzektorok(emeletek)) { Console.WriteLine($"{emelet}. Emelet {szektor}. szektor"); }

            var (atlag, atlagFeletti, atlagAlatti) = AtlagtolElteres(emeletek);

            Console.WriteLine($"\n10. feladat: {atlag} szektorban van átlagos mennyiségű, {atlagFeletti} szektorban átlag feletti mennyiségű, s {atlagAlatti} szektorban van átlag alatti mennyiségű autó");

            Console.WriteLine("\n9. feladat:");

            EgyAutoSzelektorok(emeletek);

            Console.WriteLine("A fájlbeírás megtörtént.");

            Console.WriteLine();

            LegfelsoSzint(emeletek);

            using StreamWriter sw = new(@"..\..\..\src\egyAutok.txt", true, encoding: Encoding.UTF8);

            sw.WriteLine("\n13. feladat:");

            var szabadHelyekLista = SzabadHelyek(emeletek);

            foreach (var item in szabadHelyekLista)
            {
                sw.WriteLine(item);
            }

            OsszesSzabadHely(szabadHelyekLista);

            Console.ReadKey();
        }

        static Emelet emeletenkevesebbAuto(List<Emelet> l) => l.MinBy(e => e.Szektorok.Sum());
        static Dictionary<int, int> nincsAutoSzektorok(List<Emelet> l)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            var emeletenNincsAuto = l.Where(e => e.Szektorok.Contains(0));

            foreach (var e in emeletenNincsAuto)
            {
                for (int i = 0; i < e.Szektorok.Count; i++)
                {
                    if (e.Szektorok[i] == 0)
                    {
                        dict.Add(e.Szint, i);
                    }
                }
            }

            return dict;
        }
        static (int atlag, int atlagFeletti, int atlagAlatti) AtlagtolElteres(List<Emelet> l)
        {
            var atlagosMennyiseg = Math.Round(l.Average(e => e.Szektorok.Average()), 2);

            var (atlag, atlagFeletti, atlagAlatti) = (0, 0, 0);

            atlag = l.Sum(e => e.Szektorok.Count(e => e == atlagosMennyiseg));
            atlagFeletti = l.Sum(e => e.Szektorok.Count(e => e > atlagosMennyiseg));
            atlagAlatti = l.Sum(e => e.Szektorok.Count(e => e < atlagosMennyiseg));

            return (atlag, atlagFeletti, atlagAlatti);
        }

        static void EgyAutoSzelektorok(List<Emelet> l)
        {
            List<string> sorok = new List<string>();

            foreach (var e in l)
            {
                StringBuilder sor = new StringBuilder($"{e.SzintNev} szektorai: ");

                for (var i = 0; i < e.Szektorok.Count; i++)
                {
                    if (e.Szektorok[i] == 1)
                    {
                        sor.Append($"{i + 1}-");
                    }
                }

                if (sor.Length > $"{e.SzintNev} szektorai: ".Length)
                {
                    sor.Length--;
                    sorok.Add(sor.ToString());
                }
            }

            using StreamWriter sw = new(@"..\..\..\src\egyAutok.txt", false, encoding: Encoding.UTF8);
            foreach (var item in sorok)
            {
                sw.WriteLine(item);
            }
        }

        static void LegfelsoSzint(List<Emelet> l)
        {
            var legtobbAuto = l.MaxBy(e => e.Szektorok.Sum());

            if (legtobbAuto.Szint == 12)
            {
                Console.WriteLine("A legfelső szinten van a legtöbb autó.");
            }
            else
            {
                Console.WriteLine($"Nem a legfelső, hanem a(z) {legtobbAuto.Szint}. szinten van a legtöbb autó.");
            }
        }

        static List<string> SzabadHelyek(List<Emelet> l)
        {
            List<string> sorok = new();

            foreach (var item in l)
            {
                string sor = "";

                sor += $"{item.Szint}. sor: ";
                List<int> szektorok = new();

                foreach (var e in item.Szektorok)
                {
                    szektorok.Add(15 - e);
                }
                sor += string.Join("; ", szektorok);

                sorok.Add(sor);
            }

            return sorok;
        }

        static void OsszesSzabadHely(List<string> l)
        {
            foreach (var e in l)
            {
                Console.WriteLine(e.Sum());
            }
        }

    }
}
