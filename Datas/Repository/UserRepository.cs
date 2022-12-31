using Datas.Datacontext;
using Datas.Interfaces;
using Datas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dc;

        public UserRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public async Task<User> Authentication(string userName, string password)
        {
            return await dc.Users.FirstOrDefaultAsync(x => x.Username == userName && x.Password == password);
        }
    }
}
