using ClientService.EF.DbModels;
using ClientService.Mappers.Interfaces;
using ClientService.Models.Responses;

namespace ClientService.Mappers
{
    public class DbBakedGoodToGetBakedGoodResponse : IDbBakedGoodToGetBakedGoodResponse
    {
        public GetBakedGoodResponse Map(DbBakedGoodOrder dbBakedGoodOrder,DbBakedGood dbBakedGood)
        {
            return new GetBakedGoodResponse
            {
                BakedGoodName = dbBakedGood.Name,
                BakedGoodWeight = dbBakedGoodOrder.ProductWeight
            };
        }
    }
}