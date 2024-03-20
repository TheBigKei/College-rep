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
            // Перенаправляем пользователя на другую страницу
            console.log(data);
            // Устанавливаем куки
            document.cookie = "Name=" + data.name + "; path=/";
            document.cookie = "Surname=" + data.surname + "; path=/";
            document.cookie = "Patronymic=" + data.patronymic + "; path=/";
            document.cookie = "ShopTitle=" + data.shopTitle + "; path=/";
            document.cookie = "Email=" + data.email + "; path=/";
            document.cookie = "Phone=" + data.phone + "; path=/";
            document.cookie = "Salary=" + data.salary + "; path=/";

            window.location.href = '/MainProject/pages/accounts/user/index.html';
        },
        error: function (data) {
            alert(data);
            result = false;
        }
    });
});