using ClientService.EF.DbModels;
using ClientService.Models.Responses;

namespace ClientService.Mappers.Interfaces
{
    public interface IDbBakedGoodToGetBakedGoodResponseMapper
    {
        GetBakedGoodResponse Map(DbBakedGoodOrder dbBakedGoodOrder, DbBakedGood dbBakedGood);
    }
}