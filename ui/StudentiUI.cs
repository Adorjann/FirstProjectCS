using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FirstProjectCS.model;
using FirstProjectCS.servis.Impl;
using FirstProjectCS.utils;

namespace FirstProjectCS.ui
{
    internal class StudentiUI
    {
        //"Controler"


        public static void ispisTextStudentOpcije()
        {
            Console.WriteLine("Rad sa studentima - opcije:");
            Console.WriteLine("\tOpcija broj 1 - unos podataka o novom studentu");
            Console.WriteLine("\tOpcija broj 2 - izmena podataka o studentu");
            Console.WriteLine("\tOpcija broj 3 - brisanje podataka o studentu");
            Console.WriteLine("\tOpcija broj 4 - ispis podataka svih studenata");
            Console.WriteLine("\tOpcija broj 5 - ispis podataka o odredjenom studentu sa njegovim predmetima koje pohadja");
            Console.WriteLine("\tOpcija broj 6 - ispis podataka o odredjenom studentu sa njegovim ispitnim prijavama");
            Console.WriteLine("\t\t ...");
            Console.WriteLine("\tOpcija broj 0 - IZLAZ");

        }

        public static void meniStudentiUI()
        {

            int odluka = -1;
            while(odluka != 0)
            {
                ispisTextStudentOpcije();
                odluka = Convert.ToInt32(Console.ReadLine());

                switch (odluka)
                {
                    case 0:
                        //Izlaz menija za Studente;
                        Console.WriteLine("IZLAZ");
                        break;

                    case 1:
                        //unos novog studenta [POST]
                        create();
                        break;

                    case 2:
                        //izmena podataka o studentu [PUT]
                        edit();
                        break;

                    case 3:
                        //brisanje podataka o studentu [DELETE]
                        delete();
                        break;

                    case 4:
                        //ispis svih studenata [GET]
                        getAll(); //u ovu metodu ponudi opciju sortiranje
                        break;

                    case 5:
                        //TODO: ispis podataka o odredjenom studentu sa njegovim predmetima koje pohadja [GET]
                        getOne("predmeti");
                        break;

                    case 6:
                        //TODO: ispis podataka o odredjenom studentu sa njegovim ispitnim prijavama [GET]
                        getOne("prijave");
                        break;

                    

                    default:
                        Console.WriteLine("Pogresna komanda");
                        break;

                   



                }

            }

        }
        //PostMapping
        public static void create()
        {
            Console.WriteLine("Unesite ime novog studenta: ");
            string ime = Console.ReadLine();
            Console.WriteLine("Unesite prezime novog studenta: ");
            string prezime = Console.ReadLine();
            Console.WriteLine("Unesite index novog studenta");
            string index = Console.ReadLine();
            Console.WriteLine("Unesite grad koji je prebivaliste novom studentu: ");
            string grad = Console.ReadLine();

            //id 0 jer je id autoGenerate
            Student noviStudent = new Student(0,index,ime,prezime,grad); 
            Student sacuvaniNoviStudent = StudentServisImpl.save(noviStudent);

            if(sacuvaniNoviStudent != null)
            {
                Console.WriteLine("Uspesno sacuvan novi student: \n\t" + sacuvaniNoviStudent);
            }
            else
            {
                Console.WriteLine("*** Unos novog studenta nije uspeo.");
                Console.WriteLine("*** Vec postoji student sa indexom koji ste uneli.");
            }
        }

        //PutMapping
        public static void edit()
        {
            Student s = PomocnaKlasa.indexToStudent();

            if(s != null)
            {
                Console.WriteLine("Unesite izmenjeno ime studenta: ");
                string novoIme = Console.ReadLine();
                Console.WriteLine("Unesite izmenjeno prezime studenta: ");
                string novoPrezime = Console.ReadLine();
                Console.WriteLine("Unesite izmenjeni grad studenta: ");
                string noviGrad = Console.ReadLine();

                s.Ime = novoIme;
                s.Prezime = novoPrezime;
                s.Grad = noviGrad;

                Console.WriteLine("Uspesno promenjeni podaci! \n\t " + s);
            }
            else
            {
                Console.WriteLine("Nepostoji student sa zadatim indexom");
            }
            
            

        }


