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
        private List<IspitnaPrijava> ispitniRokImaPrijavljeneIspitnePrijave = new List<IspitnaPrijava>();


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
        internal List<IspitnaPrijava> IspitniRokImaPrijavljeneIspitnePrijave { get => ispitniRokImaPrijavljeneIspitnePrijave; set => ispitniRokImaPrijavljeneIspitnePrijave = value; }

        public override string ToString()
        {
            return $"{this.naziv} ispitni rok, pocinje {pocetak.Date.ToString("dd/MM/yyyy")} i zavrsava se {kraj.Date.ToString("dd/MM/yyyy")}";
        }

        public string toFileReprezentation()
        {
            return $"{this.id},{this.naziv},{pocetak.Date.ToString("yyyy-MM-dd")},{kraj.Date.ToString("yyyy-MM-dd")}";
        }

        public List<IspitnaPrijava> dodajIspitnuPrijavu(IspitnaPrijava ispitnaPrijava)
        {
            try
            {
                if (ispitnaPrijava != null)
                {
                    if (!this.ispitniRokImaPrijavljeneIspitnePrijave.Contains(ispitnaPrijava))
                    {
                        this.ispitniRokImaPrijavljeneIspitnePrijave.Add(ispitnaPrijava);   //dodajemo ispitnu prijavu

                        if (ispitnaPrijava.IspitniRok != this)
                        {
                            ispitnaPrijava.IspitniRok = this;     //azuriramo drugu stranu veze
                        }
                        return this.ispitniRokImaPrijavljeneIspitnePrijave;
                    }
                    else { throw new AccessViolationException($"*** *** Ispitni rok {this.naziv} vec sadrzi ovu ispitnu prijavu"); }
                }
                else
                {
                    throw new AccessViolationException("*** greska pri dodavanju ispitne prijave, proverite podatke.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public List<IspitnaPrijava> izbaciIspitnuPrijavu(IspitnaPrijava ispitnaPrijava)
        {
            try
            {
                if (ispitnaPrijava != null)
                {
                    this.ispitniRokImaPrijavljeneIspitnePrijave.Remove(ispitnaPrijava);  //izbacujemo ispitnu prijavu
                    if (ispitnaPrijava.IspitniRok == this)
                    {
                        ispitnaPrijava.IspitniRok = null;                  //razvezujemo drugu stranu veze
                    }
                    return this.ispitniRokImaPrijavljeneIspitnePrijave;
                }
                else
                {
                    throw new AccessViolationException("*** greska pri izbacivanju ispitne prijave, proverite podatke.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
