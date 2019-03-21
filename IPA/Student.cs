using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPA
{
    class Student
    {
        public string name { set; get; }
        public string lastName { set; get; }

        public int examGrade { get; set; }

        public List<int> homeworkGrades { get; }

        public Student()
        {
            homeworkGrades = new List<int>();
        }

        public virtual void AddHomeworkGrade(int grade)
        {
            homeworkGrades.Add(grade);
        }

        public virtual float GetGradeAvg()
        {
            int sum = homeworkGrades.Sum();

            return 0.3f * ((float)sum / homeworkGrades.Count) + 0.7f * examGrade;
        }

        public virtual float GetGradeMedian()
        {
            float med = 0;

            List<int> temp = new List<int>(homeworkGrades);
            temp.Sort();
            
            if(temp.Count % 2 == 0)
            {
                med = temp.ElementAt(temp.Count / 2);
            } else
            {
                med = (float)(temp.ElementAt(temp.Count / 2) + temp.ElementAt((temp.Count / 2) - 1)) / 2.0f;
            }

            return 0.3f * med + 0.7f * examGrade;
        }

        public void Create()
        {
            Console.Write("Iveskite studento varda: ");
            name = Console.ReadLine();

            Console.Write("Iveskite studento pavarde: ");
            lastName = Console.ReadLine();

            Console.Write("Iveskite studento egzamino pazymi: ");
            examGrade = int.Parse(Console.ReadLine());

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
