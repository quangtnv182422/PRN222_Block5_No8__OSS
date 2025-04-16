$(document).ready(function () {
    // Handle quantity changes
    $('.quantity').on('change', function () {
        let row = $(this).closest('tr');
        let cartItemId = row.find('.cartItemId').val();
        let quantity = $(this).val();

        $.ajax({
            url: '/Cart/UpdateQuantity',
            type: 'POST',
            data: {
                cartItemId: cartItemId,
                quantity: quantity
            },
            success: function (data) {
                console.log("Cập nhật thành công", data);
                location.reload();
            },
            error: function (error) {
                console.error('Lỗi khi cập nhật:', error);
            }
        });
    });
});