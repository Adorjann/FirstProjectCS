using FirstProjectCS.model;
using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.model;
using FirstProjectCS.repository;

namespace FirstProjectCS.servis.Impl
{
    internal class StudentServisImpl 
    {
        //"Servis" 

        

        public static Student delete(Student student)
        {
            return StudentRepository.delete(student);
        }

        public static List<Student> findAll()
        {
            return StudentRepository.findAll();
        }

        public static List<Student> findAllSorted()
        {
            throw new NotImplementedException();
        }

        public static Student findById(int id)
        {
            return StudentRepository.findById(id);
        }

        public static Student findByIndex(string index)
        {
            return StudentRepository.findByIndex(index);
        }

        public static Student findByName(string name)
        {
            throw new NotImplementedException();
        }

        public static Student save(Student student)
        {
            return StudentRepository.save(student);
        }

        
    }
}
