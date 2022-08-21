using System;
using System.Collections.Generic;

namespace ClientService.Models.Responses
{
    public record GetCustomerInfoResponse()
    {
        public bool IsSuccess { get; set; } = true;
        public List<string>? Errors { get; set; }
        public Guid? Id { get; set; }
        public string? Login { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public string? SecondName { get; set; }
        public string? Email { get; set; }
        public List<GetOrderResponse> Orders { get; set; }
    };
}