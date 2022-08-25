using ClientService.EF.DbModels;
using ClientService.Models.Responses;

namespace ClientService.Mappers.Interfaces;

public interface IDbCustomerToEditCustomerPersonalInfoResponse
{
    EditCustomerPersonalInfoResponse Map(DbCustomer customer);
}