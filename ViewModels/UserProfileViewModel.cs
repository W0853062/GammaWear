using System.ComponentModel.DataAnnotations;

namespace GammaWear.ViewModels
{
    public class UserProfileViewModel
    {
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        // Add other profile properties as needed
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
