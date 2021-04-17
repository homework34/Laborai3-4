using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Laborai3_4
{
    class Program
    {
        static void Main(string[] args)
        {
            // trečiam release viskas kaip ir padaryta, try catch tikrinimai, klases atskiruose failuose,
            // bet forgood measures ikeliu atskira brancha.
            bool exit = false;
            List<FinalGradeWithList> grades = new List<FinalGradeWithList>();
            while (!exit)
            {
                int choice;
                Console.WriteLine(
                    "1. Įvesti mokinių rezultatus \n" +
                    "2. Pasiimti mokiniu pazymius is failo\n" +
                    "3. Atvaizduoti mokinių rezultatus.\n" +
                    "4. Išeiti.");
                Int32.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        grades.AddRange(enterGrades());
                        break;
                    case 2:
                        grades.AddRange(getGradesFromFile());
                        break;
                    case 3:
                        print(grades);
                        break;
                    case 4:
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
            for (int i = 0; i < 5; i++)
            {
                homeworkGrades.Add(random.Next(1, 10));
            }
            exam = random.Next(1, 10);
            return new FinalGradeWithList(name, lastName, homeworkGrades, exam);
        }

        public static void print(List<FinalGradeWithList> grades)
        {

            grades.Sort((x, y) => x.LastName.CompareTo(y.LastName));
            Console.WriteLine();
            Console.WriteLine("{0,-15} {1,-30} {2,-15} {3,-15}", "Pavarde", "Vardas", "Galutinis(Vid.)", "Galutinis(Med.)");
            Console.WriteLine("__________________________________________________________________________________________");
            foreach (FinalGradeWithList grade in grades)
            {
                Console.WriteLine("{0,-15} {1,-30} {2,-15:N2} {3,-15:N2}", grade.LastName, grade.Name,
                    grade.getFinalGradeWithAverage(), grade.getFinalGradeWithMedian());
            }
            Console.WriteLine();
        }

        public static List<FinalGradeWithList> getGradesFromFile()
        {
            List<FinalGradeWithList> grades = new List<FinalGradeWithList>();
            try
            {
                const string Path = "students.txt";
                using (StreamReader sr = new StreamReader(Path))
                {

                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        grades.Add(new FinalGradeWithList(line));
                    }
                    sr.Close();
                    Console.WriteLine("Import complete \n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("sad, {0}", e.Message);
            }
            return grades;
        }

    }
}
