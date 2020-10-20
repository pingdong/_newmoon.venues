using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace PingDong.Testings.Helper
{
    public class MapperHelper
    {
        public static IMapper Build(Assembly assembly)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.AddProfiles(
                    assembly
                        .GetTypes()
                        .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Profile)))
                        .Select(t => (Profile)Activator.CreateInstance(t))
                    )
            );

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}
