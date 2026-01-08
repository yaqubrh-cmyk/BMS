using BMS.DAL.Models;
namespace BMS.DAL.DataContext;

public class BMSDataBase
{
    public static List<Category> Categories { get; set; } = new();
    public static List<Member> Members { get; set; } = new();
    public static List<Book> Books { get; set; } = new();

    public BMSDataBase()
    {
    }
}
