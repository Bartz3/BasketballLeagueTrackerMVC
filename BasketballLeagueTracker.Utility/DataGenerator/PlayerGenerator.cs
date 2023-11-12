using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballLeagueTracker.Models;
using Bogus;
using Bogus.DataSets;

namespace BasketballLeagueTracker.Utility.DataGenerator
{
    public enum Gender
    {
        Male,
        Female
    }
    public class PlayerGenerator
    {
        private Faker<Player> fakePlayerModel;
        private Random _random = new Random();
        public PlayerGenerator()
        {
            Randomizer.Seed = new Random();

            fakePlayerModel = new Faker<Player>("pl")
                 .RuleFor(p => p.Name, f => f.Person.FirstName)
                 .RuleFor(p => p.Surname, f => f.Person.LastName)
                 /*.RuleFor(p => p.Photo, f => f.Random.Bytes(100))*/ 
                 .RuleFor(p => p.Birthday, f => f.Person.DateOfBirth)
                 .RuleFor(p => p.UniformNumber, f => f.Random.Number(1, 99))
                 .RuleFor(p => p.Height, f => f.Random.Number(170, 215))
                 .RuleFor(p => p.Positions, (f, p) =>
                 {
                     if (p.Height <= 185)
                         return PlayerPosition.PointGuard;
                     else if (p.Height <= 190)
                         return PlayerPosition.ShootingGuard;
                     else if (p.Height <= 195)
                         return PlayerPosition.SmallForward;
                     else if (p.Height <= 200)
                         return PlayerPosition.PowerForward;
                     else
                         return PlayerPosition.Center;
                 })
                 .RuleFor(p => p.Weight, f => Math.Round(f.Random.Double(50, 120), 1))
                 .RuleFor(p => p.Country, f => "Polska")
                 .RuleFor(p => p.IsInTeam, f => true)
                 .RuleFor(p=>p.TeamId,f=>9);

        }

        public Player GeneratePlayer()
        {
            return fakePlayerModel.Generate();
        }
    }
}
