// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*!
    * Start Bootstrap - SB Admin v6.0.2 (https://startbootstrap.com/template/sb-admin)
    * Copyright 2013-2020 Start Bootstrap
    * Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-sb-admin/blob/master/LICENSE)
    */
(function ($) {
    "use strict";

    // Add active state to sidbar nav links
    var path = window.location.href; // because the 'href' property of the DOM element is the absolute path
    $("#layoutSidenav_nav .sb-sidenav a.nav-link").each(function () {
        if (this.href === path) {
            $(this).addClass("active");
        }
    });

    // Toggle the side navigation
    $("#sidebarToggle").on("click", function (e) {
        e.preventDefault();
        $("body").toggleClass("sb-sidenav-toggled");
    });
})(jQuery);
// Data Table

window.addEventListener('load',
    () => {
        $('[id$=datatable]').DataTable();
    });
// Select2 Sorter

function shouldNotBeSorted(option) {
    if (!option.id) return true;
    if (option.id === 0) return true;
    else if (option.id === "ALL" || option.id === "NONE") return true;
    return false;
}
function select2OptionSorter(data) {
    return data.sort(function (a, b) {
        if (shouldNotBeSorted(a) && shouldNotBeSorted(b)) return 0;
        if (shouldNotBeSorted(a)) return -1;
        if (shouldNotBeSorted(b)) return 1;
        return a.text.toLowerCase() < b.text.toLowerCase() ? -1 : a.text.toLowerCase() > b.text.toLowerCase() ? 1 : 0;
    });
}

$(() => {
    $('.type-ahead').select2({ width: "100%", placeholder: "Select", sorter: select2OptionSorter });
});

//Create Purchase Jquery

$(document).ready(function () {

    $('#SupplierId').on('change', function (e) {

        $('#SupplierId').attr('disabled', true);
        $('#change').attr('hidden', false);


    });

    $("#change").on('click', function (e) {
        $('#SupplierId').attr('disabled', false);
        $('#change').attr('hidden', true);

    })

    $("#btnRemark").on('click', function (e) {
        $('#remarks').append("Thank you For your Purchase! Remember Once items are taken, it will not be refunded.");
        $('#btnRemark').attr('disabled', true);
    })


    function checkDuplicate(itemId) {
        event.preventDefault();
        let result = true;
        $('table tr').each(function () {

            row = $(this).closest('tr');
            find_id = parseInt(row.find('td').eq(0).text());
            console.log(itemId)
            console.log("find id :- " + find_id);

            if (itemId == find_id) {
                alert("You have entered the same Item!");
                result = false;
            }

        })
        console.log(result);
        return result;

    }

    $('#button').click(function (e) {
        e.preventDefault();

        itemId = $('#item_id').val();
        itemName = $("#item_id option:selected").text();
        rate = $('#rate').val();
        qty = $('#qty').val();
        amt = rate * qty;


        if (checkDuplicate(itemId)){
            $('table tbody').append(
                `<tr>
                        <td id="item-id" hidden> ${itemId} </td > 
                        <td id="item-name"> ${itemName} </td>
                        <td id="qty-list">
                            <i id="minus" class="fas mr-2 fa-minus-circle"></i> <span id="q"> ${qty} </span> <i id="plus" class="fas ml-2 fa-plus-circle"></i>
                        </td>
                        <td id="rate-list"> ${rate} </td >
                        <td id="amt"> ${amt} </td>
                        <td>
                            <button id="removeButton" class="btn btn-danger"> <i class="fas fa-trash"></i> </button>
                        </td>
                    </tr >`
            );

        }

        calculateTotal();

    });


    $('body').delegate("#minus", "click", function (e) {

        e.preventDefault();

        row = $(this).closest("tr");
        rate = row.find("td").eq(3).text();
        qty = parseInt(row.find("td").eq(2).text());
        qty--;

        if (qty >= 0) {

            amt = rate * qty;


            parseInt(row.find("td").eq(4).text(amt))
            calculateTotal();


            var counter = parseInt(row.find("#qty-list").text());
            counter--;
            row.find("#q").text(counter);

        } else {

            row.find("#minus").attr("disabled", true);

        }

        
        

    })


    $('body').delegate("#plus", "click", function (e) {
        e.preventDefault();

            row = $(this).closest("tr");

            rate = row.find("td").eq(3).text();
            qty = row.find("td").eq(2).text();
            qty++;
            amt = rate * qty;


            parseInt(row.find("td").eq(4).text(amt))
            calculateTotal();


            var counter = parseInt(row.find("#qty-list").text());
            counter++;
            row.find("#q").text(counter);
        
    })


    $('#item_id').trigger('change');


    function calculateTotal() {

        var total = 0;
        $('table tr').each(function () {
            var value = parseInt($('td', this).eq(4).text());
            if (!isNaN(value)) {
                total += value;
            }
            var vat = total * (13 / 100);
            var discount = $('#discount').val();
            var grand = (parseInt(total, 10) + parseInt(vat, 10)) - parseInt(discount, 10);
            $('#grand').val(grand);
            $('#totalAmt').val(total);
            $('#vat').val(vat);
        });
    }

    $('body').delegate('#removeButton', 'click', function (e) {
        e.preventDefault();
        $(this).closest('tr').remove();
        calculateTotal();
    });

    function checkIfEmpty() {
        var tbody = $(".table>tbody>tr");

        if (tbody.length > 0) {
            return true;
        }
        else {
            alert("Your Purchase List is Empty...");
            return false;
        }
    }

    $("#btnSave").click(function (e) {
        e.preventDefault();

        checkIfEmpty();

        var url = `/Purchase/Create`;

        var data = {
            SupplierId: $('#SupplierId').val(),
            ItemId: $('#item_id').val(),
            Total: $('#totalAmt').val(),
            GrandTotal: $('#grand').val(),
            Discount: $('#discount').val(),
            Vat: $('#vat').val(),
            Remarks: $('#remarks').val(),
            PurchaseDetails: [],

        }

        $('table>tbody>tr').each(function () {
            console.log('each function triggered...');

            data.PurchaseDetails.push({
                ItemId: $(this).find('#item-id').text(),
                SupId: $(this).find('#SupId').val(),
                Qty: $(this).find('#q').text(),
                Rate: $(this).find('#rate-list').text(),
                Amount: $(this).find('#amt').text(),
            });
        });


        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            dataType: 'json',
            success: function (data, status) {
                console.log(data);
                alert("Purchase Saved Successfully!")
                window.location.href = `/Purchase/Index`;

            }
        });

    });


});