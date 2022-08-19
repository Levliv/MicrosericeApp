using System;
using System.Collections.Generic;

namespace ClientService.Models.Responses
{
    public record CreateCustomerResponse()
    {
        public Guid? Id { get; set; }
        public bool IsSuccess { get; set; }
        public List<string>? Errors { get; set; }

    }
}