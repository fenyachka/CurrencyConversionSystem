namespace Application.Customer.DTO
{
    public class CreateCustomerDto
    {
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RecommenderPersonalNumber { get; set; }
    }
}