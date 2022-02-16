using FirstProjectCS.model;
using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.repository;

namespace FirstProjectCS.servis.Impl
{
    public class IspitnaPrijavaServiceImpl
    {
          private readonly IspitnaPrijavaRepository iPrijavaRepo = (IspitnaPrijavaRepository)SingletonCreator.GetInstance(typeof(IspitnaPrijavaRepository));
          private readonly CustomException exceptions = (CustomException)SingletonCreator.GetInstance(typeof(CustomException));


        public IspitnaPrijava Save(IspitnaPrijava iPrijava)
        {
            Optional OsvePrijave = iPrijavaRepo.FindAll();
            Optional Oprijava = iPrijavaRepo.FindById(iPrijava.GetId());

            if (!Oprijava.IsPresent)
            {
                if (iPrijava.IspitniRok != null && iPrijava.Predmet != null && iPrijava.Student != null)
                {
                    Optional OsavedIRok = iPrijavaRepo.Save(iPrijava);
                    if (OsavedIRok.IsPresent)
                    {
                        return (IspitnaPrijava)OsavedIRok.Get();
                    }
                }
                throw new InvalidOperationException();
            }
            throw exceptions.GetDuplicateObjectException();
        }

        public List<IspitnaPrijava> FindAll()
        {
            Optional OsvePrijave = iPrijavaRepo.FindAll();
            if (OsvePrijave.IsPresent)
            {
                return (List<IspitnaPrijava>)OsvePrijave.Get();
            }
            throw exceptions.GetCollectionIsEmptyException();
        }

        public IspitnaPrijava FindById(int prijavaID)
        {
            Optional OIspitnaPrijava = iPrijavaRepo.FindById(prijavaID);
            if (OIspitnaPrijava.IsPresent)
            {
                return (IspitnaPrijava)OIspitnaPrijava.Get();
            }
            throw exceptions.GetCollectionIsEmptyException();
        }

        public IspitnaPrijava Delete(IspitnaPrijava iPrijava)
        {
            if(iPrijava != null && iPrijava.Student != null && iPrijava.Predmet != null && iPrijava.IspitniRok != null)
            {
                Optional OdeletedIPrijava = iPrijavaRepo.Delete(iPrijava);
                if (OdeletedIPrijava.IsPresent)
                {
                    iPrijava.Student.IzbaciIspitnuPrijavu(iPrijava);
                    iPrijava.Predmet.IzbaciIspitnuPrijavu(iPrijava);
                    iPrijava.IspitniRok.IzbaciIspitnuPrijavu(iPrijava);

                    return iPrijava;
                }
                throw exceptions.GetObjectNotFoundException();
            }
            throw new InvalidOperationException();
        }
    }
}
