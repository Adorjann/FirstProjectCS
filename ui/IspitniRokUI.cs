using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FirstProjectCS.model;
using FirstProjectCS.servis.Impl;
using FirstProjectCS.utils;

namespace FirstProjectCS.ui
{
    internal class IspitniRokUI
    {
		public static void ispisiTekstIspitniRokOpcije()
		{
			Console.WriteLine("Rad sa Ispitnim Rokovima - opcije:");
			Console.WriteLine("\tOpcija broj 1 - unos podataka o novom ispitnom roku");
			Console.WriteLine("\tOpcija broj 2 - izmena podataka o ispitnom roku");
			Console.WriteLine("\tOpcija broj 3 - brisanje podataka o ispitnom roku");
			Console.WriteLine("\tOpcija broj 4 - ispis podataka svih ispitnih rokova");//sortiranje
			Console.WriteLine("\tOpcija broj 5 - ispis podataka o odredjenom Ispitnom roku sa njegovim Ispitnim prijavama");
			Console.WriteLine("\t\t ...");
			Console.WriteLine("\tOpcija broj 0 - IZLAZ");
		}

		public static void meniIspitniRokUI()
        {
			int odluka = -1;
			while(odluka != 0)
            {
				ispisiTekstIspitniRokOpcije();
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

        private static void GetOne(string prijave)
        {
			IspitniRok ispitniRok = PomocnaKlasa.nameToIspitniRok();
			if(ispitniRok != null)
            {
				Console.WriteLine("\n-|- " + ispitniRok);
				if(prijave == "prijave")
                {
					Console.WriteLine("*** Ovaj feature jos uvek nije implementiran");
					//TODO: ispisivanje ISpitnih prijava kada dimplementiras ispitne Prijave
				}
			}
        }

        private static void GetAll()
        {
			List<IspitniRok> sviRokovi = IspitniRokServiceImpl.FindAll();

			if(sviRokovi != null && sviRokovi.Count > 0)
            {
				Console.WriteLine("Svi Ispitni Rokovi: ");
				foreach(IspitniRok ir in sviRokovi)
                {
					Console.WriteLine($"\t {ir}");
                }
            }
		}

        private static void Delete()
        {
			IspitniRok  rokZaBrisanje = PomocnaKlasa.nameToIspitniRok();
			if(rokZaBrisanje != null)
            {
				Console.WriteLine($"Da li ste sigurni da zelite da izbrisete {rokZaBrisanje.Naziv} ispitni rok? ");
				bool odgovor = PomocnaKlasa.yesOrNo();

				IspitniRok izbrisaniRok = odgovor ? IspitniRokServiceImpl.Delete(rokZaBrisanje) : null;

				string finalPrintOut = izbrisaniRok == null ? 
					$"Prekinuto brisanje {rokZaBrisanje.Naziv} ispitnog roka" : $"Uspesno izbrisan {rokZaBrisanje.Naziv} ispitni rok.";

				Console.WriteLine(finalPrintOut);
            }


        }

        private static void Edit()
        {
			IspitniRok rokZaBrisanje = PomocnaKlasa.nameToIspitniRok();
			if(rokZaBrisanje != null)
            {


            }
            else
            {
				Console.WriteLine("Ne postoji ispitni rok sa zadatim imenom.");
            }
		}

        private static void Create()
        {
			Console.Write("Unesite naziv novog ispitnog roka:");
			string nazivNovog = Console.ReadLine();
			Console.WriteLine("Unesite datum pocetka novog ispitnog roka: \n\tyyyy-MM-dd");
			string pocetakNovog = Console.ReadLine();
			Console.WriteLine("Unesite datum kraj novog ispitnog roka: \n\tyyyy-MM-dd");
			string krajNovog = Console.ReadLine();

			TODO: zavrsi 

		}

		public static void ucitajIspitniRokSaFajla(string fajl)
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



					IspitniRok snimljeniISpitniRok = IspitniRokServiceImpl.Save(ir);
					if(ir == null)
                    {
						Console.WriteLine("*** Greska pri snimanju Ispitnog Roka u memoriju.");
						break;
					}

					line = sr.ReadLine();
				}

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
			}
		}
		public static void ZapisiIspitniRokUFajl(string fajl)
        {
			List<IspitniRok> sviRokovi = IspitniRokServiceImpl.FindAll();

			StreamWriter sw = new StreamWriter(fajl);
            try
            {
				foreach(IspitniRok ir in sviRokovi)
                {
					sw.WriteLine(ir.toFileReprezentation());
                }
            }catch (Exception e)
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

		private static DateTime StringToDate(string dateInString)
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
