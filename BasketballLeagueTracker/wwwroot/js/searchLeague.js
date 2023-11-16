$(document).ready(
    function () {
        loadLeagueTable();
    }
)

function loadLeagueTable() {
    var ajaxURL ='/search/GetLeagueSearchInfo';

    dataTable = $('#searchLeagueData').DataTable({
        ajax: { url: ajaxURL },

        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/pl.json',
        },
        columns: [
            {
                data: 'Logo', "width": "2%", 
                render: function (data, type, row) {
                    if (type === 'display' && data) {
                        return '<img src="data:image/jpeg;base64,' + data + '" alt="' + row.Name + '" style="max-width: 100%;">';
                    } else {
                        return '<img src="/images/default-user.jpg" alt=":(" style="max-width: 100%; ">';
                    }
                    return data;
                }
            },
            { data: 'Name'},
            { data: 'Description' },          
        ],
    });
}
