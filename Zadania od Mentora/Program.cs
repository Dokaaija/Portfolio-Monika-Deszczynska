using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;



namespace Zadania___pogoda_z_pliku_CSV
{
    class Program
    {
        public static string DaneZPliku = @"C:\Users\kotek\Desktop\fox\Zadania\PPCS-Z1_Dane.CSV";


        static void Main(string[] args)
        {
            Start();            
        }

        static void Start() 
        {
            Temperatura();
            NajwyzszeCisnienie();
            NajnizszeCisnienie();
            OpadyPowyzej10();
            OpadyPonizej10();
            NajnizaWilgotnosc();
            NajwyzszaWilgotnosc();
            SumaOpadow();
            NjacieplejszyDzien();
            NajzimniejszyDzien();
            SrCisnienie();
            SrOpady();
            SrWilgotnosc();

            Console.WriteLine("Najwyzsza roznica temperatur {0}", NajwyzszaRoznicaTemperatur(NjacieplejszyDzien(), NajzimniejszyDzien()));
            Console.WriteLine("Najwyzsza roznica Cisnienia {0}", NajwyzszaRoznicaCiśnienia(NajwyzszeCisnienie(), NajnizszeCisnienie()));
            LiczbaPomiarow();
        }

       

        // File.ReadAllLines(@"C:\Users\kotek\Desktop\fox\Zadania\PPCS-Z1_Dane.CSV");
        // Obliczenie średniej temperatury

        static void Temperatura()
        {
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("en-US");
            int value = 0;
            double temperatura = 0;
            var Dane = File.ReadAllLines(DaneZPliku);
            foreach (var item in Dane)
            {
                if (!item.Contains("Temperatura"))
                {
                    string[] words = item.Split(',');
                    value++;
                    temperatura = temperatura + Convert.ToDouble(words[2]);

                }

            }

            double obliczenia = temperatura / value;


            Console.WriteLine(@"Srednia Temperatura to {0}", obliczenia);
        }
        //. Znalezienie daty z najwyższym ciśnieniem

        static double NajwyzszeCisnienie()
        {
            double Cisnienie = 0;
            string data = " ";
            var Dane = File.ReadAllLines(DaneZPliku);
            foreach (var item in Dane)
            {
                if (!item.Contains("Ciśnienie"))
                {
                    string[] words = item.Split(',');
                    if (Cisnienie < Convert.ToDouble(words[4]))
                    {
                        Cisnienie = Convert.ToDouble(words[4]);
                        data = words[0];

                    }


                }
            }
            Console.WriteLine(@"Najwyzsze cisnienie to {0} odnotowane dnia {1}", Cisnienie, data);
            return Cisnienie;

        }
        //. Znalezienie daty z najniższym ciśnieniem


        static double NajnizszeCisnienie()
        {
            double Cisnienie = 9999;
            string data = " ";
            var Dane = File.ReadAllLines(DaneZPliku);
            foreach (var item in Dane)
            {
                if (!item.Contains("Ciśnienie"))
                {
                    string[] words = item.Split(',');
                    if (Cisnienie > Convert.ToDouble(words[4]))
                    {
                        Cisnienie = Convert.ToDouble(words[4]);
                        data = words[0];

                    }


                }
            }
            Console.WriteLine(@"Najnizsze cisnienie to {0} odnotowane dnia {1}", Cisnienie, data);
            return Cisnienie;
        }

        //. Obliczenie ilości dni z opadami powyżej 10 mm 


        static void OpadyPowyzej10()
        {
            int liczbadni = 0;
            var Dane = File.ReadAllLines(DaneZPliku);
            foreach (var item in Dane)
            {
                if (!item.Contains("Opady"))
                {
                    string[] words = item.Split(',');
                    if (Convert.ToDouble(words[5]) > 10)
                    {
                        liczbadni++;
                    }
                }
            }
            Console.WriteLine("Opady powzeyj 10mm {0}", liczbadni);

        }

        // Obliczenie ilości dni z opadami poniżej 10 mm

        static void OpadyPonizej10()
        {
            int liczbadni = 0;
            var Dane = File.ReadAllLines(DaneZPliku);
            foreach (var item in Dane)
            {
                if (!item.Contains("Opady"))
                {
                    string[] words = item.Split(',');
                    if (Convert.ToDouble(words[5]) < 10)
                    {
                        liczbadni++;
                    }
                }
            }
            Console.WriteLine("Opady ponizej 10mm {0}", liczbadni);

        }
        //6. Znalezienie daty z najniższą wilgotnością

        static void NajnizaWilgotnosc()
        {
            double Wilgotnosc = 5000;
            string data = " ";
            var Dane = File.ReadAllLines(DaneZPliku);
            foreach (var item in Dane)
            {
                if (!item.Contains("Wilgotność"))
                {
                    string[] words = item.Split(',');
                    if (Wilgotnosc > Convert.ToDouble(words[3]))
                    {
                        Wilgotnosc = Convert.ToDouble(words[3]);
                        data = words[0];

                    }


                }
            }
            Console.WriteLine("Data Najnizszej Wilgotnosci {0}", data);




        }

