alter session set "_ORACLE_SCRIPT"=true;
CREATE USER BLOG IDENTIFIED BY A1A2A3;
GRANT CONNECT, RESOURCE TO BLOG;
GRANT create session,create table,create view, create trigger TO BLOG WITH ADMIN OPTION;

alter user BLOG quota unlimited on USERS;

COMMIT;