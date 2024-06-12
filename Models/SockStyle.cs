using Humanizer;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace GammaWear.Models
{
    public class SockStyle
    {
        public int Id { get; set; }
        
        [StringLength(100)]
        public required string Name { get; set; }
    }
}