        //7. Znalezienie daty z najwyższą wilgotnością
        static void NajwyzszaWilgotnosc()
        {
            double Wilgotnosc = 0;
            string data = " ";
            var Dane = File.ReadAllLines(DaneZPliku);
            foreach (var item in Dane)
            {
                if (!item.Contains("Wilgotność"))
                {
                    string[] words = item.Split(',');
                    if (Wilgotnosc < Convert.ToDouble(words[3]))
                    {
                        Wilgotnosc = Convert.ToDouble(words[3]);
                        data = words[0];

                    }


                }
            }
            Console.WriteLine("Data Najwyzsza Wilgotnosci {0}", data);


            /*var Dane = File.ReadAllLines(DaneZPliku);
                foreach (var item in Dane)
                {
                    if (!item.Contains("Ciśnienie"))
                    {
                        string[] words = item.Split(',');
                    }
                }*/

        }




        //Obliczenie sumy opadów
        static void SumaOpadow()
        {
            double SumaOpadow = 0;
            var Dane = File.ReadAllLines(DaneZPliku);
            foreach (var item in Dane)
            {
                if (!item.Contains("Opady"))
                {
                    string[] words = item.Split(',');
                    SumaOpadow = SumaOpadow + Convert.ToDouble(words[5]);
                }
            }
            Console.WriteLine("Suma opadow wynosi {0}", SumaOpadow);

        }


        // Wskazanie najcieplejszego dnia

        static double NjacieplejszyDzien()
        {
            double Temperatura = 0;
            string data = " ";
            var Dane = File.ReadAllLines(DaneZPliku);
            foreach (var item in Dane)
            {
                if (!item.Contains("Temperatura"))
                {
                    string[] words = item.Split(',');
                    if (Temperatura< Convert.ToDouble(words[2]))
                    {
                        Temperatura = Convert.ToDouble(words[2]);
                        data = words[0];
                    }


                }
            }
            Console.WriteLine(@"Najwyzsze temperatura odnotowana dnia {0}", data);
            return Temperatura;

        }


        // Wskazanie najzimniejszego dnia
        static double NajzimniejszyDzien()
        {
            double Temperatura = 999;
            string data = " ";
            var Dane = File.ReadAllLines(DaneZPliku);
            foreach (var item in Dane)
            {
                if (!item.Contains("Temperatura"))
                {
                    string[] words = item.Split(',');
                    if (Temperatura > Convert.ToDouble(words[2]))
                    {
                        Temperatura = Convert.ToDouble(words[2]);
                        data = words[0];
                    }


                }
            }
            Console.WriteLine(@"Najnizsza temperatura odnotowana dnia {0}", data);
            return Temperatura;
        }


        //Obliczenie średniego ciśnienia
        public static void SrCisnienie() 
        {
            int value = 0;
            double Ciśnienie = 0;
            var Dane = File.ReadAllLines(DaneZPliku);
            foreach (var item in Dane)
            {
                if (!item.Contains("Ciśnienie"))
                {
                    string[] words = item.Split(',');
                    value++;
                    Ciśnienie = Ciśnienie + Convert.ToDouble(words[4]);

                }

            }

            double obliczenia = Ciśnienie / value;


            Console.WriteLine(@"Sredniae ciśnienie to {0:F3}", obliczenia);
        }

        //12. Obliczenie średniej wilgotności
        public static void SrWilgotnosc()
        {
            int value = 0;
            double Wilgotność = 0;
            var Dane = File.ReadAllLines(DaneZPliku);
            foreach (var item in Dane)
            {
                if (!item.Contains("Wilgotność"))
                {
                    string[] words = item.Split(',');
                    value++;
                    Wilgotność = Wilgotność + Convert.ToDouble(words[3]);

                }

            }

            double obliczenia = Wilgotność / value;


            Console.WriteLine(@"Srednia Wilgotność to {0}", obliczenia);
        }

        //13. Obliczenie średnich opadów
        public static void SrOpady()
        {
            int value = 0;
            double Opady = 0;
            var Dane = File.ReadAllLines(DaneZPliku);
            foreach (var item in Dane)
            {
                if (!item.Contains("Opady"))
                {
                    string[] words = item.Split(',');
                    value++;
                    Opady = Opady + Convert.ToDouble(words[5]);
                    
                }

            }

            double obliczenia = Opady / value;


            Console.WriteLine(@"Srednie Opadya to {0}", obliczenia);
        }


        // Obliczenie największej różnicy temperatury

        public static double NajwyzszaRoznicaTemperatur(double Temperatura1, double Temperatura2) => Temperatura1 - Temperatura2;


        //Obliczenie największej różnicy ciśnienia


        public static double NajwyzszaRoznicaCiśnienia(double Ciśnienie1, double Cisnienie2) => Ciśnienie1 - Cisnienie2;



        // Obliczenie największej różnicy opadów

        public static double NajwyzszaRoznicaOpadow(double Opady1, double Opady2) => Opady1 - Opady2;




        // Obliczenie największej różnicy wilgotności



        //8. Obliczenie liczby pomiarów
        public static void LiczbaPomiarow() 
        {
            int SumaPomiarow = 0;
            var Dane = File.ReadAllLines(DaneZPliku);
            foreach (var item in Dane)
            {
                if (!item.Contains("Opady"))
                {
                    SumaPomiarow++;
                }

            }
            Console.WriteLine("Suma Pomiarow to {0}", SumaPomiarow);
        }

    }
}
