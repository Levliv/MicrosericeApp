using ClientService.Models.Requests;
using FluentValidation;

namespace ClientService.Validation.Interfaces
{
    public interface ICreateCustomerRequestValidator : IValidator<CreateCustomerRequest>
    {
    }
}