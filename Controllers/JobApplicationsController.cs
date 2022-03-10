namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/job-vacancies/{id}/applications")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;
        public JobApplicationsController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }

        // POST api/job-vacancies/4/applications
        /// <summary>
        /// Aplicar pessoa para a vaga de emprego.
        /// </summary>
        /// <remarks>
        /// {
        ///   "applicantName": "Luis Fernando",
        ///   "applicanteEmail": "luis@luis.com",
        ///   "idJobVacancy": 1
        /// }   
        /// </remarks>
        /// <param name="id">Identificador</param>
        /// <param name="model">Dados de quem vai se aplicar para a vaga de emprego.</param>
        /// <returns>No Content</returns>
        [HttpPost]
        public IActionResult Post(int id, AddJobApplicationInputModel model)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            var application = new JobApplication(
                model.ApplicantName,
                model.ApplicanteEmail,
                id
            );

            _repository.AddAplication(application);
            
            return NoContent();
        }
    }
}