using BasketballLeagueTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BasketballLeagueTracker.Helpers
{
    public static class PlayerPositionsHelper
    {
        /// <summary>
        /// Function to display PlayerPositions with DisplayAttributes
        /// </summary>
        /// <returns>
        /// SelectListItem with DisplayAttributes
        /// </returns>
        public static List<SelectListItem> GetPlayerPositions()
        {
            var playerPositions = Enum.GetValues(typeof(PlayerPosition)).Cast<PlayerPosition>();
            var selectListItems = playerPositions.Select(position =>
            {
                var displayAttribute = position.GetType().GetMember(position.ToString())
                    .FirstOrDefault()?.GetCustomAttributes(false)
                    .OfType<System.ComponentModel.DataAnnotations.DisplayAttribute>()
                    .FirstOrDefault();

                var name = displayAttribute?.GetName() ?? position.ToString();
                return new SelectListItem { Value = ((int)position).ToString(), Text = name };
            }).ToList();

            return selectListItems;
        }

    }
}
