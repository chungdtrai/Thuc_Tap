//// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.

//// Write your JavaScript code.
function showConfirm(id) {
    $.ajax({
        url: '/Product/GetById',
        type: 'GET',
        data: { id: id },
        success: function (response) {
            //var modal = new bootstrap.Modal(document.getElementById('confirmdeletemodal'));
            //var message = "Bạn có muốn xóa danh mục sản phẩm " + response.name + "?";
            ////var cancelButton = document.getElementById("cancelbutton");        
            ////cancelButton.setAttribute('onclick', 'cancelDelete()');
            //document.getElementById('deletemessage').innerText = message;
            //modal.show();
            var deleteButton = document.getElementById('deletebutton');
            deleteButton.setAttribute('onclick', 'deleteProduct(' + id + ')');
            var message = "Bạn có muốn xóa danh mục sản phẩm " + response.name + "?";
            $("#confirmdeletemodal .modal-body #deletemessage").html(message);
            $("#confirmdeletemodal").modal('show');
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}
function redirectToInitialPage() {
    window.location.href = '@Url.Action("Index", "Product")';
}
function deleteProduct(id) {
    $.ajax({
        url: '/Product/Delete',
        type: 'GET',
        data: { id: id },
        success: function (result) {

            if (result.success) {
                $("#confirmdeletemodal").modal('hide');
                //$.alert("Thành Công", "Xóa thành công");
                //setTimeout(function(){
                //    window.location.href = '@Url.Action("Index", "Product")';
                //},250);
                updateProductTable();
                $.alert("Xóa thành công", {
                    autoClose: true,
                    closeTime: 1000,
                    type: 'success',
                } );
            }
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}
function updateProductTable() {
    $.ajax({
        type: 'GET',
        url: '/Product/GetAllProduct',
        dataType: 'html',
        success: function (data) {
            $("#tableContainer").html(data);
        },
    });
}
