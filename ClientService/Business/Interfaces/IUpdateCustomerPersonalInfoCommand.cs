using System.Threading.Tasks;
using ClientService.Models.Requests;
using ClientService.Models.Responses;

namespace ClientService.Business.Interfaces;

public interface IUpdateCustomerPersonalInfoCommand
{
    Task<EditCustomerPersonalInfoResponse> ExecuteAsync(
        EditCustomerPersonalInfoRequest request,
        CreateCustomerRequest customerNewInfo);
}