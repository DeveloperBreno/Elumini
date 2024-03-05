using Microsoft.EntityFrameworkCore;
using Model;

using (var db = new EluminiDbContext())
{
    db.Database.Migrate();
    Console.WriteLine("Migrations applied successfully!");
}
