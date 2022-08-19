using System;
using System.Net;
using ClientService.Business.Interfaces;
using ClientService.EF.Data;
using ClientService.EF.DbModels;
using ClientService.Models.Requests;
using ClientService.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientMainController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientMainController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpPost("CreateCustomer")]
        public CreateCustomerResponse CreateNewCustomer(
            [FromServices] ICreateCustomerCommand command,
            [FromBody] CreateCustomerRequest request)
        {
            return command.Execute(request);
        }
        
        [HttpGet("GetCustomerInfo")]
        public GetCustomerInfoResponse GetCustomerPersonalInfo(
            [FromServices] IGetCustomerInfoCommand command,
            [FromQuery] string customerLogin)
        {
            return command.Execute(new GetCustomerInfoRequest
            {
                Login = customerLogin
            });
        }

        [HttpPut("UpdateUserInfo")]
        public Guid? UpdateClientInfo(
            [FromQuery] Guid customerIdToEdit,
            [FromBody] DbCustomer newCustomerInfo)
        {
            CustomerRepository t = new (_context);
            Guid? updatedCustomerId =  t.Update(customerIdToEdit, newCustomerInfo);
            if (updatedCustomerId.HasValue)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            }

            return updatedCustomerId;
        }
        
        [HttpDelete("DeleteCustomer")]
        public Guid? DeleteCustomer(
            [FromQuery] Guid id)
        {
            CustomerRepository t = new (_context);
            Guid? customerId = t.Delete(id);
            if (customerId .HasValue)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                return customerId;
            }

            HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return null;
        }
    }
}
