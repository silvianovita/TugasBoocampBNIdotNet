﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class MyContext : DbContext
    {
        public MyContext()
        {

        }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Person> Peoples { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<University> Universities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(person => person.Account)
                .WithOne(account => account.Person)
                .HasForeignKey<Account>(account => account.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(account => account.Profiling)
                .WithOne(profiling => profiling.Account)
                .HasForeignKey<Profiling>(profiling => profiling.NIK);

        }

    }
}
