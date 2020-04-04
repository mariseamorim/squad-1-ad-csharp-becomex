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
            if (//_context.Users.Any(u => u.UserId == errorOccurrence.UserId) &&
                 _context.LogErrorOccurrence.Any(e => e.ErrorId == errorOccurrence.ErrorId) &&
                 _context.Situations.Any(s => s.SituationId == errorOccurrence.SituationId))
            {
                var state = errorOccurrence.ErrorId == 0 ? EntityState.Added : EntityState.Modified;
                _context.LogErrorOccurrence.Add(errorOccurrence);
                _context.SaveChanges();
            }

            return errorOccurrence;
        }

        public List<LogErrorOccurrence> Consult(int ambiente, int campoOrdenacao, int campoBuscado, string textoBuscado)
        {
            List<LogErrorOccurrence> errorOccurrenceList = new List<LogErrorOccurrence>();

            foreach (var item in errorOccurrenceList)
            {
                var occList = _context.LogErrorOccurrence.Where(x => x.ErrorId == item.ErrorId).ToList();

                foreach (var itemOcc in occList)
                {
                    errorOccurrenceList.Add(itemOcc);
                }
            }
            return errorOccurrenceList;
        }

        public class Occurrences
        {
            public int ErrorId { get; set; }
            public int Quantity { get; set; }
        }

        public List<LogErrorOccurrence> ListOccurencesByLevel(int level)
        {
            return _context.LogErrorOccurrence.Where(o => o.LevelId == level).ToList();
        }

        public bool ErrorOccurrenceExists(int id)
        {
            return _context.LogErrorOccurrence.Any(e => e.ErrorId == id);
        }

        public LogErrorOccurrence FileErrorOccurrence(LogErrorOccurrence errorOccurrence)
        {
            if (//_context.Users.Any(u => u.UserId == errorOccurrence.UserId) &&
                 _context.LogErrorOccurrence.Any(e => e.ErrorId == errorOccurrence.ErrorId) &&
                 _context.Situations.Any(s => s.SituationId == errorOccurrence.SituationId))
            {
                var state = errorOccurrence.ErrorId == 0 ? EntityState.Added : EntityState.Modified;
                var FileErrorOccurrence = errorOccurrence.Situation;

                if (_situationService.ConsultSituationByName("Arquivado") == null)
                    return null;

                errorOccurrence.Situation = _situationService.ConsultSituationByName("Arquivado");
                errorOccurrence.SituationId = _situationService.ConsultSituationByName("Arquivado").SituationId;

                _context.Entry(errorOccurrence).State = state;
                _context.SaveChanges();
            }

            return errorOccurrence;
        }

        public LogErrorOccurrence DeleteErrorOccurrence(LogErrorOccurrence errorOccurrence)
        {
            if (//_context.Users.Any(u => u.UserId == errorOccurrence.UserId) &&
                  _context.LogErrorOccurrence.Any(e => e.ErrorId == errorOccurrence.ErrorId) &&
                  _context.Situations.Any(s => s.SituationId == errorOccurrence.SituationId))
            {
                var state = errorOccurrence.ErrorId == 0 ? EntityState.Added : EntityState.Modified;
                var FileErrorOccurrence = errorOccurrence.Situation;

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
