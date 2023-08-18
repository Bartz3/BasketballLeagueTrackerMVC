﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballLeagueTracker.Models;
using Bogus;

namespace BasketballLeagueTracker.Utility.DataGenerator
{
    public class TeamGenerator
    {
        private Faker<Team> fakeTeamModel;

        public TeamGenerator()
        {
            Randomizer.Seed = new Random(111); // Reproducible results

            var teamNames = new[] { "Orły", "Jastrzębie", "Tygrysy", "Lwy", "Błyskawice", "Gwiazdy", "Wilkolaki", "Rycerze" };

            fakeTeamModel = new Bogus.Faker<Team>("pl")
           .RuleFor(t => t.Name, f => f.PickRandom(teamNames) + " " + f.Address.City()) // generuje nazwę drużyny z losowego wyboru z listy nazw i dodaje nazwę miasta
           .RuleFor(t => t.Description, f => f.Lorem.Paragraph())
           .RuleFor(t => t.TeamLogo, f => DataUriToByteArray(f.Image.DataUri(200, 200)));

              //.RuleFor(t => t.TeamLogo, f => f.Random.Bytes(200));

        }

        public Team GenerateTeam()
        {
            return fakeTeamModel.Generate();
        }
        public List<Team> GenerateTeams(int amount)
        {
            return fakeTeamModel.Generate(amount);
        }

 public static byte[] DataUriToByteArray(string dataUri)
{
    // Upewniamy się, że mamy dane w formacie DataUri i następnie je dzielimy.
    if (dataUri.Contains(","))
    {
        string base64Data = dataUri.Split(',')[1];
        return Convert.FromBase64String(base64Data);
    }
    else
    {
        throw new FormatException("Invalid DataUri format.");
    }
}


    }
}