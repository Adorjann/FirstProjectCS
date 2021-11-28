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
        internal static IspitniRok findByName(string uneseniNaziv)
        {
            return IspitniRokRepository.findByName(uneseniNaziv);
        }
        internal static IspitniRok findById(int id)
        {
            return IspitniRokRepository.findById(id);
        }

        internal static List<IspitniRok> FindAll()
        {
            return IspitniRokRepository.findAll();
        }

        internal static IspitniRok Save(IspitniRok ir)
        {
            List<IspitniRok> sviRokovi = FindAll();

            if(!sviRokovi.Contains(ir) && ir != null)
            {
                return IspitniRokRepository.save(ir);
            }
            return null;
        }

        internal static IspitniRok Delete(IspitniRok rokZaBrisanje)
        {
            List<IspitniRok> sviRokovi = FindAll();
            if (sviRokovi.Contains(rokZaBrisanje))
            {
                bool sucess = IspitniRokRepository.Delete(rokZaBrisanje);
                if (sucess && !sviRokovi.Contains(rokZaBrisanje))
                {
                    return rokZaBrisanje;
                }
            }
            
            return null;

        }

        
    }
}
