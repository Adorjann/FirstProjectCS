using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.model;


namespace FirstProjectCS.repository
{
    internal class IspitniRokRepository
    {

        private static List<IspitniRok> sviRokovi = new List<IspitniRok>();




        internal static IspitniRok findByName(string uneseniNaziv)
        {

            //SELECT ir FROM ispitni_rok ir WHERE ir.Naziv = ?
            foreach(IspitniRok ir in sviRokovi)
            {
                if(ir.Naziv.ToLower() == uneseniNaziv.ToLower())
                {
                    return ir;
                }
            }
            return null;
        }

        internal static IspitniRok findById(int id)
        {
            //SELECT ir FROM ispitni_rok ir WHERE ir.id = ?
            foreach (IspitniRok ir in sviRokovi)
            {
                if (ir.Id == id)
                {
                    return ir;
                }
            }
            return null;
        }

        internal static IspitniRok save(IspitniRok ir)
        {

            //INSERT INTO ispitni_rok VALUES (????);
            if (ir.Id == 0) //dummy autoincrament
            {
                ir.Id = IspitniRokRepository.sviRokovi.Count;
                ir.Id++;
            }
            sviRokovi.Add(ir);
            return ir;
        }

        internal static List<IspitniRok> findAll()
        {
            //SELECT * FROM ispitni_rok;
          return sviRokovi;
        }

        internal static bool Delete(IspitniRok rokZaBrisanje)
        {
            //DELETE ispitni_rok ir WHERE ir.id = ?
            return IspitniRokRepository.sviRokovi.Remove(rokZaBrisanje);
        }
    }

    

}
