namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeatureAbilIm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.LevelChecklist",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Level = c.Int(nullable: false),
                        HasAbilityScoreIncrease = c.Boolean(nullable: false),
                        HasIncreasedHp = c.Boolean(nullable: false),
                        HasIncreasedAbilityScores = c.Boolean(nullable: false),
                        CharacterSheet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.CharacterSheet", t => t.CharacterSheet_Id)
                .Index(t => t.CharacterSheet_Id);
            
            DropColumn("core.CharacterSheet", "IsComplete");
        }
        
        public override void Down()
        {
            AddColumn("core.CharacterSheet", "IsComplete", c => c.Boolean(nullable: false));
            DropForeignKey("core.LevelChecklist", "CharacterSheet_Id", "core.CharacterSheet");
            DropIndex("core.LevelChecklist", new[] { "CharacterSheet_Id" });
            DropTable("core.LevelChecklist");
        }
    }
}
