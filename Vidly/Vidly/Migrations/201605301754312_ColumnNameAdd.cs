using System.Data.Entity.Core.Metadata.Edm;
using System.Web.Configuration;

namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnNameAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "Name", c => c.String());
            Sql("UPDATE MembershipTypes SET Name='Pay as You Go' WHERE Id =1");
            Sql("UPDATE MembershipTypes SET Name='Monthly' WHERE Id =2");
            Sql("UPDATE MembershipTypes SET Name='Quarterly' WHERE Id =3");
            Sql("UPDATE MembershipTypes SET Name='Yearly' WHERE Id =4");


        }

        public override void Down()
        {
            DropColumn("dbo.MembershipTypes", "Name");
        }
    }
}
