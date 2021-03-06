﻿using System;
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
    [ApiController]
    [Authorize]
    public class SituationController : Controller
    {
        private readonly ISituation _service;
        private readonly IMapper _mapper;

        public SituationController(ISituation service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        /// <summary>
        /// Listar todas as situações.
        /// </summary>
        // GET: api/Situations
        [HttpGet]
        public ActionResult<IEnumerable<SituationDTO>> GetSituations()
        {
            var situations = _service.ConsultAllSituations();

            if (situations == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(situations.
                        Select(x => _mapper.Map<SituationDTO>(x)).
                        ToList());
            }
        }

        /// <summary>
        /// Listar todas a situação pelo id.
        /// </summary>
        // GET: api/Situations/
        [HttpGet("{id}")]
        public ActionResult<SituationDTO> GetSituation(int id)
        {
            var situation = _service.ConsultSituationById(id);

            if (situation == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SituationDTO>(situation));
        }
        /// <summary>
        /// Alterar situação.
        /// </summary>
        // GET: api/Situations/
        [HttpPut("{id}")]
        public ActionResult<SituationDTO> PutSituation(int id, Situation situation)
        {
            if (id != situation.SituationId)
            {
                return BadRequest();
            }

            try
            {
                return Ok(_mapper.Map<SituationDTO>(_service.RegisterOrUpdateSituation(situation)));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.SituationExists(id))
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
        ///Cadastrar situação.
        /// </summary>
        // POST: api/Situations
        [HttpPost]
        public ActionResult<SituationDTO> PostSituation([FromBody] SituationDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(_mapper.Map<SituationDTO>(_service.RegisterOrUpdateSituation(_mapper.Map<Situation>(value))));
        }
    }
}
