using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBaseService
    {
        long GetCurrentUserId();
        string GetBasePath();
        HttpRequest GetRequest();
        string CreateToken(string name);
    }
}
