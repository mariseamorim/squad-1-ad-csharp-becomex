using CentralDeErrosApi.Infrastrutura;
using CentralDeErrosApi.Interfaces;
using CentralDeErrosApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Service
{
    public class AmbienteService : IAmbiente
    {
        public ApplicationContext _context;

        public AmbienteService(ApplicationContext context)
        {
            this._context = context;
        }        

        public Ambiente RegisterOrUpdateAmbiente(Ambiente ambiente)
        {
            var state = ambiente.EnvironmentId == 0 ? EntityState.Added : EntityState.Modified;
            _context.Entry(ambiente).State = state;
            _context.SaveChanges();
            return ambiente;
        }

        public Ambiente ConsultAmbiente(int id)
        {
            return _context.Ambiente.Find(id);
        }

        public List<Ambiente> ConsultAllAmbiente()
        {
            return _context.Ambiente.Select(a => a).ToList();
        }

        public bool AmbienteExists(int id)
        {
            return _context.Ambiente.Any(e => e.EnvironmentId == id);
        }
    }
}
