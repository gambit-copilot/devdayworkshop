namespace TechItEasyBackend.Dtos
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static Customer FromModel(Persistence.Models.Customer model)
        {
            return new Customer
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth.ToString(Constants.DateFormat),
                Age = CalculateAge(model.DateOfBirth),
                Email = model.Email,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
            };
        }

        // There might be a bug in this function :)
        private static int CalculateAge(DateOnly dateOfBirth)
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Today);
            var age = currentDate.Year - dateOfBirth.Year;
            if (currentDate.Month < dateOfBirth.Month || currentDate.Day < dateOfBirth.Day)
            {
                age--;
            }
            return age;
        }
    }
}