        public static void delete()
        {
            Student s = PomocnaKlasa.indexToStudent();

            if(s != null)
            {
                Console.WriteLine("Da li ste sigurni da zelite da obrisete studenta: \n\t" + s);
                bool odgovor = PomocnaKlasa.yesOrNo();
                 
                Student IzbrisanStudent = odgovor? StudentServisImpl.delete(s) : null;

                string finallOdgovor =
                    IzbrisanStudent == null ? "Prekinuto brisanje!" : "Uspesno obrisan student: \n\t" + s;

                Console.WriteLine(finallOdgovor);
            }

            
            
        }

        //GetMapping
        public static void getAll()
        {
            List<Student> studenti = StudentServisImpl.findAll(); 
            if(studenti.Count == 0 && studenti == null)
            {
                Console.WriteLine("Nema studenata.");
            }
            else
            {
                Console.WriteLine("Svi studenti: ");
                foreach (Student student in studenti)
                {
                    Console.WriteLine("\t"+student);
                }
            }



        }


        public static void getOne(string kolekcija)
        {
            //TODO: dodati listanje predmeta koje student pohadja i listu prijavljenih ispita

            Student student = PomocnaKlasa.indexToStudent();

            if(student != null)
            {
                Console.WriteLine("\n-|- " + student);
                if(kolekcija == "predmeti")
                {
                    Console.WriteLine("Student " + student.Ime + " pohadja predmete: \n");
                    foreach(Predmet it in student.StudentPohadjaPredmete)
                    {
                        Console.WriteLine("\t" + it);
                    }
                }else if(kolekcija == "ispitnePrijave")
                {
                    Console.WriteLine("\n*** \nJos nismo implementirali mogucnost sa ispitnim prijavama");
                }
            }
            else
            {
                Console.WriteLine("Nepostoji student sa zadatim indexom");
            }

        }


        public static void ucitajStudenteSaFajla(string fajl)
        {
            StreamReader sr = new StreamReader(fajl);
            string line;
            try
            {
                //read the first line so the while loop can work
                line = sr.ReadLine();
                while(line != null)
                {
                    string[] tokeni = line.Split(',');

                    Student student = new Student();
                    student.Id = Convert.ToInt32(tokeni[0]);
                    student.Index = tokeni[1];
                    student.Prezime = tokeni[2];
                    student.Ime = tokeni[3];
                    student.Grad = tokeni[4];

                    Student snimljeniStudent = StudentServisImpl.save(student);

                    if (snimljeniStudent == null)
                    {
                        Console.WriteLine("*** Greska pri snimanju studenata u repozitorijum.");
                        break;
                    }
                    line = sr.ReadLine();
                }
                

            }catch(Exception e)
            {
                Console.WriteLine("*** Greska pri ucitavanju studenata sa fajla. ");
                Console.WriteLine("\n\t*** " + e.StackTrace);
                Console.WriteLine("\n\t*** " + e.Message);
            }
            finally
            {
                sr.Close();
                Console.WriteLine("Zavrseno ucitavanje studenata");
            }
        }
        public static void zapisiStudenteUFajl(string fajl)
        {
            List<Student> sviStudenti = StudentServisImpl.findAll();

            StreamWriter sw = new StreamWriter(fajl);
            try { 


                for(int i=0; i< sviStudenti.Count; i++) 
                {
                    
                    sw.WriteLine(sviStudenti[i].ToFileReprezentation()); 

                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine("*** Greska pri zapisivanju studenta u fajl.");
                Console.WriteLine("\n\t*** " + e.StackTrace);
                Console.WriteLine("\n\t*** "+e.Message);
            }
            finally
            {
                sw.Close();
            }

        }


        
    }
}
