using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.model;
using FirstProjectCS.servis.Impl;

namespace FirstProjectCS.utils
{
    internal class PomocnaKlasa
    {

        public static Student indexToStudent()
        {
            Console.WriteLine("Unesite index studenta :");
            string index = Console.ReadLine();
            Student s = StudentServisImpl.findByIndex(index);

            return s;
        }

        public static bool yesOrNo()
        {
            Console.WriteLine("Molim unesite  | da | i pritisnite ENTER za potvrdu. ");
            Console.WriteLine("Molim unesite  | ne | i pritisnite ENTER za prekid brisanja. ");

            string odgovor = Console.ReadLine();

            return odgovor == "da" ? true : false;
        }

        internal static Predmet nameToPredmet()
        {
            Console.WriteLine("Unesite naziv predmeta: ");
            string nazivPredmeta = Console.ReadLine();
            Predmet p = PredmetServisImpl.findOneByName(nazivPredmeta);

            return p;
        }

        public static Nastavnik nameToNastavnik()
        {
            Console.WriteLine("Unesite ime nastavnika: ");
            string ime = Console.ReadLine();
            Nastavnik n = NastavnikServisImpl.findByName(ime);

            return n;
        }

        internal static IspitniRok nameToIspitniRok()
        {
            Console.WriteLine("Unsesite naziv ispitnog roka: ");
            string uneseniNaziv = Console.ReadLine();
            IspitniRok ir = IspitniRokServiceImpl.findByName(uneseniNaziv);

            return ir;
        }
    }
}
