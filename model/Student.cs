using System;
using System.Collections.Generic;
using System.Text;

namespace FirstProjectCS.model
{
    public class Student
    {

        //fields
        
        private int id;
        private string index;
        private string ime;
        private string prezime;
        private string grad;
        private List<Predmet> studentPohadjaPredmete = new List<Predmet>();
        private List<IspitnaPrijava> studentPrijavljujeIspitnePrijave = new List<IspitnaPrijava>();


        //get - set
        public int Id { get => id; set => id = value; }
        public string Index { get => index; set => index = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public string Grad { get => grad; set => grad = value; }
        internal List<Predmet> StudentPohadjaPredmete { get => studentPohadjaPredmete; set => studentPohadjaPredmete = value; }
        internal List<IspitnaPrijava> StudentPrijavljujeIspitnePrijave { get => studentPrijavljujeIspitnePrijave; set => studentPrijavljujeIspitnePrijave = value; }

      

        //Konstruktori

        public Student() { }

        public Student(int id, string index, string ime, string prezime, string grad)
        {
            
            this.id = id;
            this.index = index;
            this.ime = ime;
            this.prezime = prezime;
            this.grad = grad;
        }

        
        public override string ToString()
        {
            return this.id+" | " + this.index + " | " + this.ime + " " + this.prezime;
        }

        public  string ToFileReprezentation()
        {
            return this.id + "," + this.index + "," + this.prezime + "," + this.ime + "," + this.grad;

        }

        public override bool Equals(object obj)
        {
            return obj is Student student &&
                   id == student.id;
        }

        public List<Predmet> dodajPredmet( Predmet predmet)
        {
            try
            {
                if (predmet != null)
                {
                    if (!this.studentPohadjaPredmete.Contains(predmet))
                    {
                        this.studentPohadjaPredmete.Add(predmet);   //dodajemo predmet
                        if (!predmet.PredmetPohadjajuStudenti.Contains(this)) 
                        {
                            predmet.PredmetPohadjajuStudenti.Add(this);     //azuriramo drugu stranu veze
                        }
                        return this.studentPohadjaPredmete;
                    }
                    else { throw new AccessViolationException("*** *** Student" + this.ime + " vec pohadja predmet"); }
                }
                else
                {
                    throw new AccessViolationException("*** greska pri dodavanju predmeta, proverite podatke.");
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            
        }

        public List<Predmet> izbaciPredmet(Predmet predmet)
        {
            try
            {
                if (predmet != null)
                {
                    this.StudentPohadjaPredmete.Remove(predmet);
                    predmet.PredmetPohadjajuStudenti.Remove(this); 
                    return this.studentPohadjaPredmete;
                }
                else
                {
                    throw new AccessViolationException("*** greska pri izbacivanju predmeta, proverite podatke.");
                }
            }
            catch(Exception e)
            {
                throw new Exception (e.Message);
            }

        }
        public List<IspitnaPrijava> dodajIspitnuPrijavu(IspitnaPrijava ispitnaPrijava)
        {
            try
            {
                if (ispitnaPrijava != null)
                {
                    if (!this.studentPrijavljujeIspitnePrijave.Contains(ispitnaPrijava))
                    {
                        this.studentPrijavljujeIspitnePrijave.Add(ispitnaPrijava);   //dodajemo ispitnu prijavu

                        if (ispitnaPrijava.Student != this)
                        {
                            ispitnaPrijava.Student = this;     //azuriramo drugu stranu veze
                        }
                        return this.studentPrijavljujeIspitnePrijave;
                    }
                    else { throw new AccessViolationException("*** *** Student" + this.ime + " ima ispitnu prijavu"); }
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
                    this.studentPrijavljujeIspitnePrijave.Remove(ispitnaPrijava);  //izbacujemo ispitnu prijavu
                    if(ispitnaPrijava.Student == this)
                    {
                        ispitnaPrijava.Student = null;                  //razvezujemo drugu stranu veze
                    }
                    return this.studentPrijavljujeIspitnePrijave;
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
