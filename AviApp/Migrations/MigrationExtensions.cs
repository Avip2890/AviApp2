using Microsoft.EntityFrameworkCore.Migrations;

namespace AviApp.Migrations;

public static class MigrationExtensions
{
    public static bool IsForeignKeyExists(this MigrationBuilder migrationBuilder, string tableName, string foreignKeyName)
    {
       
        return true; 
    }
}
