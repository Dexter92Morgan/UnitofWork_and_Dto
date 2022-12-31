using AutoMapper;
using Datas.Dtos;
using Datas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datas.Helpers_Automapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<City, CityDto>().ReverseMap();
            //or
            // for add , reverser mapping 
            //CreateMap<CityDto, City>();


            CreateMap<City, CityUpdateDto>().ReverseMap();

        }
    }
}

//reference Link
//https://www.youtube.com/watch?v=1wppuHZ1fpc&list=PL_NVFNExoAxclqXo9fLAeP0G2Qp56Fu8C&index=37