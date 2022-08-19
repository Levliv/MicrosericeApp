using ClientService.EF.Data;
using ClientService.Models.Requests;
using ClientService.Models.Responses;

namespace ClientService.Business.Interfaces
{
    public interface IGetCustomerInfoCommand
    {
        GetCustomerInfoResponse Execute(GetCustomerInfoRequest getCustomerInfoRequest);
    }
}