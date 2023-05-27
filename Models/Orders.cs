using System.ComponentModel.DataAnnotations;

namespace BikeShopDSD605.Models
{
    public class Orders
    {

        [Key]
        public Guid OrderId { get; set; }

        [Display(Name = "Order Date")]

        public DateTime? OrderDate { get; set; }

        [Display(Name = "Shipped Date")]



        public DateTime? ShippedDate { get; set; }

        [Display(Name = "Customer Name")]

        public Guid CustomerId { get; set; }

        [Display(Name = "Stock")]
        public Guid StockId { get; set; }

        [Display(Name = "Staff")]
        public Guid StaffId { get; set; }

        public Customers? Customer { get; set; }
        public Stocks? Stock { get; set; }
        public Staffs? Staff { get; set; }
    }
}
