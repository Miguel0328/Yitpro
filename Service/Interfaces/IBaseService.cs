using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBaseService
    {
        long GetCurrentUserId();
        string CreateToken(string name);
    }
}
