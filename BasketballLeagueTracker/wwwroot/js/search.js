$(document).ready(
    function () {
        loadPlayerTable();
        loadTeamTable();
        loadLeagueTable();
    }
)
var urlLanguage = '//cdn.datatables.net/plug-ins/1.13.6/i18n/pl.json';
// Player data
function loadPlayerTable() {
    var ajaxURL = '/search/GetPlayerSearchInfo';

    dataTable = $('#searchPlayerData',).DataTable({
        ajax: { url: ajaxURL },
        stateSave: true,

        language: {
            url: urlLanguage,
        },
        columns: [
            {
                data: 'Photo', "width": "7%", // Display photo of player if its not null
                render: function (data, type, row) {
                    if (type === 'display' && data) {
                        return '<img src="data:image/jpeg;base64,' + data + '" alt="' + row.Name + '" style="max-width: 100%;">';
                    } else {
                        return '<img src="/images/default-user.jpg" alt=":(" style="max-width: 100%; ">';
                    }
                    return data;
                }
            },
            {
                data: 'Name',
                render: function (data, type, row) {
                    return '<a href="/player/details?playerId=' + row.PlayerId + '" class="player-link">' + data + '</a>';
                }
            },
            {
                data: 'Surname',
                render: function (data, type, row) {
                    return '<a href="/player/details?playerId=' + row.PlayerId + '" class="player-link">' + data + '</a>';
                }
            },
            {
                data: 'UniformNumber'},
            {
                data: 'Positions',
                render: function (data, type, row) {
                    return mapPositionToText(data);
                }
            },
            {
                data: 'Team.Name',
                render: function (data, type, row) {
                    return '<a href="/team/details?teamId=' + row.TeamId + '" class="player-link">' + data + '</a>';
                }
            },
            { data: 'Weight' },
            { data: 'Height' },
            { data: 'FormattedBirthday' },
            { data: 'Country' },
        ],// Datatable language set to Polish
    });
}

// Team data
function loadTeamTable() {
    var ajaxURL = '/search/GetTeamSearchInfo';

    dataTable = $('#searchTeamData').DataTable({
        ajax: { url: ajaxURL },

        language: {
            url: urlLanguage,
        },
        columns: [
            {
                data: 'TeamLogo', "width": "6%", // Display photo of player if its not null
                render: function (data, type, row) {
                    if (type === 'display' && data) {
                        return '<img src="data:image/jpeg;base64,' + data + '" alt="' + row.Name + '" style="max-width: 50%;">';
                    } else {
                        return '<img src="/images/default-team2.png" alt=":(" style="max-width: 50%; ">';
                    }
                    return data;
                }
            },

            {
                data: 'Name',
                render: function (data, type, row) {
                    return '<a href="/team/details?teamId=' + row.TeamId + '" class="player-link">' + data + '</a>';
                }
            },
            { data: 'Description' },
        ],
    });
}


// League data
function loadLeagueTable() {
    var ajaxURL = '/search/GetLeagueSearchInfo';

    dataTable = $('#searchLeagueData').DataTable({
        ajax: { url: ajaxURL },

        language: {
            url: urlLanguage,
        },
        columns: [
            {
                data: 'Logo', "width": "6%",
                render: function (data, type, row) {
                    if (type === 'display' && data) {
                        return '<img src="data:image/jpeg;base64,' + data + '" alt="' + row.Name + '" style="max-width: 50%;">';
                    } else {
                        return '<img src="/images/deafult-league.png" alt=":(" style="max-width: 50%; ">';
                    }
                    return data;
                }
            },
            {
                data: 'Name',
                render: function (data, type, row) {
                    return '<a href="/league/details?leagueId=' + row.LeagueId + '" class="player-link">' + data + '</a>';
                }            },
            { data: 'Description' },
        ],
    });
}



function mapPositionToText(position) {
    var positions = [];
    var mapping = {
        1: "Trener",
        2: "Rozgrywający",
        4: "Rzucający obrońca",
        8: "Niski skrzydłowy",
        16: "Silny skrzydłowy",
        32: "Środkowy"
    };

    for (var weight = 32; weight >= 1; weight /= 2) {
        if (position >= weight) {
            positions.push(mapping[weight]);
            position -= weight;
        }
    }
    return positions.reverse().join(", ");
}
