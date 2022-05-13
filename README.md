# WebNetworkManager
Веб интерфейс для работы с серверами и просмотра их статуса

Данное приложение создано для работы с сетями. 
Для корректного запуска, рядом с файлом запуска разместите config.json

```json
{
    "server":"xxx",
    "db":"xxx",
    "uid":"xxx",
    "password":"xxx"
}
```

server - сервер к которому будет подключатся приложение.

db - имя базы данных где мы будем работать.

uid - имя пользователя.

password - пароль пользователя.

Для корректной работы приложения, создайте таблицы со структурами ниже:

```mysql
CREATE TABLE users (
    id INT NOT NULL AUTO_INCREMENT,
    login VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    PRIMARY KEY(id)
);

CREATE TABLE events (
    id INT NOT NULL AUTO_INCREMENT,
    level INT NOT NULL,
    event_id VARCHAR(64) NOT NULL,
    message TEXT NOT NULL,
    begin INT NOT NULL,
    end INT NOT NULL,
    PRIMARY KEY(id)
);

CREATE TABLE telegram_chats (
    id INT NOT NULL AUTO_INCREMENT,
    chat_id INT UNSIGNED NOT NULL,
    api_key VARCHAR(255) NOT NULL,
    title VARCHAR(255) NOT NULL,
    PRIMARY KEY(id)
);
```

Асинхронная ICMP пинговалка
---------------------------
https://github.com/ktoyou/AsyncIcmpPinger
Данный сервис работает с базой данных, пингует объекты, и обновляет.
В интерфейсе Web приложения раздел называется - состояние сети.


P.S - сервисы будут дополняться 
