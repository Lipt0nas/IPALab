using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace IPA
{
    class Grader
    {
        private List<Student> students;

        public Grader()
        {
            students = new List<Student>();
        }

        private void ShowMenu(string message, params Tuple<string, Action>[] choices)
        {
            Console.WriteLine(message);

            for(int i = 0; i < choices.Length; i++)
            {
                Console.WriteLine("\t" + (i + 1) + ". " + choices[i].Item1);
            }
            
            string rawInput = Console.ReadLine();
            int input;
            bool correctInput = int.TryParse(rawInput, out input);

            if(correctInput)
            {
                input -= 1;

                if(!(input >= 0 && input < choices.Length))
                {
                    correctInput = false;
                }
            }

            if(correctInput)
            {
                choices[input].Item2();
            } else
            {
                Console.Clear();
                Console.WriteLine("Neteisingas pasirinkimas [" + rawInput + "] bandykite is naujo");
                ShowMenu(message, choices);
            }
        }

        private void InputStudents(bool random)
        {
            while (true) {
                Console.Clear();
                Student student = new Student();
                
                if(random)
                {
                    student.CreateRandom();
                } else
                {
                    student.Create();
                }

                students.Add(student);

                Console.Clear();

                bool stop = false;
                ShowMenu("Ar ivesti dar viena?",
                    Tuple.Create<string, Action>("Taip", () => {}),
                    Tuple.Create<string, Action>("Ne", () => { stop = true; })
                );

                if (stop) break;
            }
        }

        private void DisplayStudentGrades(bool median)
        {
            Console.Clear();

            Console.WriteLine("{0, -15}{1, -15}{2, -15}", "Vardas", "Pavarde", "Galutinis" + (median ? "(Med.)" : "(Vid.)"));
            Console.WriteLine("----------------------------------------------");

            foreach (Student s in students)
            {
                Console.WriteLine("{0, -15}{1, -15}{2, -15}", s.Name, s.LastName, s.getGradeAvg(median).ToString("0.00"));
            }
        }

        private void DisplayAllStudentGrades()
        {
            students.Sort((o1, o2) => o1.Name.CompareTo(o2.Name));

            Console.WriteLine("{0, -15}{1, -15}{2, -30}{3, -30}", "Vardas", "Pavarde", "Galutinis(Vid.)", "Galutinis(Med.)");
            Console.WriteLine("---------------------------------------------------------------------------");

            foreach (Student s in students)
            {
                Console.WriteLine("{0, -15}{1, -15}{2, -30}{3, -30}", s.Name, s.LastName, s.getGradeAvg(false).ToString("0.00"), s.getGradeAvg(true).ToString("0.00"));
            }
        }

        public void Run()
        {
            ShowMenu("Kaip sukurti studentus?",
                Tuple.Create<string, Action>("Ivesti Ranka", () => {
                    Console.Clear();
                    ShowMenu("Ar generuoti studentu pazymius?", 
                        Tuple.Create<string, Action>("Taip", () => { InputStudents(true); }),
                        Tuple.Create<string, Action>("Ne", () => { InputStudents(false); })
                    );

                    Console.Clear();
                    ShowMenu("Skaiciuoti",
                        Tuple.Create<string, Action>("Pazymiu vidurki", () => { DisplayStudentGrades(false); }),
                        Tuple.Create<string, Action>("Pazymiu mediana", () => { DisplayStudentGrades(true); })
                    );
                }),

                Tuple.Create<string, Action>("Skaityti is failo", () => { students = Reader.ReadStudentsFromFile<List<Student>>("kursiokai.txt"); DisplayAllStudentGrades(); })
            );

            Console.ReadKey();
        }
    }
}
