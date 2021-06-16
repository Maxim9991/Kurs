using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TooLargeException.Entities
{
    public class EFDataContext : DbContext
    {
        public DbSet<AppCar> Cars { get; set; }
        public EFDataContext(DbContextOptions opt) : base(opt)
        {
            
        }
    }
}
