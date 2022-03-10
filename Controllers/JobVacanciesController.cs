namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/job-vacancies")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;

        public JobVacanciesController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }

        // GET api/job-vacancies
        /// <summary>
        /// Listagem de todas as vagas de emprego.
        /// </summary>
        /// <returns>Todas as vagas de emprego cadastradas</returns>
        [HttpGet]
        public IActionResult GetAll() {
            var jobVacancies = _repository.GetAll();

            return Ok(jobVacancies);
        }

        // GET api/job-vacancies/4
        /// <summary>
        /// Listar uma vaga de emprego por identificador(id)
        /// </summary>
        /// <param name="id">Numero identificador da vaga de emprego (id)</param>
        /// <returns>Objeto com a vaga de emprego com o numero identificador</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            return Ok(jobVacancy);
        }

        // POST api/job-vacancies
        /// <summary>
        /// Cadastrar uma vaga de emprego.
        /// </summary>
        /// <remarks>
        /// {
        ///   "title": "Dev .NET Jr",
        ///   "description": "Vaga para .Net Core",
        ///   "company": "LuisCompany",
        ///   "isRemote": true,
        ///   "salaryRange": "3000 - 5000"
        ///  }
        /// </remarks>
        /// <param name="model">Dados da vaga.</param>
        /// <returns>Objeto recém-criado.</returns>
        /// <response code="201">Sucesso.</response>
        /// <response code="400">Dados Invalidos.</response>
        [HttpPost]
        public IActionResult Post(AddJobVacancyInputModel model) {
            var jobVacancy = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
            );

            if (jobVacancy.Title.Length > 30)
                return BadRequest("Rótulo precisa ter menos de 30 caracteres.");

            _repository.Add(jobVacancy);

            return CreatedAtAction(
                "GetById",
                new { id = jobVacancy.Id},
                jobVacancy
            );
        }

        //PUT api/job-vacancies/4
        /// <summary>
        /// Atualização de dados de uma vaga de emprego.
        /// </summary>
        /// <remarks>
        /// {
        ///   "title": "Dev Java JR",
        ///   "description": "Vaga para Java JR"
        /// }
        /// </remarks>
        /// <param name="id">Numero de identificador da vaga de emprego (id)</param>
        /// <param name="model">Dados da vaga de emprego.</param>
        /// <returns>No Content</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model) {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            jobVacancy.Update(model.Title, model.Description);
            _repository.Update(jobVacancy);

            return NoContent();
        }
    }

}