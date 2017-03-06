namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Sheet_AbilityScore_Obj : DbMigration
    {
        public override void Up()
        {
            AddColumn("core.CharacterSheet", "Charisma_Id", c => c.Int());
            AddColumn("core.CharacterSheet", "Constitution_Id", c => c.Int());
            AddColumn("core.CharacterSheet", "Dexterity_Id", c => c.Int());
            AddColumn("core.CharacterSheet", "Intelligence_Id", c => c.Int());
            AddColumn("core.CharacterSheet", "Strength_Id", c => c.Int());
            AddColumn("core.CharacterSheet", "Wisdom_Id", c => c.Int());
            CreateIndex("core.CharacterSheet", "Charisma_Id");
            CreateIndex("core.CharacterSheet", "Constitution_Id");
            CreateIndex("core.CharacterSheet", "Dexterity_Id");
            CreateIndex("core.CharacterSheet", "Intelligence_Id");
            CreateIndex("core.CharacterSheet", "Strength_Id");
            CreateIndex("core.CharacterSheet", "Wisdom_Id");
            AddForeignKey("core.CharacterSheet", "Charisma_Id", "core.AbilityScore", "Id");
            AddForeignKey("core.CharacterSheet", "Constitution_Id", "core.AbilityScore", "Id");
            AddForeignKey("core.CharacterSheet", "Dexterity_Id", "core.AbilityScore", "Id");
            AddForeignKey("core.CharacterSheet", "Intelligence_Id", "core.AbilityScore", "Id");
            AddForeignKey("core.CharacterSheet", "Strength_Id", "core.AbilityScore", "Id");
            AddForeignKey("core.CharacterSheet", "Wisdom_Id", "core.AbilityScore", "Id");
            DropColumn("core.CharacterSheet", "Strength");
            DropColumn("core.CharacterSheet", "Dexterity");
            DropColumn("core.CharacterSheet", "Constitution");
            DropColumn("core.CharacterSheet", "Wisdom");
            DropColumn("core.CharacterSheet", "Intelligence");
            DropColumn("core.CharacterSheet", "Charisma");
        }
        
        public override void Down()
        {
            AddColumn("core.CharacterSheet", "Charisma", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "Intelligence", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "Wisdom", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "Constitution", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "Dexterity", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "Strength", c => c.Int(nullable: false));
            DropForeignKey("core.CharacterSheet", "Wisdom_Id", "core.AbilityScore");
            DropForeignKey("core.CharacterSheet", "Strength_Id", "core.AbilityScore");
            DropForeignKey("core.CharacterSheet", "Intelligence_Id", "core.AbilityScore");
            DropForeignKey("core.CharacterSheet", "Dexterity_Id", "core.AbilityScore");
            DropForeignKey("core.CharacterSheet", "Constitution_Id", "core.AbilityScore");
            DropForeignKey("core.CharacterSheet", "Charisma_Id", "core.AbilityScore");
            DropIndex("core.CharacterSheet", new[] { "Wisdom_Id" });
            DropIndex("core.CharacterSheet", new[] { "Strength_Id" });
            DropIndex("core.CharacterSheet", new[] { "Intelligence_Id" });
            DropIndex("core.CharacterSheet", new[] { "Dexterity_Id" });
            DropIndex("core.CharacterSheet", new[] { "Constitution_Id" });
            DropIndex("core.CharacterSheet", new[] { "Charisma_Id" });
            DropColumn("core.CharacterSheet", "Wisdom_Id");
            DropColumn("core.CharacterSheet", "Strength_Id");
            DropColumn("core.CharacterSheet", "Intelligence_Id");
            DropColumn("core.CharacterSheet", "Dexterity_Id");
            DropColumn("core.CharacterSheet", "Constitution_Id");
            DropColumn("core.CharacterSheet", "Charisma_Id");
        }
    }
}
