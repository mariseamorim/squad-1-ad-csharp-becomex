using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CentralDeErrosApi.DTO;
using CentralDeErrosApi.Interfaces;
using CentralDeErrosApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CentralDeErrosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnvironmentsController : ControllerBase
    {
        private readonly IEnvironment _service;
        private readonly IMapper _mapper;

        public EnvironmentsController(IEnvironment service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        /// <summary>
        /// Lista todos os ambientes.
        /// </summary>
        // GET: api/Environments
        [HttpGet]
        public ActionResult<IEnumerable<EnvironmentDTO>> GetEnvironments()
        {
 
            var environments = _service.ConsultAllEnvironments();

            if (environments == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(environments.
                        Select(x => _mapper.Map<EnvironmentDTO>(x)).
                        ToList());
            }

        }
        /// <summary>
        /// Consulta um ambiente por id
        /// </summary>
        // GET: api/Environments/
        [HttpGet("{id}")]
        public ActionResult<EnvironmentDTO> GetEnvironment(int id )
        {
            return Ok(_mapper.Map<EnvironmentDTO>(_service.FindByIdEnvironment(id)));
        }

        /// <summary>
        /// Alteração de ambiente
        /// </summary>
        // PUT: api/Environments/5
        [HttpPut("{id}")]
        public ActionResult<EnvironmentDTO> PutEnvironment(int id,Environment environment)
        {
            if (id != environment.EnvironmentId)
            {
                return BadRequest();
            }

            try
            {
                return Ok(_mapper.Map<EnvironmentDTO>(_service.UpdateEnvironment(environment)));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.EnvironmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Cadastra ambiente
        /// </summary>
        // POST: api/Environments
        [HttpPost]
        public ActionResult<EnvironmentDTO> PostEnvironment([FromBody] EnvironmentDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return base.Ok(_mapper.Map<EnvironmentDTO>(_service.CreateEnvironment(_mapper.Map<Environment>(value))));
        }
    }
}
