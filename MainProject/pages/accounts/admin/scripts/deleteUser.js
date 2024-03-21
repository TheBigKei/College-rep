function deleteUser() {
    // Находим пользователя с классом 'selected'
    var selectedUser = $(".user.selected");
    
    // Проверяем, что пользователь с выбранным классом существует
    if (selectedUser.length === 0) {
        alert("Выберите пользователя для удаления.");
        return;
    }

    // Получаем идентификатор пользователя из атрибута data-id
    var userId = selectedUser.data("id");

    // Отправляем запрос на удаление пользователя
    $.ajax({
        type: "DELETE",
        url: "http://localhost:5287/api/User/id=" + userId,
        success: function() {
            alert("Пользователь успешно удален!");
            // Удаляем элемент пользователя из DOM
            selectedUser.remove();
        },
        error: function(error) {
            console.log("Ошибка при удалении пользователя: " + error.responseText);
        }
    });
}

// Обработчик события клика на кнопку "Удалить"
$("button.delete").on("click", deleteUser);