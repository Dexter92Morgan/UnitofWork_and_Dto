using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }

        IStateRepository StateRepository { get; }

        IUserRepository UserRepository { get; }

        Task<bool> SaveAsync();
    }
}
