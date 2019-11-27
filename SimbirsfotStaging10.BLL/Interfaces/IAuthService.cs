using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SimbirsfotStaging10.BLL.VK;

namespace SimbirsfotStaging10.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<IAuthServUserApi> Authorize(string code, HttpContext httpContext);
        string UrlGetCode { get; }
    }
}
