using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientService.Business.Interfaces;
using ClientService.EF.Data.Interfaces;
using ClientService.EF.DbModels;
using ClientService.Mappers.Interfaces;
using ClientService.Models.Requests;
using ClientService.Models.Responses;
using ClientService.Validation.Interfaces;
using FluentValidation.Results;

namespace ClientService.Business;

public class UpdateCustomerPersonalInfoCommand : IUpdateCustomerPersonalInfoCommand
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDbCreateCustomerMapper _createCustomerMapper;
    private readonly IDbCustomerToEditCustomerPersonalInfoResponse _editCustomerPersonalInfoResponse;
    private readonly ICreateCustomerRequestValidator _createCustomerRequestValidator;
    public UpdateCustomerPersonalInfoCommand(
        ICustomerRepository customerRepository,
        IDbCreateCustomerMapper createCustomerMapper,
        IDbCustomerToEditCustomerPersonalInfoResponse editCustomerPersonalInfoResponse
        //ICreateCustomerRequestValidator createCustomerRequestValidator
        )
    {
        _customerRepository = customerRepository;
        _createCustomerMapper = createCustomerMapper;
        _editCustomerPersonalInfoResponse = editCustomerPersonalInfoResponse;
        //_createCustomerRequestValidator = createCustomerRequestValidator;
    }

    public async Task<EditCustomerPersonalInfoResponse> ExecuteAsync(EditCustomerPersonalInfoRequest request, CreateCustomerRequest customerNewInfo)
    {
        //ValidationResult validationResult = await _createCustomerRequestValidator.ValidateAsync(customerNewInfo);
        ValidationResult validationResult = new ValidationResult();
        if (!validationResult.IsValid)
        {
            return new EditCustomerPersonalInfoResponse
            {
                IsSuccess = false,
                Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList(),
                Id = default,
                Login = null,
                FirstName = null,
                SecondName = null,
                Email = null
            };
        }
        DbCustomer dbCustomer = await _customerRepository.UpdateAsync(request, _createCustomerMapper.Map(customerNewInfo));
        if (dbCustomer is null)
        {
            return new EditCustomerPersonalInfoResponse
            {
                IsSuccess = false,
                Errors = new List<string>() { "Couldn't edit that customer" }
            };
        }

        return _editCustomerPersonalInfoResponse.Map(dbCustomer);
    }
}