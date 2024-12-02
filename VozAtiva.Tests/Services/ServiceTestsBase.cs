using AutoMapper;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Options;
using System;
using VozAtiva.Application.DTOs.Mappings;
using VozAtiva.Application.Services;
using VozAtiva.Domain.Interfaces;
using VozAtiva.Infrastructure.Context;
using VozAtiva.Infrastructure.Repository;

namespace VozAtiva.Tests.Services
{
    public class ServiceTestsBase<TService> : IDisposable where TService : class
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly AppDbContext _context;
        protected readonly TService _service;
        public ServiceTestsBase() 
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

            _context = new AppDbContext(dbContextOptions);

            DatabaseInitializer.Reinitialize(dbContextOptions);
            DatabaseInitializer.Initialize(dbContextOptions);

            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            _unitOfWork = new UnitOfWork(_context);
            _service = CreateService();
        }

        protected virtual TService CreateService()
        {
            return Activator.CreateInstance(typeof(TService), _unitOfWork, _mapper) as TService;
        }

        public void Dispose() 
        {
            _context.Dispose();
        }

        public void ReinitializeDatabase()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            DatabaseInitializer.Reinitialize(dbContextOptions);
            DatabaseInitializer.Initialize(dbContextOptions);

        }
    }
}

