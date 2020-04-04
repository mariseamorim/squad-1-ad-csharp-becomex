using CentralDeErrosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Interfaces
{
    public interface ISituation
    {
        Situation RegisterOrUpdateSituation(Situation situation);

        Situation ConsultSituationById(int id);

        Situation ConsultSituationByName(string name);

        List<Situation> ConsultAllSituations();

        bool SituationExists(int id);
    }
}
