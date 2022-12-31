using System;
using System.Collections.Generic;
using System.Text;
using Datas.Interfaces;
using Datas.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Datas.Datacontext;
using System.Linq;


namespace Datas.Repository
{
    public class StateRepository : IStateRepository
    {
        private readonly DataContext dc;

        public StateRepository(DataContext dc)
        {
            this.dc = dc;
        }



        public async Task<IEnumerable<State>> GetStateAsync()
        {
            return await dc.States.ToListAsync();
        }

        public State GetSingleStateAsync(int id)
        {
            return dc.States.FirstOrDefault(t => t.Id == id);
        }

        public void AddState(State state)
        {
            dc.States.Add(state);
        }


        public void UpdateState(State state)
        {
            dc.States.Update(state);
        }

        public void DeleteState(int stateId)
        {

            var state = dc.States.Find(stateId);
            dc.States.Remove(state);

        }


    }
}
