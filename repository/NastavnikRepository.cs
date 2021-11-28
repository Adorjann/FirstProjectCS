using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.model;

namespace FirstProjectCS.repository
{
    internal class NastavnikRepository
    {
        private static List<Nastavnik> sviNastavnici = new List<Nastavnik>();

        internal static Nastavnik findByName(string ime)
        {
            //SELECT nastavnik FROM nastavnik WHERE nastavnik.ime = ?
            foreach (Nastavnik n in sviNastavnici)
            {
                if (n.Ime == ime)
                    return n;
            }
            return null;
        }

        internal static Nastavnik findByImePrezimeZvanje(Nastavnik nastavnik)
        {
            foreach(Nastavnik n in sviNastavnici)
            {
                if(n.Ime == nastavnik.Ime &&
                    n.Prezime == nastavnik.Prezime &&
                    n.Zvanje == nastavnik.Zvanje)
                {
                    return n;
                }
            }
            return null;
        }

        internal static Nastavnik findById(int id)
        {
            //SELECT nastavnik FROM nastavnik WHERE nastavnik.id = ?
            foreach (Nastavnik n in sviNastavnici)
            {
                if (n.Id == id)
                    return n;
            }
            return null;
        }

        internal static List<Nastavnik> FindAll()
        {
            //SELECT * FROM nastavnici;

            return sviNastavnici;
        }

        internal static bool delete(Nastavnik nastavnikZaBrisanje)
        {
            //DELETE nastavnik FROM nastavnik WHERE nastavnik.id = ?
          return sviNastavnici.Remove(nastavnikZaBrisanje);
            
        }

        internal static Nastavnik save(Nastavnik nastavnik)
        {
            //INSERT INTO nastavnik VALUES(?,?);

                if (nastavnik.Id == 0) //dummy autoIncrement
                {
                    nastavnik.Id = NastavnikRepository.sviNastavnici.Count;
                    nastavnik.Id++;
                }
                sviNastavnici.Add(nastavnik);
                return nastavnik;
            
            
        }
    }
}
