using System;
using System.Net;
using System.Threading.Tasks;
using BrokerRequests;
using ClientService.Business.Interfaces;
using ClientService.EF.Data;
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
        public async Task<CreateCustomerResponse> CreateNewCustomer(
            [FromServices] ICreateCustomerCommand command,
            [FromBody] CreateCustomerRequest request)
        {
            CreateCustomerResponse createCustomerResponse = await command.Execute(request);
            if (!createCustomerResponse.IsSuccess)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            return createCustomerResponse;
        }
        
        [HttpGet("GetOrders")]
        public async Task<GetCustomerOrdersResponse> GetCustomerOrders(
            [FromServices] IGetCustomerOrdersCommand command,
            [FromQuery] GetCustomerOrdersRequest request)
        {
            GetCustomerOrdersResponse customerInfoResponse = await command.Execute(request);
            HttpContext.Response.StatusCode =
                customerInfoResponse.IsSuccess ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest;
            return customerInfoResponse;
        }

        [HttpPut("UpdatePersonalInfo")] 
        public async Task<EditCustomerPersonalInfoResponse> UpdateCustomerPersonalInfo(
            [FromServices] IUpdateCustomerPersonalInfoCommand command,
            [FromQuery] EditCustomerPersonalInfoRequest editCustomerRequest,
            [FromBody] CreateCustomerRequest newCustomerInfo)
        {
            return await command.ExecuteAsync(editCustomerRequest, newCustomerInfo);
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
