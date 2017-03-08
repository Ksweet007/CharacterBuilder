namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttemptToMakeCOmplexTypeDoShit : DbMigration
    {
        public override void Up()
        {
            AddColumn("core.CharacterSheet", "AbilityScores_Strength", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "AbilityScores_Dexterity", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "AbilityScores_Constitution", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "AbilityScores_Wisdom", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "AbilityScores_Intelligence", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "AbilityScores_Charisma", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("core.CharacterSheet", "AbilityScores_Charisma");
            DropColumn("core.CharacterSheet", "AbilityScores_Intelligence");
            DropColumn("core.CharacterSheet", "AbilityScores_Wisdom");
            DropColumn("core.CharacterSheet", "AbilityScores_Constitution");
            DropColumn("core.CharacterSheet", "AbilityScores_Dexterity");
            DropColumn("core.CharacterSheet", "AbilityScores_Strength");
        }
    }
}
