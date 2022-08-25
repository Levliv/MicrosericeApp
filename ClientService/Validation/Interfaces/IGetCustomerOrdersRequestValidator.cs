using ClientService.Models.Requests;
using FluentValidation;

namespace ClientService.Validation.Interfaces
{
    public interface IGetCustomerOrdersRequestValidator : IValidator<GetCustomerOrdersRequest>
    {
    }
}