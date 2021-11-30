using FirstProjectCS.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstProjectCS.repository
{
    internal class PredmetRepository
    {
        private static List<Predmet> sviPredmeti = new List<Predmet>();


        internal static Predmet save(Predmet predmet)
        {
            //INSERT INTO predmet VALUES(?,?);
            
            Predmet testingP = findByName(predmet.Naziv);

            if(testingP == null && predmet != null)
            {
                if(predmet.Id == 0) //dummy autoIncrement
                {   
                    predmet.Id = sviPredmeti.Count;
                    predmet.Id++;
                }
                sviPredmeti.Add(predmet);
                return predmet;
            }
            return null;
        }

        internal static List<Predmet> findAll()
        {
            //SELECT * FROM predmet;


            return sviPredmeti;
        }

        internal static Predmet delete(Predmet predmetZaBrisanje)
        {
           //DELETE FROM predmet WHERE predmet.naziv = ?

            if (sviPredmeti.Contains(predmetZaBrisanje)) { sviPredmeti.Remove(predmetZaBrisanje); }
            if (!sviPredmeti.Contains(predmetZaBrisanje)) { return predmetZaBrisanje; }

            return null;

        }

        public static Predmet findByName(string name)
        {
            //SELECT predmet WHERE predmet.naziv = ?

            Predmet retVal = null;
            foreach (Predmet predmet in sviPredmeti)
            {

                if(predmet.Naziv.StartsWith(name))
                {
                    return retVal = predmet;
                    
                }
            }

            return retVal;
        }
        public static Predmet findById(int id)
        {
            //SELECT predmet WHERE predmet.id = ?

            Predmet retVal = null;
            foreach (Predmet predmet in sviPredmeti)
            {
                if(predmet.Id == id)
                {
                    return retVal = predmet;
                    
                }

            }
            return retVal;
        }
    }
}
