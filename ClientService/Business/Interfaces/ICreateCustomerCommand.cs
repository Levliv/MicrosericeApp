using System.Threading.Tasks;
using ClientService.Models.Requests;
using ClientService.Models.Responses;

namespace ClientService.Business.Interfaces
{
    public interface ICreateCustomerCommand
    {
        Task<CreateCustomerResponse> Execute(CreateCustomerRequest request);
    }
}