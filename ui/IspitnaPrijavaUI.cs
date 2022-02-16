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
    public class IspitnaPrijavaUI
    {
		private readonly IspitnaPrijavaServiceImpl ipService = (IspitnaPrijavaServiceImpl)SingletonCreator.GetInstance(typeof(IspitnaPrijavaServiceImpl));
		private readonly PomocnaKlasa pomocnaKlasa = (PomocnaKlasa)SingletonCreator.GetInstance(typeof(PomocnaKlasa));
        
        public void IspisiMenu()
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

        public void MeniIspitnaPrijava()
        {
            int odluka = -1;
            while (odluka != 0)
            {
                IspisiMenu();
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
                        DodajIspitnuPrijavu();
                        break;
                    case 5:
                        IzbrisiIspitnuPrijavu();
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda");
                        break;
                }

            }
        }

        private void IzbrisiIspitnuPrijavu()
        {
            try
            {
                Student student = pomocnaKlasa.IndexToStudent();
                Predmet predmet = pomocnaKlasa.NameToPredmet();
                IspitniRok iRok = pomocnaKlasa.NameToIspitniRok();
                int prijavaID = Convert.ToInt32($"{student.Id}{predmet.Id}{iRok.Id}");

                IspitnaPrijava iPrijava = ipService.FindById(prijavaID);

                if (pomocnaKlasa.YesOrNo())
                {
                    IspitnaPrijava deletedIPrijava = ipService.Delete(iPrijava);

                    Console.WriteLine("Uspesno obrisana ispitna prijava!");

                }
                else
                {
                    throw new OperationCanceledException();
                }

            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Brisanje ispitne prijave je otkazano.");
            }
            catch (ObjectNotFoundException)
            {
                Console.WriteLine("Doslo je do greske");
            }
            catch (Exception)
            {
                Console.WriteLine("Doslo je do greske.");
            }
		}

        private void DodajIspitnuPrijavu()
        {
            try
            {
                Student student = pomocnaKlasa.IndexToStudent();
                Predmet predmet = pomocnaKlasa.NameToPredmet();
                IspitniRok iRok = pomocnaKlasa.NameToIspitniRok();

                Console.WriteLine("Unesite bodove iz teorije:");
                int teorija = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Unesite bodove iz zadataka:");
                int zadaci = Convert.ToInt32(Console.ReadLine());

                IspitnaPrijava ispitnaPrijava = new IspitnaPrijava(student, predmet, iRok, teorija, zadaci);
                ispitnaPrijava.SetId(); // void,bez parametara
                ipService.Save(ispitnaPrijava);
			    Console.WriteLine("Uspesno sacuvana nova ispitna Prijava!");

            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Kreiranje ispitne prijave prekinuto usled pogresno unesenih podataka.");
            }
            catch (DuplicateObjectException)
            {
                Console.WriteLine("Vec postoji ispitna prijava.");
            }
            catch (Exception)
            {
                Console.WriteLine("Doslo je do greske.");
            }
			
		}

        private void IspisiIspitnePrijaveZaPredmet()
        {
            try
            {
                Predmet predmet = pomocnaKlasa.NameToPredmet();

                predmet.PredmetImaPrijavljeneIspitnePrijave.ForEach(ip =>
                {

                    Console.WriteLine(ip);
                });
            }
            catch (ObjectNotFoundException)
            {
                Console.WriteLine("Nema predmeta sa zadatim imenom.");
            }
            catch (Exception)
            {
                Console.WriteLine("Doslo je do greske.");
            }
        }

        private  void IspisiIspitnePrijaveZaStudenta()
        {
            try
            {
                Student student = pomocnaKlasa.IndexToStudent();

                student.StudentPrijavljujeIspitnePrijave.ForEach(ip =>
                {

                    Console.WriteLine(ip);
                });

            }
            catch (ObjectNotFoundException)
            {
                Console.WriteLine("Neme studenta sa zadatim imenom");
            }
            catch (Exception)
            {
                Console.WriteLine("Doslo je do greske");
            }
        }

        private void IspisiIspitnePrijaveZaIspitniRok()
        {
            try
            {
                IspitniRok iRok = pomocnaKlasa.NameToIspitniRok();

                iRok.IspitniRokImaPrijavljeneIspitnePrijave.ForEach(ip =>
                {
                    Console.WriteLine(ip);
                });

            }
            catch (ObjectNotFoundException)
            {
                Console.WriteLine("Nema ispitnog roka sa zadatim imenom");
            }
            catch (Exception)
            {
                Console.WriteLine("Doslo je do greske.");
            }
        }

		public void UcitajIspitnePrijaveSaFajla(string fajl)
        {
			StreamReader sr = new StreamReader(fajl);
			string line = null;
            try
			{
				line = sr.ReadLine();
				while(line != null)
                {
					string[] tokeni = line.Split(',');

					Student student = pomocnaKlasa.FindStudentById(Convert.ToInt32(tokeni[0]));
					Predmet predmet = pomocnaKlasa.FindPredmetById(Convert.ToInt32(tokeni[1]));
					IspitniRok ispitniRok = pomocnaKlasa.FindIspitniRokById(Convert.ToInt32(tokeni[2]));	

					IspitnaPrijava iPrijava = new IspitnaPrijava();
					iPrijava.Student = student;
					iPrijava.Predmet = predmet;
					iPrijava.IspitniRok = ispitniRok;
					iPrijava.Teorija = Convert.ToInt32(tokeni[3]);
					iPrijava.Zadaci= Convert.ToInt32(tokeni[4]);

					//TODO update drugu stranu veze
					IspitnaPrijava sacuvanaIspitnaPrijava =  ipService.Save(iPrijava);
					
					student.DodajIspitnuPrijavu(sacuvanaIspitnaPrijava);
					predmet.DodajIspitnuPrijavu(sacuvanaIspitnaPrijava);
					ispitniRok.DodajIspitnuPrijavu(sacuvanaIspitnaPrijava);
                    
					line = sr.ReadLine();

				}

			}
            catch (DuplicateObjectException)
            {
                Console.WriteLine("Greska pri snimanju u bazu");
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
		public void ZapisiIspitnePrijaveUFajl(string fajl)
        {

			StreamWriter sw = new StreamWriter(fajl);
			try {
			    
                List<IspitnaPrijava> svePrijave = ipService.FindAll();

                svePrijave.ForEach(ip => { sw.WriteLine(ip.ToFileRepresentation()); });
				
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
