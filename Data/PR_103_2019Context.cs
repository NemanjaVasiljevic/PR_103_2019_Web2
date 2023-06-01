using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PR_103_2019.Models;

namespace PR_103_2019.Data
{
    public class PR_103_2019Context : DbContext
    {
        public PR_103_2019Context (DbContextOptions<PR_103_2019Context> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = default!;

        public DbSet<Article>? Article { get; set; }

        public DbSet<Order>? Order { get; set; }
    }
}
