namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAbilityScoreClassLevelAssoc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.ClassAbilityScoreIncreases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LevelObtained = c.Int(nullable: false),
                        Class_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Class", t => t.Class_Id)
                .Index(t => t.Class_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("core.ClassAbilityScoreIncreases", "Class_Id", "core.Class");
            DropIndex("core.ClassAbilityScoreIncreases", new[] { "Class_Id" });
            DropTable("core.ClassAbilityScoreIncreases");
        }
    }
}
