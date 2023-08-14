$(document).ready(
    function () {
        loadTable();
        deletePlayersInRange();
        addPlayerListener();
    }
)

function loadTable() {

    var fromAddPlayerToTeam = $('#playerData').data('from-addplayertoteam');
    var ajaxURL='';
    if (fromAddPlayerToTeam) {
        ajaxURL = '/player/GetAllAvailablePlayers';
    }
    else {
        ajaxURL = '/player/getallPlayers';
    }
    dataTable = $('#playerData').DataTable({
        responsive: true,
        ajax: { url: ajaxURL },
        columns: [
            {
                data: 'PlayerId',
                width: "2%",
                render: function (data, type, row) {
                    if (fromAddPlayerToTeam) {
                        return `<a class="addPlayerBtn" data-playerid="${data}" style="cursor: pointer;" >
                        <i class="bi bi-patch-plus-fill"></i>
                        </a>`;

                        //return `<div class="w-75 btn-group" role="group">
                        // <a href="/player/addplayertoteampost?playerId=${data.PlayerId}">
                        // <i class="bi bi-patch-plus-fill"> </i>
                        // </a>
                        // </div>`;
                    } else {
                        return '<input type="checkbox" class="player-checkbox" value="' + data.PlayerId + '" />';
                    }
                }
            },
            { data: 'Name', "width": "20%" },
            { data: 'Surname', "width": "20%" },
            {
                data: 'Positions',
                "width": "20%",
                render: function (data, type, row) {
                    return mapPositionToText(data);
                }
            },
            { data: 'Team.Name', "width": "20%" },
            {
                data: 'PlayerId',
                render: function (data) {
                    if (fromAddPlayerToTeam) {
                        return ''
                    } else {
                        return `<div class="w-75 btn-group" role="group">
                         <a href="/player/upsert?id=${data}">
                         <i class="bi bi-pencil-square"> Edytuj</i>
                         </a>
                         <a href="/player/delete?id=${data}">
                         <i class="bi bi-trash3 p-2"> Usuń</i>
                         </a>
                         </div>`
                    }

                }
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "Brak danych",
            "info": " _START_ - _END_ z _TOTAL_ wyników",
            "infoEmpty": "0 - 0 z 0 wyników",
            "infoFiltered": "(filtrowano z _MAX_ wyników)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Pokaż _MENU_ wyników",
            "loadingRecords": "Ładowanie...",
            "processing": "",
            "search": "Wyszukaj:",
            "zeroRecords": "Nie znaleziono wyników",
            "paginate": {
                "first": "Pierwsza",
                "last": "Ostatnia",
                "next": "Następna",
                "previous": "Poprzednia"
            },
            "aria": {
                "sortAscending": ": activate to sort column ascending",
                "sortDescending": ": activate to sort column descending"
            }
        }
    });
}

function addPlayerListener() {
    $('#playerData').on('click', '.addPlayerBtn', function () {

        if (confirm('Czy na pewno chcesz dodać tego zawodnika?')) {
            var playerId = $(this).data('playerid');
            var teamId = $("#playerData").data("team-id");
            console.log(playerId, teamId);

            $.ajax({
                url: '/player/addplayertoteampost',
                type: 'POST',
                data: {
                    playerId: playerId,
                    teamId: teamId
                },
                success: function (response) {
                    if (response.success) {
                        alert('Zawodnik dodany do drużyny pomyślnie!');
                        dataTable.ajax.reload(); // Przeładowanie danych w tabeli
                    } else {
                        alert('Wystąpił błąd podczas dodawania zawodnika.');
                    }
                },
                error: function (error) {
                    console.log(error.alert);
                    alert('Wystąpił błąd podczas przetwarzania żądania.');
                }
            });
        } else {

        alert('Dodawanie zawodnika anulowane');}
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

function deletePlayersInRange() {

    $('#selectAllCheckbox').on('change', function () {
        $('.player-checkbox').prop('checked', $(this).prop('checked'));
    });

    $('#deleteSelectedBtn').on('click', function () {
        var selectedPlayers = $('.player-checkbox:checked').map(function () {
            return $(this).val();
        }).get();
        if (selectedPlayers.length > 0) {
            // Main action
            $.ajax({
                url: '/player/deleteSelectedPlayers',
                method: 'POST',
                data: { selectedPlayers: selectedPlayers },
                success: function (result) {
                    // Reload table
                    dataTable.ajax.reload();
                },
                error: function (error) {
                    console.error(error);
                }
            });
        } else {
            alert('Nie zaznaczono żadnych zawodników do usunięcia.');
        }
    });
}