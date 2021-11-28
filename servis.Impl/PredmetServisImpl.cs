using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.model;
using FirstProjectCS.repository;

namespace FirstProjectCS.servis.Impl
{
    internal class PredmetServisImpl
    {



        public static Predmet save(Predmet predmet)
        {
            return PredmetRepository.save(predmet);
        }

        public static Predmet findOneByName(string imePredmeta)
        {
            return PredmetRepository.findByName(imePredmeta); 
        }
        public static Predmet findById(int id)
        {
            return PredmetRepository.findById(id);
        }


        public static Predmet delete(Predmet predmetZaBrisanje)
        {
            return PredmetRepository.delete(predmetZaBrisanje);
        }

        public static List<Predmet> findAll()
        {
            return PredmetRepository.findAll();
        }
    }
}
