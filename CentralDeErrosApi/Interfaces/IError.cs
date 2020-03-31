using CentralDeErrosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Interfaces
{
    public interface IError
    {
        Error RegisterOrUpdateError(Error error);

        Error ConsultError(int id);

        List<Error> Consult(int ambiente, int inputOrder, int inputSearch, string textSearch);

        List<Error> ConsultAllErrors();

        bool ErrorExists(int id);
    }
}
