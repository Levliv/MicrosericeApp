using ClientService.EF.DbModels;
using ClientService.Models.Requests;

namespace ClientService.Mappers
{
    public interface IDbCustomerMapper
    {
        DbCustomer Map(CreateCustomerRequest customerRequest);
    }
}