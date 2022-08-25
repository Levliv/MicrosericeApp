using System;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.Models.Requests;

public record EditCustomerPersonalInfoRequest()
{
    [FromQuery(Name = "customerId")]
    public Guid CustomerId { get; set; }
}