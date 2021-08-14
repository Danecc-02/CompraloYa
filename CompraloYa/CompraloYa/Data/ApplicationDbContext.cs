using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CompraloYa.Models;

namespace CompraloYa.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Joyeria> Joyerias { get; set; }
        public DbSet<Oficina> Oficinas { get; set; }
        public DbSet<Ropa> Ropas { get; set; }
        public DbSet<Tecnologia> Tecnologias { get; set; }
        public DbSet<TypeRopa> TypeRopas { get; set; }
        public DbSet<TypeSend> TypeSends { get; set; }
        public DbSet<TypeTecnologia> TypeTecnologias { get; set; }
    }
}
