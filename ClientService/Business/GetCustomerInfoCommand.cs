using System;
using System.Collections.Generic;
using System.Linq;
using ClientService.Business.Interfaces;
using ClientService.EF.Data.Interfaces;
using ClientService.EF.DbModels;
using ClientService.Mappers;
using ClientService.Mappers.Interfaces;
using ClientService.Models.Requests;
using ClientService.Models.Responses;
using ClientService.Validation;
using ClientService.Validation.Interfaces;
using FluentValidation.Results;

namespace ClientService.Business
{
    public class GetCustomerInfoCommand : IGetCustomerInfoCommand
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IGetCustomerInfoMapper _getCustomerInfoMapper;
        private readonly IGetCustomerInfoRequestValidator _getCustomerInfoRequestValidator;
        private readonly IDbOrderToGetOrderResponse _dbOrderToGetOrderResponse;
        private readonly IDbBakedGoodToGetBakedGoodResponse _dbBakedGoodToGetBakedGoodResponse;

        public GetCustomerInfoCommand(
            ICustomerRepository customerRepository,
            IGetCustomerInfoRequestValidator getCustomerInfoRequestValidator,
            IGetCustomerInfoMapper getCustomerInfoMapper,
            IDbOrderToGetOrderResponse dbOrderToGetOrderResponse,
            IDbBakedGoodToGetBakedGoodResponse dbBakedGoodToGetBakedGoodResponse)
        {
            _customerRepository = customerRepository;
            _getCustomerInfoRequestValidator = getCustomerInfoRequestValidator;
            _getCustomerInfoMapper = getCustomerInfoMapper;
            _dbOrderToGetOrderResponse = dbOrderToGetOrderResponse;
            _dbBakedGoodToGetBakedGoodResponse = dbBakedGoodToGetBakedGoodResponse;
        }

        public GetCustomerInfoResponse Execute(GetCustomerInfoRequest getCustomerInfoRequest)
        {
            ValidationResult? validationResult = _getCustomerInfoRequestValidator.Validate(getCustomerInfoRequest);
            if (validationResult.IsValid)
            {
                /*
                Tuple<DbCustomer, List<Tuple<DbOrder, List<Tuple<DbBakedGood, DbBakedGoodOrder>>>>>? data= _customerRepository.Read(getCustomerInfoRequest.Login);
                if (data is null)
                {
                    return new GetCustomerInfoResponse
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Customer was not found" }
                    };
                }
                
                (DbCustomer dbCustomer, List<Tuple<DbOrder, List<Tuple<DbBakedGood, DbBakedGoodOrder>>>> orderData) = data;
                if (!dbCustomer.IsActive)
                    return new GetCustomerInfoResponse
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Customer is not active" }
                    };
                List<GetOrderResponse> getOrderResponses = new();
                foreach (Tuple<DbOrder, List<Tuple<DbBakedGood, DbBakedGoodOrder>>> orderDataElement in orderData)
                {
                    DbOrder dbOrder = orderDataElement.Item1;
                    List<Tuple<DbBakedGood, DbBakedGoodOrder>> ordersList = orderDataElement.Item2;
                    List<GetBakedGoodResponse> getBakedGoodResponses = new List<GetBakedGoodResponse>();
                    foreach (Tuple<DbBakedGood, DbBakedGoodOrder> ordersListElement in ordersList)
                    {
                        DbBakedGood t = ordersListElement.Item1;
                        DbBakedGoodOrder k = ordersListElement.Item2;
                        GetBakedGoodResponse getBakedGoodResponse = _dbBakedGoodToGetBakedGoodResponse.Map(k, t);
                        getBakedGoodResponses.Add(getBakedGoodResponse);
                    }

                    GetOrderResponse m = _dbOrderToGetOrderResponse.Map(dbOrder, getBakedGoodResponses);
                    getOrderResponses.Add(m);
                }

                return _getCustomerInfoMapper.Map(dbCustomer, getOrderResponses);
                */
                DbCustomer? k = _customerRepository.Read2(getCustomerInfoRequest.Login);
                return null;
            }

            return new GetCustomerInfoResponse
            {
                IsSuccess = false,
                Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
            };
        }
    }
}