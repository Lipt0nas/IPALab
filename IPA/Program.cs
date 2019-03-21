using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPA
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();

            Console.WriteLine("Iveskite studenta:");

            while (true)
            {
                Student s = new Student();
                s.Create();

                students.Add(s);

                Console.WriteLine("Ivesti dar viena? (t - taip, n - ne)");
                string choice = Console.ReadLine();

                if(choice[0] == 'n')
                {
                    break;
                }
            }

            Console.WriteLine("{0, -15}{1, -15}{2, -15}", "Vardas", "Pavarde", "Galutinis(Vid.)");
            Console.WriteLine("----------------------------------------------");
            foreach(Student s in students)
            {
                Console.WriteLine("{0, -15}{1, -15}{2, -15}", s.name, s.lastName, s.getGradeAvg());
            }

            Console.ReadLine();
        }
    }
}
