using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VozAtiva.Domain.Entities;
using VozAtiva.Domain.Entities.Types;
using VozAtiva.Infrastructure.Context;

namespace VozAtiva.Tests.Services;
public class DatabaseInitializer
{       
    public static void Reinitialize(DbContextOptions<AppDbContext> options)
    {
        var _context = new AppDbContext(options);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }
    public static void Initialize(DbContextOptions<AppDbContext> options) 
    {
        var _context = new AppDbContext(options);

        var pa1 = new PublicAgent
        {
            Id = 11,
            Name = "Camara Municipal",
            Email = "camara@mail.com",
            Phone = "12999999999",
            Acronym = "CM",
            AgentTypeId = 1
        };

        var pa2 = new PublicAgent
        {
            Id = 12,
            Name = "Instituto Previdência Servidor Municipal",
            Email = "ipsm@mail.com",
            Phone = "12988888888",
            Acronym = "IPSM",
            AgentTypeId = 1
        };


        _context.PublicAgents.Add(pa1);
        _context.PublicAgents.Add(pa2);

    }
}

