using System;
using System.Collections.Generic;
using System.Text;

namespace FirstProjectCS.model
{
    internal class IspitniRok
    {
        private int id;
        private string naziv;
        private DateTime pocetak;
        private DateTime kraj;
        //private List<IspitnaPrijava> ispitniRokImaPrijavljeneIspitnePrijave = new List<IspitnaPrijava>();


        //konstruktori
        public IspitniRok() { }
        public IspitniRok(int id, string naziv, DateTime pocetak, DateTime kraj)
        {
            this.Id = id;
            this.Naziv = naziv;
            this.Pocetak = pocetak;
            this.Kraj = kraj;
        }

        //get - set
        public int Id { get => id; set => id = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        public DateTime Pocetak { get => pocetak; set => pocetak = value; }
        public DateTime Kraj { get => kraj; set => kraj = value; }

        

        public override string ToString()
        {
            return $"{this.naziv} ispitni rok, pocinje {pocetak.Date.ToString("dd/MM/yyyy")} i zavrsava se {kraj.Date.ToString("dd/MM/yyyy")}";
        }

        public string toFileReprezentation()
        {
            return $"{this.id},{this.naziv},{pocetak.Date.ToString("yyyy-MM-dd")},{kraj.Date.ToString("yyyy-MM-dd")}";
        }
    }
}
