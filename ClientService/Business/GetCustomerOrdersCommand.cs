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

namespace ClientService.Business
{
    public class GetCustomerOrdersCommand : IGetCustomerOrdersCommand
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IGetCustomerOrdersMapper _getCustomerOrdersMapper;
        private readonly IGetCustomerOrdersRequestValidator _getCustomerOrdersRequestValidator;
        private readonly IDbOrderToGetOrderResponseMapper _dbOrderToGetOrderResponseMapper;
        private readonly IDbBakedGoodToGetBakedGoodResponseMapper _dbBakedGoodToGetBakedGoodResponseMapper;

        public GetCustomerOrdersCommand(
            ICustomerRepository customerRepository,
            IGetCustomerOrdersRequestValidator getCustomerOrdersRequestValidator,
            IGetCustomerOrdersMapper getCustomerOrdersMapper,
            IDbOrderToGetOrderResponseMapper dbOrderToGetOrderResponseMapper,
            IDbBakedGoodToGetBakedGoodResponseMapper dbBakedGoodToGetBakedGoodResponseMapper)
        {
            _customerRepository = customerRepository;
            _getCustomerOrdersRequestValidator = getCustomerOrdersRequestValidator;
            _getCustomerOrdersMapper = getCustomerOrdersMapper;
            _dbOrderToGetOrderResponseMapper = dbOrderToGetOrderResponseMapper;
            _dbBakedGoodToGetBakedGoodResponseMapper = dbBakedGoodToGetBakedGoodResponseMapper;
        }

        public async Task<GetCustomerOrdersResponse> Execute(GetCustomerOrdersRequest getCustomerOrdersRequest)
        {
            ValidationResult validationResult = await _getCustomerOrdersRequestValidator.ValidateAsync(getCustomerOrdersRequest);
            if (!validationResult.IsValid)
                return new GetCustomerOrdersResponse
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            
            DbCustomer dbCustomer = await _customerRepository.ReadAsync(getCustomerOrdersRequest.Login);
            return _getCustomerOrdersMapper
                .Map(dbCustomer, 
                    dbCustomer.Orders.Select(dbOrder 
                        => _dbOrderToGetOrderResponseMapper.Map(dbOrder, dbOrder.BakedGoodOrders.Select(dbBakedGoodOrder => 
                            _dbBakedGoodToGetBakedGoodResponseMapper.Map(dbBakedGoodOrder, dbBakedGoodOrder.BakedGood)))));
        }
    }
}