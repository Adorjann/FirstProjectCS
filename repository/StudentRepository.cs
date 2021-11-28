using System;
using System.Collections.Generic;
using System.Text;
using FirstProjectCS.model;


namespace FirstProjectCS.repository
{
    internal class StudentRepository
    {

        private static List<Student> sviStudent = new List<Student>();

        public static Student save(Student s)
        {
            //INSERT INTO student VALUES (id,index,ime,prezime,grad)

            Student student = findByIndex(s.Index);
            
            if(student == null && s != null)
            {   
                if(s.Id == 0) //dummy autoIncrement
                {
                    s.Id = sviStudent.Count;
                    s.Id++;
                }
                sviStudent.Add(s);
                return s;
            }
            return null;
        }

        internal static Student findById(int id)
        {
            //SELECT * FROM student WHERE student.id = ?

            Student retVal = null;

            foreach (Student student in sviStudent)
            {
                if(student.Id == id)
                {
                   return retVal = student;
                }
            }
            return retVal;
        }

        internal static Student delete(Student student)
        {
            //DELETE FROM student WHERE student.index = ?

            if (sviStudent.Contains(student)) { sviStudent.Remove(student); }
            
            if (!sviStudent.Contains(student))
            {
                return student;
            }
            return null;
        }

        public static List<Student> findAll()
        {
            //SELECT * FROM student;
            List<Student> retVal = null;

            retVal = sviStudent.Count != 0 ? sviStudent : null;

            return retVal;
        }

        internal static Student findByIndex(string index)
        {
            //SELECT * FROM student WHERE student.index = ?

            Student retVal = null;

            foreach (Student it in sviStudent)
            {
                if(it.Index == index)
                {
                    return retVal = it;
                }
            }
            return retVal;
        }
    }
}
