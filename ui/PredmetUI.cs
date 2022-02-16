using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FirstProjectCS.utils;
using FirstProjectCS.model;
using FirstProjectCS.servis.Impl;
using FirstProjectCS.exceptions;

namespace FirstProjectCS.ui
{
	public class PredmetUI
	{
		private readonly PredmetServisImpl predmetService = (PredmetServisImpl)SingletonCreator.GetInstance(typeof(PredmetServisImpl));
		private readonly PomocnaKlasa pomocnaKlasa = (PomocnaKlasa)SingletonCreator.GetInstance(typeof(PomocnaKlasa));

        private PredmetUI()
        {
        }

        private void IspisiTekstStudentOpcije()
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

		public void MeniPredmetUI()
		{
			int odluka = -1;

			while(odluka != 0)
			{
				odluka = Convert.ToInt32(Console.ReadLine());
				IspisiTekstStudentOpcije();



				switch (odluka)
				{
					case 0:
						Console.WriteLine("IZLAZ");
						break;
					case 1:
						//unos novog predmeta [POST]
						Create();
						break;
					case 2:
						//izmena predmeta [PUT]
						Edit();
						break;
					case 3:
						//brisanje predmeta [DELETE]
						Delete();
						break;
					case 4:
						//ispis svih predmeta [GET]
						GetAll(); //ponudi sortiranje
						break;
					case 5:
						//ispis 1 predmet i studenti koji pohadjaju taj predmet [GET]
						GetOne("studenti");
						break;
					case 6:
						//ispis 1 predmet i ispitne prijave za taj predmet [GET]
						GetOne("prijave");
						break;
					default:
						Console.WriteLine("Pogresna komanda");
						break;
					
						

						
				}

			}
			

		}

		private void GetOne(string kolekcija)
		{
            try
            {
				Predmet predmet = pomocnaKlasa.NameToPredmet();

				Console.WriteLine("\n-|- "+predmet);
				if (kolekcija.Equals("studenti") && predmet.PredmetPohadjajuStudenti.Count != 0)
				{
					Console.WriteLine("Predmet " + predmet.Naziv + " pohadjaju: \n");
					predmet.PredmetPohadjajuStudenti.ForEach(student => Console.WriteLine("\t" + student));
					
				}else if (kolekcija.Equals("prijave") && predmet.PredmetImaPrijavljeneIspitnePrijave.Count != 0)
				{
					predmet.PredmetImaPrijavljeneIspitnePrijave.ForEach(prijava => Console.WriteLine(prijava));
				}
			}
            catch (ObjectNotFoundException)
            {

                Console.WriteLine("Ne postoji predmet sa unesenim nazivom");
            }
			catch (Exception)
			{
				Console.WriteLine("Doslo je do greske.");
			}
		}

		private void GetAll()
		{
            try
            {
				List<Predmet> sviPredmeti = predmetService.FindAll();
			
				Console.WriteLine("Svi studenti: ");
				sviPredmeti.ForEach(predmet => Console.WriteLine("\t" + predmet));
			}
            catch (CollectionIsEmptyException)
            {
                Console.WriteLine("Nema Predmeta za ispis.");
            }
			catch (Exception)
			{
				Console.WriteLine("Doslo je do greske.");
			}
		}

		private void Delete()
		{
            try
            {
				Predmet predmetZaBrisanje = pomocnaKlasa.NameToPredmet();

				Console.WriteLine("Da li ste sigurni da zelite da obrisete predmet: \n\t" + predmetZaBrisanje);
				bool odgovor = pomocnaKlasa.YesOrNo();

				Predmet izbrisaniPredmet = odgovor ? predmetService.Delete(predmetZaBrisanje) : throw new OperationCanceledException();

				Console.WriteLine("Uspesno izbrisan predmet");
			}
			catch(OperationCanceledException) 
			{
                Console.WriteLine("Prekinuto brisanje predmeta");
			}
            catch (ObjectNotFoundException)
            {
                Console.WriteLine("Brisanje predmeta nije uspelo");
            }
			catch (Exception)
			{
				Console.WriteLine("Doslo je do greske.");
			}
		}

		private void Edit()
		{
            try
            {
				Predmet predmet = pomocnaKlasa.NameToPredmet();
			
				Console.WriteLine("Unesite Novi naziv Predmeta: ");
				string noviNaziv = Console.ReadLine();

				predmet.Naziv = noviNaziv;

				Console.WriteLine("Uspesno promenjen naziv predmeta: \n\t"+predmet);
			}
            catch (ObjectNotFoundException)
            {
				Console.WriteLine("Ne postoji predmet sa zadatim nazivom");
			}
			catch (Exception)
			{
				Console.WriteLine("Doslo je do greske.");
			}


		}

		private void Create()
		{
            try
            {
				Console.WriteLine("Unesite naziv novog predmeta:");
				string nazivNovogPredmeta = Console.ReadLine();	

				Predmet noviPredmet = new Predmet(0,nazivNovogPredmeta);
				Predmet sacuvaniPredmet = predmetService.Save(noviPredmet);

				Console.WriteLine("Uspesno sacuvan novi Predmet: "+nazivNovogPredmeta);
			}
            catch (DuplicateObjectException)
            {
				Console.WriteLine("*** Unos novog Predmeta nije uspeo.");
			}
            catch (InvalidOperationException)
            {
                Console.WriteLine("Pogresno unesen Naziv");
            }
			catch (Exception)
			{
				Console.WriteLine("Doslo je do greske.");
			}


		}

		public  void UcitajPredmeteSaFajla(string fajl)
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

					predmetService.Save(noviPredmet);
					
					line = sr.ReadLine();

				}

			}
			catch (DuplicateObjectException)
            {
                Console.WriteLine("Greska pri snimanju u bazu");
            }
			catch(Exception e)
			{
				Console.WriteLine("*** Greska pri ucitavanju predmeta sa fajla. ");
				Console.WriteLine("\n\t*** " + e.StackTrace);
				Console.WriteLine("\n\t*** " + e.Message);
			}
			finally
			{
				sr.Close();
				Console.WriteLine("Zavrseno ucitavanje predmeta");
			}
		}

		public void ZapisiPredmeteUFajl(string fajl)
		{
			StreamWriter sw = new StreamWriter(fajl);
			try
			{
				List<Predmet> sviPredmeti = predmetService.FindAll();

				sviPredmeti.ForEach(predmet => sw.WriteLine(predmet.ToFileReprezentation()));
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
