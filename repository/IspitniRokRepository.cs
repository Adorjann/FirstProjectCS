using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.model;


namespace FirstProjectCS.repository
{
    public class IspitniRokRepository
    {

        private static List<IspitniRok> sviRokovi = new List<IspitniRok>();

        private IspitniRokRepository()
        {
        }

        public Optional FindByName(string uneseniNaziv)
        {
            //SELECT ir FROM ispitni_rok ir WHERE ir.Naziv = ?
            IspitniRok iRok = sviRokovi.Find(ir => ir.Naziv.ToLower() == uneseniNaziv.ToLower());
            
            if (iRok != null)
            {
                return Optional.Of(iRok);
            }
            return Optional.Empty();
        }

        public Optional FindById(int id)
        {
            //SELECT ir FROM ispitni_rok ir WHERE ir.id = ?
            IspitniRok iRok = sviRokovi.Find(ir => ir.Id == id);

            if (iRok != null)
            {
                return Optional.Of(iRok);
            }
            return Optional.Empty();
        }

        public Optional Save(IspitniRok ir)
        {
            //INSERT INTO ispitni_rok VALUES (????);
            

            
            sviRokovi.Add(ir);
            return Optional.Of(ir);
        }

        public Optional  FindAll()
        {
            //SELECT * FROM ispitni_rok;

            if (sviRokovi.Count == 0)
            {
                return Optional.Empty();
            }
            return Optional.Of(sviRokovi);
        }

        public Optional Delete(IspitniRok rokZaBrisanje)
        {
            //DELETE ispitni_rok ir WHERE ir.id = ?

            if (sviRokovi.Remove(rokZaBrisanje))
            {
                return Optional.Of(rokZaBrisanje);
            }
            return Optional.Empty();
        }
    }

    

}
