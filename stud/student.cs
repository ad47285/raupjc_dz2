using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stud
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }
        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Student p = obj as Student;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Name == p.Name);
        }

        public bool Equals(Student p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Name == p.Name);
        }

        public static bool operator ==(Student s1, Student s2)
        {
            return s1.Equals(s2);
        }

        public static bool operator !=(Student s1, Student s2)
        {
            return !s1.Equals(s2);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public enum Gender
    {
        Male, Female
    }
}
