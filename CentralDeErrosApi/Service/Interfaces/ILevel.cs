using CentralDeErrosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
