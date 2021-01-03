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
        Task<List<OptionDTO>> GetManagers();
        Task<List<OptionDTO>> GetClients();
        Task<List<OptionDTO>> GetCatalogs();
    }
}
