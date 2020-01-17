FROM mariadb:latest

WORKDIR /db

COPY install.sh /docker-entrypoint-initdb.d/

ADD ["./database.sql", "/tmp/database.sql"]

EXPOSE 3306
