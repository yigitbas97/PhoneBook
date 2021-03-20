using Microsoft.EntityFrameworkCore;
using PhoneBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Context
{
    public class PhoneBookContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=PhoneBook;Integrated Security=True");
        }

        public DbSet<Person> People { get; set; }
    }
}
