using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetTaskApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetTaskApi.Context
{
    public class OrganizerContext(DbContextOptions<OrganizerContext> options) : DbContext(options)
    {
        public DbSet<UserTask> UserTasks { get; set; } = default!;
    }
}