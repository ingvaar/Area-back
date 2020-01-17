#!/bin/bash

mysqld_safe --skip-grant-tables &
sleep 5 &&
mariadb -u root -ptoor dashboard < /tmp/database.sql
