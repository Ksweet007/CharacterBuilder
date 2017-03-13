namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeatureAbilImprove : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "core.ClassAbilityScoreIncreases", newName: "AbilityScoreImprovement");
            CreateTable(
                "core.ClassFeatureBonus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IncreaseType = c.String(),
                        IncreaseValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("core.Feature", "FeatureBonus_Id", c => c.Int());
            CreateIndex("core.Feature", "FeatureBonus_Id");
            AddForeignKey("core.Feature", "FeatureBonus_Id", "core.ClassFeatureBonus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("core.Feature", "FeatureBonus_Id", "core.ClassFeatureBonus");
            DropIndex("core.Feature", new[] { "FeatureBonus_Id" });
            DropColumn("core.Feature", "FeatureBonus_Id");
            DropTable("core.ClassFeatureBonus");
            RenameTable(name: "core.AbilityScoreImprovement", newName: "ClassAbilityScoreIncreases");
        }
    }
}
