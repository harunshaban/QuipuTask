using System.ComponentModel.DataAnnotations;

namespace CustomerManagementMVC.Models
{
    public class Customer
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "BirthDate is required")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
