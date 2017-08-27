$(document).ready(function () {
    var registerTableEvents = function () {
        $('#productListTable').find('.removeItem').click(function () {
            var data = {
                ProductID: $(this).data('product-id')
            };

            $.ajax({
                url: '/Products/RemoveProduct',
                method: 'POST',
                data: data,
                success: function (result, textStatus, jqXHR) {
                    if (result.Success) {
                        // reload the table
                        $('#productListTable').html(result.Content);

                        // re-register click events
                        registerTableEvents();
                    }
                }
            });
        });
    };

    registerTableEvents();
});