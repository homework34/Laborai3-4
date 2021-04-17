using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborai3_4
{
    class FinalGradeWithList
    {
        private string name;
        private string lastName;
        private List<int> homeworkGrades;
        private int examGrade;
        private double finalGrade;

        public FinalGradeWithList()
        {
        }

        public FinalGradeWithList(string name, string lastName, List<int> homework, int exam)
        {
            this.Name = name;
            this.LastName = lastName;
            this.HomeworkGrades = homework;
            this.ExamGrade = exam;
        }

        public FinalGradeWithList(string line)
        {
            string[] studentInfo;
            int exam;
            line.Trim();
            Int32.TryParse(line.Substring(line.LastIndexOf(' ') + 1), out exam);
            line = line.Substring(0, line.LastIndexOf(' ') - 1);
            studentInfo = line.Split(' ');

            this.name = studentInfo[0];
            this.lastName = studentInfo[1];
            this.homeworkGrades = new List<int>();
            for(int i=2; i< studentInfo.Length; i++)
            {
                int hwGrade;
                Int32.TryParse(studentInfo[i], out hwGrade);
                this.homeworkGrades.Add(hwGrade);
            }

            this.examGrade = exam;

        }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public List<int> HomeworkGrades { get => homeworkGrades; set => homeworkGrades = value; }
        public int ExamGrade { get => examGrade; set => examGrade = value; }
        public double FinalGrade { get => finalGrade; set => finalGrade = value; }

        private double getMedian()
        {
            if (this.homeworkGrades.Count == 0)
            {
                throw new Exception("homework values null.");
            }

            this.homeworkGrades.Sort();

            int size = this.homeworkGrades.Count;
            int mid = size / 2;
            double median = (size % 2 != 0) ? (double)this.homeworkGrades[mid] :
                ((double)this.homeworkGrades[mid] + (double)this.homeworkGrades[mid - 1]) / 2;
            return median;
        }

        public double getFinalGradeWithAverage()
        {
            double a = (double)(0.3d * this.homeworkGrades.Average()) + (0.7d * this.ExamGrade);
            return a;
        }

        public double getFinalGradeWithMedian()
        {
           return (double)(0.3d * getMedian()) + (0.7d * this.ExamGrade);
        }
    }
}
