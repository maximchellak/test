
rem alter session set "_oracle_script"=true;
rem drop user user cascade;
rem create user user identified by password;
rem grant dba to user;

TIMEOUT -1

impdp system/password@orcl directory=c dumpfile=USER.2025-06-24-9-49.DMP schemas=user


TIMEOUT -1
