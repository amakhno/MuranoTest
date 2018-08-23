using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using Entity;
using Interfaces.Repositories;
using Interfaces.Services;
using MuranoTest.Web.Filters;
using Services;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Mvc;
using MuranoTest.Web.ViewModels;
using Models;

namespace MuranoTest.Web
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(GetInitializer);
        }

        public static void GetInitializer(AutoMapper.IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<SearchConnector, SearchConnectorSearchViewModel>();
        }
    }
}
