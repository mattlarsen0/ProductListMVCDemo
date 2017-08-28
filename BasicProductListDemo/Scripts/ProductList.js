$(document).ready(function () {
    var hideStatusMessage = function () {
        $('#statusMessage').hide();
    }
    var registerProductTableEvents = function () {
        $('#productListTable').find('.removeItem').click(function () {
            hideStatusMessage();

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

                        if (!result.ProductsRemain) {
                            $('#additionalData').hide();
                        }
                        else {
                            $('#additionalData').html(result.AdditionalDataTables);
                        }

                        // re-register click events
                        registerProductTableEvents();
                    }
                }
            });
        });
    };
    var registerSupplierTableEvents = function () {
        $('#supplierListTable').find('.removeItem').click(function () {
            hideStatusMessage();

            var data = {
                SupplierID: $(this).data('supplier-id')
            };

            $.ajax({
                url: '/Products/RemoveSupplier',
                method: 'POST',
                data: data,
                success: function (result, textStatus, jqXHR) {
                    if (result.Success) {
                        // reload the table
                        $('#supplierListTable').html(result.Content);

                        if (!result.SuppliersRemain) {
                            // no products left, hide the table and button
                            $('#productsContainer').hide();
                        }

                        if (!result.ProductsRemain) {
                            $('#additionalData').hide();
                        }
                        else {
                            $('#additionalData').html(result.AdditionalDataTables);
                        }

                        // re-register click events
                        registerSupplierTableEvents();
                    }
                }
            });
        });
    };

    registerProductTableEvents();
    registerSupplierTableEvents();
});