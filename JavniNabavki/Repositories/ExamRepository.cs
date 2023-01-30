using JavniNabavki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JavniNabavki.Repositories
{
    public class ExamRepository : IExamRepository
    {
        private readonly ExamContext _context;
        public ExamRepository(ExamContext context)
        {
            _context = context;
        }
        public async Task<Exam> Create(Exam exam)
        {
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();
            return exam;
        }

        public async Task Delete(int id)
        {
            var examToDelete = await _context.Exams.FindAsync(id);
            _context.Exams.Remove(examToDelete);
            await _context.SaveChangesAsync();
        }



        public async Task<IEnumerable<Exam>> Get()
        {
            return await _context.Exams.ToListAsync();

        }

        public async Task<Exam> Get(int id)
        {
            return await _context.Exams.FindAsync(id);

        }

        public async Task Update(Exam exam)
        {
            _context.Entry(exam).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


    }
}