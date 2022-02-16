using System;
using System.Collections.Generic;
using System.Text;

namespace FirstProjectCS.model
{
    public class Nastavnik
    {

        private int id;
        private string ime;
        private string prezime;
        private string zvanje;

        private List<Predmet> nastavnikPredaje = new List<Predmet>();


        //konstruktori
        public Nastavnik() { }

        public Nastavnik(int id, string ime, string prezime, string zvanje)
        {
            this.id = id;
            this.ime = ime;
            this.prezime = prezime;
            this.zvanje = zvanje;
        }

        public Nastavnik(int id, string ime, string prezime, string zvanje, List<Predmet> nastavnikPredaje)
        {
            this.id = id;
            this.ime = ime;
            this.prezime = prezime;
            this.zvanje = zvanje;
            this.nastavnikPredaje = nastavnikPredaje;
        }

        // get/set
        public int Id { get => id; set => id = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public string Zvanje { get => zvanje; set => zvanje = value; }
        internal List<Predmet> NastavnikPredaje { get => nastavnikPredaje; set => nastavnikPredaje = value; }


        //metode
        public override string ToString()
        {
            return $"| {this.id} | {this.ime} | {this.prezime} | {this.zvanje} | ";
        }
        public string ToFileRepresentation()
        {
            return $"{this.id},{this.ime},{this.prezime},{this.zvanje}";
        }

        public override bool Equals(object obj)
        {
            return obj is Nastavnik nastavnik &&
                   id == nastavnik.id &&
                   ime == nastavnik.ime &&
                   prezime == nastavnik.prezime &&
                   zvanje == nastavnik.zvanje;
        }

        public List<Predmet> DodajPredmet(Predmet predmet)
        {
            try
            {
                if (predmet != null)
                {
                    if (this.nastavnikPredaje.Contains(predmet))
                    {
                        throw new AccessViolationException($"*** {this.ime} vec predaje predmet: {predmet.Naziv}");

                    }
                    else
                    {

                        this.nastavnikPredaje.Add(predmet);  //dodajemo predmet u listu
                        if (!predmet.PredmetPredajuNastavnici.Contains(this))
                        {
                            predmet.PredmetPredajuNastavnici.Add(this);     //azuriramo i drugu stranu veze
                        }
                        return this.nastavnikPredaje;

                    }
                }
                else
                {
                    throw new AccessViolationException("*** greska pri dodavanju predmeta, proverite podatke. ");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Predmet> IzbaciPredmet(Predmet predmet)
        {
            try
            {
                if (predmet != null)
                {
                    this.nastavnikPredaje.Remove(predmet); //izbacujemo nastavnika
                    predmet.PredmetPredajuNastavnici.Remove(this);   //azuriramo i drugu stranu veze


                    return this.nastavnikPredaje;
                }
                else
                {
                    throw new AccessViolationException("*** greska pri izbacivanju nastavnika, proverite podatke.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }



}
