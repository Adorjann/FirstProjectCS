using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.model;


namespace FirstProjectCS.repository
{
    internal class IspitniRokRepository
    {

        private static List<IspitniRok> sviRokovi = new List<IspitniRok>();

        internal static Optional findByName(string uneseniNaziv)
        {
            //SELECT ir FROM ispitni_rok ir WHERE ir.Naziv = ?
            foreach(IspitniRok ir in sviRokovi)
            {
                if (ir.Naziv.ToLower() == uneseniNaziv.ToLower())
                {
                    return Optional.Of(ir);
                }
            }
            return Optional.Empty();
        }

        internal static Optional findById(int id)
        {
            //SELECT ir FROM ispitni_rok ir WHERE ir.id = ?
            foreach (IspitniRok ir in sviRokovi)
            {
                if (ir.Id == id)
                {
                    return Optional.Of(ir);
                }
            }
            return Optional.Empty();
        }

        internal static Optional save(IspitniRok ir)
        {
            //INSERT INTO ispitni_rok VALUES (????);
            Optional OispitniRok = findByName(ir.Naziv);

            if (!OispitniRok.IsPresent && ir != null)
            {
                if (ir.Id == 0) //dummy autoincrament
                {
                    ir.Id = IspitniRokRepository.sviRokovi.Count;
                    ir.Id++;
                }
                sviRokovi.Add(ir);
                return Optional.Of(ir);
            }

            return Optional.Empty();
        }

        internal static Optional  findAll()
        {
            //SELECT * FROM ispitni_rok;
            if (sviRokovi.Count == 0)
            {
                return Optional.Empty();
            }
            return Optional.Of(sviRokovi);
        }

        internal static Optional Delete(IspitniRok rokZaBrisanje)
        {
            //DELETE ispitni_rok ir WHERE ir.id = ?
            if (sviRokovi.Remove(rokZaBrisanje))
            {
                return Optional.Of(sviRokovi);
            }
            return Optional.Empty();
        }
    }

    

}
