namespace BMS.DAL.Models;

public class Book: Entity
{
    

    public required string Title { get; set; }
    public required string Author { get; set; }
    public required string ISBN { get; set; }
    public  int PublishedYear { get; set; }
    public int CategoryId { get; set; }
    public bool IsAvailable { get; set; }

}
