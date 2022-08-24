using System;
using System.Collections.Generic;

namespace ClientService.Models.Responses
{
    public record GetOrderResponse
    {
        public DateTime OrderTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public IEnumerable<GetBakedGoodResponse> BakedGoods { get; set; }
    }
}