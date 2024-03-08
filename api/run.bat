@echo off

:: Set the values based on "OBS Studio > Tools > WebSocket Server Settings"
set port="4455"
set password="scriptingpasswordtest123"
:: Set the directory based on your "obs64.exe" install location
set directory="C:\Program Files\obs-studio\bin\64bit"

:: Check if OBS Studio is open, if not, start OBS Studio exe
tasklist | findstr "obs64.exe" >nul
if not errorlevel 1 goto StartAPI
cd /d %directory%
echo Starting OBS Studio...
start "" "./obs64.exe"
echo Waiting for OBS Studio to initialize...

:: Wait for OBS Studio to open (to prevent API starting issues)
:WaitForOBS
timeout /t 1 /nobreak >nul
netstat -ano | findstr %port% >nul
if errorlevel 1 goto WaitForOBS
echo.

:: Start the API program
:StartAPI
cd /d "%~dp0"
echo Starting the API program...
:: Edit the port number and password to match the OBS WebSocket Server (tools > WebSocket Server Settings)
:: Copy and paste where needed
call "./ConnectToOBS.exe" -port=%port% -password=%password%

:: Error Handling
if %ERRORLEVEL% equ 0 (
  echo Program started successfully.
)
else if %ERRORLEVEL% equ 1 (
  echo Error encountered.
  echo Please ensure that OBS is installed and is open.
  echo Ensure that "Tools > WebSocket Server Settings > Enable WebSocket Server" is ticked and applied.
  echo Also ensure that the port and password in this .bat file match the port and password in "Tools > WebSocket Server Settings"
  echo Press any key to exit...
  pause >nul
)
else if %ERRORLEVEL% equ 2 (
  echo Program terminated by user.
)

