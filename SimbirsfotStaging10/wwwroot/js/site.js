// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.



function ChangeValidElementsView() {
    var valids = document.querySelectorAll(".validation-summary-errors li");
    for (valid of valids) {
        valid.classList.add("font-weight-bold");
        valid.classList.add("m-2");
    }
}

ChangeValidElementsView();