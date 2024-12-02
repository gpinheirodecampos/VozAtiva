using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VozAtiva.Application.DTOs;

namespace VozAtiva.Tests.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PublicAgentServiceTest test = new PublicAgentServiceTest();
            test.Verify_Unit_Of_Work_And_Mapper();
            test.Add_Agent_OK();
        }
    }
}
