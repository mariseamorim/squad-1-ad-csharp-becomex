using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Authorize]
    public class LogErrorOccurrencesController : ControllerBase
    {
        private readonly ILogErrorOccurrence  _service;
        private readonly ISituation _situationService;
        private readonly IMapper _mapper;

        public LogErrorOccurrencesController(ILogErrorOccurrence service, ISituation situationService, IMapper mapper)
        {
            _service = service;
            _situationService = situationService;
            _mapper = mapper;
        }
        /// <summary>
        /// Listar todas as ocorrencia.
        /// </summary>
        // GET: api/ErrorOccurrences
        [HttpGet]
        public ActionResult<IEnumerable<LogErrorOccurrenceDTO>> Get()
        {
            var errorOccurrences = _service.ConsultAllOccurrence();

            if (errorOccurrences == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(errorOccurrences.
                        Select(x => _mapper.Map<LogErrorOccurrenceDTO>(x)).
                        ToList());
            }
        }
        /// <summary>
        /// Listar uma ocorrencia por LevelId.
        /// </summary>
        // GET api/<controller>/{id}
        [HttpGet("Level={levelId}")]
        public ActionResult<IEnumerable<LogErrorOccurrenceDTO>> GetErrorOccurrencesByLevel(int levelId)
        {
            var errorOccurrences = _service.ListOccurencesByLevel(levelId);

            if (errorOccurrences == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(errorOccurrences.
                        Select(x => _mapper.Map<LogErrorOccurrenceDTO>(x)).
                        ToList());
            }
        }

        /// <summary>
        /// Apagar uma ocorrencia 
        /// </summary>
        [HttpDelete]
        public ActionResult<LogErrorOccurrence> DeleteErrorOccurrence([FromBody] LogErrorOccurrenceDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_service.ConsultErrorOccurrenceById(value.ErrorId) == null)
                return BadRequest("400 BadRequest: ErrorOccurence does not exists.");

            return Ok(_mapper.Map<LogErrorOccurrenceDTO>(_service.DeleteErrorOccurrence(_mapper.Map<LogErrorOccurrence>(value))));
        }
       
        /// <summary>
        /// Pesquisa Ocorrencia de Erro por Id
        /// </summary>
        // GET: api/ErrorOccurrences/5
        [HttpGet("{id}")]
        public ActionResult<LogErrorOccurrenceDTO> GetErrorOccurrence(int id)
        {
            var errorOccurrence = _service.ConsultErrorOccurrenceById(id);

            if (errorOccurrence == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LogErrorOccurrenceDTO>(errorOccurrence));
        }
        /// <summary>
        /// Altera Ocorrencia de Erro 
        /// </summary>
        // PUT: api/ErrorOccurrences/5
        [HttpPut("{id}")]
        public ActionResult<LogErrorOccurrenceDTO> PutErrorOccurrence(int id, LogErrorOccurrence errorOccurrence)
        {
            if (id != errorOccurrence.ErrorId)
            {
                return BadRequest();
            }

            try
            {
                return Ok(_mapper.Map<LogErrorOccurrenceDTO>(_service.RegisterOrUpdateErrorOccurrence(errorOccurrence)));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.ErrorOccurrenceExists(id))
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
        /// Cadastra Ocorrencia de Erro 
        /// </summary>
        // POST: api/ErrorOccurrences
        [HttpPost]
        public ActionResult<LogErrorOccurrence> PostErrorOccurrence([FromBody] LogErrorOccurrenceDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

          /*  if (!_userService.UserExists(value.UserId))
                return BadRequest("400 BadRequest: User does not exists.");*/

            if (!_situationService.SituationExists(value.SituationId))
                return BadRequest("400 BadRequest: Situation does not exists.");

            return Ok(_mapper.Map<LogErrorOccurrenceDTO>(_service.RegisterOrUpdateErrorOccurrence(_mapper.Map<LogErrorOccurrence>(value))));
        }
    }
}
