namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SheetRehaul : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.AbilityScoreSheetValue", "CharacterSheet_Id", "core.CharacterSheet");
            DropIndex("core.AbilityScoreSheetValue", new[] { "CharacterSheet_Id" });
            AddColumn("core.AbilityScoreIncrease", "CharacterSheet_Id", c => c.Int());
            AddColumn("core.ToDo", "HasSelectedSubRace", c => c.Boolean(nullable: false));
            CreateIndex("core.AbilityScoreIncrease", "CharacterSheet_Id");
            AddForeignKey("core.AbilityScoreIncrease", "CharacterSheet_Id", "core.CharacterSheet", "Id");
            DropColumn("core.CharacterSheet", "StrengthMod");
            DropColumn("core.CharacterSheet", "DexterityMod");
            DropColumn("core.CharacterSheet", "ConstitutionMod");
            DropColumn("core.CharacterSheet", "WisdomMod");
            DropColumn("core.CharacterSheet", "IntelligenceMod");
            DropColumn("core.CharacterSheet", "CharismaMod");
            DropTable("core.AbilityScoreSheetValue");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("core.CharacterSheet", "CharismaMod", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "IntelligenceMod", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "WisdomMod", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "ConstitutionMod", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "DexterityMod", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "StrengthMod", c => c.Int(nullable: false));
            DropForeignKey("core.AbilityScoreIncrease", "CharacterSheet_Id", "core.CharacterSheet");
            DropIndex("core.AbilityScoreIncrease", new[] { "CharacterSheet_Id" });
            DropColumn("core.ToDo", "HasSelectedSubRace");
            DropColumn("core.AbilityScoreIncrease", "CharacterSheet_Id");
            CreateIndex("core.AbilityScoreSheetValue", "CharacterSheet_Id");
            AddForeignKey("core.AbilityScoreSheetValue", "CharacterSheet_Id", "core.CharacterSheet", "Id");
        }
    }
}
