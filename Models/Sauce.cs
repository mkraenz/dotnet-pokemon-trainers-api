using System.ComponentModel.DataAnnotations;

namespace DotnetTest.Models;

public class Sauce
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
}