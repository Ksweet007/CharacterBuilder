﻿using System;
using System.Collections.Generic;

namespace CharacterBuilder.Core.Model
{
    public class CharacterSheet
    {
        public int Id { get; set; }
        public string UserNameOwner { get; set; }
        public Class Class { get; set; }
        public int ClassLevel { get; set; }
        public int HitPointsMax { get; set; }
        public int Strength { get; set; }
        public int StrengthMod => (int)Math.Floor((double)(Strength - 10 / 2));
        public int Dexterity { get; set; }
        public int DexterityMod => (int)Math.Floor((double)(Strength - 10 / 2));
        public int Constitution { get; set; }
        public int ConstitutionMod => (int)Math.Floor((double)(Strength - 10 / 2));
        public int Wisdom { get; set; }
        public int WisdomMod => (int)Math.Floor((double)(Strength - 10 / 2));
        public int Intelligence { get; set; }
        public int IntelligenceMod => (int)Math.Floor((double)(Strength - 10 / 2));
        public int Charisma { get; set; }
        public int CharismaMod => (int)Math.Floor((double)(Strength - 10 / 2));
        public IList<Skill> Skills { get; set; }
    }
}