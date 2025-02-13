using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nueva_practica.Models;

namespace Nueva_practica.Data
{
    public class Nueva_practicaContext : DbContext
    {
        public Nueva_practicaContext (DbContextOptions<Nueva_practicaContext> options)
            : base(options)
        {
        }

        public DbSet<Nueva_practica.Models.ClientesModels> ClientesModels { get; set; } = default!;
    }
}
