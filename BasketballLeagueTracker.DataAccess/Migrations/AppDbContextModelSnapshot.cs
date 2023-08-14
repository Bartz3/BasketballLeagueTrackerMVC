﻿// <auto-generated />
using System;
using BasketballLeagueTracker.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BasketballLeagueTracker.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BasketballLeagueTracker.Models.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArticleId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ArticleId");

                    b.HasIndex("LeagueId");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.ArticleImage", b =>
                {
                    b.Property<int>("ArticleImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArticleImageId"));

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("ArticleImageId");

                    b.HasIndex("ArticleId");

                    b.ToTable("ArticaleImages");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CommentId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.FavouritePlayer", b =>
                {
                    b.Property<int>("FavouritePlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FavouritePlayerId"));

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FavouritePlayerId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("UserId");

                    b.ToTable("FavouritePlayers");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.FavouriteTeam", b =>
                {
                    b.Property<int>("FavouriteTeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FavouriteTeamId"));

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FavouriteTeamId");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("FavouriteTeams");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameId"));

                    b.Property<int?>("AwayTeamId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("AwayTeamScore")
                        .HasColumnType("int");

                    b.Property<DateTime>("GameDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("HomeTeamId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamScore")
                        .HasColumnType("int");

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("GameId");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("LeagueId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.GamePlayerStats", b =>
                {
                    b.Property<int>("GamePlayerStatsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GamePlayerStatsId"));

                    b.Property<int?>("Assists")
                        .HasColumnType("int");

                    b.Property<int?>("Blocks")
                        .HasColumnType("int");

                    b.Property<int?>("DefensiveRebounds")
                        .HasColumnType("int");

                    b.Property<int?>("Fouls")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsOnBench")
                        .HasColumnType("bit");

                    b.Property<int?>("OffensiveRebounds")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int?>("Points")
                        .HasColumnType("int");

                    b.Property<int?>("Rebounds")
                        .HasColumnType("int");

                    b.Property<int?>("Steals")
                        .HasColumnType("int");

                    b.Property<int?>("TimeSpend")
                        .HasColumnType("int");

                    b.Property<int?>("Turnovers")
                        .HasColumnType("int");

                    b.HasKey("GamePlayerStatsId");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId");

                    b.ToTable("GamePlayerStats");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.League", b =>
                {
                    b.Property<int>("LeagueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LeagueId"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LeaugeMVPPlayerId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Logo")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Season")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("LeagueId");

                    b.HasIndex("LeaugeMVPPlayerId");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerId"));

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GamesPlayed")
                        .HasColumnType("int");

                    b.Property<int?>("Height")
                        .HasColumnType("int");

                    b.Property<bool>("IsInTeam")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte>("Positions")
                        .HasColumnType("tinyint");

                    b.Property<string>("Surname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<int?>("UniformNumber")
                        .HasColumnType("int");

                    b.Property<double?>("Weight")
                        .HasColumnType("float");

                    b.HasKey("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            PlayerId = 1,
                            IsInTeam = true,
                            Name = "Bartosz",
                            Positions = (byte)6,
                            Surname = "Późniewski",
                            TeamId = 1,
                            UniformNumber = 10
                        },
                        new
                        {
                            PlayerId = 2,
                            IsInTeam = false,
                            Name = "Tom",
                            Positions = (byte)4,
                            Surname = "Noname",
                            UniformNumber = 20
                        },
                        new
                        {
                            PlayerId = 3,
                            IsInTeam = false,
                            Name = "Test",
                            Positions = (byte)32,
                            Surname = "Example",
                            UniformNumber = 30
                        });
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.SeasonStatistics", b =>
                {
                    b.Property<int>("SeasonStatisticsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SeasonStatisticsId"));

                    b.Property<int>("GamesPlayed")
                        .HasColumnType("int");

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<int>("LeaguePoints")
                        .HasColumnType("int");

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<double>("OpponentPointsPerGame")
                        .HasColumnType("float");

                    b.Property<double>("PointsPerGame")
                        .HasColumnType("float");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("SeasonStatisticsId");

                    b.HasIndex("LeagueId");

                    b.HasIndex("TeamId");

                    b.ToTable("SeasonStatistics");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Stadium", b =>
                {
                    b.Property<int>("StadiumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StadiumId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StadiumId");

                    b.ToTable("Stadiums");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeamId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsCurrentlyPlaying")
                        .HasColumnType("bit");

                    b.Property<int?>("LeagueId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StadiumId")
                        .HasColumnType("int");

                    b.Property<byte[]>("TeamLogo")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("TeamId");

                    b.HasIndex("LeagueId");

                    b.HasIndex("StadiumId")
                        .IsUnique()
                        .HasFilter("[StadiumId] IS NOT NULL");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.UserCommentRating", b =>
                {
                    b.Property<int>("UserCommentRatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserCommentRatingId"));

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Rating")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserCommentRatingId");

                    b.HasIndex("CommentId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCommentRatings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("NotificationBool")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Article", b =>
                {
                    b.HasOne("BasketballLeagueTracker.Models.League", "League")
                        .WithMany()
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BasketballLeagueTracker.Models.ApplicationUser", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("League");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.ArticleImage", b =>
                {
                    b.HasOne("BasketballLeagueTracker.Models.Article", "Article")
                        .WithMany("Images")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Article");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Comment", b =>
                {
                    b.HasOne("BasketballLeagueTracker.Models.ApplicationUser", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.FavouritePlayer", b =>
                {
                    b.HasOne("BasketballLeagueTracker.Models.Player", "Player")
                        .WithMany("FavouritePlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BasketballLeagueTracker.Models.ApplicationUser", "User")
                        .WithMany("FavouritePlayers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.FavouriteTeam", b =>
                {
                    b.HasOne("BasketballLeagueTracker.Models.Team", "Team")
                        .WithMany("FavouriteTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BasketballLeagueTracker.Models.ApplicationUser", "User")
                        .WithMany("FavouriteTeams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Game", b =>
                {
                    b.HasOne("BasketballLeagueTracker.Models.Team", "AwayTeam")
                        .WithMany("AwayGames")
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BasketballLeagueTracker.Models.Team", "HomeTeam")
                        .WithMany("HomeGames")
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BasketballLeagueTracker.Models.League", "League")
                        .WithMany("Games")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AwayTeam");

                    b.Navigation("HomeTeam");

                    b.Navigation("League");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.GamePlayerStats", b =>
                {
                    b.HasOne("BasketballLeagueTracker.Models.Game", "Game")
                        .WithMany("GamePlayerStats")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BasketballLeagueTracker.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.League", b =>
                {
                    b.HasOne("BasketballLeagueTracker.Models.Player", "LeaugeMVP")
                        .WithMany()
                        .HasForeignKey("LeaugeMVPPlayerId");

                    b.Navigation("LeaugeMVP");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Player", b =>
                {
                    b.HasOne("BasketballLeagueTracker.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Team");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.SeasonStatistics", b =>
                {
                    b.HasOne("BasketballLeagueTracker.Models.League", "League")
                        .WithMany("SeasonStatistics")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BasketballLeagueTracker.Models.Team", "Team")
                        .WithMany("SeasonStatistics")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("League");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Team", b =>
                {
                    b.HasOne("BasketballLeagueTracker.Models.League", "League")
                        .WithMany("Teams")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("BasketballLeagueTracker.Models.Stadium", "Stadium")
                        .WithOne("Team")
                        .HasForeignKey("BasketballLeagueTracker.Models.Team", "StadiumId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("League");

                    b.Navigation("Stadium");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.UserCommentRating", b =>
                {
                    b.HasOne("BasketballLeagueTracker.Models.Comment", "Comment")
                        .WithMany("Ratings")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BasketballLeagueTracker.Models.ApplicationUser", "User")
                        .WithMany("UserCommentRaitings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Article", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Comment", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Game", b =>
                {
                    b.Navigation("GamePlayerStats");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.League", b =>
                {
                    b.Navigation("Games");

                    b.Navigation("SeasonStatistics");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Player", b =>
                {
                    b.Navigation("FavouritePlayers");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Stadium", b =>
                {
                    b.Navigation("Team")
                        .IsRequired();
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.Team", b =>
                {
                    b.Navigation("AwayGames");

                    b.Navigation("FavouriteTeams");

                    b.Navigation("HomeGames");

                    b.Navigation("Players");

                    b.Navigation("SeasonStatistics");
                });

            modelBuilder.Entity("BasketballLeagueTracker.Models.ApplicationUser", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("Comments");

                    b.Navigation("FavouritePlayers");

                    b.Navigation("FavouriteTeams");

                    b.Navigation("UserCommentRaitings");
                });
#pragma warning restore 612, 618
        }
    }
}
