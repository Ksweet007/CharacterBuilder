namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstLevelgfhfgdhdfghgfhghfghfghf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.CharacterSheet", "FirstLevelTasks_Id", "core.FirstLevelTasks");
            DropIndex("core.CharacterSheet", new[] { "FirstLevelTasks_Id" });
            AddColumn("core.CharacterSheet", "FirstLevelTasks_HasIncreasedHp", c => c.Boolean(nullable: false));
            AddColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledStrength", c => c.Boolean(nullable: false));
            AddColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledDexterity", c => c.Boolean(nullable: false));
            AddColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledConstitution", c => c.Boolean(nullable: false));
            AddColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledIntelligence", c => c.Boolean(nullable: false));
            AddColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledWisdom", c => c.Boolean(nullable: false));
            AddColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledCharisma", c => c.Boolean(nullable: false));
            DropColumn("core.CharacterSheet", "FirstLevelTasks_Id");
            DropTable("core.FirstLevelTasks");
        }
        
        public override void Down()
        {
            CreateTable(
                "core.FirstLevelTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HasIncreasedHp = c.Boolean(nullable: false),
                        HasRolledStrength = c.Boolean(nullable: false),
                        HasRolledDexterity = c.Boolean(nullable: false),
                        HasRolledConstitution = c.Boolean(nullable: false),
                        HasRolledIntelligence = c.Boolean(nullable: false),
                        HasRolledWisdom = c.Boolean(nullable: false),
                        HasRolledCharisma = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("core.CharacterSheet", "FirstLevelTasks_Id", c => c.Int());
            DropColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledCharisma");
            DropColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledWisdom");
            DropColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledIntelligence");
            DropColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledConstitution");
            DropColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledDexterity");
            DropColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledStrength");
            DropColumn("core.CharacterSheet", "FirstLevelTasks_HasIncreasedHp");
            CreateIndex("core.CharacterSheet", "FirstLevelTasks_Id");
            AddForeignKey("core.CharacterSheet", "FirstLevelTasks_Id", "core.FirstLevelTasks", "Id");
        }
    }
}
