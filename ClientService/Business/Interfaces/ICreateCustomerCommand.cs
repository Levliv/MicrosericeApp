using ClientService.Models.Requests;
using ClientService.Models.Responses;

namespace ClientService.Business.Interfaces
{
    public interface ICreateCustomerCommand
    {
        CreateCustomerResponse Execute(CreateCustomerRequest request);
    }
}