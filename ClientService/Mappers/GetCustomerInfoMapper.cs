using ClientService.EF.DbModels;
using ClientService.Models.Responses;

namespace ClientService.Mappers
{
    public class GetCustomerInfoMapper : IGetCustomerInfoMapper
    {
        public GetCustomerInfoResponse Map(DbCustomer dbCustomer)
        {
            return new GetCustomerInfoResponse
            {
                Id = dbCustomer.Id,
                Login = dbCustomer.Login,
                FirstName = dbCustomer.FirstName,
                SecondName = dbCustomer.SecondName,
                Email = dbCustomer.Email
            };
        }
    }
}