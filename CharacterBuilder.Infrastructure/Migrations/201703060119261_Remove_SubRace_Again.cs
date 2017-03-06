namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_SubRace_Again : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.Subrace", "Race_Id", "core.Race");
            DropIndex("core.Subrace", new[] { "Race_Id" });
            DropTable("core.Subrace");
        }
        
        public override void Down()
        {
            CreateTable(
                "core.Subrace",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Race_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("core.Subrace", "Race_Id");
            AddForeignKey("core.Subrace", "Race_Id", "core.Race", "Id");
        }
    }
}
