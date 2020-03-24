using CentralDeErrosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Service
{
    public interface IErrorOccurrenceService
    {
        // cadastra e retorna sucesso ou falha
        bool RegisterError(Error error, Users user, string origin, string details, DateTime dateTime, string userToken);

        // retorna a lista (detalhada) de todos os erros de um tipo de level individualmente
        List<ErrorOccurrence> ListOccurencesByLevel(int level);

        // retorna 
        List<ErrorOccurrence> Consult(int ambiente, int campoOrdenacao, int campoBuscado, string textoBuscado);
    }
}
