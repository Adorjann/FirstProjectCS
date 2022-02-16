using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FirstProjectCS.model;
using FirstProjectCS.utils;
using FirstProjectCS.servis.Impl;
using FirstProjectCS.exceptions;

namespace FirstProjectCS.ui
{
    public class NastavnikUI
    {
        private readonly NastavnikServisImpl nastavnikService = (NastavnikServisImpl)SingletonCreator.GetInstance(typeof(NastavnikServisImpl));
        private readonly PomocnaKlasa pomocnaKlasa = (PomocnaKlasa)SingletonCreator.GetInstance(typeof(PomocnaKlasa));

        private NastavnikUI() 
        {
        }

        private void IspisiTekstNastavnikOpcije()
        {
            Console.WriteLine("Rad sa nastavnicima - opcije:");
            Console.WriteLine("\tOpcija broj 1 - unos podataka o novom Nastavniku");
            Console.WriteLine("\tOpcija broj 2 - izmena podataka o Nastavniku");
            Console.WriteLine("\tOpcija broj 3 - brisanje podataka o Nastavniku");
            Console.WriteLine("\tOpcija broj 4 - ispis podataka svih Nastavnika"); //sortiranje?
            Console.WriteLine("\tOpcija broj 5 - ispis podataka o odredjenom Nastavniku");
            Console.WriteLine("\tOpcija broj 6 - ispis podataka o odredjenom Nastavniku i svih predmeta koje predaje");

            Console.WriteLine("\t\t ...");

            Console.WriteLine("\tOpcija broj 0 - IZLAZ");
        }

        public void MeniNastavnikUI()
        {
            int odluka = -1;
            while (odluka != 0)
            {
                IspisiTekstNastavnikOpcije();
                odluka = Convert.ToInt32(Console.ReadLine());

                switch (odluka)
                {
                    case 0:
                        Console.WriteLine("IZLAZ");
                        break;
                    case 1:
                        //unos novog nastavnika [POST]
                        Create();
                        break;
                    case 2:
                        //izmena podataka o nastavniku [PUT]
                        Edit();
                        break;
                    case 3:
                        //brisanje nastavnika [DELETE]
                        Delete();
                        break;
                    case 4:
                        //ispis svih nastavnik (sortirano?) [GET]
                        GetAll();
                        break;
                    case 5:
                        //ispis 1 nastavnika
                        GetOne("");
                        break;
                    case 6:
                        //ispis 1 nastavnika i svih predmeta koje predaje
                        GetOne("predmeti");
                        break;
                    default:
                        Console.WriteLine("Pogresna komanda");
                        break;
                }
            }
        }

        private void GetOne(string v)
        {
            try
            {
                Nastavnik nastavnik = pomocnaKlasa.NameToNastavnik();

                Console.WriteLine("\n-|- " + nastavnik);
                if (v == "predmeti" && nastavnik.NastavnikPredaje.Count != 0)
                {
                    Console.WriteLine($"Nastavnik {nastavnik.Ime} predaje predmete:");
                    nastavnik.NastavnikPredaje.ForEach(pr => Console.WriteLine("\t" + pr));
                }
            }
            catch (ObjectNotFoundException)
            {
                Console.WriteLine("Nema nastavnika sa zadatim imenom");
            }
            catch (Exception)
            {
                Console.WriteLine("doslo je do greske");
            }
        }

        private void GetAll()
        {
            try
            {
                List<Nastavnik> sviNastavnici = nastavnikService.FindAll();

                Console.WriteLine("Svi Nastavnici: ");
                sviNastavnici.ForEach(nastavnik => Console.WriteLine($"\t {nastavnik}"));
            }
            catch (CollectionIsEmptyException)
            {
                Console.WriteLine("Ne postoji ni jedan nastavnik!");

            }
            catch (Exception)
            {
                Console.WriteLine("doslo je do greske");
            }
        }

        private void Delete()
        {
            try
            {
                Nastavnik nastavnikZaBrisanje = pomocnaKlasa.NameToNastavnik();

                Console.WriteLine($"Da li ste sigurni da zelite da izbrisete nastavnika: \n\t {nastavnikZaBrisanje}");

                if (pomocnaKlasa.YesOrNo())
                {
                    Nastavnik obrisanNastavnik = nastavnikService.Delete(nastavnikZaBrisanje);

                    Console.WriteLine($"Uspesno izbrisan nastavnik {nastavnikZaBrisanje.Ime}");
                }
                else
                {
                    throw new OperationCanceledException();
                }

            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Prekinuto brisanje nastavnika");
            }
            catch (ObjectNotFoundException)
            {
                Console.WriteLine("Greska u brisanju nastavnika");
            }
            catch (Exception)
            {
                Console.WriteLine("Doslo je do greske");
            }


        }

