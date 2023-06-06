using System.ComponentModel.DataAnnotations;

namespace BikeShopDSD605.Models
{
    public class Stocks
    {
        //public static string productName;
        //public static string productDescription;
        //public static string productType;
        //public static string price;
        //public static int quantity;

        public static Guid Id { get; set; }
        [Key]
        public Guid? StockId { get; set; }

        [Display(Name = "Product Name")]
        public string? ProductName { get; set; }

        [Display(Name = "Product Description")]
        public string? ProductDescription { get; set; }


        [Display(Name = "Product Type")]
        public string? ProductType { get; set; }

        [Display(Name = "Price")]
        public int? Price { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

    }
}
