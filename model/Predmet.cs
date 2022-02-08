using System;
using System.Collections.Generic;
using System.Text;

namespace FirstProjectCS.model
{
    public class Predmet
    {
        private int id;
        private string naziv;

        private List<Student> predmetPohadjajuStudenti = new List<Student>();
        private List<Nastavnik> predmetPredajuNastavnici = new List<Nastavnik>();
        private List<IspitnaPrijava> predmetImaPrijavljeneIspitnePrijave = new List<IspitnaPrijava>();

        public int Id { get => id; set => id = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        internal List<Student> PredmetPohadjajuStudenti { get => predmetPohadjajuStudenti; set => predmetPohadjajuStudenti = value; }
        internal List<Nastavnik> PredmetPredajuNastavnici { get => predmetPredajuNastavnici; set => predmetPredajuNastavnici = value; }
        internal List<IspitnaPrijava> PredmetImaPrijavljeneIspitnePrijave { get => predmetImaPrijavljeneIspitnePrijave; set => predmetImaPrijavljeneIspitnePrijave = value; }



        //Constructors
        public Predmet() { }


        public Predmet(int id, string naziv, List<Student> predmetPohadjajuStudenti, List<Nastavnik> predmetPredajuNastavnici)
        {
            this.Id = id;
            this.Naziv = naziv;
            this.PredmetPohadjajuStudenti = predmetPohadjajuStudenti;
            this.predmetPredajuNastavnici = predmetPredajuNastavnici;
        }
        public Predmet(int id, string naziv)
        {
            this.Id = id;
            this.Naziv = naziv;
        }

       

        public override string ToString()
        {
            return $"| {this.id} | {this.naziv} |";
        }

        public override bool Equals(object obj)
        {
            return obj is Predmet predmet &&
                   id == predmet.id &&
                   naziv == predmet.naziv;
        }

        public List<Student> dodajStudenta (Student student) 
        {
            try
            {
                if (student != null)
                {
                    if (this.predmetPohadjajuStudenti.Contains(student))
                    {
                        throw new AccessViolationException($"*** Student {student.Ime} vec pohadja predmet.");

                    }
                    else
                    {

                        this.predmetPohadjajuStudenti.Add(student); //dodajemo studenta u listu
                        if (!student.StudentPohadjaPredmete.Contains(this))
                        {
                            student.StudentPohadjaPredmete.Add(this);   //azuriramo i drugu stranu veze
                        }
                        return this.predmetPohadjajuStudenti;

                    }
                }
                else
                {
                    throw new AccessViolationException("*** greska pri dodavanju studenta, proverite podatke. ");
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Student> izbaciStudenta(Student student)
        {
            try
            {
                if (student != null)
                {
                    this.predmetPohadjajuStudenti.Remove(student); //izbacujemo studenta
                    student.StudentPohadjaPredmete.Remove(this);   //azuriramo i drugu stranu veze
                    

                    return this.predmetPohadjajuStudenti;
                }
                else
                {
                    throw new AccessViolationException("*** greska pri izbacivanju studenta, proverite podatke.");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message); 
            }
            
        }
        public List<Nastavnik> dodajNastavnika(Nastavnik nastavnik)
        {
            try
            {
                if (nastavnik != null)
                {
                    if (this.predmetPredajuNastavnici.Contains(nastavnik))
                    {
                        throw new AccessViolationException($"*** Nastavnik {nastavnik.Ime} vec predaje predmet.");

                    }
                    else
                    {

                        this.predmetPredajuNastavnici.Add(nastavnik); //dodajemo nastavnika u listu
                        if (!nastavnik.NastavnikPredaje.Contains(this))
                        {
                            nastavnik.NastavnikPredaje.Add(this);   //azuriramo i drugu stranu veze
                        }
                        return this.predmetPredajuNastavnici;

                    }
                }
                else
                {
                    throw new AccessViolationException("*** greska pri dodavanju studenta, proverite podatke. ");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Nastavnik> izbaciNastavnika(Nastavnik nastavnik)
        {
            try
            {
                if (nastavnik != null)
                {
                    this.predmetPredajuNastavnici.Remove(nastavnik); //izbacujemo nastavnika
                    nastavnik.NastavnikPredaje.Remove(this);   //azuriramo i drugu stranu veze


                    return this.predmetPredajuNastavnici;
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
        public List<IspitnaPrijava> dodajIspitnuPrijavu(IspitnaPrijava ispitnaPrijava)
        {
            try
            {
                if (ispitnaPrijava != null)
                {
                    if (!this.predmetImaPrijavljeneIspitnePrijave.Contains(ispitnaPrijava))
                    {
                        this.predmetImaPrijavljeneIspitnePrijave.Add(ispitnaPrijava);   //dodajemo ispitnu prijavu

                        if (ispitnaPrijava.Predmet != this)
                        {
                            ispitnaPrijava.Predmet = this;     //azuriramo drugu stranu veze
                        }
                        return this.predmetImaPrijavljeneIspitnePrijave;
                    }
                    else { throw new AccessViolationException($"*** *** Predmet {this.naziv} vec sadrzi ovu ispitnu prijavu"); }
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
                    this.predmetImaPrijavljeneIspitnePrijave.Remove(ispitnaPrijava);  //izbacujemo ispitnu prijavu
                    if (ispitnaPrijava.Predmet == this)
                    {
                        ispitnaPrijava.Predmet = null;                  //razvezujemo drugu stranu veze
                    }
                    return this.predmetImaPrijavljeneIspitnePrijave;
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

        public string toFileReprezentation()
        {
            return $"{this.id},{this.naziv} ";
        }
    }
}
