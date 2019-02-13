using API_Conection_Layer.Model;
using AutoMapper;
using NASA_API_PHOTOSHOTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NASA_API_PHOTOSHOTS
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<ApodPicture, ApodPicutrePresented>();
        }
    }
}
