using System.Collections.Generic;
using ClientService.EF.DbModels;
using ClientService.Models.Responses;

namespace ClientService.Mappers.Interfaces
{
    public interface IDbOrderToGetOrderResponse
    {
        GetOrderResponse Map(DbOrder dbOrder, List<GetBakedGoodResponse> orderBakedGoods);
    }
}