namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Sheet_Ability : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.AbilityScoreValue", "AbilityScore_Id", "core.AbilityScore");
            DropForeignKey("core.CharacterSheet", "Charisma_Id", "core.AbilityScoreValue");
            DropForeignKey("core.CharacterSheet", "Constitution_Id", "core.AbilityScoreValue");
            DropForeignKey("core.CharacterSheet", "Dexterity_Id", "core.AbilityScoreValue");
            DropForeignKey("core.CharacterSheet", "Intelligence_Id", "core.AbilityScoreValue");
            DropForeignKey("core.CharacterSheet", "Strength_Id", "core.AbilityScoreValue");
            DropForeignKey("core.CharacterSheet", "Wisdom_Id", "core.AbilityScoreValue");
            DropIndex("core.CharacterSheet", new[] { "Charisma_Id" });
            DropIndex("core.CharacterSheet", new[] { "Constitution_Id" });
            DropIndex("core.CharacterSheet", new[] { "Dexterity_Id" });
            DropIndex("core.CharacterSheet", new[] { "Intelligence_Id" });
            DropIndex("core.CharacterSheet", new[] { "Strength_Id" });
            DropIndex("core.CharacterSheet", new[] { "Wisdom_Id" });
            DropIndex("core.AbilityScoreValue", new[] { "AbilityScore_Id" });
            AddColumn("core.CharacterSheet", "Strength", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "Dexterity", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "Constitution", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "Wisdom", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "Intelligence", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "Charisma", c => c.Int(nullable: false));
            DropColumn("core.CharacterSheet", "Charisma_Id");
            DropColumn("core.CharacterSheet", "Constitution_Id");
            DropColumn("core.CharacterSheet", "Dexterity_Id");
            DropColumn("core.CharacterSheet", "Intelligence_Id");
            DropColumn("core.CharacterSheet", "Strength_Id");
            DropColumn("core.CharacterSheet", "Wisdom_Id");
            DropTable("core.AbilityScoreValue");
        }
        
        public override void Down()
        {

            

        }
    }
}
