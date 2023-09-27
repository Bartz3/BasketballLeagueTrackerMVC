using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.Models.ModelsValidation
{
    public class GameValidator : AbstractValidator<Game>
    {
        public GameValidator()
        {
            //RuleFor(x => x.AwayTeamId)
            //    .NotEqual(x => x.HomeTeamId)
            //    .WithMessage("Nie");
            //RuleFor(x => x.HomeTeamId)
            //    .NotEqual(x => x.AwayTeamId)
            //    .WithMessage("Nie");
            RuleFor(x => x.AwayTeamScore)
                .InclusiveBetween(0, 256).
                WithMessage("Wynik powinien być w zakresie od 0 do 256.");

            RuleFor(x => x.HomeTeamScore)
                .InclusiveBetween(0, 256)
                .WithMessage("Wynik powinien być w zakresie od 0 do 256.");

            //RuleFor(x => x.AwayTeamScore)
            //    .LessThan(256).WithMessage("Cześć")
            //    .GreaterThan(0).WithMessage("Wynik xzxd");

            //RuleFor(x => x.HomeTeamScore)
            //    .LessThan(256).WithMessage("Cześć2")
            //    .GreaterThan(0).WithMessage("Wynik xzxd");

        }
    }
}
