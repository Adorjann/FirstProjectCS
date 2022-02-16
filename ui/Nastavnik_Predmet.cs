using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FirstProjectCS.servis.Impl;
using FirstProjectCS.model;
using FirstProjectCS.utils;

namespace FirstProjectCS.ui
{
    public class Nastavnik_Predmet
    {
        private readonly PomocnaKlasa pomocnaKlasa = (PomocnaKlasa)SingletonCreator.GetInstance(typeof(PomocnaKlasa));
        private readonly NastavnikServisImpl nastavnikService = (NastavnikServisImpl)SingletonCreator.GetInstance(typeof(NastavnikServisImpl));

        private Nastavnik_Predmet()
        {
        }

        public  void UcitajVezuNastavnik_Predmet(string fajl)
        {

            StreamReader sr = new StreamReader(fajl);
            string line;

            try
            {
                line = sr.ReadLine();
                while (line != null)
                {
                    string[] tokeni = line.Split(',');
                    Nastavnik nastavnik = pomocnaKlasa.FindNastavnikById(Convert.ToInt32(tokeni[0]));
                    Predmet predmet = pomocnaKlasa.FindPredmetById(Convert.ToInt32(tokeni[1]));

                    predmet.DodajNastavnika(nastavnik);

                    //nastavnik.dodajPredmet(predmet); ---> nema potrebe za ovom linijom
                    //azuriranje druge strane veze je izvrseno unutar dodajNastavnika()
                    
                    line = sr.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("*** Greska pri ucitavanju nastavnik_predmet sa fajla. ");
                Console.WriteLine("\n\t*** " + e.Message);
                Console.WriteLine("\n\t*** " + e.StackTrace);
            }
            finally
            {
                sr.Close();
            }
        }
        public void ZapisiVezuNastavnik_Predmet(string fajl)
        {
            StreamWriter sw = new StreamWriter(fajl);
            try
            {
                List<Nastavnik> sviNastavnici = nastavnikService.FindAll();

                foreach (Nastavnik n in sviNastavnici)
                {
                    foreach (Predmet p in n.NastavnikPredaje)
                    {
                        sw.WriteLine($"{n.Id},{p.Id}");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("*** Greska pri zapisivanju nastavnik_predmet fajla. ");
                Console.WriteLine("\n\t*** " + e.Message);
                Console.WriteLine("\n\t*** " + e.StackTrace);

            }
            finally
            {

                sw.Close();
            }
        }
    }
}
