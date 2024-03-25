function goBack(){
    window.history.back()
}

function getCookie(name) {
    const cookieValue = document.cookie.match('(^|;) ?' + name + '=([^;]*)(;|$)');
    return cookieValue ? cookieValue[2] : null;
}

// Получаем значения из куки
const store = getCookie('ShopTitle');

$.ajax({
    type: "GET",
    url: "http://localhost:5287/api/User/shopTitle=" + store, 
    dataType: "json",
    headers: {
        "Access-Control-Allow-Origin": "true"
    },
    success: function (data) {
        data.forEach(function(user) {
            // Создаем элементы label для каждого поля пользователя
            var userId = user.id;
            var firstNameElement = $("<label class='info' id='firstName'></label>").text(user.name);
            var midNameElement = $("<label class='info' id='midName'></label>").text(user.surname);
            var lastNameElement = $("<label class='info' id='lastName'></label>").text(user.patronymic);
            var salaryElement = $("<label class='info' id='salary'></label>").text(user.salary);
            var phoneElement = $("<label class='info' id='phone'></label>").text(user.phone);
            var emailElement = $("<label class='info' id='email'></label>").text(user.email);

            // Создаем div элемент для пользователя и добавляем в него label элементы
            var userElement = $("<div class='user'></div>").append(firstNameElement,salaryElement, midNameElement, phoneElement, lastNameElement,   emailElement);

            userElement.attr("data-id", user.id);
            // Добавляем созданный div элемент в контейнер .users
            $(".users").append(userElement);
            
            userElement.on("click", function() {
                // Удаляем класс 'selected' у всех элементов .user
                $(".user").removeClass("selected");
                // Добавляем класс 'selected' только текущему элементу .user
                $(this).addClass("selected");
            });
        });
    },
    error: function () {
        alert("Error fetching users from API.");
    }
});

$(function() {
    $('#btnSubmit').click(function(event) {
        event.preventDefault(); // Предотвращаем отправку формы по умолчанию

        var selectedUserId = $(".selected").data("id");

        // Подготавливаем данные пользователя для отправки на сервер
        var updatedUserData = {
            name: $('#firstName').val(),
            surname: $('#midName').val(),
            patronymic: $('#lastName').val(),
            Salary: $('#salary').val(),
            phone: $('#phone').val(),
            email: $('#email').val(),
            login: $('#login').val(),
            password: $('#password').val(),
            shopTitle:store
        };

        // Отправляем AJAX-запрос на обновление пользователя
        $.ajax({    
            type: "PUT",
            url: "http://localhost:5287/api/User/" + selectedUserId,
            contentType: "application/json",
            data: JSON.stringify(updatedUserData),
            success: function(response) {
                // В случае успешного обновления пользователя выводим сообщение об успешном обновлении
                alert("Пользователь успешно обновлен!");
                // После успешного обновления можно выполнить другие действия, например, обновление списка пользователей или закрытие формы
            },
            error: function(xhr, status, error) {
                // В случае ошибки выводим сообщение об ошибке
                alert("Произошла ошибка при обновлении пользователя: " + error);
            }
        });
    });
});

