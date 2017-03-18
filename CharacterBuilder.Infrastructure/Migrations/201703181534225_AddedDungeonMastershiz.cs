namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDungeonMastershiz : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.Campaign",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "core.PlayerCharacterCard",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PassivePerception = c.Int(nullable: false),
                        HitPoints = c.String(),
                        ArmorClass = c.Int(nullable: false),
                        Saves_StrengthSave = c.Int(nullable: false),
                        Saves_Dexterity = c.Int(nullable: false),
                        Saves_ConstitutionSave = c.Int(nullable: false),
                        Saves_IntelligenceSave = c.Int(nullable: false),
                        Saves_WisdomSave = c.Int(nullable: false),
                        Saves_CharismaSave = c.Int(nullable: false),
                        Campaign_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Campaign", t => t.Campaign_Id)
                .Index(t => t.Campaign_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("core.Campaign", "User_Id", "core.AspNetUsers");
            DropForeignKey("core.PlayerCharacterCard", "Campaign_Id", "core.Campaign");
            DropIndex("core.PlayerCharacterCard", new[] { "Campaign_Id" });
            DropIndex("core.Campaign", new[] { "User_Id" });
            DropTable("core.PlayerCharacterCard");
            DropTable("core.Campaign");
        }
    }
}
