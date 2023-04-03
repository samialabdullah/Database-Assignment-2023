using System.ComponentModel.DataAnnotations;

namespace CaseManagementSystem.Models.Entities
{


    internal class AddressEntity
    {
        [Key]
        public int Id { get; set; }
        public string StreetName { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;  
        public string City { get; set; } = string.Empty;

        public ICollection<CustomerEntity> Customers = new HashSet<CustomerEntity>();
    }
}
