namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Sheet_SCOREVALUEOBJ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.AbilityScoreSheetValue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.Int(nullable: false),
                        CharacterSheet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.CharacterSheet", t => t.CharacterSheet_Id)
                .Index(t => t.CharacterSheet_Id);
            
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
            DropForeignKey("core.AbilityScoreSheetValue", "CharacterSheet_Id", "core.CharacterSheet");
            DropIndex("core.AbilityScoreSheetValue", new[] { "CharacterSheet_Id" });
            DropTable("core.AbilityScoreSheetValue");
        }
    }
}
