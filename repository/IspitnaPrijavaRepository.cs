using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.model;

namespace FirstProjectCS.repository
{
    public class IspitnaPrijavaRepository
    {
        private static List<IspitnaPrijava> sveIspitnePrijave = new List<IspitnaPrijava>();

        public Optional FindAll()
        {
            if (sveIspitnePrijave.Count == 0)
            {
                return Optional.Empty();
            }
            return Optional.Of(sveIspitnePrijave);
        }

        public Optional Save(IspitnaPrijava iPrijava)
        {
            if (!sveIspitnePrijave.Contains(iPrijava))
            {
                sveIspitnePrijave.Add(iPrijava);
                return Optional.Of(iPrijava);
            }
            return Optional.Empty();
        }

        public Optional Delete(IspitnaPrijava iPrijava)
        {
            if (sveIspitnePrijave.Remove(iPrijava))
            {
                return Optional.Of(iPrijava);
            }
            return Optional.Empty();
        }

        public Optional FindById(int prijavaID)
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
