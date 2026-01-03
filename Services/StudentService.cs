
using studentConsoleManager.Models;
using System.Text.Json.Serialization;

namespace studentConsoleManager.Services;

using System.Linq;
using System.Text.Json;

public class StudentService
{
    private readonly string secret = "my safe password";
    private readonly List<Student> _students = new();
    private const string DataFilePath = "studentsData.json";
    private int _nextID;
    private bool _isModified = false;

    public StudentService()
    {
        try
        {
            LoadStudents();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"⚠️  Error loading data: {ex.Message}");
            Console.ResetColor();
            _nextID = 1;
        }
    }

    public bool IsNull()
    {
        return (_students == null) || _students.Count == 0;
    }

    public bool AddStudent(string schoolId, string name, string firstname = "", string classname = "", string phone = "", string email = "", List<string>? validationErrors = null)
    {
        validationErrors ??= new List<string>();

        var student = new Student
        {
            Id = _nextID++,
            SchoolId = schoolId,
            Name = name,
            FirstName = firstname,
            ClassName = classname,
            Phone = phone,
            Email = email
        };

        if (!student.IsValid(out var errors))
        {
            _nextID--;
            validationErrors.AddRange(errors);
            return false;
        }

        _students.Add(student);
        _isModified = true;
        return true;
    }

    public bool UpdateStudent(Student std, string oldschoolid, List<string>? validationErrors = null)
    {
        validationErrors ??= new List<string>();

        if (!std.IsValid(out var errors))
        {
            validationErrors.AddRange(errors);
            return false;
        }

        var studentExit = GetBySchoolId(oldschoolid);
        if (studentExit != null)
        {
            studentExit.SchoolId = std.SchoolId;
            studentExit.Name = std.Name;
            studentExit.FirstName = std.FirstName;
            studentExit.ClassName = std.ClassName;
            studentExit.Phone = std.Phone;
            studentExit.Email = std.Email;
            studentExit.UpdateTimestamp();
            _isModified = true;
            return true;
        }
        validationErrors.Add("Student not found");
        return false;
    }

    public List<Student> GetAllStudents()
    {
        return _students;
    }

    public Student? GetById(int id) => _students.FirstOrDefault(s => s.Id == id);

    public Student? GetByName(string name) => _students.FirstOrDefault(s => s.Name == name);

    public Student? GetBySchoolId(string shoolid) => _students.FirstOrDefault(s => s.SchoolId == shoolid);

    public Student? GetByEmail(string mail) => _students.FirstOrDefault(s => s.Email == mail);

    public Student? GetByPhone(string tel) => _students.FirstOrDefault(s => s.Phone == tel);

    public bool DeleteStudent(string shoolid)
    {
        var studentExit = GetBySchoolId(shoolid);
        if (studentExit == null) return false;
        _students.Remove(studentExit);
        _isModified = true;
        return true;
    }

    public void SSave()
    {
        SaveStudents();
    }

    public bool SecureDelete(string password)
    {
        if (password == secret)
        {
            _students.Clear();
            _isModified = true;
            return true;
        }
        return false;
    }

    public bool IsModified()
    {
        return _isModified;
    }

    private void SaveStudents()
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(_students, options);
            File.WriteAllText(DataFilePath, json);
            _isModified = false;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ Data saved successfully!");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Error saving data: {ex.Message}");
            Console.ResetColor();
        }
    }

    private void LoadStudents()
    {
        // Create file if it doesn't exist
        if (!File.Exists(DataFilePath))
        {
            CreateEmptyDataFile();
            _nextID = 1;
            return;
        }

        try
        {
            var json = File.ReadAllText(DataFilePath);

            // Handle empty or invalid JSON
            if (string.IsNullOrWhiteSpace(json) || json.Trim() == "[]")
            {
                _nextID = 1;
                return;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var loadedStudents = JsonSerializer.Deserialize<List<Student>>(json, options) 
                ?? new List<Student>();
            
            _students.Clear();
            _students.AddRange(loadedStudents);
            
            // Calculate next ID based on existing students
            _nextID = _students.Count == 0 ? 1 : _students.Max(s => s.Id) + 1;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Loaded {_students.Count} student(s)");
            Console.ResetColor();
        }
        catch (JsonException ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠️  Invalid JSON file. Creating backup and new file: {ex.Message}");
            Console.ResetColor();
            
            // Create backup of corrupted file
            string backupPath = $"{DataFilePath}.backup";
            if (File.Exists(DataFilePath))
            {
                File.Copy(DataFilePath, backupPath, true);
            }
            
            CreateEmptyDataFile();
            _nextID = 1;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Error loading file: {ex.Message}");
            Console.ResetColor();
            _nextID = 1;
        }
    }

    private void CreateEmptyDataFile()
    {
        try
        {
            File.WriteAllText(DataFilePath, "[]");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("📝 New data file created");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Error creating data file: {ex.Message}");
            Console.ResetColor();
        }
    }
}