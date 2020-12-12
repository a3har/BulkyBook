﻿var dataTable;

$('document').ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
                "ajax": "/Admin/CoverType/GetAll",
                "columns": [{ "data": "name", "width": "60%" },
                    {
                        "data": "id",
                        "render": function (data) {
                            return `
                                    <div class="text-center">
                                        <a href="/Admin/CoverType/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                           <i class="fas fa-edit"></i>
                                        </a>
                                        <a onclick=Delete("CoverType/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                            <i class="fas fa-trash-alt"></i>
                                        </a>
                                    </div>
                                    `
                        }
                    }
                ]
            })
}


function Delete(url) {
    swal({
        title: "Are you sure you want to delete ?",
        text: "This is a permanent action and you will not be able to undo it",
        buttons: true,
        dangerMode: true,
        icon:"warning"
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    })
}