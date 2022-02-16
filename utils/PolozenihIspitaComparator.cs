using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using FirstProjectCS.model;

namespace FirstProjectCS.utils
{
    internal class PolozenihIspitaComparator : IComparer<Student>
    {
        int direction = 1;

        public PolozenihIspitaComparator(int direction)
        {
            if(direction !=1 && direction != -1)
            {
                direction = 1;
            }
            this.direction = direction;
        }


        public int Compare([AllowNull] Student x, [AllowNull] Student y)
        {
            int retVal = 0;
            if (x == null || y == null){ return retVal; }

            int ocenax = 0;
            int ocenay=0;

            foreach (IspitnaPrijava ip in x.StudentPrijavljujeIspitnePrijave)
            {
                ocenax = ip.SracunajOcenu() > 5 ? ocenax++ : ocenax ; // zasto ovo ne radi?

                if (ip.SracunajOcenu() > 5) { ocenax++; }   // ovo radi

            }
            foreach (IspitnaPrijava ip in y.StudentPrijavljujeIspitnePrijave)
            {
                ocenay = ip.SracunajOcenu() > 5 ? ocenay++ : ocenay ;   //ovo ne radi

                if (ip.SracunajOcenu() > 5) { ocenay++; }   // ovo radi
            }

            retVal = ocenax - ocenay;

            return retVal * this.direction;
        }


    }
}
