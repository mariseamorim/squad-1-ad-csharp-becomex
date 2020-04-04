using CentralDeErrosApi.Infrastrutura;
using CentralDeErrosApi.Interfaces;
using CentralDeErrosApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace CentralDeErrosApi.Service
{
    public class EnvironmentService : IEnvironment
    {
        public ApplicationContext _context;

        public EnvironmentService(ApplicationContext context)
        {
            _context = context;
        }        

        public Environment CreateEnvironment(Environment ambiente)
        {
            var state = ambiente.EnvironmentId== 0 ? EntityState.Added : EntityState.Modified;
            _context.Entry(ambiente).State = state;
            _context.SaveChanges();
            return ambiente;
        }
        public Environment  UpdateEnvironment(Environment ambiente)
        {
            _context.Update(ambiente);
            _context.SaveChanges();
            return ambiente;
        }
        
        public Environment FindByIdEnvironment(int id)
        {
            return _context.Environments.Find(id);
        }

        public List<Environment> ConsultAllEnvironments()
        {
            return _context.Environments.Select(a => a).ToList();
        }

        public bool EnvironmentExists(int id)
        {
            return _context.Environments.Any(e => e.EnvironmentId == id);
        }

    }
}
