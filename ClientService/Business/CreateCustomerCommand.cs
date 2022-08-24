using System.Linq;
using ClientService.Business.Interfaces;
using ClientService.EF.Data.Interfaces;
using ClientService.Mappers.Interfaces;
using ClientService.Models.Requests;
using ClientService.Models.Responses;
using FluentValidation;
using FluentValidation.Results;

namespace ClientService.Business
{
    public class CreateCustomerCommand : ICreateCustomerCommand
    {
        private readonly IValidator<CreateCustomerRequest> _validator;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDbCreateCustomerMapper _dbCreateCustomerMapper;

        public CreateCustomerCommand(
            IValidator<CreateCustomerRequest> validator,
            ICustomerRepository customerRepository,
            IDbCreateCustomerMapper dbCreateCustomerMapper)
        {
            _validator = validator;
            _customerRepository = customerRepository;
            _dbCreateCustomerMapper = dbCreateCustomerMapper;
        }
        public CreateCustomerResponse Execute(CreateCustomerRequest request)
        {
            ValidationResult validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new CreateCustomerResponse
                {
                    Id = null,
                    IsSuccess = false,
                    Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            return new CreateCustomerResponse
            {
                Id = _customerRepository.Create(_dbCreateCustomerMapper.Map(request)),
                IsSuccess = true,
                Errors = default
            };
        }
    }
}