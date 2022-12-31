using System;
using System.Collections.Generic;
using System.Text;
using Datas.Models;
using Datas.Repository;
using Datas.Interfaces;
using System.Threading.Tasks;

namespace Datas.Datacontext
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dc;

        public UnitOfWork(DataContext dc)
        {
            this.dc = dc;
        }

        public ICityRepository CityRepository => new CityRepository(dc);

        public IStateRepository StateRepository => new StateRepository(dc);

        public IUserRepository UserRepository => new UserRepository(dc);

        public async Task<bool> SaveAsync()
        {
            return await dc.SaveChangesAsync() > 0;
        }
    }
}
