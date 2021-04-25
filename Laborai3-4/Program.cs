using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            bool exit = false;
            List<FinalGradeWithList> grades = new List<FinalGradeWithList>();

            while (!exit)
            {
                int choice;
                Console.WriteLine(
                    "1. Įvesti mokinių rezultatus \n" +
                    "2. Pasiimti mokiniu pazymius is failo\n" +
                    "3. Atvaizduoti mokinių rezultatus.\n" +
                    "4. Filtruoti studentus pagal balus\n" +
                    "5. Išeiti.");
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
                        filterByGrades(grades);
                        break;
                    case 5:
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
                        try
                        {
                        grades.Add(gradeInput());
                        } catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 2:
                        grades.AddRange(generate());
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

        public static List<FinalGradeWithList> generate()
        {
            Random random = new Random();
            int choice;
            List<FinalGradeWithList> grades = new List<FinalGradeWithList>();
            Console.WriteLine(
                "1. Generuoti 10k studentų\n" +
                "2. Generuoti 100k studentų\n" +
                "3. Generuoti 1kk studentų\n" +
                "4. Generuoti 10kk studentų\n" +
                "5. Išeiti");
            Int32.TryParse(Console.ReadLine(), out choice);
            switch (choice)
            {
                case 1:
                    grades.AddRange(generateGrades(10000, "students1.txt", random));
                    break;
                case 2:
                    grades.AddRange(generateGrades(100000, "students2.txt", random));
                    break;
                case 3:
                    grades.AddRange(generateGrades(1000000, "students3.txt", random));
                    break;
                case 4:
                    grades.AddRange(generateGrades(10000000, "students4.txt", random));
                    break;
                case 5:
                    break;
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
            if (gradeArray.Length > 1)
            {
                foreach (string grade in gradeArray)
                {
                    int parsedGrade;
                    Int32.TryParse(grade, out parsedGrade);
                    if (parsedGrade == 0)
                    {
                        throw new Exception("Ivesti netinkami pazymiai, bandykite dar karta :)");
                    }
                    homeworkGrades.Add(parsedGrade);
                }
            }
            if(homeworkGrades == null || homeworkGrades.Count == 0 || exam == 0)
            {
                throw new Exception("Ivesti netinkami pazymiai, bandykite dar karta :)");
            }
            return new FinalGradeWithList(name, lastName, homeworkGrades, exam);

        }

        public static List<FinalGradeWithList> generateGrades(int numberOfStudents, string fileName, Random random)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<FinalGradeWithList> grades = new List<FinalGradeWithList>();
            for (int i = 0; i < numberOfStudents; i++)
            {

                string name = "Name" + i.ToString();
                string lastName = "Lastname" + i.ToString();
                List<int> homeworkGrades = new List<int>();
                int exam;

                for (int j = 0; j < 5; j++)
                {
                    homeworkGrades.Add(random.Next(1, 10));
                }
                exam = random.Next(1, 10);
                grades.Add(new FinalGradeWithList(name, lastName, homeworkGrades, exam));
            }
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Console.WriteLine("Sugeneruota {0} studentų", numberOfStudents);
            Console.WriteLine("Užtruko: {0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            saveToFile(grades, fileName);
            return grades;
        }

        public static void saveToFile(List<FinalGradeWithList> grades, string fileName)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (FinalGradeWithList a in grades)
                    {
                        writer.WriteLine(a.ToString());
                    }
                    Console.WriteLine("Studentai išsaugoti faile: {0}", fileName);
                    writer.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("sad write,{0}", e.Message);
            }
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Console.WriteLine("Saugojimas užtruko: {0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
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

        public static void filterByGrades(List<FinalGradeWithList> grades)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<FinalGradeWithList> passed = new List<FinalGradeWithList>();
            List<FinalGradeWithList> failed = new List<FinalGradeWithList>();
            foreach (FinalGradeWithList grade in grades)
            {
                if (grade.getFinalGradeWithAverage() >= 5.0d)
                {
                    passed.Add(grade);
                }
                else
                {
                    failed.Add(grade);
                }
            }
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Console.WriteLine("Isskaidymas i 2 sarasus uztruko: {0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("Išlaikė: {0}, neišlaikė: {1}", passed.Count, failed.Count);
            saveToFile(passed, "Passed.txt");
            saveToFile(failed, "Failed.txt");

        }
    }
}
