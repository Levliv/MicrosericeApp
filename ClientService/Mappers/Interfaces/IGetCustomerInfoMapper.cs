using ClientService.EF.DbModels;
using ClientService.Models.Responses;

namespace ClientService.Mappers
{
    public interface IGetCustomerInfoMapper
    {
        GetCustomerInfoResponse Map(DbCustomer dbCustomer);
    }
}