using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPA
{
    class Reader
    {
        public static T ReadStudentsFromFile<T>(string filePath) where T : IEnumerable<Student>
        {
            List<Student> students = new List<Student>();

            string line;

            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader("kursiokai.txt");
                while ((line = file.ReadLine()) != null)
                {
                    string[] args = line.Split(' ');

                    if (args.Length != 8) continue;

                    Student student = new Student();
                    student.Name = args[0];
                    student.LastName = args[1];
                    for (int i = 0; i < 5; i++)
                    {
                        try
                        {
                            student.AddHomeworkGrade(int.Parse(args[2 + i]));
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Neisejo nuskaityti namu darbo pazymio [" + args[2 + i] + "]");
                        }
                    };

                    try
                    {
                        student.ExamGrade = (int.Parse(args[7]));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Neisejo nuskaityti egzamino pazymio [" + args[7] + "]");
                    }

                    students.Add(student);
                }

                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ivyko klaida skaitant faila:");
                Console.WriteLine(e.Message);
            }

            return (T)Activator.CreateInstance(typeof(T), students);
        }
    }
}
