$(document).ready(function () {
    GetProducts();

});
function readUrl(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imageFile')
                .attr('src', e.target.result)
                .width(100)
                .height(100);
        };
        reader.readAsDataURL(input.files[0]);
    }
}
function GetProducts() {
    $.ajax({
        url: '/Product/GetProducts',
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            var object = '';
            if (!response || response.length === 0) {
                object += '<tr>';
                object += '<td colspan="6" class="text-center">Product Not Available</td>';
                object += '</tr>';
            } else {
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + item.id + '</td>';
                    object += '<td>' + item.productName + '</td>';
                    object += '<td><img src="' + item.productImage + '" alt="' + item.productName + '" style="max-width: 100px; max-height: 100px;"></td>';
                    object += '<td>' + item.price + '</td>';
                    object += '<td>' + item.qty + '</td>';
                    object += '<td><a href="#" class="btn btn-primary" onclick="Edit(' + item.id + ')">Edit</a> <a href="#" class="btn btn-danger" onclick="Delete(' + item.id + ')">Delete</a></td>';
                    object += '</tr>';
                });
            }

            $('#productTblBody').html(object);
        },
        error: function () {
            alert('Unable to Fetch Data');
        }
    });
}


$('#addProductBtn').click(function () {
    $('#ProductModalLabel').text("Insert Product");
    $('#save').css('display', 'block');
    $('#update').css('display', 'none');
    $('#Id').val(0);
    $('#ProductModal').modal('show');
});

function Insert() {
    var formData = new FormData();
    formData.append('ProductName', $('#ProductName').val());
    formData.append('Price', parseFloat($('#Price').val()));
    formData.append('Qty', parseInt($('#Qty').val()));

    var imageFile = $('#ProductImage')[0].files[0];
    if (imageFile) {
        formData.append('ProductImageFile', imageFile);
    }

    $.ajax({
        url: '/Product/Insert',
        data: formData,
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.message) {
                alert('Error: ' + response.message + '\nDetails: ' + (response.errors ? response.errors.join(', ') : response.error));
            } else {
                GetProducts();
                $('#ProductModal').modal('hide');
                $('#ProductModal').find('input').val('');
                $('#imageFile').attr('src', ''); // Clear image preview if needed
            }
        },
        error: function () {
            alert('Unable to Insert Data');
        }
    });
}


function Edit(id) {
    $.ajax({
        url: '/Product/EditProducct?id=' + id, 
        type: 'GET', 
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response.message) {
                alert('Error: Data Not Fetched!');
            } else {
                $('#ProductModal').modal('show');
                $('#ProductModalLabel').text("Edit Product");
                $('#save').css('display', 'none');
                $('#update').css('display', 'block');
                $('#Id').val(response.id);
                $('#ProductName').val(response.productName);
                $('#Price').val(response.price);
                $('#Qty').val(response.qty);
            }
        },
        error: function () {
            alert('Unable to Fetch Product Data');
        }
    });
}


function Update() {
    var formData = {
        Id: parseInt($('#Id').val()), 
        ProductName: $('#ProductName').val(),
        Price: parseFloat($('#Price').val()),
        Qty: parseInt($('#Qty').val())
    };

    $.ajax({
        url: '/Product/UpdateProducct',
        data: JSON.stringify(formData), 
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            if (response.message) {
                alert('Error: ' + response.message + '\nDetails: ' + (response.errors ? response.errors.join(', ') : response.error));
            } else {
                GetProducts();
                $('#ProductModal').modal('hide');
                $('#ProductModal').find('input').val('');
            }
        },
        error: function () {
            alert('Unable to Update Product');
        }
    });
}


