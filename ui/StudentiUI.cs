using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FirstProjectCS.model;
using FirstProjectCS.servis.Impl;
using FirstProjectCS.utils;
using FirstProjectCS.exceptions;

namespace FirstProjectCS.ui
{
    //"Controler"
    public class StudentiUI
    {

        //DI
        private readonly StudentServisImpl studentService = (StudentServisImpl)SingletonCreator.GetInstance(typeof(StudentServisImpl));
        private readonly PomocnaKlasa pomocnaKLasa = (PomocnaKlasa)SingletonCreator.GetInstance(typeof(PomocnaKlasa));

        private StudentiUI()
        {

        }

        private  void IspisTextStudentOpcije()
        {
            Console.WriteLine("Rad sa studentima - opcije:");
            Console.WriteLine("\tOpcija broj 1 - unos podataka o novom studentu");
            Console.WriteLine("\tOpcija broj 2 - izmena podataka o studentu");
            Console.WriteLine("\tOpcija broj 3 - brisanje podataka o studentu");
            Console.WriteLine("\tOpcija broj 4 - ispis podataka svih studenata");
            Console.WriteLine("\tOpcija broj 5 - ispis podataka o odredjenom studentu sa njegovim predmetima koje pohadja");
            Console.WriteLine("\tOpcija broj 6 - ispis podataka o odredjenom studentu sa njegovim ispitnim prijavama");
            Console.WriteLine("\tOpcija broj 7 - ispis podataka svih studenata SORTIRANO po broju polozenih predmeta");
            Console.WriteLine("\t\t ...");
            Console.WriteLine("\tOpcija broj 0 - IZLAZ");

        }

        public  void MeniStudentiUI()
        {

            int odluka = -1;
            while(odluka != 0)
            {
                IspisTextStudentOpcije();
                odluka = Convert.ToInt32(Console.ReadLine());

                switch (odluka)
                {
                    case 0:
                        //Izlaz menija za Studente;
                        Console.WriteLine("IZLAZ");
                        break;

                    case 1:
                        //unos novog studenta [POST]
                        Create();
                        break;

                    case 2:
                        //izmena podataka o studentu [PUT]
                        Edit();
                        break;

                    case 3:
                        //brisanje podataka o studentu [DELETE]
                        Delete();
                        break;

                    case 4:
                        //ispis svih studenata [GET]
                        GetAll(); 
                        break;

                    case 5:
                        //ispis podataka o odredjenom studentu sa njegovim predmetima koje pohadja [GET]
                        GetOne("predmeti");
                        break;

                    case 6:
                        //ispis podataka o odredjenom studentu sa njegovim ispitnim prijavama [GET]
                        GetOne("prijave");
                        break;
                    case 7:
                        SortirajStudentePoPolozenimIspitima();
                        break;
                    

                    default:
                        Console.WriteLine("Pogresna komanda");
                        break;

                   



                }

            }

        }

        private  void SortirajStudentePoPolozenimIspitima()
        {
            try
            {
                List<Student> studenti = studentService.FindAll();

                Console.WriteLine("Studente je moguce sortirati \n\t|1| - Br Polozenih predmeta Rastuce\n\t|2| - Br Polozenih predmeta Opadajuce");
                Console.WriteLine("Izaberi opciju: ");
                int odluka = Convert.ToInt32(Console.ReadLine());
                switch (odluka)
                {
                    case 1:
                        studenti.Sort(new PolozenihIspitaComparator(1));
                        break;
                    case 2:
                        studenti.Sort(new PolozenihIspitaComparator(-1));
                        break;
                    default:
                        break;
                }

                PrintSviStudenti(studenti);
            }
            catch (CollectionIsEmptyException )
            {
                Console.WriteLine("*** Nema Studenata u bazi.");
            }
            catch (Exception)
            {
                Console.WriteLine("Doslo je do greske.");
            }
        }

        //PostMapping
        public  void Create()
        {
            Console.WriteLine("Unesite ime novog studenta: ");
            string ime = Console.ReadLine();
            Console.WriteLine("Unesite prezime novog studenta: ");
            string prezime = Console.ReadLine();
            Console.WriteLine("Unesite index novog studenta");
            string index = Console.ReadLine();
            Console.WriteLine("Unesite grad koji je prebivaliste novom studentu: ");
            string grad = Console.ReadLine();

            try
            {
                //id = 0 because id is autoIncrement
                Student noviStudent = new Student(0, index, ime, prezime, grad);
                Student sacuvaniNoviStudent = studentService.Save(noviStudent);
                Console.WriteLine("Uspesno sacuvan novi student: \n\t" + sacuvaniNoviStudent);

            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Pogresno uneseni podaci");
            }
            catch (DuplicateObjectException )
            {
                Console.WriteLine("*** Unos novog studenta nije uspeo.");
                Console.WriteLine("*** Vec postoji student sa indexom koji ste uneli.");
            }
            catch (Exception)
            {
                Console.WriteLine("Doslo je do greske.");
            }
        }

