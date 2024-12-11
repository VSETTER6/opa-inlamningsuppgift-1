namespace Domain.Models
{
    public class AuthorModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Category { get; set; }

        public AuthorModel() { }
    }
}
