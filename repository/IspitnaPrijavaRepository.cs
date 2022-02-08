using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.model;

namespace FirstProjectCS.repository
{
    internal class IspitnaPrijavaRepository
    {
        private static List<IspitnaPrijava> sveIspitnePrijave = new List<IspitnaPrijava>();

        internal static Optional FindAll()
        {
            if (sveIspitnePrijave.Count == 0)
            {
                return Optional.Empty();
            }
            return Optional.Of(sveIspitnePrijave);
        }

        internal static Optional Save(IspitnaPrijava iPrijava)
        {
            if (!sveIspitnePrijave.Contains(iPrijava))
            {
                sveIspitnePrijave.Add(iPrijava);
                return Optional.Of(iPrijava);
            }
            return Optional.Empty();
        }

        internal static Optional Delete(IspitnaPrijava iPrijava)
        {
            if (sveIspitnePrijave.Remove(iPrijava))
            {
                return Optional.Of(iPrijava);
            }
            return Optional.Empty();
        }

        internal static Optional FindById(int prijavaID)
        {
            IspitnaPrijava iPrijava = sveIspitnePrijave.Find(ip => ip.GetId() == prijavaID);

            if(iPrijava != null)
            {
                return Optional.Of(iPrijava);
            }
            return Optional.Empty();
        }
    }
}
