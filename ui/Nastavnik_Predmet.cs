using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FirstProjectCS.servis.Impl;
using FirstProjectCS.model;

namespace FirstProjectCS.ui
{
    internal class Nastavnik_Predmet
    {
        public static void ucitajVezuNastavnik_Predmet(string fajl)
        {

            StreamReader sr = new StreamReader(fajl);
            string line;

            try
            {
                line = sr.ReadLine();
                while (line != null)
                {
                    string[] tokeni = line.Split(',');
                    Nastavnik nastavnik = NastavnikServisImpl.findById(Convert.ToInt32(tokeni[0]));
                    Predmet predmet = PredmetServisImpl.findById(Convert.ToInt32(tokeni[1]));

                    if (nastavnik != null && predmet != null)
                    {
                        predmet.dodajNastavnika(nastavnik);

                        //nastavnik.dodajPredmet(predmet); ---> nema potrebe za ovom linijom
                        //azuriranje druge strane veze je izvrseno unutar dodajNastavnika()
                    }
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
        public static void zapisiVezuNastavnik_Predmet(string fajl)
        {
            StreamWriter sw = new StreamWriter(fajl);
            try
            {
                foreach (Nastavnik n in NastavnikServisImpl.findAll())
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
