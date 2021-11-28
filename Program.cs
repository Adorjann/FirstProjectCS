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

            string studentiFajl = putDoFajla + "studenti.csv";
            string nastavniciFajl = putDoFajla + "nastavnici.csv";
            string nastavnik_predmetFajl = putDoFajla + "nastavnik_predmet.csv";
            string student_predmetFajl = putDoFajla + "student_predmet.csv";
            string predmetiFajl = putDoFajla + "predmeti.csv";
            string ispitne_prijaveFajl = putDoFajla + "ispitne_prijave.csv";
            string ispitni_rokoviFajl = putDoFajla + "ispitni_rokovi.csv";

            //TODO: UI klase ucitavaju podatke iz fajlova

            StudentiUI.ucitajStudenteSaFajla(studentiFajl);
            PredmetUI.ucitajPredmeteSaFajla(predmetiFajl);
            Student_Predmet.ucitajVezuStudent_Predmet(student_predmetFajl);
            NastavnikUI.ucitajNastavnikeSaFajla(nastavniciFajl);
            Nastavnik_Predmet.ucitajVezuNastavnik_Predmet(nastavnik_predmetFajl);
            IspitniRokUI.ucitajIspitniRokSaFajla(ispitni_rokoviFajl);




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
                        StudentiUI.meniStudentiUI();
                        break;
                    case 2:
                        Console.WriteLine("Predmeti UI");
                        PredmetUI.meniPredmetUI();
                        break;
                    case 3:
                        Console.WriteLine("Nastavnici UI");
                        NastavnikUI.meniNastavnikUI();
                        break;
                    case 4:
                        Console.WriteLine("Ispitni Rokovi UI");
                        IspitniRokUI.meniIspitniRokUI();
                        break;
                    case 5:
                        Console.WriteLine("Ispitna PrijavaUI");
                        Console.WriteLine("\n** jos uvek nije implementirano.\n");
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda");
                        break;
                }


            }
            //TODO UI klase zapis podataka u fajlove i potom program zavrsen;

            StudentiUI.zapisiStudenteUFajl(studentiFajl);
            PredmetUI.zapisiPredmeteUFajl(predmetiFajl);
            Student_Predmet.zapisiVezuStudent_Predmet(student_predmetFajl);
            NastavnikUI.zapisiNastavnikeUFajl(nastavniciFajl);
            IspitniRokUI.ZapisiIspitniRokUFajl(ispitni_rokoviFajl);
            
            Nastavnik_Predmet.zapisiVezuNastavnik_Predmet(nastavnik_predmetFajl);

            Console.WriteLine("Program zavrsen");

        }
        
           
            


    }
}
