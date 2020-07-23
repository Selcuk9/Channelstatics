using System;
using System.Collections.Generic;
using System.Text;
using Core.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.DataBase.Context
{
    public class Db:DbContext
    {
        public DbSet<TelegramChannel> TelegramChannels { get; set; }
        public DbSet<TelegramPost> TelegramPosts { get; set; }
        public DbSet<StatisticsChannel> StatisticsChannels { get; set; }
        public DbSet<StatisticsPost> StatisticsPosts { get; set; }

        public Db(DbContextOptions<Db> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            #region AutoDatetime

            modelBuilder.Entity<TelegramChannel>()
                .Property(p => p.CreateTime)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<TelegramPost>()
                .Property(p => p.CreateTime)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<StatisticsPost>()
                .Property(p => p.CreateTime)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<StatisticsChannel>()
                .Property(p => p.CreateTime)
                .HasDefaultValueSql("GETDATE()");
            #endregion
        }
    }
}
