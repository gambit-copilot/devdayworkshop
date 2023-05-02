namespace TechItEasyBackend.Requests
{
    public class CreateCustomerRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Email { get; set; }
    }
}
