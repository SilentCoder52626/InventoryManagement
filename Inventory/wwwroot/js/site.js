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

