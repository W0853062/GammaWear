using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using GammaWear.Attributes;


namespace GammaWear.Models
{
    public enum SockSize
    {
        [Display(Name = "Extra Small")]
        ExtraSmall,

        [Display(Name = "Small")]
        Small,

        [Display(Name = "Medium")]
        Medium,

        [Display(Name = "Large")]
        Large,

        [Display(Name = "Extra Large")]
        ExtraLarge,

        [Display(Name = "One Size Fits All")]
        OneSizeFitsAll
    }
    public enum ConsumerGroup
    {
        Male,
        Female,
        Kids,
        Unisex
    }

    public class Sock
    {
        public int Id { get; set; }

        [Required]
        public int MaterialId { get; set; }
        //public Material Material { get; set; }
        [Required]
        public int SockStyleId { get; set; }
        //public SockStyle SockStyle { get; set; }

        [Required] 
        public SockSize SockSize { get; set; } 

        [Required]
        public int OutdoorSportId { get; set; }
        //public OutdoorSport OutdoorSport { get; set; }
        
        public ConsumerGroup ConsumerGroup { get; set; }

        [Required] 
        public int SeasonId { get; set; }
        //public Season Season { get; set; }
        public int BrandId { get; set; }
        //public Brand Brand { get; set; }
        [Range(1, 1000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public required decimal Price { get; set; }

        // quantity in stock
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        [Display(Name = "In Stock")] 
        public int? Quantity { get; set; }

        public string ImageFile { get; set; } = "hs1gayk9.png";
        public string Description { get; set; } = "";

    }



}
