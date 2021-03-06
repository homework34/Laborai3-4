using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Laborai3_4
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            List<FinalGradeWithList> grades = new List<FinalGradeWithList>();
            while (!exit)
            {
                int choice;
                Console.WriteLine("1. Įvesti mokinių rezultatus \n2. Atvaizduoti mokinių rezultatus.\n3. Išeiti.");
                Int32.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        grades.AddRange(enterGrades());
                        break;
                    case 2:
                        print(grades);
                        break;
                    case 3:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Pasirinkite iš galimų variantų");
                        break;
                }
            }
        }

        public static List<FinalGradeWithList> enterGrades()
        {
            bool exit = false;
            int choice;
            List<FinalGradeWithList> grades = new List<FinalGradeWithList>();
            while (!exit)
            {
                Console.WriteLine("1. Įvesti studento pažymius \n2. Sugeneruoti pazymius studentui\n3. Išeiti.");
                Int32.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        grades.Add(gradeInput());
                        break;
                    case 2:
                        grades.Add(generateGrades());
                        break;
                    case 3:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Pasirinkite iš galimų variantų");
                        break;
                }
            }

            return grades;
        }

        public static FinalGradeWithList gradeInput()
        {
            string name;
            string lastName;
            List<int> homeworkGrades = new List<int>();
            string homework;
            int exam;
            Console.WriteLine("Mokinio vardas:");
            name = Console.ReadLine();
            Console.WriteLine("Mokinio pavarde:");
            lastName = Console.ReadLine();
            Console.WriteLine("Mokinio namų darbų pažymiai (išrašykite visus atskirtus tarpu)");
            homework = Console.ReadLine();
            Console.WriteLine("Mokinio egzamino pažymys");
            Int32.TryParse(Console.ReadLine(), out exam);

            string[] gradeArray = homework.Split(' ');
            foreach (string grade in gradeArray)
            {
                homeworkGrades.Add(int.Parse(grade));
            }
            return new FinalGradeWithList(name, lastName, homeworkGrades, exam);
            
        }

        public static FinalGradeWithList generateGrades()
        {
            string name;
            string lastName;
            List<int> homeworkGrades = new List<int>();
            int exam;
            Console.WriteLine("Mokinio vardas:");
            name = Console.ReadLine();
            Console.WriteLine("Mokinio pavarde:");
            lastName = Console.ReadLine();

            Random random = new Random();
            for(int i = 0; i<5; i++)
            {
                homeworkGrades.Add(random.Next(1, 10));
            }
            exam = random.Next(1, 10);
            return new FinalGradeWithList(name, lastName, homeworkGrades, exam);
        }

        public static void print(List<FinalGradeWithList> grades)
        {
            Console.WriteLine("1. Vidurki\n2. Mediana");
            int choice = int.Parse(Console.ReadLine());
            bool average = choice == 1 ? true : false;
            if(average)
            {
                printGradesAvg(grades);
            } 
            else
            {
                printGradesMedian(grades);
            }
        }

        public static void printGradesAvg(List<FinalGradeWithList> grades)
        {
            Console.WriteLine("{0,-20} {1,-20} {2,-60}", "Pavarde", "Vardas", "Galutinis(Vid.)");
            Console.WriteLine("_________________________________________________________________");
            foreach (FinalGradeWithList grade in grades)
            {
                Console.WriteLine("{0,-20} {1,-20} {2,-60:N2}", grade.LastName, grade.Name, grade.getFinalGradeWithAverage());
            }
        }

        public static void printGradesMedian(List<FinalGradeWithList> grades)
        {
            Console.WriteLine("{0,-20} {1,-20} {2,-60}", "Pavarde", "Vardas", "Galutinis(Med.)");
            Console.WriteLine("_________________________________________________________________");
            foreach (FinalGradeWithList grade in grades)
            {
                Console.WriteLine("{0,-20} {1,-20} {2,-60:N2}", grade.LastName, grade.Name, grade.getFinalGradeWithMedian());
            }
        }

        public static void generateRandomGrades()
        {

        }
    }
}
