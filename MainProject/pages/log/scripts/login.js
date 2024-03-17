document.getElementById("loginForm").addEventListener("submit", function(event) {
    event.preventDefault();
    var login = document.getElementById("login").value;
    var password = document.getElementById("password").value;
    window.location.href = "account.html";
});