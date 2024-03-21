function toCreate(){
    window.location.href = "/MainProject/pages/accounts/admin/createPage/create.html"
}

function toUpdate(){
    window.location.href = "/MainProject/pages/accounts/admin/updatePage/update.html"
}

function getCookie(name) {
    const cookieValue = document.cookie.match('(^|;) ?' + name + '=([^;]*)(;|$)');
    return cookieValue ? cookieValue[2] : null;
}

// Получаем значения из куки
const firstName = getCookie('Name');
const midName = getCookie('Surname');
const store = getCookie('ShopTitle');
const email = getCookie('Email');

// Заполняем соответствующие элементы на странице
document.getElementById('FirstName').innerText = firstName;
document.getElementById('MidName').innerText = midName;
document.getElementById('Store').innerText = store;
document.getElementById('Email').innerText = email;

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
