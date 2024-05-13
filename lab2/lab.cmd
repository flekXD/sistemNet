@echo off
setlocal enabledelayedexpansion

set "LogFile=lab.log"
set "PathToFiles=C:\Users\Fleks\sistemNet\lab2\baza"
set "ProcessToKill=steam.exe"
set "ArchivePath=C:\Users\Fleks\sistemNet\lab2\archive"
set "ComputerIP=192.168.1.7"
set "LogSizeLimit=500"

if not exist "%LogFile%" (
    echo Файл з ім’ям %LogFile% створено > "%LogFile%"
) else (
    echo Файл з ім’ям %LogFile% відкрито >> "%LogFile%"
)

for /f "tokens=1-4 delims=: " %%a in ('net time \\ntp.carnet.hr ^| find "теперішній час"') do (
    set "time=%%c:%%d:%%e"
    set "date=%%b"
)
echo %date% %time% >> "%LogFile%"

tasklist >> "%LogFile%"

taskkill /IM "%ProcessToKill%" /F
echo Процес %ProcessToKill% завершено >> "%LogFile%"

for /R "%PathToFiles%" %%G in (temp*.*) do (
    del "%%G"
    set /a count+=1
)
echo Видалено %count% файлів >> "%LogFile%"

set "zipfile=%date:~6,4%%date:~3,2%%date:~0,2%%time:~0,2%%time:~3,2%%time:~6,2%.zip"
"C:\Program Files\7-Zip\7z.exe" a -tzip "%zipfile%" "%PathToFiles%\*"

move /Y "%zipfile%" "%ArchivePath%"

set "yesterday=%date:~6,4%%date:~3,2%%date:~0,2%"
set /a "yesterday-=1"
if exist "%ArchivePath%\%yesterday%*.zip" (
    echo Файл з архівом за минулий день існує >> "%LogFile%"
) else (
    echo Файл з архівом за минулий день не існує >> "%LogFile%"
)

forfiles /P "%ArchivePath%" /M *.zip /D -30 /C "cmd /c del @path"
echo Видалено архіви, старші 30 днів >> "%LogFile%"

ping -n 1 google.com > nul && (
    echo Є підключення до Internet >> "%LogFile%"
) || (
    echo Немає підключення до Internet >> "%LogFile%"
)

ping -n 1 %ComputerIP% > nul && (
    shutdown /s /m \\%ComputerIP% /t 0
    echo Комп'ютер з IP %ComputerIP% вимкнено >> "%LogFile%"
) || (
    echo Комп'ютер з IP %ComputerIP% не знайдено >> "%LogFile%"
)

net view >> "%LogFile%"

for %%I in ("%LogFile%") do set "size=%%~zI"
if %size% gtr %LogSizeLimit% (
    echo Розмір log файлу перевищено >> "%LogFile%"
)

fsutil volume diskfree c: >> "%LogFile%"

systeminfo > "systeminfo%date:~6,4%%date:~3,2%%date:~0,2%%time:~0,2%%time:~3,2%%time:~6,2%.txt"

endlocal
