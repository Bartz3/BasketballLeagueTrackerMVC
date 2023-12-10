jQuery(document).ready(function ($) {
    function calculateAndDisplayTeamStats(teamTableSelector, resultPrefix) {
        var stats = {
            Points: 0,
            FGM: 0,
            FGA: 0,
            PM3: 0,
            PA3: 0,
            FTM: 0,
            FTA: 0,
            Rebounds: 0,
            DefensiveRebounds: 0,
            OffensiveRebounds: 0,
            Assists: 0,
            Steals: 0,
            Blocks: 0,
            Turnovers: 0,
            // Dodatkowe statystyki dla całej drużyny
            TotalFGM: 0,
            TotalFGA: 0,
            TotalPM3: 0,
            TotalPA3: 0,
            TotalFTM: 0,
            TotalFTA: 0
        };


        // Nowe zmienne dla obliczania procentów
        var total2PPercent = 0, total3PPercent = 0, totalFTPercent = 0;
        var playersCount = 0; // Liczba zawodników, którzy oddali rzuty

        $(teamTableSelector + ' tbody tr').each(function () {
            var cells = $(this).find('td');
            stats.Points += parseInt(cells.eq(2).text()) || 0;
            stats.FGM += parseInt(cells.eq(3).text()) || 0;
            stats.FGA += parseInt(cells.eq(4).text()) || 0;
            stats.PM3 += parseInt(cells.eq(6).text()) || 0;
            stats.PA3 += parseInt(cells.eq(7).text()) || 0;
            stats.FTM += parseInt(cells.eq(9).text()) || 0;
            stats.FTA += parseInt(cells.eq(10).text()) || 0;
            stats.Rebounds += parseInt(cells.eq(12).text()) || 0;
            stats.DefensiveRebounds += parseInt(cells.eq(13).text()) || 0;
            stats.OffensiveRebounds += parseInt(cells.eq(14).text()) || 0;
            stats.Assists += parseInt(cells.eq(15).text()) || 0;
            stats.Steals += parseInt(cells.eq(16).text()) || 0;
            stats.Blocks += parseInt(cells.eq(17).text()) || 0;
            stats.Turnovers += parseInt(cells.eq(18).text()) || 0;


            var player2PPercent = (parseInt(cells.eq(3).text()) / parseInt(cells.eq(4).text())) || 0;
            var player3PPercent = (parseInt(cells.eq(6).text()) / parseInt(cells.eq(7).text())) || 0;
            var playerFTPercent = (parseInt(cells.eq(9).text()) / parseInt(cells.eq(10).text())) || 0;

            // Wyświetlanie procentów dla zawodnika
            cells.eq(5).text((player2PPercent * 100).toFixed(1)); // 2P %
            cells.eq(8).text((player3PPercent * 100).toFixed(1)); // 3P %
            cells.eq(11).text((playerFTPercent * 100).toFixed(1)); // FT %

            // Sumowanie procentów dla całej drużyny
            //total2PPercent += player2PPercent;
            //total3PPercent += player3PPercent;
            //totalFTPercent += playerFTPercent;
            //playersCount++;

            // Dodawanie do sumy drużynowej
            stats.TotalFGM += parseInt(cells.eq(3).text()) || 0;
            stats.TotalFGA += parseInt(cells.eq(4).text()) || 0;
            stats.TotalPM3 += parseInt(cells.eq(6).text()) || 0;
            stats.TotalPA3 += parseInt(cells.eq(7).text()) || 0;
            stats.TotalFTM += parseInt(cells.eq(9).text()) || 0;
            stats.TotalFTA += parseInt(cells.eq(10).text()) || 0;
        });

        for (var key in stats) {
            $('#' + resultPrefix + '_' + key).text(stats[key]);
        }

        // Obliczanie procentów dla całej drużyny
        var team2PPercent = (stats.TotalFGM / stats.TotalFGA) * 100 || 0;
        var team3PPercent = (stats.TotalPM3 / stats.TotalPA3) * 100 || 0;
        var teamFTPercent = (stats.TotalFTM / stats.TotalFTA) * 100 || 0;

        // Wyświetlanie procentów dla drużyny
        $('#' + resultPrefix + '_2P_Percent').text(team2PPercent.toFixed(1) + '%');
        $('#' + resultPrefix + '_3P_Percent').text(team3PPercent.toFixed(1) + '%');
        $('#' + resultPrefix + '_FT_Percent').text(teamFTPercent.toFixed(1) + '%');
    }
    calculateAndDisplayTeamStats('#homeTeamTable', 'HomeTeam');
    calculateAndDisplayTeamStats('#awayTeamTable', 'AwayTeam');
});

