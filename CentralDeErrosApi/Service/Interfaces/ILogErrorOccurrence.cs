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

        List<LogErrorOccurrence> ConsultAllOccurrence();

        bool ErrorOccurrenceExists(int id);

        LogErrorOccurrence DeleteErrorOccurrence(LogErrorOccurrence errorOccurrence);
    }
}
