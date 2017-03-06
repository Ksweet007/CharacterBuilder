﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data.Contexts;

namespace CharacterBuilder.Infrastructure.Data
{
    public class RaceRepository
    {
        private readonly CharacterBuilderDbContext _db;

        public RaceRepository()
        {
            _db = new CharacterBuilderDbContext();
        }

        public IList<Race> GetAllRaces()
        {
            return _db.Races.Include(r => r.AbilityScoreIncreases).ToList();
        }

    }
}
