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


        public  Student IndexToStudent()
        {
            Console.WriteLine("Unesite index studenta :");
            string index = Console.ReadLine();

            Student s = (Student)studentService.FindByIndex(index);
            return s;

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
            Predmet p = PredmetServisImpl.findOneByName(nazivPredmeta);

            return p;
        }

        public  Nastavnik NameToNastavnik()
        {
            Console.WriteLine("Unesite ime nastavnika: ");
            string ime = Console.ReadLine();
            Nastavnik n = NastavnikServisImpl.findByName(ime);

            return n;
        }

        public  IspitniRok NameToIspitniRok()
        {
            Console.WriteLine("Unsesite naziv ispitnog roka: ");
            string uneseniNaziv = Console.ReadLine();
            IspitniRok ir = IspitniRokServiceImpl.findByName(uneseniNaziv);

            return ir;
        }
    }
}
