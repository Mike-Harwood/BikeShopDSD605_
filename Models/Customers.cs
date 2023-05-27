using System.ComponentModel.DataAnnotations;

namespace BikeShopDSD605.Models
{
    public class Customers
    {


        [Key]
        public Guid CustomerId { get; set; }


        public string? Name { get; set; }

        public string? Address { get; set; }

        public int? Phone { get; set; }

        public string? Email { get; set; }
    }
}
