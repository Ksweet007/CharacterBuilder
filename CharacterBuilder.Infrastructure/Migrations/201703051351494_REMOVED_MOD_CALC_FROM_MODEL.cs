namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class REMOVED_MOD_CALC_FROM_MODEL : DbMigration
    {
        public override void Up()
        {
            AddColumn("core.CharacterSheet", "StrengthMod", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "DexterityMod", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "ConstitutionMod", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "WisdomMod", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "IntelligenceMod", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "CharismaMod", c => c.Int(nullable: false));
            AddColumn("core.CharacterSheet", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("core.CharacterSheet", "CreatedDate");
            DropColumn("core.CharacterSheet", "CharismaMod");
            DropColumn("core.CharacterSheet", "IntelligenceMod");
            DropColumn("core.CharacterSheet", "WisdomMod");
            DropColumn("core.CharacterSheet", "ConstitutionMod");
            DropColumn("core.CharacterSheet", "DexterityMod");
            DropColumn("core.CharacterSheet", "StrengthMod");
        }
    }
}
