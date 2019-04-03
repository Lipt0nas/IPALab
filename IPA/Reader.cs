using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
                StreamReader file = new StreamReader(filePath);
                while ((line = file.ReadLine()) != null)
                {
                    string[] values = line.Split(';');

                    Student student = new Student();

                    foreach (string pair in values)
                    {
                        string[] pairVal = pair.Split(':');

                        if (pairVal.Length != 2) continue;

                        string identifier = pairVal[0].Trim().ToLower();
                        string value = pairVal[1].Trim();

                        if (identifier.Equals("name"))
                        {
                            student.Name = value;
                        }

                        if (identifier.Equals("sirname"))
                        {
                            student.LastName = value;
                        }

                        if (identifier.Equals("homework"))
                        {
                            string[] grades = value.Split(',');
                            int numeric;

                            foreach (string grade in grades)
                            {
                                if (int.TryParse(grade, out numeric))
                                {
                                    if (numeric >= 0 && numeric <= 10)
                                        student.AddHomeworkGrade(numeric);
                                    else
                                        Console.WriteLine("Namu darbo pazimys negali buti neigiama reiksme arba virsyti 10");
                                }
                                else
                                {
                                    Console.WriteLine("Neisejo nuskaityti namu darbo pazymio [" + grade + "]");
                                }
                            }
                        }

                        if (identifier.Equals("exam"))
                        {
                            int numeric;
                            if (int.TryParse(value, out numeric))
                            {
                                if (numeric >= 0 && numeric <= 10)
                                    student.ExamGrade = numeric;
                                else
                                    Console.WriteLine("Egzamino pazimys negali buti neigiama reiksme arba virsyti 10");
                            }
                            else
                            {
                                Console.WriteLine("Neisejo nuskaityti egzamino pazymio [" + value + "]");
                            }
                        }

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
