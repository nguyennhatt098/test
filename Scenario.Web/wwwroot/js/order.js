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
                { "data": "productName", "name": "ProductName", "autoWidth": true },
                { "data": "categoryName", "name": "CategoryName", "autoWidth": true },
                { "data": "customerName", "name": "CustomerName", "autoWidth": true },
                { "data": "orderDate", "name": "OrderDate", "autoWidth": true },
                { "data": "amount", "name": "amount", "autoWidth": true },
                //{
                //    "render": function (data, row) { return ""; }
                //},
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