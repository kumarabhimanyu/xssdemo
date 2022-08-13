using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XSSDemo.Models;

namespace XSSDemo.Data
{
    public class XSSDemoContext : DbContext
    {
        public XSSDemoContext (DbContextOptions<XSSDemoContext> options)
            : base(options)
        {
        }

        public DbSet<XSSDemo.Models.PersonInfo> PersonInfo { get; set; } = default!;
    }
}
