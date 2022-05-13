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

У Web интерфейса есть система авторизации, прежде чем запускать, создайте таблицу с структурой ниже:

```mysql
CREATE TABLE users (
    id INT NOT NULL AUTO_INCREMENT,
    login VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    PRIMARY KEY(id)
);
```
Так же создайте таблицу:
```mysql
CREATE TABLE events (
    id INT NOT NULL AUTO_INCREMENT,
    level INT NOT NULL,
    event_id VARCHAR(64) NOT NULL,
    message TEXT NOT NULL,
    begin INT NOT NULL,
    end INT NOT NULL,
    PRIMARY KEY(id)
);
```

create table events ( id int not null auto_increment, level int not null, event_id varchar(64) not null, message text not null, begin int not null, end int not null, primary key (id));

Асинхронная ICMP пинговалка
---------------------------
https://github.com/ktoyou/AsyncIcmpPinger
Данный сервис работает с базой данных, пингует объекты, и обновляет.
В интерфейсе Web приложения раздел называется - состояние сети.


P.S - сервисы будут дополняться 
