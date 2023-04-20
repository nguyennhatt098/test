var order = {
    init:function() {
        $("#customerDatatable").DataTable({
            "processing": true,
            "serverSide": true,
            "filter": true,
            "ajax": {
                "url": "/order/GetOrder",
                "type": "POST",
                "datatype": "json"
            },
            "columnDefs": [{
                "targets": [0],
                "visible": false,
                "searchable": false
            }],
            "columns": [
                { "data": "id", "name": "Id", "autoWidth": true },
                { "data": "orderName", "name": "First Name", "autoWidth": true },
                { "data": "lastName", "name": "Last Name", "autoWidth": true },
                { "data": "contact", "name": "Country", "autoWidth": true },
                { "data": "email", "name": "Email", "autoWidth": true },
                { "data": "dateOfBirth", "name": "Date Of Birth", "autoWidth": true },
                {
                    "render": function (data, row) { return "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + row.id + "'); >Delete</a>"; }
                },
            ]
        });
    },
    view: function (id) {
        $.ajax({

            type: "POST",

            url: '/order/Create/' + id,

            contentType: "application/text; charset=utf-8",

            dataType: "text",

            async: false,

            success: function (data) {

                $('.modal-body').html(data);

                $("#myModal").modal("show");

            }

        })
    },
    save: function () {
        
        $.ajax({

            type: "POST",

            url: '/order/CreateOrder/',
            data: {
                OrderName: $('#OrderName').val(),
                ProductId: $('#ProductId').val(),
                CustomerId: $('#CustomerId').val(),
                OrderDate: $('#OrderDate').val(),
                Amount: $('#Amount').val()
            },

            //contentType: "application/text; charset=utf-8",

            //dataType: "json",

            async: false,

            success: function (data) {
                if (data.code == "Error") {
                    alert(data.message);
                } else {
                    location.reload();
                }

            }

        })
    }
};
order.init();