using System;

namespace Domain.Entities.Customer
{
    public class Customer : BaseEntity<Guid>
    {
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RecommenderPersonalNumber { get; set; }
    }
}