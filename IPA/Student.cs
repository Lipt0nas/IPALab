using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPA
{
    class Student
    {
        public string Name { set; get; }
        public string LastName { set; get; }

        public int ExamGrade { get; set; }

        public List<int> HomeworkGrades { get; }

        public Student()
        {
            HomeworkGrades = new List<int>();
        }

        public void AddHomeworkGrade(int grade)
        {
            HomeworkGrades.Add(grade);
        }

        public float getGradeAvg(bool median)
        {
            return 0.3f * (median ? HomeworkMedian() : HomeworkAvg()) + 0.7f * ExamGrade;
        }

        private float HomeworkAvg()
        {
            int sum = HomeworkGrades.Sum();

            if(HomeworkGrades.Count == 0)
            {
                return 0.0f;
            } else
            {
                return (float)sum / HomeworkGrades.Count;
            }
        }

        private float HomeworkMedian()
        {
            if (HomeworkGrades.Count == 0) return 0.0f;
            if (HomeworkGrades.Count == 1) return HomeworkGrades.First();

            List<int> temp = new List<int>(HomeworkGrades);
            temp.Sort();

            if(temp.Count % 2 == 0)
            {
               return temp.ElementAt(temp.Count / 2);
            } else
            {
              return (float)(temp.ElementAt(temp.Count / 2) + temp.ElementAt((temp.Count / 2) - 1)) / 2.0f;
            }
        }

        public void CreateRandom()
        {
            Console.Write("Iveskite studento varda: ");
            Name = Console.ReadLine();

            Console.Write("Iveskite studento pavarde: ");
            LastName = Console.ReadLine();

            Random r = new Random();
            ExamGrade = r.Next(1, 11);

            for(int i = 0; i < r.Next(1, 21); i++)
            {
                AddHomeworkGrade(r.Next(1, 11));
            }
        }

        public void Create()
        {
            Console.Write("Iveskite studento varda: ");
            Name = Console.ReadLine();

            Console.Write("Iveskite studento pavarde: ");
            LastName = Console.ReadLine();

            while (true)
            {
                Console.Write("Iveskite studento egzamino pazymi: ");
                int examGrade;

                if(int.TryParse(Console.ReadLine(), out examGrade)) {
                    ExamGrade = examGrade;
                    break;
                } else
                {
                    Console.Write("Neteisingas Ivedimas, bandykite dar karta");
                }
            }

            Console.WriteLine("Iveskite studento namu darbu pazymius (-1 kad baigti): ");
            int grade = 0;
            while(true)
            {
                if(int.TryParse(Console.ReadLine(), out grade)) {
                    if (grade == -1)
                    {
                        break;
                    }
                    else
                    {
                        AddHomeworkGrade(grade);
                    }
                } else
                {
                    Console.Write("Neteisingas Ivedimas, bandykite dar karta");
                }
            }
        }
    }
}
