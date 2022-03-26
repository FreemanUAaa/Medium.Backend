using AutoMapper;
using Castle.Core.Logging;
using Medium.Users.Api;
using Medium.Users.Application.Common.Mapper;
using Medium.Users.Application.Services;
using Medium.Users.Core.Interfaces;
using Medium.Users.Core.Interfaces.Mapper;
using Medium.Users.Core.Interfaces.Services;
using Medium.Users.Database;
using Medium.Users.Tests.Database;
using Microsoft.Extensions.Logging;
using Moq;

namespace Medium.Users.Tests.Tests.Base
{
    public abstract class BaseQueryTests<TLogger> where TLogger : class
    {
        public readonly IDatabaseContext Database;

        public readonly IFileManager FileManager;

        public readonly ILogger<TLogger> Logger;

        public readonly IMapper Mapper;

        public BaseQueryTests()
        {
            Database = DatabaseContextFactory.Create();

            FileManager = new FileManager(
                @"D:\sharp\Medium\Medium\Services\Users\Medium.Users.Tests\SavedPhotos\UserPhotos\",
                @"D:\sharp\Medium\Medium\Services\Users\Medium.Users.Tests\SavedPhotos\BioPhotos\"
            );

            MapperConfiguration configurationProvider = new MapperConfiguration(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(typeof(Startup).Assembly));
                config.AddProfile(new AssemblyMappingProfile(typeof(DatabaseContext).Assembly));
                config.AddProfile(new AssemblyMappingProfile(typeof(IMapWith<>).Assembly));
                config.AddProfile(new AssemblyMappingProfile(typeof(AssemblyMappingProfile).Assembly));
            });

            Mapper = configurationProvider.CreateMapper();

            Logger = new Mock<ILogger<TLogger>>().Object;
        }
    }
}