        //PutMapping
        public  void Edit()
        {
            try 
	        {	        
		        Student s = pomocnaKLasa.IndexToStudent();

                Console.WriteLine("Unesite izmenjeno ime studenta: ");
                string novoIme = Console.ReadLine();
                Console.WriteLine("Unesite izmenjeno prezime studenta: ");
                string novoPrezime = Console.ReadLine();
                Console.WriteLine("Unesite izmenjeni grad studenta: ");
                string noviGrad = Console.ReadLine();

                if (novoIme != "" && novoPrezime != "" && noviGrad != "")
                {
                    s.Ime = novoIme;
                    s.Prezime = novoPrezime;
                    s.Grad = noviGrad;
                    Console.WriteLine("Uspesno promenjeni podaci! \n\t " + s);
                }
                else
                {
                    throw new InvalidOperationException();
                }
	        }
	        catch (ObjectNotFoundException )
	        {
                Console.WriteLine("Nepostoji student sa zadatim indexom");    
	        }
            catch(InvalidOperationException)
            {
                Console.WriteLine("Pogresno uneseni novi podaci.");
            }
            catch (Exception)
            {
                Console.WriteLine("Doslo je od greske");
            }
        }

        private void Delete()
        {
            try
            {
                Student s = pomocnaKLasa.IndexToStudent();

                Console.WriteLine("Da li ste sigurni da zelite da obrisete studenta: \n\t" + s);
                bool odgovor = pomocnaKLasa.YesOrNo();
                 
                Student izbrisanStudent = odgovor? studentService.Delete(s) : throw new OperationCanceledException();

                Console.WriteLine("Uspesno obrisan student: \n\t" + izbrisanStudent);

            }
            catch (ObjectNotFoundException )
            {
                Console.WriteLine("Brisanje studenta nije uspelo.");
            }
            catch (OperationCanceledException )
            {
                Console.WriteLine("Prekinuto brisanje studenta");
            }
            catch (Exception)
            {
                Console.WriteLine("Doslo je do greske.");
            }


        }

        //GetMapping
        private  void GetAll()
        {
            try
            {
                List<Student> studenti = studentService.FindAll();
                this.PrintSviStudenti(studenti);
            }
            catch (CollectionIsEmptyException )
            {
                Console.WriteLine("Nema studenata u bazi.");
            }
            catch (Exception)
            {
                Console.WriteLine("Doslo je do greske.");
            }
            
        }

        private void PrintSviStudenti(List<Student> studenti)
        {
            Console.WriteLine("Svi studenti: ");
            studenti.ForEach(student => {

                Console.WriteLine("\t" + student);
            });
            
        }


        private void GetOne(string kolekcija)
        {
            //TODO: dodati listanje predmeta koje student pohadja i listu prijavljenih ispita

            try
            {
                Student student = pomocnaKLasa.IndexToStudent();

                Console.WriteLine("\n-|- " + student);
                if (kolekcija == "predmeti" && student.StudentPohadjaPredmete.Count != 0)
                {
                    Console.WriteLine("\t\t pohadja predmete: \n");
                    student.StudentPohadjaPredmete.ForEach(predmet => Console.WriteLine("\t"+predmet));
                }
                else if (kolekcija == "ispitnePrijave" && student.StudentPrijavljujeIspitnePrijave.Count != 0) 
                {
                    Console.WriteLine("\t\t ima ispitne prijave: \n");  
                    student.StudentPrijavljujeIspitnePrijave.ForEach(prijava => Console.WriteLine(prijava));
                }
                else
                {
                    throw new CollectionIsEmptyException("\t nema "+kolekcija);
                }
            }
            catch (ObjectNotFoundException )
            {
                Console.WriteLine("Nepostoji student sa zadatim indexom");
            }
            catch (CollectionIsEmptyException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Doslo je do greske.");
            }
        }

        public void UcitajStudenteSaFajla(string fajl)
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

                    Student snimljeniStudent = studentService.Save(student);

                    if (snimljeniStudent == null)
                    {
                        break;
                    }
                    line = sr.ReadLine();
                }
                

            }catch (DuplicateObjectException)
            {
                Console.WriteLine("*** Student je vec snimljen u repozitorijum.");
            }
            catch(Exception e)
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
        public void ZapisiStudenteUFajl(string fajl)
        {
                StreamWriter sw = new StreamWriter(fajl);
            try { 
                List<Student> sviStudenti = studentService.FindAll();

                sviStudenti.ForEach(student => sw.WriteLine(student.ToFileReprezentation()));
            }
            catch (CollectionIsEmptyException e)
            {
                Console.WriteLine("*** Nema studenata za zapisivanje.");
                Console.WriteLine("\n\t*** " + e.Message);
            }
            catch (Exception e)
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
