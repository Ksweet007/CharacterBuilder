namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedPointlessPropertiesFromFeature : DbMigration
    {
        public override void Up()
        {
            DropColumn("core.Feature", "ActionType");
            DropColumn("core.Feature", "RecoveryType");
        }
        
        public override void Down()
        {
            AddColumn("core.Feature", "RecoveryType", c => c.String());
            AddColumn("core.Feature", "ActionType", c => c.String());
        }
    }
}
