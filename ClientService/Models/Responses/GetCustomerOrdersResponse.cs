using System;
using System.Collections.Generic;

namespace ClientService.Models.Responses
{
    public record GetCustomerOrdersResponse()
    {
        public bool IsSuccess { get; set; } = true;
        public List<string> Errors { get; set; }
        public Guid? Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public IEnumerable<GetOrderResponse> Orders { get; set; }
    };
}