using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FirstProjectCS.model;
using FirstProjectCS.utils;
using FirstProjectCS.servis.Impl;



namespace FirstProjectCS.ui
{
    internal class NastavnikUI
    {

		public static void ispisiTekstNastavnikOpcije()
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


		public static void meniNastavnikUI()
        {
			int odluka = -1;
			while (odluka != 0) 
			{
				ispisiTekstNastavnikOpcije();
				odluka = Convert.ToInt32(Console.ReadLine());

                switch (odluka)
                {
					case 0:
						Console.WriteLine("IZLAZ");
						break;
					case 1:
						//unos novog nastavnika [POST]
						create();
						break;
					case 2:
						//izmena podataka o nastavniku [PUT]
						edit();
						break;
					case 3:
						//brisanje nastavnika [DELETE]
						delete();
						break;
					case 4:
						//ispis svih nastavnik (sortirano?) [GET]
						getAll();
						break;
					case 5:
						//ispis 1 nastavnika
						getOne("");
						break;
					case 6:
						//ispis 1 nastavnika i svih predmeta koje predaje
						getOne("predmeti");
						break;
					default:
						Console.WriteLine("Pogresna komanda");
						break;
				}
			}      
		}

        private static void getOne(string v)
        {
			Nastavnik nastavnik = PomocnaKlasa.nameToNastavnik();
			if(nastavnik != null)
            {
				Console.WriteLine("\n-|- " + nastavnik);
				if(v == "predmeti")
                {
					Console.WriteLine($"Nastavnik {nastavnik.Ime} predaje predmete:");
					foreach(Predmet pr in nastavnik.NastavnikPredaje)
                    {
						Console.WriteLine("\t" + pr);
					}
				}
			}
        }

        private static void getAll()
        {
			List<Nastavnik> sviNastavnici = NastavnikServisImpl.findAll();
			if(sviNastavnici.Count > 0 && sviNastavnici != null)
            {
				Console.WriteLine("Svi Nastavnici: ");
				foreach(Nastavnik nastavnik in sviNastavnici)
                {
					Console.WriteLine($"\t {nastavnik}");
                }
            }else if(sviNastavnici.Count == 0 && sviNastavnici != null)
            {
				Console.WriteLine("Ne postoji ni jedan nastavnik!");
            }
            else
            {
				Console.WriteLine("*** Greska u programu");
            }
        }

        private static void delete()
        {
			Nastavnik nastavnikZaBrisanje = PomocnaKlasa.nameToNastavnik();
			if(nastavnikZaBrisanje != null)
            {
				Console.WriteLine($"Da li ste sigurni da zelite da izbrisete nastavnika: \n\t {nastavnikZaBrisanje}");
				bool odgovor = PomocnaKlasa.yesOrNo();

				Nastavnik izbrisaniNastavnik = odgovor ? NastavnikServisImpl.delete(nastavnikZaBrisanje) : null;
				string finalPrintOut = izbrisaniNastavnik == null ? "Prekinuto brisanje nastavnika" : $"Uspesno izbrisan nastavnik {nastavnikZaBrisanje.Ime}";
				Console.WriteLine(finalPrintOut);
            }

        }

        private static void edit()
        {
			Nastavnik nastavnik = PomocnaKlasa.nameToNastavnik();
			if(nastavnik != null)
            {
				Console.WriteLine($"Unesite novo ime nastavnika: ");
				string novoIme = Console.ReadLine();
				Console.WriteLine($"Unesite novo prezime nastavnika: ");
				string novoPrezime = Console.ReadLine();
				Console.WriteLine($"Unesite novo zvanje nastavnika: ");
				string novoZvanje = Console.ReadLine();

				nastavnik.Ime = novoIme;
				nastavnik.Prezime = novoPrezime;
				nastavnik.Zvanje = novoZvanje;

				Console.WriteLine($"Uspesno promenjeni podaci o nastavniku: \n\t {nastavnik}");
            }
            else
            {
				Console.WriteLine("Ne postoji nastavnk sa unesenim imenom.");
            }
			

			


        }

        private static void create()
        {
			Console.WriteLine($"Unesite ime nastavnika: ");
			string ime = Console.ReadLine();
			Console.WriteLine($"Unesite prezime nastavnika: ");
			string prezime = Console.ReadLine();
			Console.WriteLine($"Unesite zvanje nastavnika: ");
			string zvanje = Console.ReadLine();

			if(ime !=null && ime != "" && prezime != null && prezime != "" 
				&& zvanje != null && zvanje != "")
            {
				Nastavnik noviNastavnik = new Nastavnik(0,ime,prezime,zvanje);
				Nastavnik sacuvaniNastavnik = NastavnikServisImpl.save(noviNastavnik);

				if(sacuvaniNastavnik != null)
                {
					Console.WriteLine($"Uspesno sacuvan novi nastavnik: {zvanje}, {ime} {prezime}");

                }
                else
                {
					Console.WriteLine("*** Unos novog nastavnika nije uspeo.");
                }
            }

		}

		public static void ucitajNastavnikeSaFajla(string fajl)
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

					Nastavnik snimljeniNastavnik = NastavnikServisImpl.save(nastavnik);
					if(snimljeniNastavnik == null)
                    {
						Console.WriteLine("*** Greska pri snimanju nastavnika u memoriju.");
						break;
					}

					line = sr.ReadLine();
				}
				



            }catch(Exception e)
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
		public static void  zapisiNastavnikeUFajl(string fajl)
        {
			List<Nastavnik> sviNastavnici = NastavnikServisImpl.findAll();

			StreamWriter sw = new StreamWriter(fajl);
            try
            {
				foreach(Nastavnik it in sviNastavnici)
                {
					sw.WriteLine(it.toFileRepresentation());
                }

            }catch(Exception e)
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
