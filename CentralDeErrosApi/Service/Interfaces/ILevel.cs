using CentralDeErrosApi.Models;
using System.Collections.Generic;

namespace CentralDeErrosApi.Interfaces
{
    public interface ILevel
    {
        Level RegisterOrUpdateLevel(Level level);

        Level ConsultLevelById(int id);

        Level ConsultLevelByName(string name);

        List<Level> ConsultAllLevels();

        bool LevelExists(int id);
    }
}
