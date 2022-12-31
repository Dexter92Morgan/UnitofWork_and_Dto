using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datas.Interfaces;
using Datas.Repository;
using Datas.Models;

namespace UnitofWork.Controllers
{
    
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public StateController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [Route("api/State")]
        [HttpGet]
        public async Task<IActionResult> GetState()
        {
            var state = await uow.StateRepository.GetStateAsync();

            return Ok(state);
        }

        [Route("api/State/{id}")]
        [HttpGet]
        public IActionResult GetSingleState(int id)
        {
            var State = uow.StateRepository.GetSingleStateAsync(id);

            return Ok(State);
        }



        [Route("api/State/post")]
        [HttpPost]
        public async Task<IActionResult> AddState(State state)
        {
            uow.StateRepository.AddState(state);
            await uow.SaveAsync();
            return Ok("Added Successfully");
        }

        [Route("api/State/update")]
        [HttpPut]
        public async Task<IActionResult> UpdateState(State state)
        {
            if (ModelState.IsValid)
            {
                uow.StateRepository.UpdateState(state);
                await uow.SaveAsync();
                return Ok("Updated Successfully");
            }
            return BadRequest();

        }

        [Route("api/State/delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteState(int id)
        {
            uow.StateRepository.DeleteState(id);
            await uow.SaveAsync();
            return Ok("Deleted Id: " + id);
        }

    }
}
