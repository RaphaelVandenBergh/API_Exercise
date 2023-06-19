using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Exercise.Infrastructure.Data
{
    public class DapperContext
    {
        private readonly IConfiguration configuration;

        public DapperContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=API_Exercise;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    }
}
