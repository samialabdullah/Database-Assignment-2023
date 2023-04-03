﻿namespace CaseManagementSystem.Models.Entities
{
    internal class CustomerEntity 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }

        public int AddressId { get; set; }


    }
}
