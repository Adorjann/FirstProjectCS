using System;
using System.Collections.Generic;
using System.Text;

namespace FirstProjectCS.model
{
    internal class IspitnaPrijava
    {
        private int id;
        private Student student;
        private Predmet predmet;
        private IspitniRok ispitniRok;
        private int teorija;
        private int zadaci;

        //Konstruktori
        public IspitnaPrijava() {
            
        }
        public IspitnaPrijava(Student student, Predmet predmet, IspitniRok ispitniRok, int teorija, int zadaci)
        {
            
            this.student = student;
            this.predmet = predmet;
            this.ispitniRok = ispitniRok;
            this.teorija = teorija;
            this.zadaci = zadaci;
            this.id = Convert.ToInt32($"{student.Id}{predmet.Id}{ispitniRok.Id}");
        }

        //get - set
        public int Teorija { get => teorija; set => teorija = value; }
        public int Zadaci { get => zadaci; set => zadaci = value; }
        internal Student Student { get => student; set => student = value; }
        internal Predmet Predmet { get => predmet; set => predmet = value; }
        internal IspitniRok IspitniRok { get => ispitniRok; set => ispitniRok = value; }
        public int GetId() { return this.id; }
        public void SetId() { this.id = Convert.ToInt32($"{student.Id}{predmet.Id}{ispitniRok.Id}"); }

        




        public string toFileRepresentation()
        {
            return $"{this.student.Id},{this.predmet.Id},{this.ispitniRok.Id},{this.teorija},{this.zadaci}";
        }

        public override string ToString()
        {
            return $"Ispitna prijava za studenta {this.student.Ime} iz predmeta {this.predmet.Naziv} " +
                $"u roku {this.ispitniRok.Naziv}| bodovi iz teorije: {this.teorija} | bodovi iz zadataka: {this.zadaci}";
        }

        public double sracunajProsek()
        {
            return (this.teorija + this.zadaci) / 2;
        }

        public int sracunajOcenu()
        {
            double bodovi = sracunajProsek();
            int ocena;
            if(bodovi >= 95)
            {
                ocena = 10;
            }else if(bodovi >= 85)
            {
                ocena = 9;
            }else if(bodovi >= 75)
            {
                ocena= 8;
            }else if(bodovi >= 65)
            {
                ocena = 7;
            }else if(bodovi >= 55)
            {
                ocena = 6;
            }else
            {
                ocena = 5; 
            }
            return ocena;
        }

        public override bool Equals(object obj)
        {
            return obj is IspitnaPrijava prijava &&
                   id == prijava.id &&
                   EqualityComparer<Student>.Default.Equals(student, prijava.student) &&
                   EqualityComparer<Predmet>.Default.Equals(predmet, prijava.predmet) &&
                   EqualityComparer<IspitniRok>.Default.Equals(ispitniRok, prijava.ispitniRok) &&
                   teorija == prijava.teorija &&
                   zadaci == prijava.zadaci;
        }
    }
}
