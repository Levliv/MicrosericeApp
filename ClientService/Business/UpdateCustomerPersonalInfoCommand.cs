using System.Collections.Generic;
using System.Threading.Tasks;
using ClientService.Business.Interfaces;
using ClientService.EF.Data.Interfaces;
using ClientService.EF.DbModels;
using ClientService.Mappers.Interfaces;
using ClientService.Models.Requests;
using ClientService.Models.Responses;

namespace ClientService.Business;

public class UpdateCustomerPersonalInfoCommand : IUpdateCustomerPersonalInfoCommand
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDbCreateCustomerMapper _createCustomerMapper;
    private readonly IDbCustomerToEditCustomerPersonalInfoResponse _editCustomerPersonalInfoResponse;
    
    public UpdateCustomerPersonalInfoCommand(
        ICustomerRepository customerRepository,
        IDbCreateCustomerMapper createCustomerMapper,
        IDbCustomerToEditCustomerPersonalInfoResponse editCustomerPersonalInfoResponse)
    {
        _customerRepository = customerRepository;
        _createCustomerMapper = createCustomerMapper;
        _editCustomerPersonalInfoResponse = editCustomerPersonalInfoResponse;
    }

    public async Task<EditCustomerPersonalInfoResponse> ExecuteAsync(EditCustomerPersonalInfoRequest request, CreateCustomerRequest customerNewInfo)
    {
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