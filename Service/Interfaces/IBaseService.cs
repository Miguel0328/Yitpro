using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBaseService
    {
        string GetCurrentUsername();
        Task<bool> View(string controller, string view);
        string CreateToken(string name);
    }
}
