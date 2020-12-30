using Resources.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IOptionService
    {
        Task<List<OptionDTO>> GetRoles();
        Task<List<OptionDTO>> GetLineManagers();
        Task<List<OptionDTO>> GetClients();
    }
}
