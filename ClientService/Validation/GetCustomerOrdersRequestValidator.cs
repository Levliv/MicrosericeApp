using System.Text.RegularExpressions;
using ClientService.EF.Data.Interfaces;
using ClientService.Models.Requests;
using ClientService.Validation.Interfaces;
using FluentValidation;

namespace ClientService.Validation
{
    public class GetCustomerOrdersRequestValidator : AbstractValidator<GetCustomerOrdersRequest>,IGetCustomerOrdersRequestValidator
    {
        private readonly Regex _loginRegex = new("^[a-zA-Z0-9]*$");
        
        public GetCustomerOrdersRequestValidator(
            ICustomerRepository customerRepository)
        {
            RuleFor(request => request.Login)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Login can't be empty")
                .MaximumLength(50)
                .WithMessage("Login can contain only 50 symbols")
                .Matches(_loginRegex)
                .WithMessage("Login can contain only letters and numbers")
                .Must(login => customerRepository.DoesSameLoginExist(login))
                .WithMessage("This login wasn't found.");
        }
    }
}