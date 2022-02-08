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

        public  Optional save(Student s)
        {
            //INSERT INTO student VALUES (id,index,ime,prezime,grad)

            Optional Ostudent= findByIndex(s.Index);
            
            if(!Ostudent.IsPresent && s != null)
            {   
                if(s.Id == 0) //dummy autoIncrement
                {
                    s.Id = sviStudent.Count;
                    s.Id++;
                }
                sviStudent.Add(s);
                return Optional.Of(s);
            }
            return Optional.Empty();
        }

        internal  Optional findById(int id)
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

        internal  Optional delete(Student student)
        {
            //DELETE FROM student WHERE student.index = ?

            if (sviStudent.Contains(student)) { sviStudent.Remove(student); }
            
            if (!sviStudent.Contains(student))
            {
                Optional.Of(student);
            }
            return Optional.Empty();
        }

        public  Optional findAll()
        {
            //SELECT * FROM student;
            if (sviStudent.Count == 0)
            {
                return Optional.Empty();
            }
            return Optional.Of(sviStudent);
        }

        internal  Optional findByIndex(string index)
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
