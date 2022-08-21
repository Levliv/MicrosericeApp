using ClientService.EF.DbModels;
using ClientService.Models.Responses;

namespace ClientService.Mappers.Interfaces
{
    public interface IDbBakedGoodToGetBakedGoodResponse
    {
        GetBakedGoodResponse Map(DbBakedGoodOrder dbBakedGoodOrder, DbBakedGood dbBakedGood);
    }
}