
$(document).ready(function () {
    function updateScores() {
        var homeTeamTotal = 0;
        var awayTeamTotal = 0;

        // Sumowanie punktów dla drużyny gospodarzy
        $('input[name^="HomeTeamGPS["]').each(function () {
            if ($(this).attr('name').includes(".Points")) {
                homeTeamTotal += parseInt($(this).val()) || 0;
            }
        });

        // Sumowanie punktów dla drużyny gości
        $('input[name^="AwayTeamGPS["]').each(function () {
            if ($(this).attr('name').includes(".Points")) {
                awayTeamTotal += parseInt($(this).val()) || 0;
            }
        });

        // Aktualizacja pól wynikowych
        $('#Game_HomeTeamScore').val(homeTeamTotal);
        $('#Game_AwayTeamScore').val(awayTeamTotal);
    }

    // Nasłuchiwanie zmian w polach punktowych
    $('input[name$=".Points"]').on('input', function () {
        updateScores();
    });

    // Początkowe obliczenie wyników
    updateScores();
});

$(document).ready(function () {
    function updateTeamStats() {
        var stats = {
            'HomeTeam': { 'TimeSpend': 0, 'Points': 0, 'Rebounds': 0, 'DefensiveRebounds': 0, 'OffensiveRebounds': 0, 'Assists': 0, 'Steals': 0, 'Blocks': 0, 'Turnovers': 0, 'FGA': 0, 'FGM': 0, 'PM3': 0, 'PA3': 0, 'FTA': 0, 'FTM': 0 },
            'AwayTeam': { 'TimeSpend': 0, 'Points': 0, 'Rebounds': 0, 'DefensiveRebounds': 0, 'OffensiveRebounds': 0, 'Assists': 0, 'Steals': 0, 'Blocks': 0, 'Turnovers': 0, 'FGA': 0, 'FGM': 0, 'PM3': 0, 'PA3': 0, 'FTA': 0, 'FTM': 0 }
        };

        $('input[name^="HomeTeamGPS["], input[name^="AwayTeamGPS["]').each(function () {
            var nameParts = $(this).attr('name').split('.');
            var team = nameParts[0].includes("HomeTeamGPS") ? 'HomeTeam' : 'AwayTeam';
            var statName = nameParts[1];

            if (stats[team].hasOwnProperty(statName)) {
                stats[team][statName] += parseInt($(this).val()) || 0;
            }
        });

        // Obliczanie punktów na podstawie trafionych rzutów
        for (var team in stats) {
            stats[team].Points = (stats[team].FGM * 2) + (stats[team].PM3 * 3) + stats[team].FTM;
        }

        // Aktualizacja pól wynikowych dla drużyn
        for (var team in stats) {
            for (var stat in stats[team]) {
                $('#' + team + '_' + stat).text(stats[team][stat]);
            }
        }
    }

    function calculateRebounds() {
        $('input[name$=".DefensiveRebounds"], input[name$=".OffensiveRebounds"]').each(function () {
            var namePrefix = $(this).attr('name').split('.')[0];
            var defensiveRebounds = parseInt($(`input[name="${namePrefix}.DefensiveRebounds"]`).val()) || 0;
            var offensiveRebounds = parseInt($(`input[name="${namePrefix}.OffensiveRebounds"]`).val()) || 0;
            var totalRebounds = defensiveRebounds + offensiveRebounds;

            $(`input[name="${namePrefix}.Rebounds"]`).val(totalRebounds);
        });
    }
    function updateShotsAttempted(inputField) {
        var nameParts = $(inputField).attr('name').split('.');
        var teamPrefix = nameParts[0].includes("HomeTeamGPS") ? "HomeTeamGPS" : "AwayTeamGPS";
        var statName = nameParts[1];
        var playerId = nameParts[0].match(/\[(\d+)\]/)[1];

        if (statName === "FGM" || statName === "PM3" || statName === "FTM") {
            var attemptedStatName = statName === "FTM" ? "FTA" : (statName === "PM3" ? "PA3" : "FGA");
            var attemptedFieldName = `${teamPrefix}[${playerId}].${attemptedStatName}`;
            var attemptedInputField = $(`input[name="${attemptedFieldName}"]`);

            var madeValue = parseInt($(inputField).val()) || 0;
            var previousMadeValue = $(inputField).data('previous') || 0;
            $(inputField).data('previous', madeValue); // Zapisywanie bieżącej wartości dla przyszłych porównań

            var attemptedValue = parseInt(attemptedInputField.val()) || 0;

            // Zwiększaj liczbę oddanych rzutów, gdy liczba trafionych wzrasta
            if (madeValue > previousMadeValue) {
                attemptedInputField.val(attemptedValue + (madeValue - previousMadeValue));
            }
        }
    }


    // istniejące nasłuchiwanie zmian w polach statystyk i zbiórek
    $('input[name$=".TimeSpend"], input[name$=".Points"], input[name$=".DefensiveRebounds"], input[name$=".OffensiveRebounds"], input[name$=".Assists"], input[name$=".Steals"], input[name$=".Blocks"], input[name$=".Turnovers"]').on('input', function () {
        calculateRebounds();
        updateTeamStats();
    });

    // Dodanie nasłuchiwania zmian dla nowych pól
    $('input[name$=".FGA"], input[name$=".FGM"], input[name$=".PM3"], input[name$=".PA3"], input[name$=".FTA"], input[name$=".FTM"]').on('input', function () {
        updateShotsAttempted(this);
        updateTeamStats();
    });

    $('input[name$=".Rebounds"]').prop('readonly', true);
    $('input[name$=".Points"]').prop('readonly', true);

    // Początkowe obliczenie statystyk, zbiórek i punktów
    calculateRebounds();
    updateTeamStats();
});

