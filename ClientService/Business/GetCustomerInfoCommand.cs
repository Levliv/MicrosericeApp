using System.Linq;
using ClientService.Business.Interfaces;
using ClientService.EF.Data.Interfaces;
using ClientService.EF.DbModels;
using ClientService.Mappers.Interfaces;
using ClientService.Models.Requests;
using ClientService.Models.Responses;
using ClientService.Validation.Interfaces;
using FluentValidation.Results;

namespace ClientService.Business
{
    public class GetCustomerInfoCommand : IGetCustomerInfoCommand
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IGetCustomerInfoMapper _getCustomerInfoMapper;
        private readonly IGetCustomerInfoRequestValidator _getCustomerInfoRequestValidator;
        private readonly IDbOrderToGetOrderResponseMapper _dbOrderToGetOrderResponse;
        private readonly IDbBakedGoodToGetBakedGoodResponseMapper _dbBakedGoodToGetBakedGoodResponse;

        public GetCustomerInfoCommand(
            ICustomerRepository customerRepository,
            IGetCustomerInfoRequestValidator getCustomerInfoRequestValidator,
            IGetCustomerInfoMapper getCustomerInfoMapper,
            IDbOrderToGetOrderResponseMapper dbOrderToGetOrderResponse,
            IDbBakedGoodToGetBakedGoodResponseMapper dbBakedGoodToGetBakedGoodResponse)
        {
            _customerRepository = customerRepository;
            _getCustomerInfoRequestValidator = getCustomerInfoRequestValidator;
            _getCustomerInfoMapper = getCustomerInfoMapper;
            _dbOrderToGetOrderResponse = dbOrderToGetOrderResponse;
            _dbBakedGoodToGetBakedGoodResponse = dbBakedGoodToGetBakedGoodResponse;
        }

        public GetCustomerInfoResponse Execute(GetCustomerInfoRequest getCustomerInfoRequest)
        {
            ValidationResult validationResult = _getCustomerInfoRequestValidator.Validate(getCustomerInfoRequest);
            if (!validationResult.IsValid)
                return new GetCustomerInfoResponse
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            
            DbCustomer dbCustomer = _customerRepository.Read(getCustomerInfoRequest.Login);
            return _getCustomerInfoMapper
                .Map(dbCustomer, 
                    dbCustomer.Orders.Select(dbOrder 
                        => _dbOrderToGetOrderResponse.Map(dbOrder, dbOrder.BakedGoodOrders.Select(dbBakedGoodOrder => 
                            _dbBakedGoodToGetBakedGoodResponse.Map(dbBakedGoodOrder, dbBakedGoodOrder.BakedGood)))));
        }
    }
}