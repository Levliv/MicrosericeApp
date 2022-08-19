using ClientService.EF.DbModels;
using ClientService.Models.Requests;

namespace ClientService.Mappers
{
    public interface IDbCreateCustomerMapper
    {
        DbCustomer Map(CreateCustomerRequest customerRequest);
    }
}