using System.Web.Http;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data;

namespace CharacterBuilder.Controllers.Api
{
    [RoutePrefix("api/weapon")]
    public class WeaponController : ApiController
    {
        private readonly WeaponRepository _weaponRepository;
        private readonly ProficiencyRepository _proficiencyRepository;

        public WeaponController()
        {
            _weaponRepository = new WeaponRepository();
            _proficiencyRepository = new ProficiencyRepository();
        }

        [HttpGet]
        [Route("GetAllWeapons")]
        public IHttpActionResult GetAllWeapons()
        {
            var weaponList = _weaponRepository.GetAllWeapons();

            return Ok(weaponList);
        }

        [HttpGet]
        [Route("GetWeaponProficiencyTypes")]
        public IHttpActionResult GetWeaponProficiencyTypes()
        {
            var profList = _proficiencyRepository.GetWeaponProficiencies();

            return Ok(profList);
        }

        [HttpPost]
        [Route("AddWeapon/")]
        public IHttpActionResult AddArmor([FromBody] Weapon weaponToAdd)
        {
            _weaponRepository.AddWeapon(weaponToAdd);
            var armorAddedProficiency = _proficiencyRepository.GetProficiencyById(weaponToAdd.ProficiencyId);
            weaponToAdd.Proficiency = armorAddedProficiency;

            return Ok(weaponToAdd);
        }

        [HttpGet]
        [Route("GetAllWeaponProperties")]
        public IHttpActionResult GetAllWeaponProperties()
        {
            var profList = _weaponRepository.GetAllWeaponProperties();

            return Ok(profList);
        }

        [HttpPut]
        [Route("EditWeapon/")]
        public IHttpActionResult EditWeapon([FromBody] Weapon weaponToEdit)
        {
            _weaponRepository.EditWeapon(weaponToEdit);

            return Ok(weaponToEdit);
        }

        [HttpDelete]
        [Route("DeleteWeapon/{weaponId}")]
        public IHttpActionResult DeleteArmorById(int weaponId)
        {
            _weaponRepository.DeleteWeaponById(weaponId);

            return Ok(weaponId);
        }
    }
}
