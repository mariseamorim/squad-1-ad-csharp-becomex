using CentralDeErrosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Interfaces
{
    public interface ILogErrorOccurrence
    {
        LogErrorOccurrence RegisterOrUpdateErrorOccurrence(LogErrorOccurrence errorOccurrence);

        LogErrorOccurrence ConsultErrorOccurrenceById(int errorOccurrenceId);

        List<LogErrorOccurrence> ListOccurencesByLevel(int level);

        List<LogErrorOccurrence> Consult(int ambiente, int campoOrdenacao, int campoBuscado, string textoBuscado);

        bool ErrorOccurrenceExists(int id);

        LogErrorOccurrence FileErrorOccurrence(LogErrorOccurrence errorOccurrence);

        LogErrorOccurrence DeleteErrorOccurrence(LogErrorOccurrence errorOccurrence);
    }
}
