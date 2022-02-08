using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.model;

namespace FirstProjectCS.repository
{
    internal class NastavnikRepository
    {
        private static List<Nastavnik> sviNastavnici = new List<Nastavnik>();

        internal static Optional findByName(string ime)
        {
            //SELECT nastavnik FROM nastavnik WHERE nastavnik.ime = ?
            foreach (Nastavnik n in sviNastavnici)
            {
                if (n.Ime.StartsWith(ime))
                    return Optional.Of(n);
            }
            return Optional.Empty();
        }

        internal static Optional findByImePrezimeZvanje(Nastavnik nastavnik)
        {
            foreach(Nastavnik n in sviNastavnici)
            {
                if(n.Ime == nastavnik.Ime &&
                    n.Prezime == nastavnik.Prezime &&
                    n.Zvanje == nastavnik.Zvanje)
                {
                    return Optional.Of(n);
                }
            }
            return Optional.Empty();
        }

        internal static Optional findById(int id)
        {
            //SELECT nastavnik FROM nastavnik WHERE nastavnik.id = ?
            foreach (Nastavnik n in sviNastavnici)
            {
                if (n.Id == id)
                    return Optional.Of(n);
            }
            return Optional.Empty();
        }

        internal static Optional FindAll()
        {
            //SELECT * FROM nastavnici;

            if(sviNastavnici.Count == 0)
            {
                return Optional.Empty();
            }
            return Optional.Of(sviNastavnici);
        }

        internal static Optional Delete(Nastavnik nastavnikZaBrisanje)
        {
            //DELETE nastavnik FROM nastavnik WHERE nastavnik.id = ?
            if (sviNastavnici.Remove(nastavnikZaBrisanje))
            {
                return Optional.Of(nastavnikZaBrisanje);
            }
            return Optional.Empty();
        }

        internal static Optional save(Nastavnik nastavnik)
        {
            //INSERT INTO nastavnik VALUES(?,?);
            Optional Onastavnik = findByImePrezimeZvanje(nastavnik);
            if (!Onastavnik.IsPresent && nastavnik != null)
            {
                if (nastavnik.Id == 0) //dummy autoIncrement
                {
                    nastavnik.Id = NastavnikRepository.sviNastavnici.Count;
                    nastavnik.Id++;
                }
                sviNastavnici.Add(nastavnik);
                return Optional.Of(nastavnik);
            }
            return Optional.Empty();
        }
    }
}
