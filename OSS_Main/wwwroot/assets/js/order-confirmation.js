$(document).ready(function () {
    loadProvinces();

    $("#province").change(function () {
        loadDistricts($(this).val());
        $("#shipping-fee").text("Calculating...");
        $("#ward").empty().append('<option value="">-- Select Ward --</option>');
    });

    $("#district").change(function () {
        loadWards($(this).val());
        $("#shipping-fee").text("Calculating...");
    });

    $("#ward").change(function () {
        if ($(this).val()) {
            getAvailableServices();
        }
    });

    function loadProvinces() {
        $.get("/api/ghn/provinces", function (data) {
            var provinces = data.data;
            $("#province").empty().append('<option value="">-- Select Province --</option>');
            $.each(provinces, function (index, province) {
                $("#province").append(`<option value="${province.ProvinceID}">${province.ProvinceName}</option>`);
            });
        })
            .fail(function (jqXHR, textStatus, errorThrown) {
                console.error("Error loading provinces: ", textStatus, errorThrown);
            });
    }

    function loadDistricts(provinceId) {
        $.get(`/api/ghn/districts/${provinceId}`, function (data) {
            var districts = data.data;
            $("#district").empty().append('<option value="">-- Select District --</option>');
            $.each(districts, function (index, district) {
                $("#district").append(`<option value="${district.DistrictID}">${district.DistrictName}</option>`);
            });
        });
    }

    function loadWards(districtId) {
        $.get(`/api/ghn/wards/${districtId}`, function (data) {
            var wards = data.data;
            $("#ward").empty().append('<option value="">-- Select Ward --</option>');
            $.each(wards, function (index, ward) {
                $("#ward").append(`<option value="${ward.WardCode}">${ward.WardName}</option>`);
            });
        });
    }

    function getAvailableServices() {
        var fromDistrictId = 1711; // ID kho hàng
        var toDistrictId = $("#district").val();
        var shopId = 5656073; // Shop ID

        $.ajax({
            url: "/api/ghn/available-services",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                shopId: shopId,
                fromDistrictId: fromDistrictId,
                toDistrictId: toDistrictId
            }),
            success: function (data) {
                try {
                    var response = typeof data === 'string' ? JSON.parse(data) : data;
                    if (response.code === 200 && response.data && response.data.length > 0) {
                        var serviceId = response.data[0].service_id;
                        var serviceTypeId = 2;
                        calculateShipping(serviceId, serviceTypeId);
                    } else {
                        $("#shipping-fee").text("Không có dịch vụ vận chuyển phù hợp");
                        console.error("No shipping services available:", response);
                    }
                } catch (e) {
                    console.error("Error parsing response:", e, data);
                    $("#shipping-fee").text("Lỗi khi xử lý dữ liệu vận chuyển");
                }
            },
            error: function (xhr) {
                console.error("Error in available-services:", xhr.responseText);
                $("#shipping-fee").text("Lỗi khi kiểm tra dịch vụ vận chuyển");
            }
        });
    }

    function calculateShipping(serviceId, serviceTypeId) {
        var fromDistrictId = 1711;
        var toDistrictId = $("#district").val();
        var weight = 500;
        var length = 20, width = 20, height = 10;
        var shopId = 5656073;

        $.ajax({
            url: "/api/ghn/calculate-shipping",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                shopId: shopId,
                fromDistrictId: fromDistrictId,
                toDistrictId: toDistrictId,
                weight: weight,
                length: length,
                width: width,
                height: height,
                service_id: serviceId,
                service_type_id: serviceTypeId
            }),
            success: function (data) {
                try {
                    var response = typeof data === 'string' ? JSON.parse(data) : data;
                    if (response.code === 200 && response.data) {
                        var fee = response.data.total;
                        $("#shipping-fee").text(fee.toLocaleString() + " VND");

                        // Get the subtotal from the model and calculate the new total
                        var subTotal = parseFloat($("#subtotal-value").val());
                        $("#total-cost").text((subTotal + fee).toLocaleString() + " VND");
                    } else {
                        $("#shipping-fee").text("Không thể tính phí vận chuyển: " + response.message);
                        console.error("Calculate shipping failed:", response);
                    }
                } catch (e) {
                    console.error("Error parsing response:", e, data);
                    $("#shipping-fee").text("Lỗi khi xử lý dữ liệu vận chuyển");
                }
            },
            error: function (xhr) {
                console.error("Error calculating shipping:", xhr.responseText);
                $("#shipping-fee").text("Lỗi khi tính phí vận chuyển");
            }
        });
    }
});