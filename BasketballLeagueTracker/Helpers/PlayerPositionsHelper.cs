using BasketballLeagueTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public static List<SelectListItem> GetPlayerPositions(Player player)
        {

            string[] elements =new string[6];
            if (player != null)
            {
                elements= player.Positions.ToString().Split(new char[] { '|' });

            }
            var playerPositions = Enum.GetValues(typeof(PlayerPosition)).Cast<PlayerPosition>();
            var selectListItems = playerPositions.Select(position =>
            {
                var displayAttribute = position.GetType().GetMember(position.ToString())
                    .FirstOrDefault()?.GetCustomAttributes(false)
                    .OfType<System.ComponentModel.DataAnnotations.DisplayAttribute>()
                    .FirstOrDefault();

                var name = displayAttribute?.GetName() ?? position.ToString();
                bool isSelected = false;
                if (player != null)
                {
                    if (elements.Contains(position.ToString()))
                    {
                        isSelected = true;
                    }
                }
                return new SelectListItem { Value = ((int)position).ToString(), Text = name, Selected = isSelected };
            }).ToList();

            return selectListItems;
        }

    }
}
