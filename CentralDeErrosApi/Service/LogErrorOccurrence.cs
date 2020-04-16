using CentralDeErrosApi.Infrastrutura;
using CentralDeErrosApi.Interfaces;
using CentralDeErrosApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Service
{
    public class LogErrorOccrurence : ILogErrorOccurrence
    {
        private readonly ApplicationContext _context;
        private readonly ISituation _situationService;

        public LogErrorOccrurence(ApplicationContext context, ISituation situationService)
        {
            _context = context;
            _situationService = situationService;

        }

        public LogErrorOccurrence RegisterOrUpdateErrorOccurrence(LogErrorOccurrence errorOccurrence)
        {
            var state = errorOccurrence.ErrorId == 0 ? EntityState.Added : EntityState.Modified;
            _context.LogErrorOccurrence.Add(errorOccurrence);
            _context.SaveChanges();

            return errorOccurrence;
        }

        public List<LogErrorOccurrence> ConsultAllOccurrence()
        {
            return _context.LogErrorOccurrence.Select(a => a).ToList();
        }
        public List<LogErrorOccurrence> ListOccurencesByLevel(int level)
        {
            return _context.LogErrorOccurrence.Where(o => o.LevelId == level).ToList();
        }

        public bool ErrorOccurrenceExists(int id)
        {
            return _context.LogErrorOccurrence.Any(e => e.ErrorId == id);
        }

        public LogErrorOccurrence DeleteErrorOccurrence(LogErrorOccurrence errorOccurrence)
        {
            if (//_context.Users.Any(u => u.UserId == errorOccurrence.UserId) &&
                  _context.LogErrorOccurrence.Any(e => e.ErrorId == errorOccurrence.ErrorId) &&
                  _context.Situations.Any(s => s.SituationId == errorOccurrence.SituationId))
            {
                var state = errorOccurrence.ErrorId == 0 ? EntityState.Added : EntityState.Modified;
                var FileErrorOccurrence = errorOccurrence.SituationId;

                if (_situationService.ConsultSituationByName("Arquivado") == null)
                    return null;

                errorOccurrence.Situation = _situationService.ConsultSituationByName("Apagado (Inativo)");
                errorOccurrence.SituationId = _situationService.ConsultSituationByName("Apagado (Inativo)").SituationId;

                _context.Entry(errorOccurrence).State = state;
                _context.SaveChanges();
            }

            return errorOccurrence;
        }

        public LogErrorOccurrence ConsultErrorOccurrenceById(int errorOccurrenceId)
        {
            return _context.LogErrorOccurrence.Find(errorOccurrenceId);
        }
    }
}
