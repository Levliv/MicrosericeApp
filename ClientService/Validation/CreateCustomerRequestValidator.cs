using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClientService.EF.Data.Interfaces;
using ClientService.Models.Requests;
using ClientService.Validation.Interfaces;
using FluentValidation;

namespace ClientService.Validation
{
    public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>, ICreateCustomerRequestValidator
    {
        private readonly Regex _loginRegex = new("^[a-zA-Z0-9]*$");
        private readonly Regex _nameRegex = new("^[a-zA-Z]*$");
        
        public CreateCustomerRequestValidator(
            ICustomerRepository customerRepository)
        { 
            RuleFor(request =>request.Login)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Login can't be empty")
                .MaximumLength(50)
                .WithMessage("Login can contain only 50 symbols")
                .Matches(_loginRegex)
                .WithMessage("Login can contain only letters and numbers")
                .Must( login => customerRepository.DoesSameLoginExist(login))
                .WithMessage("This login already taken.");
                
            RuleFor(request => request.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("First name can't be empty")
                .MaximumLength(50)
                .WithMessage("First name can contain only 50 symbols")
                .Matches(_nameRegex)
                .WithMessage("First name can contain only letters");
            
            When(request => !string.IsNullOrEmpty(request.SecondName),
            () => RuleFor(request => request.SecondName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Second name can't be empty")
                .MaximumLength(50)
                .WithMessage("Second name can contain only 50 symbols")
                .Matches(_nameRegex)
                .WithMessage("Second name can contain only letters")
            );

            When(request => !string.IsNullOrEmpty(request.Email), 
            () => RuleFor(request => request.Email)
                .EmailAddress()
                .WithMessage("Email is incorrect")
            );
        }
    }
}