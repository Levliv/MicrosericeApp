using ClientService.Business.Interfaces;
using ClientService.EF.Data;
using ClientService.EF.Data.Interfaces;
using ClientService.EF.DbModels;
using ClientService.Mappers;
using ClientService.Models.Requests;
using ClientService.Models.Responses;

namespace ClientService.Business
{
    public class GetCustomerInfoCommand : IGetCustomerInfoCommand
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IGetCustomerInfoMapper _getCustomerInfoMapper;

        public GetCustomerInfoCommand(
            ICustomerRepository customerRepository,
            IGetCustomerInfoMapper getCustomerInfoMapper)
        {
            _customerRepository = customerRepository;
            _getCustomerInfoMapper = getCustomerInfoMapper;
        }

        public GetCustomerInfoResponse Execute(GetCustomerInfoRequest getCustomerInfoRequest)
        {
            DbCustomer? t = _customerRepository.Read(getCustomerInfoRequest.Login);
            if (t is null)
            {
                return new GetCustomerInfoResponse
                {
                    IsSuccess = false
                };
            }
            
            return _getCustomerInfoMapper.Map(t);
        }
    }
}