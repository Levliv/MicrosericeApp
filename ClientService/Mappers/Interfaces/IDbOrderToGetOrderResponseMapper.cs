using System.Collections.Generic;
using ClientService.EF.DbModels;
using ClientService.Models.Responses;

namespace ClientService.Mappers.Interfaces
{
    public interface IDbOrderToGetOrderResponseMapper
    {
        GetOrderResponse Map(DbOrder dbOrder, IEnumerable<GetBakedGoodResponse> orderBakedGoods);
    }
}