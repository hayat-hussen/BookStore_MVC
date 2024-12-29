using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.DataAccess.Migrations
{
   
    /// This class is for adding the Categories table to the database.
 
    public partial class AddCategoryTableDb : Migration
    {
     
        /// This method creates the Categories table when the migration is applied.
  
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories", // Name of the table
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false) // Unique identifier for each category
                        .Annotation("SqlServer:Identity", "1, 1"), // Auto-increment value
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false), // Name of the category
                    DisplayOrder = table.Column<int>(type: "int", nullable: false) // Order in which categories are displayed
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id); // Set Id as the primary key
                });
        }

    
        /// This method removes the Categories table when the migration is rolled back.
     
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories"); // Drop the Categories table
        }
    }
}