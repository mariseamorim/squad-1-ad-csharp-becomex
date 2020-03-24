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
    public class LevelService : ILevel
    {
        public ApplicationContext _context;

        public LevelService(ApplicationContext context)
        {
            this._context = context;
        }

        public Level RegisterOrUpdateLevel(Level level)
        {
            var state = level.LevelId == 0 ? EntityState.Added : EntityState.Modified;
            _context.Entry(level).State = state;
            _context.SaveChanges();
            return level;
        }

        public Level ConsultLevelById(int id)
        {
            return _context.Levels.Find(id);
        }

        public Level ConsultLevelByName(string name)
        {
            return _context.Levels.FirstOrDefault(l => l.LevelName == name);
        }

        public List<Level> ConsultAllLevels()
        {
            return _context.Levels.Select(l => l).ToList();
        }

        public bool LevelExists(int id)
        {
            return _context.Levels.Any(e => e.LevelId == id);
        }
    }
}
