
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace studentConsoleManager.Models;

public class Student
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("schoolId")]
    [Required(ErrorMessage = "School ID is required")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "School ID must be between 1 and 20 characters")]
    public string? SchoolId { get; set; }

    [JsonPropertyName("name")]
    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 50 characters")]
    public string? Name { get; set; }

    [JsonPropertyName("firstName")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    public string? FirstName { get; set; }

    [JsonPropertyName("className")]
    [StringLength(20, ErrorMessage = "Class name cannot exceed 20 characters")]
    public string? ClassName { get; set; }

    [JsonPropertyName("phone")]
    [Phone(ErrorMessage = "Invalid phone format")]
    [StringLength(15, ErrorMessage = "Phone cannot exceed 15 characters")]
    public string? Phone { get; set; }

    [JsonPropertyName("email")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }

    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Validates the student data
    /// </summary>
    public bool IsValid(out List<string> errors)
    {
        errors = new List<string>();

        if (string.IsNullOrWhiteSpace(SchoolId))
            errors.Add("School ID is required");

        if (string.IsNullOrWhiteSpace(Name))
            errors.Add("Last name is required");

        if (!string.IsNullOrEmpty(Email) && !Email.Contains("@"))
            errors.Add("Invalid email format");

        if (!string.IsNullOrEmpty(Phone) && Phone.Length < 8)
            errors.Add("Phone must be at least 8 characters");

        return errors.Count == 0;
    }

    public void UpdateTimestamp()
    {
        UpdatedAt = DateTime.Now;
    }

    public override string ToString()
    {
        return $@"|  ID           : {Id}
|  SchoolId     : {SchoolId} 
|  Name         : {Name} 
|  FirstName    : {FirstName} 
|  ClassName    : {ClassName} 
|  Phone        : {Phone} 
|  Email        : {Email}
|  Created      : {CreatedAt:yyyy-MM-dd HH:mm:ss}
|  Updated      : {UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? "Never"}";
    }

    public string ToJsonString()
    {
        return System.Text.Json.JsonSerializer.Serialize(this, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
    }
}
