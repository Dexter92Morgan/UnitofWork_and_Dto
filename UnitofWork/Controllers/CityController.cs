using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datas.Interfaces;
using Datas.Repository;
using Datas.Models;
using Datas.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace UnitofWork.Controllers
{
    [Authorize]
    //[ApiController]
    public class CityController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }


        // [Route("api/City")]
        [HttpGet]
        //[AllowAnonymous] // to view all
        public async Task<IActionResult> GetCities()
        {
           // throw new UnauthorizedAccessException();

            var cities = await uow.CityRepository.GetCitiesAsync();

            var citiesDto = mapper.Map<IEnumerable<CityDto>>(cities);

            ////Manually Mapping for DTO Value
            //var citiesDto = from c in cities
            //                select new CityDto()
            //                {
            //                    Id = c.Id,
            //                    Name = c.Name


            //                };

            return Ok(citiesDto);
        }

        [Route("api/City/{id}")]
        [HttpGet]
        public IActionResult GetSingleCities(int id)
        {
            var cities =  uow.CityRepository.GetSingleCitiesAsync(id);

            return Ok(cities);
        }



        [Route("api/City/post")]
        [HttpPost]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            //// Manually Mappingfor DTO Value
            //var city = new City
            //{
            //    Name = cityDto.Name,
            //    LastUpdatedBy = 1,
            //    LastUpdatedOn = DateTime.Now

            //};

            // AutoMapper

            var city = mapper.Map<City>(cityDto);
            city.LastUpdatedBy = 1;
            city.LastUpdatedOn = DateTime.Now;
            ////////////////////////////////////////
            
            uow.CityRepository.AddCity(city);
            await uow.SaveAsync();
            return Ok("Added Successfully");
        }

        [Route("api/City/update/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCity(int id, CityDto cityDto)
        {
            // //error handling - wrong id in post table
            if(id != cityDto.Id)
                return BadRequest("Update Not Allowed - Id not forund in Tabel");

            var cityFromdb = await uow.CityRepository.FindCity(id);


            //error handling - wrong id in url
            if (cityFromdb == null)
                return BadRequest("Update Not Allowed - Id not forund");

            cityFromdb.LastUpdatedBy = 1;
            cityFromdb.LastUpdatedOn = DateTime.Now;
            mapper.Map(cityDto, cityFromdb);

            throw new Exception("Some unknown error occured");

            await uow.SaveAsync();
            return Ok("Updated Successfully");
            //return StatusCode(200);
        }

        // to update only Name by createing new CityUpdateDTo
        [Route("api/City/updateCityname/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCityName(int id, CityUpdateDto cityDto)
        {
            var cityFromdb = await uow.CityRepository.FindCity(id);
            cityFromdb.LastUpdatedBy = 1;
            cityFromdb.LastUpdatedOn = DateTime.Now;
            mapper.Map(cityDto, cityFromdb);
            await uow.SaveAsync();
            return Ok("Updated Successfully");
            //return StatusCode(200);
        }


        // Another Type Update

        //[Route("api/City/update")]
        //[HttpPut]
        //public async Task<IActionResult> UpdateCity(City city)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        uow.CityRepository.UpdateCity(city);
        //        await uow.SaveAsync();
        //        return Ok("Updated Successfully");
        //    }
        //    return BadRequest();

        //}

        [Route("api/City/delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCity(int id)
        {
            uow.CityRepository.DeleteCity(id);
            await uow.SaveAsync();
            return Ok("Deleted Id: "+ id);
        }


        // Post api/city/add?cityname=Miami
        // Post api/city/add/Los Angeles
        // [HttpPost("add")]
        // [HttpPost("add/{cityname}")]
        // public async Task<IActionResult> AddCity(string cityName)
        // {
        //     City city = new City();
        //     city.Name = cityName;
        //     await dc.Cities.AddAsync(city);
        //     await dc.SaveChangesAsync();
        //     return Ok(city);
        // }
    }
}
