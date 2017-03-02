namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SOMETHING_CHANGED_NOTSUREWHAT : DbMigration
    {
        public override void Up()
        {
            AddColumn("core.Class", "SkillPickCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("core.Class", "SkillPickCount");
        }
    }
}
