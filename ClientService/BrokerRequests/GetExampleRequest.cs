using Microsoft.AspNetCore.Mvc;

namespace BrokerRequests;

public class GetExampleRequest
{
    [FromQuery(Name = "param")]
    public int Param { get; set; }
}