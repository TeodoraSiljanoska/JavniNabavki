using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace JavniNabavki.Models
{
    public class ExamContext : DbContext
    {
        public ExamContext(DbContextOptions<ExamContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Exam> Exams { get; set; }
    }
}