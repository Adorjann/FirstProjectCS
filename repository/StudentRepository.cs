using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.model;
using FirstProjectCS.utils;


namespace FirstProjectCS.repository
{
    public class StudentRepository
    {
        
        private StudentRepository()
        {

        }
        
        private  List<Student> sviStudent = new List<Student>();

        public  Optional Save(Student s)
        {
            //INSERT INTO student VALUES (?,?,?,?,?)

            sviStudent.Add(s);
            return Optional.Of(s);
        }

        public  Optional FindById(int id)
        {
            //SELECT * FROM student WHERE student.id = ?

            foreach (Student student in sviStudent)
            {
                if(student.Id == id)
                {
                   return Optional.Of(student);
                }
            }
            return Optional.Empty();
        }

        public  Optional Delete(Student student)
        {
            //DELETE FROM student WHERE student.index = ?

            if (sviStudent.Remove(student))
            {
                Optional.Of(student);
            }
            return Optional.Empty();
        }

        public  Optional FindAll()
        {
            //SELECT * FROM student;
            if (sviStudent.Count == 0)
            {
                return Optional.Empty();
            }
            return Optional.Of(sviStudent);
        }

        public  Optional FindByIndex(string index)
        {
            //SELECT * FROM student WHERE student.index = ?

            foreach (Student it in sviStudent)
            {
                if(it.Index == index)
                {
                    return Optional.Of(it);
                }
            }
            return Optional.Empty();
        }
    }
}
