using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IPA
{
    class Writer
    {
        public static void WriteStudentsToFile<T>(string filename, T list) where T : IEnumerable<Student>
        {
            try
            {
                StreamWriter writer = new StreamWriter(filename);
                StringBuilder builder = new StringBuilder();

                foreach (Student student in list)
                {
                    builder.Clear();

                    builder.Append("name:").Append(student.Name).Append(';');
                    builder.Append("lastName:").Append(student.LastName).Append(';');
                    builder.Append("exam:").Append(student.ExamGrade).Append(';');
                    builder.Append("homework:");
                    foreach(int grade in student.HomeworkGrades)
                    {
                        builder.Append(grade);
                        builder.Append(',');
                    }
                    builder.Length--;
                    builder.Append(';');
                    builder.Append('\n');

                    writer.Write(builder.ToString());
                }

                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ivyko klaida rasant i faila faila:");
                Console.WriteLine(e.Message);
            }
        }
    } 
}
