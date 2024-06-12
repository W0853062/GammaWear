using System.ComponentModel.DataAnnotations;

namespace GammaWear.Models
{
    public class Season
    {
        public int Id { get; set; }
        
        [StringLength(100)]
        public required string Name { get; set; }
    }

}
