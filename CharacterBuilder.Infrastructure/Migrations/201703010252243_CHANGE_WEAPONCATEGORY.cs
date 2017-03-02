namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CHANGE_WEAPONCATEGORY : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.WeaponCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("core.Weapon", "WeaponCategory_Id", c => c.Int());
            CreateIndex("core.Weapon", "WeaponCategory_Id");
            AddForeignKey("core.Weapon", "WeaponCategory_Id", "core.WeaponCategory", "Id");
            DropColumn("core.Weapon", "WeaponCategory");
        }
        
        public override void Down()
        {
            AddColumn("core.Weapon", "WeaponCategory", c => c.Int(nullable: false));
            DropForeignKey("core.Weapon", "WeaponCategory_Id", "core.WeaponCategory");
            DropIndex("core.Weapon", new[] { "WeaponCategory_Id" });
            DropColumn("core.Weapon", "WeaponCategory_Id");
            DropTable("core.WeaponCategory");
        }
    }
}
