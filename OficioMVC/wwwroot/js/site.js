// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code
Window.onload = showBtn();

function showBtn(){
var URL = window.location.href;
var Btn;
if(URL !== "https://localhost:44375/"){
     Btn = document.getElementById("btn-login");
    
}
else{
Btn = document.getElementById("btn-logout");
}
Btn.style.display == "none";
}