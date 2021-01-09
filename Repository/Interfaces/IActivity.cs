using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IActivity
    {
        Task<long> Post(ActivityModel _activity);
        Task<List<ActivityModel>> Get(long userId);
        Task<ActivityModel> GetDetail(long id);
    }
}
