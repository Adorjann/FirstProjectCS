using System;
using System.IO;
using System.Collections.Generic;
using FirstProjectCS.ui;
using FirstProjectCS.model;

namespace FirstProjectCS
{
    class Program
    {
        public static void ispisTextualneOpcije()
        {
            Console.WriteLine("Studentska Sluzba - Osnovne opcije: ");
            Console.WriteLine("\tOpcija broj 1 - Rad sa studentima");
            Console.WriteLine("\tOpcija broj 2 - Rad sa predmetima");
            Console.WriteLine("\tOpcija broj 3 - Rad sa nastavnicima");
            Console.WriteLine("\tOpcija broj 4 - Rad sa ispitnim rokovima");
            Console.WriteLine("\tOpcija broj 5 - Rad sa ispitnim prijavama");
            Console.WriteLine("\t\t ...");
            Console.WriteLine("\tOpcija broj 0 - IZLAZ IZ PROGRAMA");
        }
        
        
        static void Main(string[] args)
        {
            //file separator
            char sp = Path.DirectorySeparatorChar;
            string putDoFajla = ".." + sp + ".." + sp + ".." + sp + "data" + sp;

            //data
            string studentiFajl = putDoFajla + "studenti.csv";
            string nastavniciFajl = putDoFajla + "nastavnici.csv";
            string nastavnik_predmetFajl = putDoFajla + "nastavnik_predmet.csv";
            string student_predmetFajl = putDoFajla + "student_predmet.csv";
            string predmetiFajl = putDoFajla + "predmeti.csv";
            string ispitne_prijaveFajl = putDoFajla + "ispitne_prijave.csv";
            string ispitni_rokoviFajl = putDoFajla + "ispitni_rokovi.csv";

            // UI klase ucitavaju podatke iz fajlova

            StudentiUI studentiUI = (StudentiUI)SingletonCreator.GetInstance(typeof(StudentiUI));
            studentiUI.UcitajStudenteSaFajla(studentiFajl);

            PredmetUI predmetUI = (PredmetUI)SingletonCreator.GetInstance(typeof(PredmetUI));
            predmetUI.UcitajPredmeteSaFajla(predmetiFajl);

            Student_Predmet student_predmet = (Student_Predmet)SingletonCreator.GetInstance(typeof(Student_Predmet));
            student_predmet.UcitajVezuStudent_Predmet(student_predmetFajl);

            NastavnikUI nastavnikUI = (NastavnikUI)SingletonCreator.GetInstance(typeof(NastavnikUI));
            nastavnikUI.UcitajNastavnikeSaFajla(nastavniciFajl);

            Nastavnik_Predmet nastavnik_Predmet = (Nastavnik_Predmet)SingletonCreator.GetInstance(typeof(Nastavnik_Predmet));
            nastavnik_Predmet.UcitajVezuNastavnik_Predmet(nastavnik_predmetFajl);

            IspitniRokUI ispitniRokUI = (IspitniRokUI)SingletonCreator.GetInstance(typeof(IspitniRokUI));
            ispitniRokUI.UcitajIspitniRokSaFajla(ispitni_rokoviFajl);

            IspitnaPrijavaUI ispitnaPrijavaUI = (IspitnaPrijavaUI)SingletonCreator.GetInstance(typeof(IspitnaPrijavaUI));
            ispitnaPrijavaUI.UcitajIspitnePrijaveSaFajla(ispitne_prijaveFajl);




            int odluka = -1;

            while (odluka != 0)
            {
                ispisTextualneOpcije();
                Console.WriteLine("Upisi jednu opciju: ");
                odluka = Convert.ToInt32(Console.ReadLine());
                switch (odluka)
                {
                    case 0:
                        Console.WriteLine("Izlaz iz programa");
                        break;

                    case 1:
                        Console.WriteLine("Studenti UI");
                        studentiUI.MeniStudentiUI();
                        break;
                    case 2:
                        Console.WriteLine("Predmeti UI");
                        predmetUI.MeniPredmetUI();
                        break;
                    case 3:
                        Console.WriteLine("Nastavnici UI");
                        nastavnikUI.MeniNastavnikUI();
                        break;
                    case 4:
                        Console.WriteLine("Ispitni Rokovi UI");
                        ispitniRokUI.MeniIspitniRokUI();
                        break;
                    case 5:
                        Console.WriteLine("Ispitna PrijavaUI");
                        ispitnaPrijavaUI.MeniIspitnaPrijava();
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda");
                        break;
                }


            }
            

            studentiUI.ZapisiStudenteUFajl(studentiFajl);
            predmetUI.ZapisiPredmeteUFajl(predmetiFajl);
            student_predmet.ZapisiVezuStudent_Predmet(student_predmetFajl);
            nastavnikUI.ZapisiNastavnikeUFajl(nastavniciFajl);
            ispitniRokUI.ZapisiIspitniRokUFajl(ispitni_rokoviFajl);
            nastavnik_Predmet.ZapisiVezuNastavnik_Predmet(nastavnik_predmetFajl);
            ispitnaPrijavaUI.ZapisiIspitnePrijaveUFajl(ispitne_prijaveFajl);

            Console.WriteLine("Program zavrsen");

        }
        
           
            


    }
}
