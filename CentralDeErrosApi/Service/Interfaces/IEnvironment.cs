using CentralDeErrosApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Interfaces
{
    public interface IEnvironment
    {
        Environment CreateEnvironment(Environment environment);

        Environment UpdateEnvironment(Environment environment);
        Environment FindByIdEnvironment(int id);

        List<Environment> ConsultAllEnvironments();

        bool EnvironmentExists(int id);
    }
        
}
