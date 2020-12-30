using Persistence.Models;
using Resources.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDTO> Login(string name, string password);
        Task<ProfileDTO> CurrentUser();
    }
}
