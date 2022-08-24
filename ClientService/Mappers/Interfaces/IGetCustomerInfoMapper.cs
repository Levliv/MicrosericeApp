using System.Collections.Generic;
using ClientService.EF.DbModels;
using ClientService.Models.Responses;

namespace ClientService.Mappers.Interfaces
{
    public interface IGetCustomerInfoMapper
    {
        GetCustomerInfoResponse Map(DbCustomer dbCustomer, IEnumerable<GetOrderResponse> getOrderResponses);
    }
}