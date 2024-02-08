using System.Linq;

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

            var (atlag,atlagFeletti,atlagAlatti) = AtlagtolElteres(emeletek);

            Console.WriteLine($"\n10. feladat: {atlag} szektorban van átlagos mennyiségű, {atlagFeletti} szektorban átlag feletti mennyiségű, s {atlagAlatti} szektorban van átlag alatti mennyiségű autó");

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
    }
}
