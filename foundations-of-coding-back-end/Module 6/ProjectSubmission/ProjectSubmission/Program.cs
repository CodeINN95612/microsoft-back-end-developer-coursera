
using System.Text.Json;

StudentManagementSystem.Run();

record Student(string id, string name, List<decimal> Grades)
{
    public decimal AverageGrade => Grades.Count > 0 ? Grades.Average() : 0;
}

class StudentManagementSystem
{
    private readonly List<Student> students = new List<Student>();
    private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
    {
        WriteIndented = true,
    };
    public void AddStudent(string id, string name)
    {
        if (students.Any(s => s.id == id))
        {
            Console.WriteLine($"Student with ID {id} already exists.");
            return;
        }
        students.Add(new Student(id, name, new List<decimal>()));
        Console.WriteLine($"Student {name} added successfully.");
    }
    public void AddGrade(string id, decimal grade)
    {
        var student = students.FirstOrDefault(s => s.id == id);
        if (student == null)
        {
            Console.WriteLine($"Student with ID {id} not found.");
            return;
        }
        student.Grades.Add(grade);
        Console.WriteLine($"Grade {grade} added to student {student.name}.");
    }
    public void CalculateAverage()
    {
        foreach (var student in students)
        {
            Console.WriteLine($"Average grade for {student.name} is {student.AverageGrade}");
        }
    }
    public void DisplayStudents()
    {
        Console.WriteLine("Students:");
        Console.WriteLine(JsonSerializer.Serialize(students, jsonSerializerOptions));
    }

    public static void Run()
    {
        var system = new StudentManagementSystem();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Add Grade");
            Console.WriteLine("3. Calculate Average");
            Console.WriteLine("4. Display Students");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Enter student ID: ");
                    var id = Console.ReadLine();
                    Console.Write("Enter student name: ");

                    if (string.IsNullOrEmpty(id))
                    {
                        Console.WriteLine("ID cannot be empty.");
                        break;
                    }

                    var name = Console.ReadLine();

                    if (string.IsNullOrEmpty(name))
                    {
                        Console.WriteLine("Name cannot be empty.");
                        break;
                    }
                    system.AddStudent(id, name);
                    break;
                case "2":
                    Console.Write("Enter student ID: ");
                    id = Console.ReadLine();
                    Console.Write("Enter grade: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal grade) && !string.IsNullOrEmpty(id))
                    {
                        system.AddGrade(id, grade);
                    }
                    else
                    {
                        Console.WriteLine("Invalid grade.");
                    }
                    break;
                case "3":
                    system.CalculateAverage();
                    break;
                case "4":
                    system.DisplayStudents();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}