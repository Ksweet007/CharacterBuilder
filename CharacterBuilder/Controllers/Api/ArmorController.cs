using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CharacterBuilder.Core.Enums;
using CharacterBuilder.Core.Model;

namespace CharacterBuilder.Controllers.Api
{
    public class ArmorController : ApiController
    {
        [HttpGet]
        [Route("api/GetAllArmor/")]
        public IHttpActionResult GetAllArmor()
        {
            var armorList = new List<Armor>();
            var armorToAdd = new Armor
            {
                Id = 1,
                ArmorClass = "14 + Dex Modifier",
                Cost = "1 sp.",
                Name = "Armor",
                Proficiency = new Proficiency
                {
                    Id = 1,
                    Name = "Armor Prof",
                    ProficiencyTypeId = ProficiencyType.Armor
                },
                ProficiencyId = 1,
                Stealth = true,
                Strength = "",
                Weight = "10 lbs."
            };

            armorList.Add(armorToAdd);

            return Ok(armorList);
        }

        [HttpGet]
        [Route("api/GetArmorProficiencyTypes/")]
        public IHttpActionResult GetArmorProficiencyTypes()
        {
            var profList = new Proficiency
            {
                Id = 1,
                Name = "Armor Prof",
                ProficiencyTypeId = ProficiencyType.Armor
            };

            return Ok(profList);
        }
    }
}
