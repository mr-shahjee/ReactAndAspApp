using System.ComponentModel.DataAnnotations;

namespace ReactAndAspApp.Server.Models
{
    public class CustomerType
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Customer>? Customers { get; set; }
    }
}
