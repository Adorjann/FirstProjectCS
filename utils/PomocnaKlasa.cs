using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.exceptions;
using FirstProjectCS.model;
using FirstProjectCS.servis.Impl;

namespace FirstProjectCS.utils
{
	public class PomocnaKlasa
	{
		private readonly CustomException exceptions = (CustomException)SingletonCreator.GetInstance(typeof(CustomException));
		private readonly StudentServisImpl studentService = (StudentServisImpl)SingletonCreator.GetInstance(typeof(StudentServisImpl));
		private readonly PredmetServisImpl predmetService = (PredmetServisImpl)SingletonCreator.GetInstance(typeof(PredmetServisImpl));
		private readonly NastavnikServisImpl nastavnikService = (NastavnikServisImpl)SingletonCreator.GetInstance(typeof(NastavnikServisImpl));
		private readonly IspitniRokServiceImpl irService = (IspitniRokServiceImpl)SingletonCreator.GetInstance(typeof(IspitniRokServiceImpl));

		private PomocnaKlasa()
		{

		}


		public  Student IndexToStudent()
		{
			Console.WriteLine("Unesite index studenta :");
			string index = Console.ReadLine();
			
			try 
			{	        
				Student s = (Student)studentService.FindByIndex(index);
				return s;
			}
			catch (Exception ) 
			{
				throw exceptions.GetObjectNotFoundException();
			}
				

		}

		public  bool YesOrNo()
		{
			Console.WriteLine("Molim unesite  | da | i pritisnite ENTER za potvrdu. ");
			Console.WriteLine("Molim unesite  | ne | i pritisnite ENTER za prekid brisanja. ");

			string odgovor = Console.ReadLine();

			return odgovor == "da";
		}

		public  Predmet NameToPredmet()
		{
			Console.WriteLine("Unesite naziv predmeta: ");
			string nazivPredmeta = Console.ReadLine();
            try
            {
				Predmet p = predmetService.FindOneByName(nazivPredmeta);

				return p;

            }
            catch (Exception)
            {

				throw exceptions.GetObjectNotFoundException();
            }
		}

		public  Nastavnik NameToNastavnik()
		{
            try
            {
				Console.WriteLine("Unesite ime nastavnika: ");
				string ime = Console.ReadLine();
				Nastavnik n = nastavnikService.FindByName(ime);

				return n;
            }
            catch (Exception)
            {
				throw exceptions.GetObjectNotFoundException();
            }
		}

		public  IspitniRok NameToIspitniRok()
		{
            try
            {
                Console.WriteLine("Unsesite naziv ispitnog roka: ");
                string uneseniNaziv = Console.ReadLine();
                IspitniRok ir = irService.FindByName(uneseniNaziv);

                return ir;
            }
            catch (ObjectNotFoundException)
            {
                throw exceptions.GetObjectNotFoundException() ;
            }
            catch (InvalidOperationException)
            {
				throw new InvalidOperationException();
            }
		}

		public IspitniRok FindIspitniRokById(int id)
        {
            try
            {
				IspitniRok irok = irService.FindById(id);
				return irok;
            }
            catch (Exception)
            {
				throw exceptions.GetObjectNotFoundException();
            }
        }

		public Student FindStudentById(int id)
		{
			try
			{
				Student student = studentService.FindById(id);
				return student;
			}
			catch (Exception)
			{
				throw exceptions.GetObjectNotFoundException();
			}
		}
		public Predmet FindPredmetById(int id)
		{
			try
			{
				Predmet predmet = predmetService.FindById(id);
				return predmet;
			}
			catch (Exception)
			{
				throw exceptions.GetObjectNotFoundException();
			}
		}
		public Nastavnik FindNastavnikById(int id)
		{
			try
			{
				Nastavnik nastavnik = nastavnikService.FindById(id);
				return nastavnik;
			}
			catch (Exception)
			{
				throw exceptions.GetObjectNotFoundException();
			}
		}
	}
}
