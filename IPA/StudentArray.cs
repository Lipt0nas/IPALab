using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPA
{
    class StudentArray : Student
    {
        public int[] hwa;
        
        public StudentArray()
        {
            hwa = new int[1];
        }

        public override void AddHomeworkGrade(int grade)
        {
            if (hwa.Length != 1)
            {
                Array.Resize(ref hwa, hwa.Length + 1);
            }

            hwa[hwa.Length - 1] = grade;
        }

        public override float GetGradeAvg()
        {
            int sum = hwa.Sum();

            return 0.3f * ((float)sum / hwa.Length) + 0.7f * examGrade;
        }

        public override float GetGradeMedian()
        {
            float med = 0;

            int[] temp = new int[hwa.Length];
            Array.Copy(hwa, temp, hwa.Length);
            Array.Sort(temp);

            if (temp.Length % 2 == 0)
            {
                med = temp[(temp.Length / 2)];
            }
            else
            {
                med = (float)(temp[(temp.Length / 2)] + temp[((temp.Length / 2) - 1)]) / 2.0f;
            }

            return 0.3f * med + 0.7f * examGrade;
        }
    }
}
