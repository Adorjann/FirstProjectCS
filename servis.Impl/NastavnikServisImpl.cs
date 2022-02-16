using FirstProjectCS.model;
using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.repository;

namespace FirstProjectCS.servis.Impl
{
    public class NastavnikServisImpl
    {
        //Service
        private readonly NastavnikRepository nastavnikRepository = 
            (NastavnikRepository)SingletonCreator.GetInstance(typeof(NastavnikRepository));
        private readonly CustomException exception =
            (CustomException)SingletonCreator.GetInstance(typeof(CustomException));
        private NastavnikServisImpl()
        {
        }

        public Nastavnik FindByName(string ime)
        {
         
            Optional Onastavnik = nastavnikRepository.FindByName(ime);
            if (Onastavnik.IsPresent)
            {
                return (Nastavnik)Onastavnik.Get();
            }
            throw exception.GetObjectNotFoundException();
            
        }
        public Nastavnik FindImePrezimeZvanje(string ime, string prezime, string zvanje)
        {
            Optional Onastavnik = nastavnikRepository.FindByImePrezimeZvanje(ime,prezime, zvanje);
            if (Onastavnik.IsPresent)
            {
                return (Nastavnik)Onastavnik.Get();
            }
            throw exception.GetObjectNotFoundException();
        }

        public Nastavnik FindById(int id)
        {
            Optional Onastavnik = nastavnikRepository.FindById(id);
            if (Onastavnik.IsPresent)
            {
                return (Nastavnik)Onastavnik.Get();
            }
            throw exception.GetObjectNotFoundException();
        }

        public List<Nastavnik> FindAll()
        {
            Optional OsviNastavnici = nastavnikRepository.FindAll();
            if (OsviNastavnici.IsPresent)
            {
                return (List<Nastavnik>)OsviNastavnici.Get();
            }
            throw exception.GetCollectionIsEmptyException();
        }

        public Nastavnik Delete(Nastavnik nastavnikZaBrisanje)
        {
            Optional Onastavnik = nastavnikRepository.Delete(nastavnikZaBrisanje);
            if (Onastavnik.IsPresent)
            {
                return (Nastavnik)Onastavnik.Get();
            }
            throw exception.GetObjectNotFoundException();
        }

        public Nastavnik Save(Nastavnik nastavnik)
        {
            Optional Onastavnik = nastavnikRepository.FindByImePrezimeZvanje(nastavnik.Ime, nastavnik.Prezime, nastavnik.Zvanje);
            Optional OsviNastavnici = nastavnikRepository.FindAll();

            if (!Onastavnik.IsPresent)
            {
                if (nastavnik.Ime != "" &&
                    nastavnik.Prezime != "" &&
                    nastavnik.Zvanje != "")
                {
                    if (nastavnik.Id == 0) //dummy autoIncrement
                    {
                        List<Nastavnik> sviNastavnici = OsviNastavnici.IsPresent ?
                           (List<Nastavnik>)OsviNastavnici.Get() : new List<Nastavnik>();

                        nastavnik.Id = sviNastavnici.Count;
                        nastavnik.Id++;
                    }
                    Optional OsavedNastavnik = nastavnikRepository.Save(nastavnik);

                    if (OsavedNastavnik.IsPresent)
                    {
                        return (Nastavnik)OsavedNastavnik.Get();
                    }
                }
                throw new InvalidOperationException();
            }
            throw exception.GetDuplicateObjectException();
            
        }
    }
}
