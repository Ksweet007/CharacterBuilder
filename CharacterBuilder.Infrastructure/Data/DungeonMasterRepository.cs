using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CharacterBuilder.Core.DTO;
using CharacterBuilder.Core.Model.DungeonMaster;
using CharacterBuilder.Core.Model.User;
using CharacterBuilder.Infrastructure.Data.Contexts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CharacterBuilder.Infrastructure.Data
{
    public class DungeonMasterRepository
    {
        private readonly CharacterBuilderDbContext _db;
        private readonly UserManager<ApplicationUser> _manager;

        public DungeonMasterRepository()
        {
            _db = new CharacterBuilderDbContext();
            _manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
        }

        public Campaign CreateCampaign(string userId, string campaignName)
        {
            var currentUser = _manager.FindById(userId);
            var newCamp = new Campaign
            {
                Name = campaignName,
                User = currentUser
            };

            _db.Campaigns.Add(newCamp);
            Save();

            return newCamp;
        }

        public PlayerCharacterCard CreatePlayerCard(PlayerCharacterCardDto cardDto)
        {
            var camp = GetById(cardDto.CampaignId);

            var newCard = new PlayerCharacterCard
            {
                PlayerName = cardDto.PlayerName,
                CharacterName = cardDto.CharacterName,
                HitPoints = cardDto.HitPoints,
                ArmorClass = cardDto.ArmorClass,
                PassivePerception = cardDto.PassivePerception,
                Saves = new PlayerCardSaves(),
                Campaign = camp          
            };

            _db.PlayerCharacterCards.Add(newCard);
            Save();

            return newCard;
        }
        
        public Campaign GetById(int campaignId)
        {
            return _db.Campaigns
                .Include(u=>u.User)
                .Single(c => c.Id == campaignId);
        }

        public IList<PlayerCharacterCard> ListByCampaignId(int campaignId)
        {
            return _db.PlayerCharacterCards.Where(c => c.Campaign.Id == campaignId).ToList();
        }

        public IList<Campaign> ListByUserId(string userId)
        {
            return _db.Campaigns.Where(u => u.User.Id == userId).ToList();
        }

        public void DeleteCampaignById(int campaignId)
        {
            var campFromDb = _db.Campaigns.Single(c => c.Id == campaignId);

            _db.Campaigns.Remove(campFromDb);
            Save();
        }

        public void DeleteCardById(int cardId)
        {
            var cardFromDb = _db.PlayerCharacterCards.Single(c => c.Id == cardId);

            _db.PlayerCharacterCards.Remove(cardFromDb);
            Save();
        }
        
        public bool DoesUserOwnCampaign(string userId, int campaignId)
        {
            var campaignFromDb = GetById(campaignId);
            return campaignFromDb.User.Id == userId;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
