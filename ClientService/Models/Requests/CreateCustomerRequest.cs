namespace ClientService.Models.Requests
{
    public record CreateCustomerRequest()
    {
        public string Login { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; }
        public string Email { get; set; }
    }
}