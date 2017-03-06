namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_Extrenious_HP_Info : DbMigration
    {
        public override void Up()
        {
            DropColumn("core.Class", "Hpatfirstlevel");
            DropColumn("core.Class", "Hpathigherlevels");
        }
        
        public override void Down()
        {
            AddColumn("core.Class", "Hpathigherlevels", c => c.String());
            AddColumn("core.Class", "Hpatfirstlevel", c => c.String());
        }
    }
}
