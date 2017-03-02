using System.Web.Http;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data;

namespace CharacterBuilder.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/armor")]
    public class ArmorController : ApiController
    {
        private readonly ProficiencyRepository _profRepository;
        private readonly ArmorRepository _armorRepository;

        public ArmorController()
        {
            _armorRepository = new ArmorRepository();
            _profRepository = new ProficiencyRepository();
        }

        [HttpGet]
        [Route("GetAllArmor")]
        public IHttpActionResult GetAllArmor()
        {
            var armorList = _armorRepository.GetAllArmors();

            return Ok(armorList);
        }

        [HttpGet]
        [Route("GetArmorProficiencyTypes")]
        public IHttpActionResult GetArmorProficiencyTypes()
        {
            var profList = _profRepository.GetArmorProficiencies();

            return Ok(profList);
        }

        [HttpPost]
        [Route("AddArmor/")]
        public IHttpActionResult AddArmor([FromBody] Armor armorToAdd)
        {
            _armorRepository.AddArmor(armorToAdd);
            var armorAddedProficiency = _profRepository.GetProficiencyById(armorToAdd.ProficiencyId);
            armorToAdd.Proficiency = armorAddedProficiency;

            return Ok(armorToAdd);
        }

        [HttpPut]
        [Route("EditArmor/")]
        public IHttpActionResult EditArmor([FromBody] Armor armorToEdit)
        {
            _armorRepository.EditArmor(armorToEdit);

            return Ok(armorToEdit);
        }

        [HttpDelete]
        [Route("DeleteArmor/{armorId}")]
        public IHttpActionResult DeleteArmorById(int armorId)
        {
            _armorRepository.DeleteArmorById(armorId);

            return Ok(armorId);
        }

    }
}