        private void Edit()
        {
            try
            {
                Nastavnik nastavnik = pomocnaKlasa.NameToNastavnik();

                Console.WriteLine($"Unesite novo ime nastavnika: ");
                string novoIme = Console.ReadLine();
                Console.WriteLine($"Unesite novo prezime nastavnika: ");
                string novoPrezime = Console.ReadLine();
                Console.WriteLine($"Unesite novo zvanje nastavnika: ");
                string novoZvanje = Console.ReadLine();

                if (novoIme != "" && novoPrezime != "" && novoZvanje != "")
                {
                    nastavnik.Ime = novoIme;
                    nastavnik.Prezime = novoPrezime;
                    nastavnik.Zvanje = novoZvanje;

                    Console.WriteLine($"Uspesno promenjeni podaci o nastavniku: \n\t {nastavnik}");
                }
                else
                {
                    Console.WriteLine("Pogresno unesena vrednost.");
                }

            }
            catch (ObjectNotFoundException)
            {
                Console.WriteLine("Ne postoji nastavnk sa unesenim imenom.");
            }
            catch (Exception)
            {
                Console.WriteLine("doslo je do greske");
            }
        }

        private void Create()
        {
            try
            {
                Console.WriteLine($"Unesite ime nastavnika: ");
                string ime = Console.ReadLine();
                Console.WriteLine($"Unesite prezime nastavnika: ");
                string prezime = Console.ReadLine();
                Console.WriteLine($"Unesite zvanje nastavnika: ");
                string zvanje = Console.ReadLine();

                Nastavnik noviNastavnik = new Nastavnik(0, ime, prezime, zvanje);
                nastavnikService.Save(noviNastavnik);

                Console.WriteLine($"Uspesno sacuvan novi nastavnik: {zvanje}, {ime} {prezime}");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("*** Unos novog nastavnika nije uspeo.");
                Console.WriteLine("Nepravilno uneseni podaci.");
            }
            catch (DuplicateObjectException)
            {
                Console.WriteLine("*** Unos novog nastavnika nije uspeo.");
                Console.WriteLine("Vec postoji nastavnik sa unesenim podacima");
            }
            catch (Exception)
            {
                Console.WriteLine("doslo je do greske");
            }
         }

        public void UcitajNastavnikeSaFajla(string fajl)
        {
            StreamReader sr = new StreamReader(fajl);
            string line = null;

            try
            {
                line = sr.ReadLine();
                while (line != null)
                {
                    string[] tokeni = line.Split(',');
                    //npr linija:	 1,Petar,Petrovic,Docent
                    //tokeni		 0	 1		2		3

                    Nastavnik nastavnik = new Nastavnik();
                    nastavnik.Id = Convert.ToInt32(tokeni[0]);
                    nastavnik.Ime = tokeni[1];
                    nastavnik.Prezime = tokeni[2];
                    nastavnik.Zvanje = tokeni[3];

                    nastavnikService.Save(nastavnik);
                    
                    line = sr.ReadLine();
                }
            }
            catch (DuplicateObjectException)
            {
                Console.WriteLine("Greska pri snimanju u bazu");
            }
            catch (Exception e)
            {
                Console.WriteLine("*** Greska pri ucitavanju nastavnika sa fajla. ");
                Console.WriteLine("\n\t*** " + e.StackTrace);
                Console.WriteLine("\n\t*** " + e.Message);
            }
            finally
            {
                sr.Close();
                Console.WriteLine("Zavrseno ucitavanje nastavnika");
            }

        }

        public void ZapisiNastavnikeUFajl(string fajl)
        {

            StreamWriter sw = new StreamWriter(fajl);
            try
            {
                List<Nastavnik> sviNastavnici = nastavnikService.FindAll();

                sviNastavnici.ForEach(nastavnik => 
                sw.WriteLine(nastavnik.ToFileRepresentation()));
            }
            catch (CollectionIsEmptyException)
            {
                Console.WriteLine("Nema nastavnika za upisivanje u fajl");
            }
            catch (Exception e)
            {
                Console.WriteLine("*** Greska pri zapisivanju nastavnika u fajl.");
                Console.WriteLine("\n\t*** " + e.StackTrace);
                Console.WriteLine("\n\t*** " + e.Message);
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
    }
}
