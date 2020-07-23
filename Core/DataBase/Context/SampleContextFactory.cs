using System;
using System.Collections.Generic;
using System.Text;
using Core.Source.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Core.DataBase.Context
{

    class SampleContextFactory : IDesignTimeDbContextFactory<Db>
    {
        public Db CreateDbContext(string[] args)
        {
            DbContextOptions<Db> options = HelperDatabase.DB_OPTIONS;
            return new Db(options);
        }
    }
}
