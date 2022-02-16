using FirstProjectCS.model;
using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.repository;

namespace FirstProjectCS.servis.Impl
{

    internal class IspitniRokServiceImpl
    {
        //Service

        private readonly IspitniRokRepository irRepo = (IspitniRokRepository)SingletonCreator.GetInstance(typeof(IspitniRokRepository));
        private readonly CustomException exception = (CustomException)SingletonCreator.GetInstance(typeof(CustomException));
        
        private IspitniRokServiceImpl()
        {
        }

        public IspitniRok FindByName(string uneseniNaziv)
        {
            if (uneseniNaziv != "")
            {
                Optional OIspitniRok = irRepo.FindByName(uneseniNaziv);
                if (OIspitniRok.IsPresent)
                {
                    return (IspitniRok)OIspitniRok.Get();
                }
                throw exception.GetObjectNotFoundException(); 
            }
            throw new InvalidOperationException();
        }
        public IspitniRok FindById(int id)
        {
            
            Optional OIspitniRok = irRepo.FindById(id);
            if (OIspitniRok.IsPresent)
            {
                return (IspitniRok)OIspitniRok.Get();
            }
            throw exception.GetObjectNotFoundException();
        }

        public List<IspitniRok> FindAll()
        {
            Optional OsviRokovi = irRepo.FindAll();
            if (OsviRokovi.IsPresent)
            {
                return (List<IspitniRok>)OsviRokovi.Get();
            }
            throw exception.GetCollectionIsEmptyException();
        }

        public IspitniRok Save(IspitniRok ir)
        {
            Optional OsviRokovi = irRepo.FindAll();
            Optional OIspitniROk = irRepo.FindByName(ir.Naziv);

            if (!OIspitniROk.IsPresent)
            {
                if (ir.Naziv != "" && 
                    ir.Pocetak != null &&
                    ir.Kraj != null)
                {
                    if (ir.Id == 0) //dummy autoincrament
                    {
                        List<IspitniRok> sviRokovi = OsviRokovi.IsPresent ?
                            (List<IspitniRok>)OsviRokovi.Get() : new List<IspitniRok>();

                        ir.Id = sviRokovi.Count;
                        ir.Id++;
                    }
                    Optional OsavedIspitniRok = irRepo.Save(ir);

                    if (OsavedIspitniRok.IsPresent)
                    {
                        return (IspitniRok)OsavedIspitniRok.Get();
                    } 
                }
                throw new InvalidOperationException();
            }
            throw exception.GetDuplicateObjectException();
        }

        public IspitniRok Delete(IspitniRok rokZaBrisanje)
        {
            Optional OIspitniRok = irRepo.Delete(rokZaBrisanje);
            if (OIspitniRok.IsPresent)
            {
                return (IspitniRok)OIspitniRok.Get();
            }
            throw exception.GetObjectNotFoundException();
        }

        
    }
}
