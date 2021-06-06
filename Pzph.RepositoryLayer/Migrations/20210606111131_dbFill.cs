using Microsoft.EntityFrameworkCore.Migrations;

namespace Pzph.RepositoryLayer.Migrations
{
    public partial class dbFill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"INSERT INTO Category (Id, Name, CreatedAt) VALUES
                ('turystyka', 'Turystyka', GETDATE()),
                ('transport', 'Transport', GETDATE()),
                ('budownictwo', 'Budownictwo', GETDATE()),
                ('gastronomia', 'Gastronomia', GETDATE()),
                ('it', 'IT', GETDATE()),
                ('motoryzacja', 'Motoryzacja', GETDATE())
 
                INSERT INTO AspNetUsers (Id, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount) VALUES
                ('user', 0, 0, 0 , 0, 0)

                INSERT INTO Contractors(Id, UserId, CreatedAt) VALUES
                ('contractor', 'user', GETDATE())

                INSERT INTO Services (Id, ContractorId, Name, Description, CreatedAt, CategoryId) VALUES
                (NEWID(), 'contractor', 'Tynkarstwo', 'Najlepsze tynki w mieście', GETDATE(), 'budownictwo'),
                (NEWID(), 'contractor', 'Wycieczki', 'Słoneczna Italia', GETDATE(), 'turystyka'),
                (NEWID(), 'contractor', 'Przeprowadzki', 'Z nami przeprowadzka to żaden kłopot', GETDATE(), 'transport'),
                (NEWID(), 'contractor', 'Pizzeria', 'Oferujemy oryginalną włoską pizzę', GETDATE(), 'gastronomia'),
                (NEWID(), 'contractor', 'Karting', 'Wspaniała zabawa na torze kartingowym. Zwietna rozrywka dla całej rodziny.', GETDATE(), 'motoryzacja'),
                (NEWID(), 'contractor', 'Kodowanie na żądanie', 'Aplikacje skrojone na każdą miarę. Krótkie terminy.', GETDATE(), 'it'),
                (NEWID(), 'contractor', 'Wynajem autobusu', 'Najlepsze autobusy', GETDATE(), 'turystyka'),
                (NEWID(), 'contractor', 'Mursrstwo', 'Najlepsze cegły w mieście', GETDATE(), 'budownictwo'),
                (NEWID(), 'contractor', 'Strony internetowe', 'Najlepsze strony internetowe w mieście', GETDATE(), 'it'),
                (NEWID(), 'contractor', 'Pojekty sieci', 'Porojektowanie sieci w domach i mieszkaniach', GETDATE(), 'it'),
                (NEWID(), 'contractor', 'Wynajem samochodów', 'Wypożyczalnia samochodów na żądanie', GETDATE(), 'motoryzacja') "
              );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
