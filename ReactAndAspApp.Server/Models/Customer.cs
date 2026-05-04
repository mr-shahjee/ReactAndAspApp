using System.ComponentModel.DataAnnotations;

namespace ReactAndAspApp.Server.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string? Description { get; set; }

        [Required, MaxLength(50)]
        public string Address { get; set; }

        [Required, MaxLength(50)]
        public string City { get; set; }

        [Required, MaxLength(2)]
        public string State { get; set; }

        [Required, MaxLength(10)]
        public string Zip { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        [Required]
        public int CustomerTypeId { get; set; }
        public CustomerType CustomerType { get; set; }
    }
}
