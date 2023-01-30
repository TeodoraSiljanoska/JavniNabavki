using JavniNabavki.Models;

namespace JavniNabavki.Repositories
{
    public interface IExamRepository
    {
        Task<IEnumerable<Exam>> Get();
        Task<Exam> Get(int id);
        Task<Exam> Create(Exam exam);
        Task Update(Exam exam);
        Task Delete(int id);

    }
}