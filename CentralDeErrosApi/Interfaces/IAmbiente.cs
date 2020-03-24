using CentralDeErrosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Interfaces
{
    interface IAmbiente
    {
        Ambiente RegisterOrUpdateAmbiente(Ambiente ambiente);

        Ambiente ConsultAmbiente(int id);

        List<Ambiente> ConsultAllAmbiente();

        bool AmbienteExists(int id);
    }
}
