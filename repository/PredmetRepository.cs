using FirstProjectCS.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstProjectCS.repository
{
    internal class PredmetRepository
    {
        private static List<Predmet> sviPredmeti = new List<Predmet>();


        internal static Optional save(Predmet predmet)
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
                return Optional.Of(predmet);
            }
            return Optional.Empty();
        }

        internal static Optional findAll()
        {
            //SELECT * FROM predmet;
            if(sviPredmeti.Count == 0)
            {
                return Optional.Empty();
            }

            return Optional.Empty();
        }

        internal static Optional delete(Predmet predmetZaBrisanje)
        {
           //DELETE FROM predmet WHERE predmet.naziv = ?

            if (sviPredmeti.Remove(predmetZaBrisanje)) 
            {
                return Optional.Of(predmetZaBrisanje);
            }

            return Optional.Empty();

        }

        public static Optional findByName(string name)
        {
            //SELECT predmet WHERE predmet.naziv = ?

            
            foreach (Predmet predmet in sviPredmeti)
            {

                if(predmet.Naziv.StartsWith(name))
                {
                    return Optional.Of(predmet);
                    
                }
            }
            return Optional.Empty();
        }
        public static Optional findById(int id)
        {
            //SELECT predmet WHERE predmet.id = ?

            foreach (Predmet predmet in sviPredmeti)
            {
                if(predmet.Id == id)
                {
                    return Optional.Of(predmet);
                    
                }

            }
            return Optional.Empty();
        }
    }
}
