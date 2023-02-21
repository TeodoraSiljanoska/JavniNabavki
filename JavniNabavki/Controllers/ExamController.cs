using JavniNabavki.Models;
using JavniNabavki.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JavniNabavki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamRepository _examRepository;
        public ExamsController(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Exam>> GetExams()
        {
            return await _examRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExams(int id)
        {
            return await _examRepository.Get(id);
        }

        /* [HttpPost]
        public async Task<ActionResult<Exam>> PostExams([FromBody] Exam exam)
        {
            var newExam = await _examRepository.Create(exam);
            return CreatedAtAction(nameof(GetExams), new { id = newExam.Id }, newExam);
        }
        */

        [HttpPost]
         
        public async Task<ActionResult<Exam>> PostExams([FromBody] Exam exam)
        {
            var exams = await _examRepository.Get();



            var existingExam = exams.Where(x =>
            (x.Pocetok != null ? x.Pocetok.Value.Date == exam.Pocetok.Value.Date : x.Pocetok == exam.Pocetok)
            && (x.Kraj != null ? x.Kraj.Value.Date == exam.Kraj.Value.Date : x.Kraj == exam.Pocetok)
        ).FirstOrDefault();

            if (existingExam != null)
            {
                return Ok("Obuka so istiot datum na pocetok i datum na kraj vekje postoi.");
            }

          

            var existingExam_datum = exams.Where(y =>
                   (y.Datum != null ? y.Datum.Value.Date == exam.Datum.Value.Date : y.Datum == exam.Datum)

               ).FirstOrDefault();

                if (existingExam_datum != null)
                {
                    return Ok("Obuka so istiot datum vekje postoi.");
                }
            


            var newExam = await _examRepository.Create(exam);
            return CreatedAtAction(nameof(GetExams), new { id = newExam.Id }, newExam);
        }
        [HttpPut]
        public async Task<ActionResult> PutExams(int id, [FromBody] Exam exam)
        {
            if (id != exam.Id)
            {
                return BadRequest();
            }
            await _examRepository.Update(exam);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var examToDelete = await _examRepository.Get(id);

            if (examToDelete == null)
                return NotFound();

            await _examRepository.Delete(examToDelete.Id);
            return NoContent();
        }

    }
}