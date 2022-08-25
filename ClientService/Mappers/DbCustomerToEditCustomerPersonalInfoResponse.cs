using ClientService.EF.DbModels;
using ClientService.Mappers.Interfaces;
using ClientService.Models.Responses;

namespace ClientService.Mappers;

public class DbCustomerToEditCustomerPersonalInfoResponse : IDbCustomerToEditCustomerPersonalInfoResponse
{
    public EditCustomerPersonalInfoResponse Map(DbCustomer customer)
    {
        return new EditCustomerPersonalInfoResponse
        {
            Id = customer.Id,
            Login = customer.Login,
            Email = customer.Email,
            FirstName = customer.FirstName,
            SecondName = customer.SecondName
        };
    }
}