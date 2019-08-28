using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.WebApp.Helpers
{
    public interface ISecurityTokenHelper
    {
        Task<string> GetAccessToken();
        Task<string> GetSessionToken();
    }
}
