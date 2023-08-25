$(document).ready(
    function () {
        loadTable();
        deletePlayersInRange();
        addPlayerToTeamListener();

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
    dataTable = $('#playerData',).DataTable({
        ajax: { url: ajaxURL },
        autoWidth:false,
/*        responsive: true,*/
        //deferRender: true,
/*        scrollY: '70vh',*/
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/pl.json',
        },
        columns: [
            { 
                data: 'PlayerId', // Add to team/ delete from database
                render: function (data, type, row) {
                    if (fromAddPlayerToTeam) {
                        return `<a class="addPlayerBtn" data-playerid="${data}" style="cursor: pointer;" >
                        <i class="bi bi-patch-plus-fill"></i>
                        </a>`;
                    } else {
                        return '<input type="checkbox" class="player-checkbox" value="' + data + '" />';
                    }
                }
            },
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
                data: 'Positions',  
                render: function (data, type, row) {
                    return mapPositionToText(data);
                }
            },
            { data: 'Team.Name' },
            { data: 'Weight' },        
            { data: 'Height'},
            { data: 'FormattedBirthday'},
            { data: 'UniformNumber' },          
            {
                data: 'PlayerId',
                render: function (data) {
                    if (fromAddPlayerToTeam) {
                        return null;
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
        ]// Datatable language set to Polish
    });
}

function addPlayerToTeamListener() {
    $('#playerData').on('click', '.addPlayerBtn', function () {

        if (confirm('Czy na pewno chcesz dodać tego zawodnika?')) {
            var playerId = $(this).data('playerid'); // PlayerId from tabledata
            var teamId = $("#playerData").data("team-id"); //team-id from
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
                        toastr.success(response.message);
                        dataTable.ajax.reload(); // Przeładowanie danych w tabeli
                        //alert('Zawodnik dodany do drużyny pomyślnie!');
                    } else {
                        toastr.error(response.message);
                    }
                },
                error: function (error) {
                    console.log(error.alert);
                    toastr.error('Wystąpił błąd podczas przetwarzania żądania.');
                }
            });
        } else {

            toastr.error('Dodawanie zawodnika anulowane');
        }
    });
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
            // Dodaj potwierdzenie przed usunięciem zawodników
            if (confirm('Czy na pewno chcesz usunąć wybranych zawodników?')) {
                $.ajax({
                    url: '/player/deleteSelectedPlayers',
                    method: 'POST',
                    data: { selectedPlayers: selectedPlayers },
                    success: function (result) {
                        dataTable.ajax.reload();
                    },
                    error: function (error) {
                        console.error(error);
                    }
                });
            }
        } else {
            alert('Nie zaznaczono żadnych zawodników do usunięcia.');
        }
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



        //"language": {
        //    "decimal": "",
        //    "emptyTable": "Brak danych",
        //    "info": " _START_ - _END_ z _TOTAL_ wyników",
        //    "infoEmpty": "0 - 0 z 0 wyników",
        //    "infoFiltered": "(filtrowano z _MAX_ wyników)",
        //    "infoPostFix": "",
        //    "thousands": ",",
        //    "lengthMenu": "Pokaż _MENU_ wyników",
        //    "loadingRecords": "Ładowanie...",
        //    "processing": "",
        //    "search": "Wyszukaj:",
        //    "zeroRecords": "Nie znaleziono wyników",
        //    "paginate": {
        //        "first": "Pierwsza",
        //        "last": "Ostatnia",
        //        "next": "Następna",
        //        "previous": "Poprzednia"
        //    },
        //    "aria": {
        //        "sortAscending": ": activate to sort column ascending",
        //        "sortDescending": ": activate to sort column descending"
        //    }
        //}