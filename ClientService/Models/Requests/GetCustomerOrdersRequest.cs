using Microsoft.AspNetCore.Mvc;

namespace ClientService.Models.Requests
{
    public record GetCustomerOrdersRequest
    {
        [FromQuery(Name = "customerLogin")]
        public string Login { get; set; } = null!;
    }
}