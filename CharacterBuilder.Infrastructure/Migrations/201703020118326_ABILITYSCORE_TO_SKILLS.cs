namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ABILITYSCORE_TO_SKILLS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.AbilityScore",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("core.Skill", "AbilityScore_Id", c => c.Int());
            CreateIndex("core.Skill", "AbilityScore_Id");
            AddForeignKey("core.Skill", "AbilityScore_Id", "core.AbilityScore", "Id");
            DropColumn("core.Skill", "AbilityScore");
        }
        
        public override void Down()
        {
            AddColumn("core.Skill", "AbilityScore", c => c.Int(nullable: false));
            DropForeignKey("core.Skill", "AbilityScore_Id", "core.AbilityScore");
            DropIndex("core.Skill", new[] { "AbilityScore_Id" });
            DropColumn("core.Skill", "AbilityScore_Id");
            DropTable("core.AbilityScore");
        }
    }
}
