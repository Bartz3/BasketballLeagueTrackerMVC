$(document).ready(
    function () {
        loadTable();
        addTeamToTheLeagueListener();
        //deletePlayersInRange();

    }
)
    //< link rel = "stylesheet" type = "text/css" href = "~/css/default.css" >
function myCSS() {
    var element = document.getElementById("link"); 
    element.classList.add("~/css/default.css"); 
}
function loadTable() {
 

    var fromAddTeamToLeague = $('#teamData').data('from-addteamtoleague');
    console.log(fromAddTeamToLeague);
    var ajaxURL='';
    if (fromAddTeamToLeague) {
        ajaxURL = '/team/GetAllAvailableTeams';
    }
    else {
        ajaxURL = '/team/GetAllTeams';
    }
    dataTable = $('#teamData',).DataTable({
        ajax: { url: ajaxURL },
        
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/pl.json',
        },
        columns: [
            {
                data: 'teamId', // Add to league/ delete from database
                render: function (data, type, row) {
                    if (fromAddTeamToLeague) {

                        return `<a class="addTeamBtn" data-teamid="${data}" style="cursor: pointer;" >
                        <i class="bi bi-patch-plus-fill"></i>
                        </a>`;
                    } else {

                        return '<input type="checkbox" class="team-checkbox" value="' + data + '" />';
                    }
                }
            },
            {
                data: 'teamLogo', "width": "7%",
                render: function (data, type, row) {
                    if (type === 'display' && data) {
                        return '<img src="data:image/jpeg;base64,' + data + '" alt="' + row.Name + '" style="max-width: 100%;">';
                    } else {
                        return '<img src="/images/default-team.png" alt=":(" style="max-width: 100%; ">';
                    }
                    return data;
                }
            },
            {
                //data:'name'
                data: 'name',
                render: function (data, type, row) {
                    return '<a href="/team/details?teamid=' + row.teamId + '" id="link">' + data + '</a>';
                }
            },
            {
                data: 'description'
            },
            {
                data: 'teamId',
                render: function (data) {
                    if (fromAddTeamToLeague) {
                        return ''
                    } else {
                        return `<div class="w-75 btn-group" role="group">
                         <a href="/team/upsert?id=${data}">
                         <i class="bi bi-pencil-square"> Edytuj</i>
                         </a>
                         <a href="/team/delete?id=${data}">
                         <i class="bi bi-trash3 p-2"> Usuń</i>
                         </a>
                         </div>`
                    }

                }
            }
        ],// Datatable language set to Polish
 
    });
}

function addTeamToTheLeagueListener() {
    $('#teamData').on('click','.addTeamBtn', function () {

        if (confirm('Czy na pewno chcesz dodać tę drużynę?')) {
            var teamId = $(this).data('teamid');
            var leagueId = $("#teamData").data("league-id");
            console.log(teamId, leagueId);

            $.ajax({
                url: '/team/addteamtoleaguepost',
                type: 'POST',
                data: {
                    teamId: teamId,
                    leagueId: leagueId
                },
                success: function (response) {
                    if (response.success) {
                        toastr.success(response.message);
                        dataTable.ajax.reload(); // Przeładowanie danych w tabeli
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

            toastr.error('Dodawanie drużyny anulowane');}
    });
}

function deleteTeamsInRange() {

    $('#selectAllCheckbox').on('change', function () {
        $('.team-checkbox').prop('checked', $(this).prop('checked'));
    });

    $('#deleteSelectedBtn').on('click', function () {
        var selectedPlayers = $('.player-checkbox:checked').map(function () {
            return $(this).val();
        }).get();

        if (selectedPlayers.length > 0) {
            // Dodaj potwierdzenie przed usunięciem zawodników
            if (confirm('Czy na pewno chcesz usunąć wybrane drużyny?')) {
                $.ajax({
                    url: '/team/deleteSelectedTeams',
                    method: 'POST',
                    data: { selectedTeams: selectedTeams },
                    success: function (result) {
                        dataTable.ajax.reload();
                    },
                    error: function (error) {
                        console.error(error);
                    }
                });
            }
        } else {
            alert('Nie zaznaczono żadnych drużyn do usunięcia.');
        }
    });
}
