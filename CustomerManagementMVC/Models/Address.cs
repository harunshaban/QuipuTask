using System.ComponentModel.DataAnnotations;

namespace CustomerManagementMVC.Models
{
    public class Address
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string AddressType { get; set; } = null!;

        [Required(ErrorMessage = "AddressText is required")]
        public string AddressLine { get; set; } = null!;
    }
}
