using Microsoft.AspNetCore.Mvc;

namespace ClientService.Models.Requests
{
    public record GetCustomerInfoRequest
    {
        [FromQuery(Name = "customerLogin")]
        public string Login { get; set; } = null!;
    }
}