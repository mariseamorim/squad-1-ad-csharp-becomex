using CentralDeErrosApi.Models;
using System.Collections.Generic;

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
