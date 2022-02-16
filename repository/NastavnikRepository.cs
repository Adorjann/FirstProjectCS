using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.model;

namespace FirstProjectCS.repository
{
    public class NastavnikRepository
    {
        private static List<Nastavnik> sviNastavnici = new List<Nastavnik>();
        private NastavnikRepository()
        {
        }

        public Optional FindByName(string ime)
        {
            //SELECT nastavnik FROM nastavnik WHERE nastavnik.ime = ?
            Nastavnik nastavnik = sviNastavnici.Find(n => n.Ime.StartsWith(ime));

            if (nastavnik != null)
            {
                return Optional.Of(nastavnik);
            }
            return Optional.Empty();
        }

        public Optional FindByImePrezimeZvanje(string ime, string prezime, string zvanje)
        {
            Nastavnik nastavnik = sviNastavnici.Find(n =>
                    n.Ime == ime &&
                    n.Prezime == prezime &&
                    n.Zvanje == zvanje);

            if (nastavnik != null)
            {
                return Optional.Of(nastavnik);
            }
            return Optional.Empty();
        }

        public Optional FindById(int id)
        {
            //SELECT nastavnik FROM nastavnik WHERE nastavnik.id = ?
            Nastavnik nastavnik = sviNastavnici.Find(n => n.Id == id);

            if (nastavnik != null)
            {
                return Optional.Of(nastavnik);
            }
            return Optional.Empty();
        }

        public Optional FindAll()
        {
            //SELECT * FROM nastavnici;

            if(sviNastavnici.Count == 0)
            {
                return Optional.Empty();
            }
            return Optional.Of(sviNastavnici);
        }

        public Optional Delete(Nastavnik nastavnikZaBrisanje)
        {
            //DELETE nastavnik FROM nastavnik WHERE nastavnik.id = ?
            if (sviNastavnici.Remove(nastavnikZaBrisanje))
            {
                return Optional.Of(nastavnikZaBrisanje);
            }
            return Optional.Empty();
        }

        public Optional Save(Nastavnik nastavnik)
        {
            //INSERT INTO nastavnik VALUES(?,?);
            
            sviNastavnici.Add(nastavnik);
            return Optional.Of(nastavnik);
        }
    }
}
