using System.Threading.Tasks;
using ClientService.EF.Data;
using ClientService.EF.DbModels;
using ClientService.Models.Requests;
using ClientService.Models.Responses;

namespace ClientService.Business.Interfaces
{
    public interface IGetCustomerOrdersCommand
    {
        Task<GetCustomerOrdersResponse> Execute(GetCustomerOrdersRequest getCustomerOrdersRequest);
    }
}