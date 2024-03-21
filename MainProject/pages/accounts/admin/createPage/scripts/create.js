function goBack(){
    window.history.back()
}

$(function(){
    // Функция для получения значения из куки по имени
    function getCookie(name) {
        const cookieValue = document.cookie.match('(^|;)\\s*' + name + '\\s*=\\s*([^;]+)');
        return cookieValue ? cookieValue.pop() : '';
    }

    // Функция для создания пользователя с переданным ShopTitle из куки
    function createUserWithShopTitle() {
        // Получаем ShopTitle из куки
        var shopTitle = getCookie('ShopTitle');

        // Проверяем, что ShopTitle был получен
        if (!shopTitle) {
            console.log('ShopTitle не найден в куках.');
            return;
        }

        // Собираем данные из формы
        var userData = {
            name: $("#firstName").val(),
            surname: $("#midName").val(),
            patronymic: $("#lastName").val(),
            salary: $("#salary").val(),
            phone: $("#phone").val(),
            email: $("#email").val(),
            login: $("#login").val(),
            password: $("#password").val(),
            shopTitle: shopTitle // Добавляем полученный ShopTitle в данные пользователя
        };
        
        // Отправляем данные на сервер
        $.ajax({
            type: "POST",
            url: "http://localhost:5287/api/User/reg",
            contentType: "application/json",
            data: JSON.stringify(userData),
            success: function() {
                alert("Пользователь успешно создан!");
                // Очищаем форму после успешного создания пользователя
                $("#createUserForm")[0].reset();
            },
            error: function(error) {
                console.log("Ошибка при создании пользователя: " + error.responseText);
            }
        });
    }

    // Обработчик события отправки формы
    $("#createUserForm").on("submit", function(event) {
        // Предотвращаем стандартное поведение отправки формы
        event.preventDefault();
        // Создаем пользователя с использованием ShopTitle из куки
        createUserWithShopTitle();
    });
});