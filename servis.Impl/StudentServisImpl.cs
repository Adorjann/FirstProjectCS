using FirstProjectCS.model;
using System;
using System.Collections.Generic;
using FirstProjectCS.repository;

namespace FirstProjectCS.servis.Impl
{
    //"Servis" 
    public class StudentServisImpl 
    {
        
        //DI
        private readonly CustomException exceptions = (CustomException)SingletonCreator.GetInstance(typeof(CustomException));
        private readonly StudentRepository studentRepo = (StudentRepository)SingletonCreator.GetInstance(typeof(StudentRepository));
        private StudentServisImpl()
        {

        }
        

        public  Student Delete(Student student)
        {
            Optional Ostudent = studentRepo.Delete(student);
            if (Ostudent.IsPresent)
            {
                Student deletedS = (Student)Ostudent.Get();
                
                return (Student)Ostudent.Get();
            }
            throw exceptions.GetObjectNotFoundException();
        }

        public  List<Student> FindAll()
        {
            Optional OallStudents = studentRepo.FindAll();
            if (OallStudents.IsPresent)
            {
                return (List<Student>)OallStudents.Get();
            }
            throw exceptions.GetCollectionIsEmptyException();
        }


        public  Student FindById(int id)
        {
            Optional Ostudent =  studentRepo.FindById(id);
            if (Ostudent.IsPresent)
            {
                return (Student)Ostudent.Get();
            }
            throw exceptions.GetObjectNotFoundException();
        }

        public  Student FindByIndex(string index)
        {
            Optional Ostudent = studentRepo.FindByIndex(index);
            if (Ostudent.IsPresent)
            {
                return(Student)Ostudent.Get();
            }
            throw exceptions.GetObjectNotFoundException();
        }

        public Student FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public  Student Save(Student s)
        {
            Optional Ostudent = studentRepo.FindByIndex(s.Index);
            Optional OsviStudenti = studentRepo.FindAll();

            if (!Ostudent.IsPresent)
            {
                if ((s.Ime != "" && s.Ime != null) &&
                    (s.Prezime != "" && s.Prezime != null))
                {
                    if (s.Id == 0) //dummy autoIncrement
                    {
                        List<Student> sviStudenti = OsviStudenti.IsPresent ? 
                            (List<Student>)OsviStudenti.Get() : new List<Student>();
                        s.Id =  sviStudenti.Count;
                        s.Id++;
                    }
                    Optional OsavedStudent = studentRepo.Save(s);
                    if (OsavedStudent.IsPresent)
                    {
                        return (Student)OsavedStudent.Get();
                    }
                }
                throw new InvalidOperationException();
            }
            throw exceptions.GetDuplicateObjectException();




        }

        
    }
}
