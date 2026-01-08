namespace BMS.DAL.Models;
public  class Member: Entity
{
    
    
   

    public required string FullName { get; set; } 
    public required string Email { get; set; } 
    public required string PhoneNumber { get; set; }
    public DateTime MembershipDate { get; set; }
    public bool IsActive { get; set; }
}
