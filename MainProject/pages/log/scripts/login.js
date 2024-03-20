document.getElementById("loginForm").addEventListener("submit", function(event) {
    event.preventDefault(); // Предотвратить отправку формы по умолчанию

    var login = document.getElementById("login").value;
    var password = document.getElementById("password").value;

    $.ajax({
        type: "GET",
        url: "http://localhost:5287/api/User/login="+login +"&pass=" + password,
        dataType: "json",
        headers: {
            "Access-Control-Allow-Origin": "true"
        },
        success: function (data) {
            window.location.href = '/MainProject/pages/accounts/user/index.html';
            console.log(data);
        },
        error: function (data) {
            alert(data);
    
            result = false;
        }
    });
});