using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FirstProjectCS.model;
using FirstProjectCS.servis.Impl;
using FirstProjectCS.utils;
using FirstProjectCS.exceptions;

namespace FirstProjectCS.ui
{
    public class IspitniRokUI
    {
        private readonly IspitniRokServiceImpl irService = (IspitniRokServiceImpl)SingletonCreator.GetInstance(typeof(IspitniRokServiceImpl));
        private readonly PomocnaKlasa pomocnaKlasa = (PomocnaKlasa)SingletonCreator.GetInstance(typeof(PomocnaKlasa));

        private IspitniRokUI()
        {
        }

        public static void IspisiTekstIspitniRokOpcije()
        {
            Console.WriteLine("Rad sa Ispitnim Rokovima - opcije:");
            Console.WriteLine("\tOpcija broj 1 - unos podataka o novom ispitnom roku");
            Console.WriteLine("\tOpcija broj 2 - izmena podataka o ispitnom roku");
            Console.WriteLine("\tOpcija broj 3 - brisanje podataka o ispitnom roku");
            Console.WriteLine("\tOpcija broj 4 - ispis podataka svih ispitnih rokova"); //sortiranje?
            Console.WriteLine("\tOpcija broj 5 - ispis podataka o odredjenom Ispitnom roku sa njegovim Ispitnim prijavama");
            Console.WriteLine("\t\t ...");
            Console.WriteLine("\tOpcija broj 0 - IZLAZ");
        }

        public void MeniIspitniRokUI()
        {
            int odluka = -1;
            while(odluka != 0)
            {
                IspisiTekstIspitniRokOpcije();
                odluka = Convert.ToInt32(Console.ReadLine());

                switch (odluka)
                {
                    case 0:
                        Console.WriteLine("Izlaz iz Ispitni Rok menija.");
                        break;
                    case 1:
                        Create();
                        break;
                    case 2:
                        Edit();
                        break;
                    case 3:
                        Delete();
                        break;
                    case 4:
                        GetAll(); //sortiranje?
                        break;
                    case 5:
                        GetOne("prijave");
                        break;
                    default:
                        Console.WriteLine("Pogresna komanda.");
                        break;

                }

            }

        }

        private void GetOne(string prijave)
        {
            try
            {
                IspitniRok ispitniRok = pomocnaKlasa.NameToIspitniRok();

                Console.WriteLine("\n-|- " + ispitniRok);
                if (prijave == "prijave")
                {
                    ispitniRok.IspitniRokImaPrijavljeneIspitnePrijave.ForEach(ip => Console.WriteLine(ip));
                }
            }
            catch (ObjectNotFoundException)
            {
                Console.WriteLine("Trazeni ispitni rok ne postoji");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Pogresno uneseni parametri pretrage");
            }
            catch (Exception)
            {
                Console.WriteLine("Doslo je do greske, pokusajte ponovo");
            }
        }

        private void GetAll()
        {
            try
            {
                List<IspitniRok> sviRokovi = irService.FindAll();

                Console.WriteLine("Svi Ispitni Rokovi: ");

                sviRokovi.ForEach(ir => Console.WriteLine($"\t {ir}"));

            }
            catch (CollectionIsEmptyException)
            {
                Console.WriteLine("Nema ispitnih rokova za prikaz");
            }
            catch(Exception) 
            {
                Console.WriteLine("Doslo je do greske.");
            }
        }

