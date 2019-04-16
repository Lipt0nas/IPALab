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

        private void generateStudentLists()
        {
            Console.Clear();

            Stopwatch w = new Stopwatch();
            w.Start();

            Random r = new Random();

            for (int i = 0; i < 7; i++)
            {
                List<Student> list = new List<Student>();

                for (int j = 0; j < Math.Pow(10, i + 1); j++)
                {
                    Student student = new Student();
                    student.Name = "Student_" + j;
                    student.LastName = "Sirname_" + j;
                    student.ExamGrade = r.Next(0, 11);

                    for (int k = 0; k < 10; k++)
                    {
                        student.AddHomeworkGrade(r.Next(0, 11));
                    }

                    student.getGradeAvg(false);

                    list.Add(student);
                }


                Writer.WriteStudentsToFile("_" + Math.Pow(10, i + 1) + ".txt", list);

                list.Sort((o1, o2) => o1.AverageGrade.CompareTo(o2.AverageGrade));
                int marker = list.Count / 2;
                int direction = 0;

                if (list.ElementAt(marker).AverageGrade >= 5.0f)
                {
                    direction = -1;
                } else
                {
                    direction = 1;
                }

                while(true)
                {
                    marker += direction;

                    if (direction > 0)
                    {
                        if (list.ElementAt(marker).AverageGrade >= 5.0f)
                        {
                            break;
                        }
                    }

                    if(direction < 0)
                    {
                        if (list.ElementAt(marker).AverageGrade < 5.0f)
                        {
                            marker++;
                            break;
                        }
                    }
                }

                Writer.WriteStudentsToFile("_" + Math.Pow(10, i + 1) + "_vargsiukai.txt", list.GetRange(0, marker));
                Writer.WriteStudentsToFile("_" + Math.Pow(10, i + 1) + "_galvociai.txt", list.GetRange(marker, list.Count - marker));
            }

            w.Stop();
            Console.WriteLine("Ivykdyta per: " + w.ElapsedMilliseconds + "ms");

            Run();
        }

        public void testContainers()
        {
            //LIST
            {
                Stopwatch perf = new Stopwatch();
                perf.Start();

                List<Student> students = new List<Student>();
                Reader.ReadStudentsFromFile("_1000000.txt", (Student s) => {
                    students.Add(s);
                });

                List<Student> high = new List<Student>();
                List<Student> low = new List<Student>();

                foreach(Student s in students)
                {
                    s.getGradeAvg(false);

                    if(s.AverageGrade >= 5.0f)
                    {
                        high.Add(s);
                    }
                    else
                    {
                        low.Add(s);
                    }
                }

                perf.Stop();

                Console.WriteLine("List<> uztruko " + perf.ElapsedMilliseconds + "ms");
            }

            //Queue
            {
                Stopwatch perf = new Stopwatch();
                perf.Start();

                Queue<Student> students = new Queue<Student>();
                Reader.ReadStudentsFromFile("_1000000.txt", (Student s) => {
                    students.Enqueue(s);
                });
                Queue<Student> high = new Queue<Student>();
                Queue<Student> low = new Queue<Student>();

                foreach (Student s in students)
                {
                    s.getGradeAvg(false);

                    if (s.AverageGrade >= 5.0f)
                    {
                        high.Enqueue(s);
                    }
                    else
                    {
                        low.Enqueue(s);
                    }
                }

                perf.Stop();

                Console.WriteLine("Queue<> uztruko " + perf.ElapsedMilliseconds + "ms");
            }

            //Queue
            {
                Stopwatch perf = new Stopwatch();
                perf.Start();

                LinkedList<Student> students = new LinkedList<Student>();
                Reader.ReadStudentsFromFile("_1000000.txt", (Student s) => {
                    students.AddLast(s);
                });
                LinkedList<Student> high = new LinkedList<Student>();
                LinkedList<Student> low = new LinkedList<Student>();

                foreach (Student s in students)
                {
                    s.getGradeAvg(false);

                    if (s.AverageGrade >= 5.0f)
                    {
                        high.AddLast(s);
                    }
                    else
                    {
                        low.AddLast(s);
                    }
                }

                perf.Stop();

                Console.WriteLine("LinkedList<> uztruko " + perf.ElapsedMilliseconds + "ms");
            }
        }

        public void compareContainerTest()
        {

        }

        public void Run()
        {
            ShowMenu("Kaip sukurti studentus?",
                Tuple.Create<string, Action>("Ivesti Ranka \t\t\t\t\t[V0.1]", () => {
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

                Tuple.Create<string, Action>("Skaityti is failo \t\t\t\t\t[V0.2 / V0.3]", () => { students = Reader.ReadStudentsFromFile<List<Student>>("kursiokai.txt"); DisplayAllStudentGrades(); }),
                Tuple.Create<string, Action>("Sugeneruoti studentu sarasus \t\t\t[V0.4]", () => { generateStudentLists(); }),
                Tuple.Create<string, Action>("Testuoti konteineriu greiti \t\t\t\t[V0.5]", () => { testContainers(); }),
                Tuple.Create<string, Action>("Testuoti konteineriu greiti ir optimizacijas \t[V1.0]", () => { compareContainerTest(); })
            );

            Console.ReadKey();
        }
    }
}
