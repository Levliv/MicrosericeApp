using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokerRequests;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientInteractionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet("Get")]
        public GetExampleResponse Get(
            [FromServices] IRequestClient<GetExampleRequest> requestClient,
            [FromQuery] GetExampleRequest getExampleRequest)
        {
            Task<Response<GetExampleResponse>> response = requestClient.GetResponse<GetExampleResponse>(getExampleRequest);
            return response.Result.Message;
        }
    }
}