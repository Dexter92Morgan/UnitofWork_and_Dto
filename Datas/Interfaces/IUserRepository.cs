using Datas.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authentication(string userName, string password);
    }
}
