using CentralDeErrosApi.Models;
using System.Collections.Generic;

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
