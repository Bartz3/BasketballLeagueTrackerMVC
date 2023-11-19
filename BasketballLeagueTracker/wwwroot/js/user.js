$(document).ready(
    function () {
        loadTable();
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
            },
            {
                data: 'PhoneNumber'
            },
            {
                data: 'Role'
            },

            {
                data: { Id: "Id", LockoutEnd: "lockoutEnd" },
                render: function (data) {
                    var date = new Date().getTime();
                    var lockout = new Date(data.LockoutEnd).getTime();
                    if (lockout > date) {
                        return `<div  >
                           <a  onclick=LockAccount('${data.Id}') class='btn btn-danger mx-2' style="cursor:pointer width:50%;">
                          Zablokowane <i class="bi bi-lock-fill "></i> 
                           </a>
                           <a href="/user/EditUserRole?userId=${data.Id}" class='btn btn-warning' style="cursor:pointer">
                         Role <i class="bi bi-pencil-square"></i> 
                           </a>
                        </div>`
                    } else {
                        return `<div  >
                           <a  onclick=LockAccount('${data.Id}') class='btn btn-success mx-2' style="cursor:pointer width:50%;">
                          Odblokowane <i class="bi bi-unlock-fill "></i> 
                           </a>
                           <a href="/user/EditUserRole?userId=${data.Id}" class='btn btn-warning' style="cursor:pointer">
                         Role <i class="bi bi-pencil-square"></i> 
                           </a>
                        </div>`
    
                    }
                }
            }
        ]
    });
}

function LockAccount(Id) {
    $.ajax({
        type: "POST",
        url: '/User/LockAccount',
        data: JSON.stringify(Id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
        }
        })
}


