namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBonusTypeFeature : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.ClassFeatureBonusType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Feature_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Feature", t => t.Feature_Id)
                .Index(t => t.Feature_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("core.ClassFeatureBonusType", "Feature_Id", "core.Feature");
            DropIndex("core.ClassFeatureBonusType", new[] { "Feature_Id" });
            DropTable("core.ClassFeatureBonusType");
        }
    }
}
