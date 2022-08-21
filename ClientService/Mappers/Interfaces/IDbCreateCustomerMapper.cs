using ClientService.EF.DbModels;
using ClientService.Models.Requests;

namespace ClientService.Mappers.Interfaces
{
    public interface IDbCreateCustomerMapper
    {
        DbCustomer Map(CreateCustomerRequest customerRequest);
    }
}