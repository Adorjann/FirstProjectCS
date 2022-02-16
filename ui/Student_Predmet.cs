using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FirstProjectCS.servis.Impl;
using FirstProjectCS.model;
using FirstProjectCS.utils;

namespace FirstProjectCS.ui
{
    public class Student_Predmet
    {
        private readonly PomocnaKlasa pomocnaKlasa = (PomocnaKlasa)SingletonCreator.GetInstance(typeof(PomocnaKlasa));
        private readonly StudentServisImpl studentService = (StudentServisImpl)SingletonCreator.GetInstance(typeof(StudentServisImpl));

        private Student_Predmet()
        {
        }

        public void UcitajVezuStudent_Predmet(string fajl)
        {
            
            StreamReader sr = new StreamReader(fajl);
            string line;

            try
            {
                line = sr.ReadLine();
                while(line != null)
                {
                    string[] tokeni = line.Split(',');
                    Student student = pomocnaKlasa.FindStudentById(Convert.ToInt32(tokeni[0]));
                    Predmet predmet = pomocnaKlasa.FindPredmetById(Convert.ToInt32(tokeni[1]));

                    if(student != null && predmet != null)
                    {
                        predmet.DodajStudenta(student);

                        //student.dodajPredmet(predmet); ---> nema potrebe za ovom linijom
                        //azuriranje druge strane veze je izvrseno unutar dodajStudenta()
                    }
                    line = sr.ReadLine();  
                }
            }catch (Exception e)
            {
                Console.WriteLine("*** Greska pri ucitavanju studenat_predmet sa fajla. ");
                Console.WriteLine("\n\t*** " + e.Message);
                Console.WriteLine("\n\t*** " + e.StackTrace);
            }
            finally{
                sr.Close();
            }
        }
        public void ZapisiVezuStudent_Predmet(string fajl)
        {
            StreamWriter sw = new StreamWriter(fajl);
            try
            {
                List<Student> sviStudenti = studentService.FindAll();
                sviStudenti.ForEach(student => {

                    student.StudentPohadjaPredmete.ForEach(predmet => {

                        sw.WriteLine(student.Id + "," + predmet.Id);
                    });
                });
            }catch (Exception e)
            {
                Console.WriteLine("*** Greska pri zapisivanju studenat_predmet fajla. ");
                Console.WriteLine("\n\t*** " + e.Message);
                Console.WriteLine("\n\t*** " + e.StackTrace);
                
            }
            finally
            {
                
                sw.Close();
            }
        }

    }
}
