for /f "tokens=1-7 delims=:/-, " %%i in ('echo exit^|cmd /q /k"prompt $d $t"') do (
  for /f "tokens=2-4 delims=/-,() skip=1" %%a in ('echo.^|date') do (
 set dow=%%i
 set %%a=%%j
 set %%b=%%k
 set %%c=%%l
 set hh=%%m
 set min=%%n
 set ss=%%o
  )
)

set FILENAME=user.%yy%-%mm%-%dd%-%hh%-%min%.dmp
 
 
expdp user/password@orcl directory=c dumpfile=%FILENAME% schemas=user

TIMEOUT -1
