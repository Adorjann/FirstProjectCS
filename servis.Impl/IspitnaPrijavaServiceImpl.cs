using FirstProjectCS.model;
using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.repository;

namespace FirstProjectCS.servis.Impl
{
    internal class IspitnaPrijavaServiceImpl
    {
        
        internal static IspitnaPrijava delete(Student student, Predmet predmet, IspitniRok iRok)
        {
            IspitnaPrijava prijavaZaBrisanje = 
                student.StudentPrijavljujeIspitnePrijave.Find(ip => 
                ip.Student.Id == student.Id &&
                ip.Predmet.Id == predmet.Id &&
                ip.IspitniRok.Id == iRok.Id 
                );

            if (prijavaZaBrisanje != null)
            {
                bool bulStud = student.izbaciIspitnuPrijavu(prijavaZaBrisanje);
                bool bulpred = predmet.izbaciIspitnuPrijavu(prijavaZaBrisanje);
                bool bulIRok = iRok.izbaciIspitnuPrijavu(prijavaZaBrisanje);

                if(bulIRok && bulpred && bulStud)
                {
                    return prijavaZaBrisanje;
                }
            }
            return null;
            
        }

        internal static IspitnaPrijava save(IspitnaPrijava ispitnaPrijava)
        {
            bool bulStud = ispitnaPrijava.Student.dodajIspitnuPrijavu(ispitnaPrijava);
            bool bulPred = ispitnaPrijava.Predmet.dodajIspitnuPrijavu(ispitnaPrijava);
            bool bulIRok = ispitnaPrijava.IspitniRok.dodajIspitnuPrijavu(ispitnaPrijava);

             if(bulIRok && bulStud && bulPred)
            {
                return ispitnaPrijava;
            }
            return null;
                

        }
    }
}
