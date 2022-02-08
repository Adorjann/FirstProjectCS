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
            Optional Ostudent = studentRepo.delete(student);
            if (Ostudent.IsPresent)
            {
                return (Student)Ostudent.Get();
            }
            throw exceptions.GetObjectNotFoundException();
        }

        public  List<Student> FindAll()
        {
            Optional OallStudents = studentRepo.findAll();
            if (OallStudents.IsPresent)
            {
                return (List<Student>)OallStudents.Get();
            }
            throw exceptions.GetCollectionIsEmptyException();
        }

        public  List<Student> FindAllSorted()
        {
            throw new NotImplementedException();
        }

        public  Student FindById(int id)
        {
            Optional Ostudent =  studentRepo.findById(id);
            if (Ostudent.IsPresent)
            {
                return (Student)Ostudent.Get();
            }
            throw exceptions.GetObjectNotFoundException();
        }

        public  Student FindByIndex(string index)
        {
            Optional Ostudent = studentRepo.findByIndex(index);
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

        public  Student Save(Student student)
        {
            Optional Ostudent = studentRepo.save(student);
            if (Ostudent.IsPresent)
            {
                return (Student)Ostudent.Get();
            }
            throw exceptions.GetDuplicateObjectException();
        }

        
    }
}
