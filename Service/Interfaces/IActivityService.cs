using Resources.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IActivityService
    {
        Task<long> Post(ActivityDetailDTO _activity);
        Task<List<ActivityDTO>> Get();
        Task<ActivityDetailDTO> GetDetail(long id);
    }
}
