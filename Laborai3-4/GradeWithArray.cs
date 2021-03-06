using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborai3_4
{
    class FinalGradeWithArray
    {
        private string name;
        private string lastName;
        private int[] homeworkGrades;
        private int examGrade;
        private double finalGrade;

        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int[] HomeworkGrades { get => homeworkGrades; set => homeworkGrades = value; }
        public int ExamGrade { get => examGrade; set => examGrade = value; }
        public double FinalGrade { get => finalGrade; set => finalGrade = value; }

        public FinalGradeWithArray()
        {
        }

        public FinalGradeWithArray(string name, string lastName, int[] homework, int exam)
        {
            this.Name = name;
            this.LastName = lastName;
            this.HomeworkGrades = homework;
            this.ExamGrade = exam;
            //Final_points = 0.3 * average_of_hw + 0.7 * egzam
            //if (average)
            //{
            //    this.FinalGrade = (double)(0.3d * getAverage(homework)) + (0.7d * exam);
            //}
            //else
            //{
            //    this.FinalGrade = (double)(0.3d * getMedian(homework)) + (0.7d * exam);
            //}
        }

        private int getSum(int[] homework)
        {
            int sum = 0;
            foreach (int grade in homework)
            {
                sum += grade;
            }
            return sum;
        }

        //private double getAverage(int[] homework)
        //{
        //    if(homework.Length > 0)
        //    {
        //    return (double)getSum(homework) / homework.Length;
        //    }
        //    throw new Exception("homework values null.");
        //}

        private double getMedian()
        {
            if (this.homeworkGrades.Length == 0)
            {
                throw new Exception("homework values null.");
            }

            int[] sortedPNumbers = (int[])this.homeworkGrades.Clone();
            Array.Sort(sortedPNumbers);

            int size = sortedPNumbers.Length;
            int mid = size / 2;
            double median = (size % 2 != 0) ? (double)sortedPNumbers[mid] : ((double)sortedPNumbers[mid] + (double)sortedPNumbers[mid - 1]) / 2;
            return median;
        }
        public double getFinalGradeWithAverage()
        {
            return (double)(0.3d * this.homeworkGrades.Average()) + (0.7d * this.ExamGrade);
        }

        public double getFinalGradeWithMedian()
        {
            return (double)(0.3d * getMedian()) + (0.7d * this.ExamGrade);
        }
    }
}
