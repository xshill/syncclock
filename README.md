# Windows 10 Dual Boot Clock Workaround
This program is intended as a workaround to a bug with Windows 10, where if you boot to another
operating system (Linux, OS X, ...) and then reboot to Windows, the Windows clock is not set to the proper time.

The program needs administrator privileges because it attempts to start the W32Time service and uses
the w32tm program to resync the system clock.

## Usage
Simply run the program as administrator and it should hopefully fix your clock. You can also make
the program run at startup (see [this article](http://www.thewindowsclub.com/autostart-programs-windows-10-make)).

## How it works
The program simply attempts to start the W32Time service (if it isn't started already) and then it
starts the `w32tm` program with the `/resync` argument.
