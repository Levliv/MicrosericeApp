using System.Collections.Generic;
using System.Linq;
using ClientService.EF.Data;
using ClientService.EF.DbModels;
using ClientService.Mappers.Interfaces;
using ClientService.Models.Responses;

namespace ClientService.Mappers
{
    public class DbOrderToGetOrderResponseMapper : IDbOrderToGetOrderResponseMapper
    {
        public GetOrderResponse Map(DbOrder dbOrder, IEnumerable<GetBakedGoodResponse> orderBakedGoods)
        {
            return new GetOrderResponse
            {
                OrderTime = dbOrder.OrderTime,
                DeliveryTime = dbOrder.DeliveryTime,
                BakedGoods = orderBakedGoods
            };
        }
    }
}