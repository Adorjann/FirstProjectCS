using FirstProjectCS.model;
using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.repository;

namespace FirstProjectCS.servis.Impl
{
    internal class NastavnikServisImpl
    {
        //Service
        
        

        internal static Nastavnik findByName(string ime)
        {
            return NastavnikRepository.findByName(ime);
        }
        internal static Nastavnik findImePrezimeZvanje(Nastavnik nastavnik)
        {
            return NastavnikRepository.findByImePrezimeZvanje(nastavnik);
        }

        internal static Nastavnik findById(int id)
        {
            return NastavnikRepository.findById(id);
        }

        internal static List<Nastavnik> findAll()
        {
            return NastavnikRepository.FindAll();
        }

        internal static Nastavnik delete(Nastavnik nastavnikZaBrisanje)
        {
            List<Nastavnik> sviNastavnici = findAll();
            if (sviNastavnici.Contains(nastavnikZaBrisanje))
            {
                bool sucess = NastavnikRepository.delete(nastavnikZaBrisanje);
                if (sucess && !sviNastavnici.Contains(nastavnikZaBrisanje))
                {
                    return nastavnikZaBrisanje;
                }
            }
            
            return null;
        }

        internal static Nastavnik save(Nastavnik nastavnik)
        {
            List<Nastavnik> sviNastavnici = findAll();

            if(nastavnik != null && !sviNastavnici.Contains(nastavnik))
            {
                return NastavnikRepository.save(nastavnik);
            }
            return null;
        }
    }
}
