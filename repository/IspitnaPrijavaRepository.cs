using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.model;

namespace FirstProjectCS.repository
{
    internal class IspitnaPrijavaRepository
    {
        private static List<IspitnaPrijava> sveIspitnePrijave = new List<IspitnaPrijava>();

        internal static List<IspitnaPrijava> findAll()
        {
            return sveIspitnePrijave;
        }

        internal static IspitnaPrijava save(IspitnaPrijava iPrijava)
        {
            if (!sveIspitnePrijave.Contains(iPrijava))
            {
                sveIspitnePrijave.Add(iPrijava);
                return iPrijava;
            }
            return null;
        }

        internal static bool delete(IspitnaPrijava iPrijava)
        {
            return sveIspitnePrijave.Remove(iPrijava);
        }

        internal static IspitnaPrijava findById(int prijavaID)
        {
            IspitnaPrijava iPrijava = sveIspitnePrijave.Find(ip => ip.GetId() == prijavaID);

            if(iPrijava != null)
            {
                return iPrijava;
            }
            return null;
        }
    }
}
