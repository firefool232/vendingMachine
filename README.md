Проект выполнен в среде разработки Visual Studio 2019.

AdministratorController.cs - Контроллер для обработки запросов создания/изменения/удаления напитков.
Включает в себя:
	POST метод "addDrink" для добавления напитка в автомат.
	POST метод "removeDrink" для удаления напитка.
	POST метод "changeDrink" для изменения данных напитка.

VendingController.cs - Контроллер для обработки пользовательских запросов.
Включает в себя:
	GET метод "getDrinks" для получения списка напитков.
	POST метод "putCoin" для внесения монет в торговый автомат.
	POST метод "buyDrink" для покупки напитков.
	POST метод "isThereAChange" для проверки наличия сдачи.
	
database.mdf - база данных приложения.

Из не обязательных требований выполнено отображение количества и номинала монет при возврате сдачи.
