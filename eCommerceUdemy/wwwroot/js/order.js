var dataTable;
$(document).ready(function () {

    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadDateTable("inprocess");

    } else {
        if (url.includes("completed")) {
            loadDateTable("completed");

        } else {
            if (url.includes("pending")) {
                loadDateTable("pending");


            } else {

                if (url.includes("approved")) {
                    loadDateTable("approved");

                } else {

                    loadDateTable("all");


                }
            }


        }

    }

});

function loadDateTable(status)
{
    console.log(status);
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/order/getall?status=' + status },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "25%" },
            { data: 'phoneNumber', "width": "20%" },
            { data: 'applicationUser.email', "width": "20%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'orderTotal', "width": "10%" },

            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                                    <a href="/admin/order/details?orderId=${data}" class="btn btn-primary mx-2">
                                        <i class="bi bi-pen"></i>
                                    </a>
                                
                            </div>`
                }
                , "width": "20%"
            },


        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }

            })
        }
    });
}

