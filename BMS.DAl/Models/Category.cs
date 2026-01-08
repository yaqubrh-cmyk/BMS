
namespace BMS.DAL.Models;

public class Category : Entity
{
    public required string Name { get; set; } 
    public required string Description { get; set; }

    public List<Book> Books { get; set; } = new();


    
}

