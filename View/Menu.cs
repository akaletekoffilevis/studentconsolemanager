using studentConsoleManager.Services;

namespace studentConsoleManager.View;

public class Menu(StudentService service)
{
    private readonly StudentService _service = service;

    public void Start()
    {
        string? choix;
        bool isRunning = true;
        Console.Clear();
        
        // Configuration de la console
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"
╔═══════════════════════════════════════════════════════╗
║                                                       ║
║        STUDENT MANAGEMENT CONSOLE SYSTEM              ║
║                                                       ║
╚═══════════════════════════════════════════════════════╝");
        Console.ResetColor();

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(@"
╔═══════════════════════════════════════════════════════╗
║                    📋 DASHBOARD                       ║
╠═══════════════════════════════════════════════════════╣
║                                                       ║
║     1. 📄 Display all students                        ║
║     2. 🔍 Search one student                          ║
║     3. ➕ Add a student                               ║
║     4. ✏️  Update one student                         ║
║     5. ❌ Delete one student                          ║
║     6. 🗑️  Delete all students (Danger)               ║
║     7. 💾 Save                                        ║
║     8. 🚪 Save and Exit                               ║
║     9. 🧹 Clear console                               ║
║                                                       ║
╚═══════════════════════════════════════════════════════╝"
                            );
                Console.ResetColor();
                Console.Write("  ➤ Choose an option: ");
                choix = Console.ReadLine();
                Console.WriteLine();
                
                switch (choix)
                {
                    case "1":
                        ShowHeader("📄 DISPLAY ALL STUDENTS");
                        ListAll();
                        break;
                    case "2":
                        ShowHeader("🔍 SEARCH ONE STUDENT");
                        ListOne();
                        break;
                    case "3":
                        ShowHeader("➕ ADD A STUDENT");
                        Add();
                        break;
                    case "4":
                        ShowHeader("✏️  UPDATE ONE STUDENT");
                        Update();
                        break;
                    case "5":
                        ShowHeader("❌ DELETE ONE STUDENT");
                        Delete();
                        break;
                    case "6":
                        ShowHeader("🗑️  DELETE ALL STUDENTS");
                        DeleteAll();
                        break;
                    case "7":
                        ShowHeader("💾 SAVE DATA");
                        Save();
                        break;
                    case "8":
                        ShowHeader("🚪 SAVE AND EXIT");
                        Save();
                        isRunning = false;
                        break;
                    case "9":
                        Console.Clear();
                        ShowSuccess("Console cleared successfully!");
                        break;
                    default:
                        ShowError("Please enter a valid option!");
                        break;
                }
           
             ShowFooter();
            } while (isRunning == true);
    }

    // Méthodes d'affichage du design
    private void ShowHeader(string title)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  {title.PadRight(51)} ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
        Console.ResetColor();
    }

    private void ShowFooter()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("═════════════════════════════════════════════════════════");
        Console.ResetColor();
    }

    private void ShowSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  ✓ {message}");
        Console.ResetColor();
    }

    private void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  ✗ {message}");
        Console.ResetColor();
    }

    private void ShowInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  ℹ {message}");
        Console.ResetColor();
    }

    private void Add()
    {
        Console.Write("  📝 SchoolId: ");
        var schoolId = Console.ReadLine();
        Console.Write("  📝 Name: ");
        var name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(schoolId) || string.IsNullOrWhiteSpace(name))
        {
            ShowError("SchoolId and Name are required!");
            return;
        }

        Console.Write("  📝 FirstName: ");
        var firstname = Console.ReadLine() ?? "";
        Console.Write("  📝 Class Name: ");
        var classname = Console.ReadLine() ?? "";
        Console.Write("  📝 Phone: ");
        var phone = Console.ReadLine() ?? "";
        Console.Write("  📝 Email: ");
        var email = Console.ReadLine() ?? "";

        var errors = new List<string>();
        if (_service.AddStudent(schoolId, name, firstname, classname, phone, email, errors))
        {
            ShowSuccess("Student added successfully!");
        }
        else
        {
            ShowError("Failed to add student:");
            foreach (var error in errors)
            {
                Console.WriteLine($"  • {error}");
            }
        }

    }

    private void ListAll()
    {
        if (_service.IsNull() == true)
        {
            ShowInfo("No students in register!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.White;
            int count = 0;
            foreach (var student in _service.GetAllStudents())
            {
                count++;
                Console.WriteLine($"\n  ┌─ Student #{count} " + new string('─', 40));
                Console.WriteLine($"  │ 🆔 SchoolId  : {student.SchoolId}");
                Console.WriteLine($"  │ 👤 Name      : {student.Name}");
                Console.WriteLine($"  │ 👤 FirstName : {student.FirstName}");
                Console.WriteLine($"  │ 📚 Class     : {student.ClassName}");
                Console.WriteLine($"  │ 📞 Phone     : {student.Phone}");
                Console.WriteLine($"  │ ✉️  Email     : {student.Email}");
                Console.WriteLine("  └" + new string('─', 52));
            }
            Console.ResetColor();
            ShowInfo($"Total: {count} student(s)");
        }
    }

    private void ListOne()
    {
        Console.Write("  🔍 Enter School ID: ");
        var id = Console.ReadLine() ?? "";
        
        var student = _service.GetBySchoolId(id);
        if (student != null)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n  ┌─ Student Details " + new string('─', 34));
            Console.WriteLine($"  │ 🆔 SchoolId  : {student.SchoolId}");
            Console.WriteLine($"  │ 👤 Name      : {student.Name}");
            Console.WriteLine($"  │ 👤 FirstName : {student.FirstName}");
            Console.WriteLine($"  │ 📚 Class     : {student.ClassName}");
            Console.WriteLine($"  │ 📞 Phone     : {student.Phone}");
            Console.WriteLine($"  │ ✉️  Email     : {student.Email}");
            Console.WriteLine("  └" + new string('─', 52));
            Console.ResetColor();
        }
        else
        {
            ShowError("Student doesn't exist!");
        }

    }
     
    private void Delete()
    {
        Console.Write("  ❌ School ID to delete: ");
        var schoolId = Console.ReadLine() ?? "";

        if (_service.DeleteStudent(schoolId))
        {
            ShowSuccess("Student deleted successfully!");
        }
        else
        {
            ShowError("Student doesn't exist!");
        }
    }

    private void Update()
    {
        Console.Write("  ✏️  School ID to update: ");
        var id = Console.ReadLine() ?? "";
        
        var student = _service.GetBySchoolId(id);
        if (student == null)
        {
            ShowError("Student doesn't exist!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n  ℹ️  Leave blank to keep current value");
            Console.ResetColor();
            
            Console.Write($"  📝 New School ID [{student.SchoolId}]: ");
            string? tmp = Console.ReadLine();
            student.SchoolId = (tmp != "" ? tmp : student.SchoolId);
            
            Console.Write($"  📝 New Name [{student.Name}]: ");
            tmp = Console.ReadLine();
            student.Name = (tmp != "" ? tmp : student.Name);
            
            Console.Write($"  📝 New FirstName [{student.FirstName}]: ");
            tmp = Console.ReadLine();
            student.FirstName = (tmp != "" ? tmp : student.FirstName);
            
            Console.Write($"  📝 New Class Name [{student.ClassName}]: ");
            tmp = Console.ReadLine();
            student.ClassName = (tmp != "" ? tmp : student.ClassName);
            
            Console.Write($"  📝 New Phone [{student.Phone}]: ");
            tmp = Console.ReadLine();
            student.Phone = (tmp != "" ? tmp : student.Phone);
            
            Console.Write($"  📝 New Email [{student.Email}]: ");
            tmp = Console.ReadLine();
            student.Email = (tmp != "" ? tmp : student.Email);
            
            var errors = new List<string>();
            if (_service.UpdateStudent(student, id, errors))
            {
                ShowSuccess("Student updated successfully!");
            }
            else
            {
                ShowError("Failed to update student:");
                foreach (var error in errors)
                {
                    Console.WriteLine($"  • {error}");
                }
            }
        }
    }

    public void DeleteAll()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n  ⚠️  WARNING: This action is IRREVERSIBLE!");
        Console.ResetColor();
        
        Console.Write("  🔐 Enter Secret Password: ");
        var passkey = Console.ReadLine() ?? "";
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n  ❓ Are you sure to delete ALL students?");
        Console.WriteLine("     Type 'yes' to confirm or 'no' to cancel");
        Console.ResetColor();
        
        Console.Write("  ➤ Your choice: ");
        string? ok = Console.ReadLine();
        
        bool success = false;
        if (ok is "y" or "yes") 
        { 
            success = _service.SecureDelete(passkey); 
        }
        
        if (success)
        {
            ShowSuccess("All students deleted!");
        }
        else
        {
            ShowError("Operation denied! Wrong password or cancelled.");
        }
    }

    public void Save()
    {
        _service.SSave();
        // ShowSuccess("Data saved successfully!");
    }
}

