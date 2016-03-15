using A11_RBS.Domain;
using A11_RBS.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A11_RBS.App_Start
{
    public static class AutoMapperConfig
    {   
        public static void RegisteMappings()
        {
            Mapper.CreateMap<Leaves, LeavesModel>();
            Mapper.AssertConfigurationIsValid();
        }

    }
}