$(document).ready(
    function () {
        loadTable();
        //deletePlayersInRange();
        //addPlayerToTeamListener();

    }
)


function loadTable() {
    var ajaxURL = '/user/getallusers';

    dataTable = $('#userData',).DataTable({
        ajax: { url: ajaxURL },
        autoWidth: false,
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/pl.json',
        },
        columns: [
            { data: 'Id' },
            {
                data: 'Email'
            },
            {
                data: 'Nickname'
            },
            {
                data: 'CreatedAt',
                render: function (data, type, row) {
                    if (type === 'display' || type === 'filter') {
                        var date = new Date(data);
                        return date.toLocaleString('pl-PL', {
                            day: 'numeric',
                            month: 'long',
                            year: 'numeric',
                            hour: 'numeric',
                            minute: 'numeric',
                        });
                    }
                    return data;
                }
            }, {
                data: 'PhoneNumber'
            }, {
                data: 'Id',
                render: function (data) {

                        return `<div class="w-75 btn-group" role="group">
                         <a href="/user/upsert?id=${data}">
                         <i class="bi bi-pencil-square"> Edytuj</i>
                         </a>
                         <a href="/player/delete?id=${data}">
                         <i class="bi bi-trash3 p-2"> Usuń</i>
                         </a>
                         </div>`

                }
            }
        ]
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

