using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.model;
using FirstProjectCS.repository;

namespace FirstProjectCS.servis.Impl
{
    public class PredmetServisImpl
    {
        private PredmetRepository predmetRepo = (PredmetRepository)SingletonCreator.GetInstance(typeof(PredmetRepository));
        private CustomException exceptions = (CustomException)SingletonCreator.GetInstance(typeof(CustomException));

        private PredmetServisImpl()
        {
        }
        

        public  Predmet Save(Predmet predmet)
        {
            if (predmet.Naziv != "")
            {
                Optional Opredmet = predmetRepo.Save(predmet);
                if (Opredmet.IsPresent)
                {
                    return (Predmet)Opredmet.Get();
                }
                throw exceptions.GetDuplicateObjectException();
            }
            throw new InvalidOperationException();
        }

        public  Predmet FindOneByName(string imePredmeta)
        {
            Optional Opredmet = predmetRepo.FindByName(imePredmeta);
            if (Opredmet.IsPresent)
            {
                return (Predmet)Opredmet.Get();
            }
            throw exceptions.GetObjectNotFoundException();
        }
        public Predmet FindById(int id)
        {
            Optional Opredmet = predmetRepo.FindById(id);
            if (Opredmet.IsPresent)
            {
                return (Predmet)Opredmet.Get();
            }
            throw exceptions.GetObjectNotFoundException();
        }


        public Predmet Delete(Predmet predmetZaBrisanje)
        {
            Optional Opredmet = predmetRepo.Delete(predmetZaBrisanje);
            if (Opredmet.IsPresent)
            {
                return (Predmet)Opredmet.Get();
            }
            throw exceptions.GetObjectNotFoundException();
        }

        public  List<Predmet> FindAll()
        {
            Optional Opredmeti = predmetRepo.FindAll();
            if (Opredmeti.IsPresent)
            {
                return (List<Predmet>)Opredmeti.Get();
            }
            throw exceptions.GetCollectionIsEmptyException();

        }
    }
}
