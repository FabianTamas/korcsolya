using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace korcsolya
{
    class Program
    {
        static List<Versenyzo> rovidprogram = new List<Versenyzo>();
        static List<Versenyzo> donto = new List<Versenyzo>();

        //Egyszerűsített!
        //static void beolvas(List<Versenyzo> lista, string filename)
        //{
        //    StreamReader file = new StreamReader(filename);
        //    file.ReadLine();
        //    while (!file.EndOfStream)
        //    {
        //        string[] adat = file.ReadLine().Split(';');
        //        lista.Add(new Versenyzo(adat[0], adat[1], Convert.ToDouble(adat[2].Replace('.', ',')), Convert.ToDouble(adat[3].Replace('.', ',')), Convert.ToDouble(adat[4].Replace('.', ','))));
        //    }
        //    file.Close();
        //}

        static void beolvasrovid()
        {
            StreamReader file = new StreamReader("rovidprogram.csv");
            file.ReadLine();
            while (!file.EndOfStream)
            {
                string[] adat = file.ReadLine().Split(';');
                for (int i = 0; i < adat.Length; i++)
                {
                    if (adat[i].Contains('.'))
                    {
                        adat[i] = adat[i].Replace('.', ',');
                    }
                }
                rovidprogram.Add(new Versenyzo(adat[0],adat[1],Convert.ToDouble(adat[2]),Convert.ToDouble(adat[3]), Convert.ToDouble(adat[4])));
            }
            file.Close();
        }

        static void beolvasdonto()
        {
            StreamReader file = new StreamReader("donto.csv");
            file.ReadLine();
            while (!file.EndOfStream)
            {
                string[] adat = file.ReadLine().Split(';');
                for (int i = 0; i < adat.Length; i++)
                {
                    if (adat[i].Contains('.'))
                    {
                        adat[i] = adat[i].Replace('.', ',');
                    }
                }
                donto.Add(new Versenyzo(adat[0], adat[1], Convert.ToDouble(adat[2]), Convert.ToDouble(adat[3]), Convert.ToDouble(adat[4])));
            }
            file.Close();
        }

        static void Masodik()
        {
            Console.WriteLine("2. feladat");
            Console.WriteLine($"\tA rövid programban {rovidprogram.Count} induló volt");
        }

        static void Harmadik()
        {
            Console.WriteLine("3. feladat");
            int i = 0;
            while (i < donto.Count)
            {
                if (donto[i].Orszag.Contains("HUN"))
                {
                    Console.WriteLine("\tA magyar versenyző bejutott a döntőbe");
                }
                i++;
            }
        }

        static double OsszPontszam(string beker)
        {
            double osszpont = 0;
            foreach (var r in rovidprogram)
            {
                if (r.Nev == beker)
                {
                    osszpont += r.Pont;
                    foreach (var d in donto)
                    {
                        if (d.Nev == beker)
                        {
                            osszpont += r.Pont;
                        }
                    }
                }
            }
            return osszpont;
        }

        static void Otodik()
        {
            Console.WriteLine("5. feladat");
            Console.Write("\tKérem a versenyző nevét: ");
            string beker = Console.ReadLine();
            bool vane = false;
            int i = 0;
            while (i < rovidprogram.Count && vane==false)
            {
                if (rovidprogram[i].Nev==beker)
                {
                    vane = true;
                }
                else
                {
                    vane = false;
                }
                i++;
            }

            if (!vane)
            {
                Console.WriteLine("\tIlyen nevű induló nem volt!");
            }
            else if (vane)
            {
                Console.WriteLine("6. feladat");
                Console.WriteLine("\tA versenyző összpontszáma: {0}", OsszPontszam(beker));
            }
        }

        static void Hetedik()
        {

        }

        static string Orszag(string nev)
        {
            int i = 0;
            while (rovidprogram[i].Nev != nev)
            {
                i++;
            }
            return rovidprogram[i].Orszag;
        }

        static void Nyolcadik()
        {
            StreamWriter file = new StreamWriter("vegeredmeny.csv");
            Dictionary<string, double> adatok = new Dictionary<string, double>();
            //Dictionary<string, string> orszagok = new Dictionary<string, string>();
            foreach (var r in rovidprogram)
            {
                if (!adatok.ContainsKey(r.Nev))
                {
                    adatok.Add(r.Nev, OsszPontszam(r.Nev));
                    //orszagok.Add(r.Nev, r.Orszag);
                }
            }
            int hely = 1;
            var rendezett = adatok.OrderByDescending(a => a.Value);
            foreach (var adat in rendezett)
            {
                //file.WriteLine($"{hely++};{adat.Key};{orszagok[adat.Key]};{adat.Value}");
                file.WriteLine($"{hely++};{adat.Key};{Orszag(adat.Key)};{adat.Value}");
            }
            file.Close();
        }

        static void Main(string[] args)
        {
            //beolvas(rovidprogram, "rovidprogram.csv");
            //beolvas(donto, "donto.csv");
            beolvasrovid();
            beolvasdonto();
            Masodik();
            Harmadik();
            Otodik();
            Hetedik();
            Nyolcadik();

            Console.ReadKey();
        }
    }
}
