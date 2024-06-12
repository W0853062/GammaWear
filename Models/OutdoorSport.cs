using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GammaWear.Models
{
    public class OutdoorSport
    {
        public int Id { get; set; }
        
        [StringLength(100)]
        public required string Name { get; set; }
    }

}
