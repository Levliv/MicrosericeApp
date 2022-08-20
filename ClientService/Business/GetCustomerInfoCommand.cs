using System.Collections.Generic;
using System.Linq;
using ClientService.Business.Interfaces;
using ClientService.EF.Data.Interfaces;
using ClientService.EF.DbModels;
using ClientService.Mappers;
using ClientService.Models.Requests;
using ClientService.Models.Responses;
using ClientService.Validation.Interfaces;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace ClientService.Business
{
    public class GetCustomerInfoCommand : IGetCustomerInfoCommand
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IGetCustomerInfoMapper _getCustomerInfoMapper;
        private readonly IGetCustomerInfoValidator _getCustomerInfoValidator;

        public GetCustomerInfoCommand(
            ICustomerRepository customerRepository,
            IGetCustomerInfoValidator getCustomerInfoValidator,
            IGetCustomerInfoMapper getCustomerInfoMapper)
        {
            _customerRepository = customerRepository;
            _getCustomerInfoValidator = getCustomerInfoValidator;
            _getCustomerInfoMapper = getCustomerInfoMapper;
        }

        public GetCustomerInfoResponse Execute(GetCustomerInfoRequest getCustomerInfoRequest)
        {
            ValidationResult? validationResult = _getCustomerInfoValidator.Validate(getCustomerInfoRequest);
            if (validationResult.IsValid)
            {
                DbCustomer? dbCustomer = _customerRepository.Read(getCustomerInfoRequest.Login);
                if (dbCustomer!.IsActive)
                {
                    return _getCustomerInfoMapper.Map(dbCustomer);
                }
                
                return new GetCustomerInfoResponse
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Customer is not active" }
                };
            }

            return new GetCustomerInfoResponse
            {
                IsSuccess = false,
                Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
            };
        }
    }
}