        private void Delete()
        {
            try
            {
                IspitniRok rokZaBrisanje = pomocnaKlasa.NameToIspitniRok();

                Console.WriteLine($"Da li ste sigurni da zelite da izbrisete {rokZaBrisanje.Naziv} ispitni rok? ");

                if (pomocnaKlasa.YesOrNo())
                {
                    IspitniRok obrisanIRok = irService.Delete(rokZaBrisanje);

                    Console.WriteLine($"Uspesno izbrisan {rokZaBrisanje.Naziv} ispitni rok.");
                }
                else
                {
                    throw new OperationCanceledException();
                }

            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Prekinuto brisanje ispitnog roka");
            }
            catch (ObjectNotFoundException)
            {
                Console.WriteLine("Greska u brisanju ispitnog roka");
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
                IspitniRok rokZaIzmenu = pomocnaKlasa.NameToIspitniRok();

                Console.Write("Unesite novi naziv ispitnog roka:");
                string noviNaziv = Console.ReadLine();
                Console.WriteLine("Unesite novi datum pocetka ispitnog roka: \n\tyyyy-MM-dd");
                string noviPocetak = Console.ReadLine();
                Console.WriteLine("Unesite datum kraja novog ispitnog roka: \n\tyyyy-MM-dd");
                string noviKraj = Console.ReadLine();

                if (noviNaziv != "" && noviPocetak != null && noviKraj != null)
                {
                    rokZaIzmenu.Naziv = noviNaziv;
                    rokZaIzmenu.Pocetak = StringToDate(noviPocetak);
                    rokZaIzmenu.Kraj = StringToDate(noviKraj);

                    Console.WriteLine($"Uspesno promenjeni podaci o {noviNaziv} ispitnom roku");
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            catch (ObjectNotFoundException)
            {
                Console.WriteLine("Ne postoji ispitni rok sa zadatim nazivom");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Pogresno uneseni novi podaci");
            }
            catch (Exception)
            {
                Console.WriteLine("Doslo je do greske");
            }
            
        }

        private void Create()
        {
            try
            {
                Console.Write("Unesite naziv novog ispitnog roka:");
                string nazivNovog = Console.ReadLine();
                Console.WriteLine("Unesite datum pocetka novog ispitnog roka: \n\tyyyy-MM-dd");
                string pocetakNovog = Console.ReadLine();
                Console.WriteLine("Unesite datum kraja novog ispitnog roka: \n\tyyyy-MM-dd");
                string krajNovog = Console.ReadLine();

                IspitniRok noviRok = new IspitniRok(0, nazivNovog, StringToDate(pocetakNovog), StringToDate(krajNovog));
                IspitniRok sacuvaniRok = irService.Save(noviRok);
                Console.WriteLine("Uspesno sacuvan novi Ispitni Rok");

            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Pogresno uneseni podaci.");
            }
            catch (DuplicateObjectException)
            {
                Console.WriteLine("Vec postoji ispitni rok sa zadatim nazivom.");
            }
            catch (Exception)
            {
                Console.WriteLine("Doslo je do greske.");
            }

        }

        public void UcitajIspitniRokSaFajla(string fajl)
        {
            StreamReader sr = new StreamReader(fajl);
            string line = null;

            try
            {
                line = sr.ReadLine();
                while (line != null)
                {
                    string[] tokeni = line.Split(',');
                    
                    IspitniRok ir = new IspitniRok();
                    ir.Id = Convert.ToInt32(tokeni[0]);
                    ir.Naziv = tokeni[1];
                    ir.Pocetak = StringToDate(tokeni[2]);
                    ir.Kraj = StringToDate(tokeni[3]);

                    irService.Save(ir);
                    
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
                Console.WriteLine("Zavrseno ucitavanje ispitnih rokova");
            }
        }

        public void ZapisiIspitniRokUFajl(string fajl)
        {
            StreamWriter sw = new StreamWriter(fajl);
            try
            {
                List<IspitniRok> sviRokovi = irService.FindAll();
                sviRokovi.ForEach(ir => sw.WriteLine(ir.ToFileReprezentation()));
            }
            catch (Exception e)
            {
                Console.WriteLine("*** Greska pri zapisivanju Ispitnog Roka u fajl.");
                Console.WriteLine("\n\t*** " + e.StackTrace);
                Console.WriteLine("\n\t*** " + e.Message);
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        private DateTime StringToDate(string dateInString)
        {
            //dateInString je string datum iz fajla, u formatu yyyy-MM-dd

            string[] yyyyMMdd = dateInString.Split('-');

            DateTime date1 = new DateTime(Convert.ToInt32(yyyyMMdd[0]),
                                          Convert.ToInt32(yyyyMMdd[1]),
                                          Convert.ToInt32(yyyyMMdd[2]));
            return date1;
        }


    }
}
