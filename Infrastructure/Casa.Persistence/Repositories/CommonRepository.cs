using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CLN.Persistence.Repositories
{
    //public class CommonRepository : GenericRepository<Common>, ICommonRepository
    //{
    //    public CommonRepository(ApplicationDbContext dbContext) : base(dbContext)
    //    {
    //    }

    //    public Task<List<Common>> CheckList()
    //    {
    //        var CommonEntity = new List<Common>();

    //        Common test = new Common()
    //        {
    //            Id = 1,
    //            FisrtName = "Sujay",
    //            LastName = "Jena",
    //        };
    //        Common test1 = new Common()
    //        {
    //            Id = 1,
    //            FisrtName = "Jhumi",
    //            LastName = "Patra",
    //        };
    //        CommonEntity.Add(test);
    //        CommonEntity.Add(test1);

    //        return Task.FromResult(CommonEntity);
    //    }

    //    public Task<Common> CheckName(Common entity)
    //    {
    //        var CommonEntity = new Common();
    //        CommonEntity.FisrtName = entity.FisrtName;
    //        CommonEntity.LastName = entity.LastName;
    //        CommonEntity.Fullname = entity.FisrtName + "---" + entity.LastName;
    //        return Task.FromResult(CommonEntity);
    //    }
    //}
}
