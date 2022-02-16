using FirstProjectCS.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstProjectCS.repository
{
    public class PredmetRepository
    {
        private static List<Predmet> sviPredmeti = new List<Predmet>();

        private PredmetRepository()
        {
        }

        public  Optional Save(Predmet predmet)
        {
            //INSERT INTO predmet VALUES(?,?);
            
            Optional Opredmet = FindByName(predmet.Naziv);

            if (!Opredmet.IsPresent && 
                (predmet != null) && 
                (predmet.Naziv != ""))
            {
                if (predmet.Id == 0) //dummy autoIncrement
                {   
                    predmet.Id = sviPredmeti.Count;
                    predmet.Id++;
                }
                sviPredmeti.Add(predmet);
                return Optional.Of(predmet);
            }
            return Optional.Empty();
        }

        public Optional FindAll()
        {
            //SELECT * FROM predmet;
            if(sviPredmeti.Count != 0)
            {
                return Optional.Of(sviPredmeti);
            }

            return Optional.Empty();
        }

        public Optional Delete(Predmet predmetZaBrisanje)
        {
           //DELETE FROM predmet WHERE predmet.naziv = ?

            if (sviPredmeti.Remove(predmetZaBrisanje)) 
            {
                return Optional.Of(predmetZaBrisanje);
            }

            return Optional.Empty();

        }

        public Optional FindByName(string name)
        {
            //SELECT predmet WHERE predmet.naziv LIKE = ?%

            Predmet predmet = sviPredmeti.Find(p => p.Naziv.StartsWith(name));
            
            if (predmet != null)
            {
                return Optional.Of(predmet);
            }

            return Optional.Empty();
        }
        public Optional FindById(int id)
        {
            //SELECT predmet WHERE predmet.id = ?

            Predmet predmet = sviPredmeti.Find(p => p.Id == id);

            if (predmet != null)
            {
                return Optional.Of(predmet);
            }

            return Optional.Empty();
        }
    }
}
