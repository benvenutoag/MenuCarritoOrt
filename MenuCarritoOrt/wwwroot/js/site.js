// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.




const navMenu = document.querySelector("#navMenu");

navMenu.addEventListener("click", () => {
    navMenu.classList.toggle("active");
})

/* Toggle between showing and hiding the navigation menu links when the user clicks on the hamburger menu / bar icon */
//function myFunction() {
//    var x = document.getElementById("#navMenu");
//    if (x.style.display === "block") {
//        x.style.display = "none";
//    } else {
//        x.style.display = "block";
//    }
//}