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

        public float GetGradeAvg()
        {
            int sum = HomeworkGrades.Sum();

            return 0.3f * ((float)sum / HomeworkGrades.Count) + 0.7f * ExamGrade;
        }

        public float GetGradeMedian()
        {
            float med = 0;

            List<int> temp = new List<int>(HomeworkGrades);
            temp.Sort();
            
            if(temp.Count % 2 == 0)
            {
                med = temp.ElementAt(temp.Count / 2);
            } else
            {
                med = (float)(temp.ElementAt(temp.Count / 2) + temp.ElementAt((temp.Count / 2) - 1)) / 2.0f;
            }

            return 0.3f * med + 0.7f * ExamGrade;
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

            Console.Write("Iveskite studento egzamino pazymi: ");
            ExamGrade = int.Parse(Console.ReadLine());

            Console.WriteLine("Iveskite studento namu darbu pazymius (-1 kad baigti): ");
            int grade = 0;
            while(true)
            {
                grade = int.Parse(Console.ReadLine());

                if(grade == -1)
                {
                    break;
                } else
                {
                    AddHomeworkGrade(grade);    
                }
            }
        }
    }
}
