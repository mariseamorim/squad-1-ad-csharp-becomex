using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CentralDeErrosApi.DTO;
using CentralDeErrosApi.Interfaces;
using CentralDeErrosApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CentralDeErrosApi.Controllers
{
    [Route("api/[controller]")]
    public class LevelsController : Controller
    {
        private readonly ILevel _service;
        private readonly IMapper _mapper;

        public LevelsController(ILevel service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }
        /// <summary>
        /// Lista todos os levels.
        /// </summary>
        // GET: api/Levels
        [HttpGet]
        public ActionResult<IEnumerable<LevelDTO>> GetLevels()
        {
            var levels = _service.ConsultAllLevels();

            if (levels == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(levels.
                        Select(x => _mapper.Map<LevelDTO>(x)).
                        ToList());
            }
        }
        /// <summary>
        /// Busca Level por id.
        /// </summary>
        // GET: api/Levels/
        [HttpGet("{id}")]
        public ActionResult<LevelDTO> GetLevel(int id)
        {
            var level = _service.ConsultLevelById(id);

            if (level == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LevelDTO>(level));
        }
        /// <summary>
        /// Altera Level.
        /// </summary>
        // PUT: api/Levels/5
        [HttpPut("{id}")]
        public ActionResult<LevelDTO> PutLevel(int id, Level level)
        {
            if (id != level.LevelId)
            {
                return BadRequest();
            }

            try
            {
                return Ok(_mapper.Map<LevelDTO>(_service.RegisterOrUpdateLevel(level)));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.LevelExists(id))
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
        /// Cadastra um Level.
        /// </summary>
        // POST: api/Levels
        [HttpPost]
        public ActionResult<LevelDTO> PostLevel([FromBody] LevelDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(_mapper.Map<LevelDTO>(_service.RegisterOrUpdateLevel(_mapper.Map<Level>(value))));
        }

    }
}
