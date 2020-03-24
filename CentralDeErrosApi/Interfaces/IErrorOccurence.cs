using CentralDeErrosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Interfaces
{
    public interface IErrorOccurence
    {
        ErrorOccurrence RegisterOrUpdateErrorOccurrence(ErrorOccurrence errorOccurrence);

        ErrorOccurrence ConsultErrorOccurrenceById(int errorOccurrenceId);

        List<ErrorOccurrence> ListOccurencesByLevel(int level);

        List<ErrorOccurrence> Consult(int ambiente, int campoOrdenacao, int campoBuscado, string textoBuscado);

        bool ErrorOccurrenceExists(int id);

        ErrorOccurrence FileErrorOccurrence(ErrorOccurrence errorOccurrence);

        ErrorOccurrence DeleteErrorOccurrence(ErrorOccurrence errorOccurrence);
    }
}
