using FirstProjectCS.model;
using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.repository;

namespace FirstProjectCS.servis.Impl
{
    internal class IspitnaPrijavaServiceImpl
    {
           


        internal static IspitnaPrijava save(IspitnaPrijava iPrijava)
        {
            List<IspitnaPrijava> svePrijave = findAll();

            if (!svePrijave.Contains(iPrijava))
            {
                return IspitnaPrijavaRepository.save(iPrijava);
            }
            return null;
        }

        public static List<IspitnaPrijava> findAll()
        {
            return IspitnaPrijavaRepository.findAll();
        }

        internal static IspitnaPrijava findById(int prijavaID)
        {
            return IspitnaPrijavaRepository.findById(prijavaID);
        }

        internal static IspitnaPrijava delete(IspitnaPrijava iPrijava)
        {
            List<IspitnaPrijava> svePrijave = findAll();

            if(iPrijava != null && svePrijave.Contains(iPrijava))
            {
                bool success = IspitnaPrijavaRepository.delete(iPrijava);
                if (success)
                {
                    iPrijava.Student.izbaciIspitnuPrijavu(iPrijava);
                    iPrijava.Predmet.izbaciIspitnuPrijavu(iPrijava);
                    iPrijava.IspitniRok.izbaciIspitnuPrijavu(iPrijava);

                    return iPrijava;
                }
                return null;
            }
            return null;
        }
    }
}
