using System.ComponentModel.DataAnnotations;

namespace BikeShopDSD605.Models
{
    public class Staffs
    {


        [Key]
        public Guid StaffId { get; set; }

        public string? Name { get; set; }
        public int? Phone { get; set; }
        public string? Email { get; set; }

        public string? Role { get; set; }
    }
}
