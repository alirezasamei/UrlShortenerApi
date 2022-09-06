﻿using Microsoft.EntityFrameworkCore;
using UrlShortener.Core.Entity;

namespace Infrastructure.UrlShortener.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {            
        }
        public DbSet<Url> Urls { get; set; }
    }
}
