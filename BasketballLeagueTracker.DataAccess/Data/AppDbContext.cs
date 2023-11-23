using BasketballLeagueTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BasketballLeagueTracker.DataAccess.Data
{

    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<League> Leagues { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GamePlayerStats> GamePlayerStats { get; set; }

        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<SeasonStatistics> SeasonStatistics { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleImage> ArticaleImages { get; set; }

        public DbSet<FavouritePlayer> FavouritePlayers { get; set; }
        public DbSet<FavouriteTeam> FavouriteTeams { get; set; }
        public DbSet<FavouriteTeam> FavouriteLeagues { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserCommentRating> UserCommentRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Needed for IdentityDbContext

            ConfigureEntityRelationships(modelBuilder);
          

            modelBuilder.Entity<Player>().HasData(
                new Player { PlayerId = 1, Name = "Bartosz", Surname = "Późniewski", UniformNumber = 10, Positions = PlayerPosition.PointGuard | PlayerPosition.ShootingGuard, TeamId = 1, IsInTeam = true },
                new Player { PlayerId = 2, Name = "Tom", Surname = "Noname", UniformNumber = 20, Positions = PlayerPosition.ShootingGuard },
                new Player { PlayerId = 3, Name = "Test", Surname = "Example", UniformNumber = 30, Positions = PlayerPosition.Center }
                );

            modelBuilder.Entity<League>().HasData(
                new League { LeagueId = 1, Name = "Testowa liga", Description = "Liga 1 " }
              );
        }

        private void ConfigureEntityRelationships(ModelBuilder modelBuilder)
        {
            // Jedna liga ma wiele zespołów
            modelBuilder.Entity<League>()
                .HasMany(l => l.Teams)
                .WithOne(t => t.League)
                .OnDelete(DeleteBehavior.NoAction);
            // Jedna liga ma wiele meczów
            modelBuilder.Entity<League>()
                .HasMany(l => l.Games)
                .WithOne(g => g.League)
                .OnDelete(DeleteBehavior.NoAction);
            // Jedna drużyna ma wielu zawodników
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .OnDelete(DeleteBehavior.NoAction);
            // Jeden zawodnik - jedna drużyna
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);
            // Jedna drużyna ma jeden stadion, po usunięciu drużyny usuwamy stadion
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Stadium)
                .WithOne(s => s.Team)
                .HasForeignKey<Stadium>(s => s.TeamId) 
                .OnDelete(DeleteBehavior.Cascade);

            // Jedna drużyna ma wiele statystyk sezonu
            modelBuilder.Entity<SeasonStatistics>()
                .HasOne(ss => ss.Team)
                .WithMany(t => t.SeasonStatistics)
                .OnDelete(DeleteBehavior.NoAction);
            // Jedna liga ma wiele statystyk 
            modelBuilder.Entity<SeasonStatistics>()
                .HasOne(ss => ss.League)
                .WithMany(l => l.SeasonStatistics)
                .OnDelete(DeleteBehavior.NoAction);
            // Jeden użytkownik mopże posiadać wiele artykułów
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.Articles)
                .WithOne(a => a.User)
                .OnDelete(DeleteBehavior.NoAction);
            // Jeden użytkownik może mieć wiele komentarzy
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.Comments)
                .WithOne(c => c.User)
                .OnDelete(DeleteBehavior.NoAction);


            // Jeden komentarz może mieć wiele ocen komentarzy
            modelBuilder.Entity<Comment>()
                .HasMany(c => c.Ratings)
                .WithOne(r => r.Comment)
                .OnDelete(DeleteBehavior.Cascade);
            // Jedna ocena ma jednego użytkownika, który może mieć wiele ocen komentarzy
            modelBuilder.Entity<UserCommentRating>()
                .HasOne(ucr => ucr.User)
                .WithMany(u => u.UserCommentRaitings)
                .OnDelete(DeleteBehavior.NoAction);

            // Jeden artykuł może posaidać wiele zdjęć, jedno zdjęcie przypisane jest do danego artykułu
            modelBuilder.Entity<Article>()
                .HasMany(a => a.Images)
                .WithOne(i => i.Article)
                .OnDelete(DeleteBehavior.NoAction);
            // Jeden artykył przypisany jest do jednej ligi, liga może posiadać wiele artykułów
            modelBuilder.Entity<Article>()
                .HasOne(a => a.League)
                .WithMany(l => l.Articles)
                .HasForeignKey(a => a.LeagueId);

            modelBuilder.Entity<Article>()
                .HasMany(a => a.Comments)
                .WithOne(c => c.Article)
                .HasForeignKey(c => c.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            // //////////////////////////////////////////// FavouritePlayers

            modelBuilder.Entity<FavouritePlayer>()
                 .HasKey(up => new { up.UserId, up.PlayerId });

            modelBuilder.Entity<FavouritePlayer>()
                .HasOne(up => up.User)
                .WithMany(u => u.FavouritePlayers)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavouritePlayer>()
                .HasOne(up => up.Player)
                .WithMany(p => p.PlayerFollowers)
                .HasForeignKey(up => up.PlayerId);

            // ////////////////////////////////////////////  FavouriteTeams
            modelBuilder.Entity<FavouriteTeam>()
                .HasKey(up => new { up.UserId, up.TeamId });

            modelBuilder.Entity<FavouriteTeam>()
                .HasOne(up => up.User)
                .WithMany(u => u.FavouriteTeams)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavouriteTeam>()
                .HasOne(up => up.Team)
                .WithMany(p => p.TeamFollowers)
                .HasForeignKey(up => up.TeamId);

            // ////////////////////////////////////////////  FavouriteLeagues
            modelBuilder.Entity<FavouriteLeague>()
                .HasKey(up => new { up.UserId, up.LeagueId });

            modelBuilder.Entity<FavouriteLeague>()
                .HasOne(up => up.User)
                .WithMany(u => u.FavouriteLeagues)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<FavouriteLeague>()
                .HasOne(up => up.League)
                .WithMany(p => p.LeagueFollowers)
                .HasForeignKey(up => up.LeagueId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Game>()
                 .HasOne(g => g.HomeTeam)
                 .WithMany(ht => ht.HomeGames)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.AwayTeam)
                .WithMany(ht => ht.AwayGames)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Game>()
                .HasMany(g => g.GamePlayerStats)
                .WithOne(gps => gps.Game)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Player>()
                 .HasMany(p => p.PlayerStats)
                 .WithOne(ps => ps.Player)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GamePlayerStats>()
                .HasKey(g => new { g.PlayerId, g.GameId });

            modelBuilder.Entity<Player>()
                .HasMany(p => p.PlayerStats)
                .WithOne(gps => gps.Player)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Player>()
                .Property(p => p.Positions)
                 .HasConversion(
                        v => (byte)v,
                        v => (PlayerPosition)v
            );
        }

    }

    // Class for scaffolding
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            string serverCS = "Server=.;Database=BLTDb;Trusted_Connection=True;TrustServerCertificate = True";
            string localCS = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\BasketballLeagueTracker\\BasketballLeagueTracker.DataAccess\\Database\\BLTdatabase.mdf;Integrated Security=True";

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(localCS);

            return new AppDbContext(optionsBuilder.Options);
        }
    }

}
