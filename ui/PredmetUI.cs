using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FirstProjectCS.utils;
using FirstProjectCS.model;
using FirstProjectCS.servis.Impl;

namespace FirstProjectCS.ui
{
    internal class PredmetUI
    {

		public static void ispisiTekstStudentOpcije()
		{
			Console.WriteLine("Rad sa predmetima - opcije:");
			Console.WriteLine("\tOpcija broj 1 - unos podataka o novom Predmetu");
			Console.WriteLine("\tOpcija broj 2 - izmena podataka o Predmetu");
			Console.WriteLine("\tOpcija broj 3 - brisanje podataka o Predmetu");
			Console.WriteLine("\tOpcija broj 4 - ispis podataka svih predmeta"); //sortiranje?
			Console.WriteLine("\tOpcija broj 5 - ispis podataka o odredjenom Predmetu i svih studenta koji pohadjaju predmet");
			Console.WriteLine("\tOpcija broj 6 - ispis podataka o odredjenom Predmetu i svih ispitnih prijava za predmet");
			Console.WriteLine("\t\t ...");
			Console.WriteLine("\tOpcija broj 0 - IZLAZ");
		}

		public static void meniPredmetUI()
        {
			int odluka = -1;

			while(odluka != 0)
            {
				ispisiTekstStudentOpcije();
				odluka = Convert.ToInt32(Console.ReadLine());

                switch (odluka)
                {
					case 0:
						Console.WriteLine("IZLAZ");
						break;
					case 1:
						//unos novog predmeta [POST]
						create();
						break;
					case 2:
						//izmena predmeta [PUT]
						edit();
						break;
					case 3:
						//brisanje predmeta [DELETE]
						delete();
						break;
					case 4:
						//ispis svih predmeta [GET]
						getAll(); //ponudi sortiranje
						break;
					case 5:
						//ispis 1 predmet i studenti koji pohadjaju taj predmet [GET]
						getOne("studenti");
						break;
					case 6:
						//ispis 1 predmet i ispitne prijave za taj predmet [GET]
						getOne("prijave");
						break;
					default:
						Console.WriteLine("Pogresna komanda");
						break;
					
						

						
				}

			}
			

        }

        private static void getOne(string kolekcija)
        {
			Predmet predmet = PomocnaKlasa.nameToPredmet();

			Console.WriteLine("\n-|- "+predmet);
            if (kolekcija.Equals("studenti"))
            {
				Console.WriteLine("Predmet " + predmet.Naziv + " pohadjaju: \n");
				foreach (Student it in predmet.PredmetPohadjajuStudenti)
                {
					Console.WriteLine("\t" + it); 
                }	
            }else if (kolekcija.Equals("prijave"))
            {
				Console.WriteLine("Ispitne prijave jos uvek nisu implementirane");
            }

			//TODO: ispis kolekcije po zelji
		}

        private static void getAll()
        {
			List<Predmet> sviPredmeti = PredmetServisImpl.findAll();
			if(sviPredmeti == null || sviPredmeti.Count == 0)
            {
				Console.WriteLine("Nema predmeta");
            }
            else
            {
				Console.WriteLine("Svi studenti: ");
				foreach(Predmet predmet in sviPredmeti)
                {
					Console.WriteLine("\t" + predmet);
                }	
            }

        }

        private static void delete()
        {
			Predmet predmetZaBrisanje = PomocnaKlasa.nameToPredmet();

			if(predmetZaBrisanje != null)
            {
				Console.WriteLine("Da li ste sigurni da zelite da obrisete predmet: \n\t" + predmetZaBrisanje);
				bool odgovor = PomocnaKlasa.yesOrNo();

				Predmet izbrisaniPredmet = odgovor ? PredmetServisImpl.delete(predmetZaBrisanje) : null;

				string finalPrintOut = izbrisaniPredmet == null ? "Prekinuto brisanje predmeta" : "Uspesno izbrisan predmet";
				Console.WriteLine(finalPrintOut);
            }
        }

        private static void edit()
        {
			Predmet predmet = PomocnaKlasa.nameToPredmet();
			if(predmet != null)
            {
				Console.WriteLine("Unesite Novi naziv Predmeta: ");
				string noviNaziv = Console.ReadLine();

				predmet.Naziv = noviNaziv;

				Console.WriteLine("Uspesno promenjen naziv predmeta: \n\t"+predmet);
            }
            else
            {
				Console.WriteLine("Ne postoji predmet sa zadatim nazivom");
            }

        }

        private static void create()
        {
			Console.WriteLine("Unesite naziv novog predmeta:");
			string nazivNovogPredmeta = Console.ReadLine();	

			if(nazivNovogPredmeta != null && nazivNovogPredmeta != "")
            {
				Predmet noviPredmet = new Predmet(0,nazivNovogPredmeta);
				Predmet sacuvaniPredmet = PredmetServisImpl.save(noviPredmet);

				if(sacuvaniPredmet != null)
                {
					Console.WriteLine("Uspesno sacuvan novi Predmet: "+nazivNovogPredmeta);
                }
                else
                {
					Console.WriteLine("*** Unos novog Predmeta nije uspeo.");
				}
            }
        }

		public static void ucitajPredmeteSaFajla(string fajl)
        {
			StreamReader sr = new StreamReader(fajl);
			string line;

			try { 
				line = sr.ReadLine();
				while(line != null)
                {
					string[] tokeni = line.Split(',');
					Predmet noviPredmet = new Predmet();
					noviPredmet.Id = Convert.ToInt32(tokeni[0]);
					noviPredmet.Naziv = tokeni[1];

					Predmet snimljeniPredmet = PredmetServisImpl.save(noviPredmet);
					if(snimljeniPredmet == null)
                    {
						Console.WriteLine("*** Greska pri snimanju predmeta u memoriju.");
						break;
					}
					line = sr.ReadLine();

                }


			}catch(Exception e)
            {
				Console.WriteLine("*** Greska pri ucitavanju predmeta sa fajla. ");
				Console.WriteLine("\n\t*** " + e.StackTrace);
				Console.WriteLine("\n\t*** " + e.Message);
			}
			finally
			{
				sr.Close();
				Console.WriteLine("Zavrseno ucitavanje studenata");
			}
		}

		public static void zapisiPredmeteUFajl(string fajl)
        {
			List<Predmet> sviPredmeti = PredmetServisImpl.findAll();

			StreamWriter sw = new StreamWriter(fajl);
            try
            {
				foreach(Predmet it in sviPredmeti)
                {
					sw.WriteLine(it.toFileReprezentation());
				}
            }
			catch (Exception e)
			{
				Console.WriteLine("*** Greska pri zapisivanju predmeta u fajl.");
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
