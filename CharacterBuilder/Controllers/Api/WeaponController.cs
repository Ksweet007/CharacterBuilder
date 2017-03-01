using System.Web.Http;
using CharacterBuilder.Core.DTO;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data;
using CharacterBuilder.Infrastructure.Services;

namespace CharacterBuilder.Controllers.Api
{
    [RoutePrefix("api/weapon")]
    public class WeaponController : ApiController
    {
        private readonly WeaponRepository _weaponRepository;
        private readonly ProficiencyRepository _proficiencyRepository;
        private readonly WeaponService _weaponService;

        public WeaponController()
        {
            _weaponRepository = new WeaponRepository();
            _proficiencyRepository = new ProficiencyRepository();
            _weaponService = new WeaponService();
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
        public IHttpActionResult AddWeapon([FromBody] Weapon weaponToAdd)
        {
            _weaponService.CreateWeapon(weaponToAdd);

            return Ok(weaponToAdd);            
        }

        [HttpGet]
        [Route("GetAllWeaponProperties")]
        public IHttpActionResult GetAllWeaponProperties()
        {
            var profList = _weaponRepository.GetAllWeaponProperties();

            return Ok(profList);
        }

        [HttpGet]
        [Route("GetAllWeaponCategories")]
        public IHttpActionResult GetAllWeaponCategories()
        {
            var catList = _weaponRepository.GetAllWeaponCategories();

            return Ok(catList);
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
