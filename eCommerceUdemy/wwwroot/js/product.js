var dataTable;
$(document).ready(function () {
    loadDateTable();
})

function loadDateTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {url:'/admin/product/getall'} ,
        "columns": [
            { data: 'title', "width": "25%" },
            { data: 'isbn', "width": "15%" },
            { data: 'listPrice', "width": "10%" },
            { data: 'author', "width": "10%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                                    <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2">
                                        <i class="bi bi-pen"></i>Edit
                                    </a>
                                    <a  onClick = Delete("/admin/product/delete/${data}") class="btn btn-danger mx-2">
                                        <i class="bi bi-trash"></i>Delete
                                    </a>

                            </div>`
                }
            , "width": "20%" },


        ]
    });
}

function Delete(url){
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

