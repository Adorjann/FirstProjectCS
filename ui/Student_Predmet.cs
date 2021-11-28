using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FirstProjectCS.servis.Impl;
using FirstProjectCS.model;


namespace FirstProjectCS.ui
{
    internal class Student_Predmet
    {
        public static void ucitajVezuStudent_Predmet(string fajl)
        {
            
            StreamReader sr = new StreamReader(fajl);
            string line;

            try
            {
                line = sr.ReadLine();
                while(line != null)
                {
                    string[] tokeni = line.Split(',');
                    Student student = StudentServisImpl.findById(Convert.ToInt32(tokeni[0]));
                    Predmet predmet = PredmetServisImpl.findById(Convert.ToInt32(tokeni[1]));

                    if(student != null && predmet != null)
                    {
                        predmet.dodajStudenta(student);

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
        public static void zapisiVezuStudent_Predmet(string fajl)
        {
            StreamWriter sw = new StreamWriter(fajl);
            try
            {
                foreach(Student s in StudentServisImpl.findAll())
                {
                    foreach(Predmet p in s.StudentPohadjaPredmete)
                    {
                        sw.WriteLine(s.Id+","+p.Id);
                    }
                }

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
