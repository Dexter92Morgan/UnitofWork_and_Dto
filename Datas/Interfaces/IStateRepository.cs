using Datas.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Interfaces
{
    public interface IStateRepository
    {
        Task<IEnumerable<State>> GetStateAsync();

        State GetSingleStateAsync(int id);

        void AddState(State state);
        void DeleteState(int stateId);
        void UpdateState(State state);
    }
}
