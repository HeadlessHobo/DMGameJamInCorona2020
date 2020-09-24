set build_dir=%1
set output_installer_dir=%2
set game_name=%3
set current_dir=%~dp0

:: Change encoding to UTF8, since that is required by the config file.
chcp 65001

:: Overwrite existing config file with new game name.
@echo off
@echo ;!@Install@!UTF-8! > %current_dir%config.txt
@echo Title="%game_name%" >> %current_dir%config.txt
@echo BeginPrompt="Do you want to install %game_name%?" >> %current_dir%config.txt
@echo RunProgram="%game_name%.exe" >> %current_dir%config.txt
@echo ;!@InstallEnd@! >> %current_dir%config.txt
@echo on

:: Change encoding back to normal
chcp 1252

:: Create a 7z archive with all of the contents of the build dir
7z.exe a %current_dir%Installer.7z %build_dir%/* -r

:: Create the installer.exe file.
copy /b %current_dir%7zS.sfx + %current_dir%config.txt + %current_dir%Installer.7z %output_installer_dir%\%game_name%.exe