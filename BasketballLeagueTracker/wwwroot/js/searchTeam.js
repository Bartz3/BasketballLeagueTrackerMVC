$(document).ready(
    function () {
        loadTeamTable();
    }
)


function loadTeamTable() {
    var ajaxURL ='/search/GetTeamSearchInfo';

    dataTable = $('#searchTeamData').DataTable({
        ajax: { url: ajaxURL },

        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/pl.json',
        },
        columns: [
            {
                data: 'TeamLogo', "width": "7%", // Display photo of player if its not null
                render: function (data, type, row) {
                    if (type === 'display' && data) {
                        return '<img src="data:image/jpeg;base64,' + data + '" alt="' + row.Name + '" style="max-width: 100%;">';
                    } else {
                        return '<img src="/images/default-team.jpg" alt=":(" style="max-width: 100%; ">';
                    }
                    return data;
                }
            },
 
            { data: 'Name'},               
            { data: 'Description' },          
        ],
    });
}
