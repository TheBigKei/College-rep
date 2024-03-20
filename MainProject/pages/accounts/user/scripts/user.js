function getCookie(name) {
    const cookieValue = document.cookie.match('(^|;) ?' + name + '=([^;]*)(;|$)');
    return cookieValue ? cookieValue[2] : null;
}

// Получаем значения из куки
const firstName = getCookie('Name');
const midName = getCookie('Surname');
const lastName = getCookie('Patronymic');
const phone = getCookie('Phone');
const store = getCookie('ShopId');
const email = getCookie('Email');
const salary = getCookie('Salary');

// Заполняем соответствующие элементы на странице
document.getElementById('firstName').innerText = firstName;
document.getElementById('midName').innerText = midName;
document.getElementById('lastName').innerText = lastName;
document.getElementById('store').innerText = store;
document.getElementById('email').innerText = email;
document.getElementById('salary').innerText = salary;