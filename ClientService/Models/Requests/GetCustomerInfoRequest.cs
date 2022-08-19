namespace ClientService.Models.Requests
{
    public record GetCustomerInfoRequest
    {
        public string Login { get; set; } = null!;
    }
}