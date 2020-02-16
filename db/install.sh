#!/bin/bash

mysqld_safe --skip-grant-tables &
sleep 5 &&
mariadb -u root -ptoor area < /tmp/database.sql
