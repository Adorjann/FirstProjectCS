using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FirstProjectCS.model;
using FirstProjectCS.servis.Impl;
using FirstProjectCS.utils;

namespace FirstProjectCS.ui
{
    internal class IspitnaPrijavaUI
    {

		private static void ispisiMenu()
		{
			Console.WriteLine("Rad sa ispitnim prijavama - opcije:");
			Console.WriteLine("\tOpcija broj 1 - ispitne prijave za ispitni rok");
			Console.WriteLine("\tOpcija broj 2 - ispitne prijave za studenta");
			Console.WriteLine("\tOpcija broj 3 - ispitne prijave za predmet");
			Console.WriteLine("\tOpcija broj 4 - dodavanje ispitne prijave");
			Console.WriteLine("\tOpcija broj 5 - uklanjanje ispitne prijave");
			Console.WriteLine("\t\t ...");
			Console.WriteLine("\tOpcija broj 0 - IZLAZ");
		}

		public static void meniIspitnaPrijava()
        {
			int odluka = -1;
			while(odluka != 0)
            {
				ispisiMenu();
				odluka = Convert.ToInt32(Console.ReadLine());
                switch (odluka) 
				{ 
					case 0:
						Console.WriteLine("Izlaz");
						break;
					case 1:
						IspisiIspitnePrijaveZaIspitniRok();
						break;
					case 2:
						IspisiIspitnePrijaveZaStudenta();
						break;
					case 3:
						IspisiIspitnePrijaveZaPredmet();
						break;
					case 4:
						dodajIspitnuPrijavu();
						break;
					case 5:
						izbrisiIspitnuPrijavu();
						break;
					default:
						Console.WriteLine("Nepostojeca komanda");
						break;
				}

            }
        }

        private static void izbrisiIspitnuPrijavu()
        {
			Student student = PomocnaKlasa.indexToStudent();
			Predmet predmet = PomocnaKlasa.nameToPredmet();
			IspitniRok iRok = PomocnaKlasa.nameToIspitniRok();
			
			
				Console.WriteLine("* Da li ste sigurni da zelite da obrisete ispitnu prijavu " +
					$"studenta {student.Ime} za predmet {predmet.Naziv} u ispitnom roku {iRok.Naziv} ?");
				bool response = PomocnaKlasa.yesOrNo();

			IspitnaPrijava obrisanaPrijava = IspitnaPrijavaServiceImpl.delete(student,predmet,iRok);

				string finallOdgovor =
					obrisanaPrijava == null ? "Brisanje ispitne prijave nije uspelo." : "Uspesno obrisana ispitna prijava!";
				
            Console.WriteLine(finallOdgovor);
		}

        private static void dodajIspitnuPrijavu()
        {
			Student student = PomocnaKlasa.indexToStudent();
			Predmet predmet = PomocnaKlasa.nameToPredmet();
			IspitniRok iRok = PomocnaKlasa.nameToIspitniRok();

			Console.WriteLine("Unesite bodove iz teorije:");
			int teorija = Convert.ToInt32(Console.ReadLine());

			Console.WriteLine("Unesite bodove iz zadataka:");
			int zadaci = Convert.ToInt32(Console.ReadLine());

			IspitnaPrijava ispitnaPrijava = new IspitnaPrijava(student,predmet,iRok,teorija,zadaci);
			IspitnaPrijava sacuvanaIP = IspitnaPrijavaServiceImpl.save(ispitnaPrijava);

			if(sacuvanaIP != null)
            {
				Console.WriteLine("Uspesno sacuvana nova ispitna Prijava!");
            }
            else
            {
				Console.WriteLine("kreiranje nove ispitne prijave nije uspelo.");
            }
		}

        private static void IspisiIspitnePrijaveZaPredmet()
        {
			Predmet predmet = PomocnaKlasa.nameToPredmet();
			if(predmet != null)
            {
				foreach(IspitnaPrijava ip in predmet.PredmetImaPrijavljeneIspitnePrijave)
                {
					Console.WriteLine(ip);
                }
            }
        }

        private static void IspisiIspitnePrijaveZaStudenta()
        {
			Student student = PomocnaKlasa.indexToStudent();
			if(student != null)
            {
				foreach(IspitnaPrijava ip in student.StudentPrijavljujeIspitnePrijave)
                {
					Console.WriteLine(ip);
                }
            }
        }

        private static void IspisiIspitnePrijaveZaIspitniRok()
        {
            IspitniRok iRok = PomocnaKlasa.nameToIspitniRok();
			if(iRok != null)
            {
				foreach(IspitnaPrijava ip in iRok.IspitniRokImaPrijavljeneIspitnePrijave)
                {
					Console.WriteLine(ip);
                }
            }
        }

		public static void ucitajIspitnePrijaveSaFajla(string fajl)
        {
			StreamReader sr = new StreamReader(fajl);
			string line = null;
            try
			{
				line = sr.ReadLine();
				while(line != null)
                {
					string[] tokeni = line.Split(',');

					Student student = StudentServisImpl.findById(Convert.ToInt32(tokeni[0]));
					Predmet predmet = PredmetServisImpl.findById(Convert.ToInt32(tokeni[1]));
					IspitniRok ispitniRok = IspitniRokServiceImpl.findById(Convert.ToInt32(tokeni[2]));	

					IspitnaPrijava iPrijava = new IspitnaPrijava();
					iPrijava.Student = student;
					iPrijava.Predmet = predmet;
					iPrijava.IspitniRok = ispitniRok;
					iPrijava.Teorija = Convert.ToInt32(tokeni[3]);
					iPrijava.Zadaci= Convert.ToInt32(tokeni[4]);

					
					IspitnaPrijavaServiceImpl.save(iPrijava);
					
					line = sr.ReadLine();

				}

			}
			catch (Exception e)
            {
				Console.WriteLine("*** Greska pri ucitavanju ispitne prijave sa fajla. ");
				Console.WriteLine("\n\t*** " + e.StackTrace);
				Console.WriteLine("\n\t*** " + e.Message);
			}
            finally
            {
				sr.Close();

				Console.WriteLine("Zavrseno ucitavanje ispitnih prijava");
			}

        }
		public static void zapisiIspitnePrijaveUFajl(string fajl)
        {
			List<IspitniRok> sviRokovi = IspitniRokServiceImpl.FindAll();

			StreamWriter sw = new StreamWriter(fajl);
			try {

				sviRokovi.ForEach(rok =>
					rok.IspitniRokImaPrijavljeneIspitnePrijave.ForEach(prijava =>
						sw.Write(prijava.toFileRepresentation())
					)
				); 

			}catch (Exception e)
            {
				Console.WriteLine("*** Greska pri zapisivanju ispitne prijave u fajl");
				Console.WriteLine("\n\t***"+e.StackTrace);
				Console.WriteLine("\n\t***" + e.Message);

			}
            finally
            {
				sw.Flush();
				sw.Close();
            }

        }
    }
}
