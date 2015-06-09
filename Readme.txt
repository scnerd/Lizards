= Lizards =

(c) David Maxson, 2015
All code is made available under the MIT license, so feel free to hack and reuse as desired

== Installation ==

The Lizards application may be installed by unzipping the WebInstall.zip file (or Installer.zip if you need to install on a machine with no internet access) and running setup.exe. Note that previous versions may have to be uninstalled before the installer will work properly.

The code can also be compiled in Visual Studio 2010, 2012, or 2013. See "Code Notes" below for prerequisites for compiling.

== Basic Usage ==

=== Instructions ===

Launch the Lizards application and initialize your experiment as desired.

Once initialized, the computer will connect to the Arduino (which you specified in initialization), and you should start receiving temperature readings. Note that the heating element should not be on at this time.

Press the "Hold Initial Temp" button (or in console, CONTROL MENU -> Begin holding start temperature) to tell the Arduino to try to hold the lizard chambers' ambient temperature at what you specified (27 C by default)

Press the "Start Ramping Temp" button (or in console, CONTROL MENU -> Begin ramping heater) to tell the Arduino to begin its temperature ramp. It will attempt to hold the lizard chambers' ambient temperature at your hold temperature (27 C) plus the number of minutes elapsed times your ramp temperature (1 C). E.g., after three minutes, it will attempt to make the temperature 30 C. Note that it is incapable of cooling the ambient temperature, so if the starting temperature was higher than what you asked for, the heater will not turn on until after it is trying to get hotter than the actual ambient temperature. E.g., if the environment was 30 C to start with, the heater won't turn on for the first 3 minutes while it is ramping the target temperature from 27 C to 30 C, after which it will start heating to reach the target temperature.

Press the "End Experiment" button (or in console, CONTROL MENU -> Stop ramping heater) to tell the Arduino to turn off the heater. This will also save your data for you, and should display the filepath of the saved file.

You may also save your data at any time using the "Save Results" button or CTRL + S (or in console, MAIN MENU -> Save data). The data will be in the same format, though will obviously be incomplete if the experiment is still running.

Closing the application will also send the stop signal to the Arduino, and will save the data if you haven't already saved it or if there's new data that you didn't save yet.

=== Output Format ===

Result data is saved in My Documents\Lizards, and each filename is timestamped so that they are sortable by time. The CSV data can be opened in any spreadsheet application (Google Sheets, Microsoft Excel, Libre/Open Office).

The CSV file consists of three tables. The first is a simple two-entry row that indicates when the experiment was started. Note that pressing "Start Experiment" multiple times will reset this start time.

The second table gives timestamps for each event for each lizard (cells are left blank if the event wasn't ever marked), followed by ambient and lizard temperature readings for every time interval as specified during initialization. By default, this means you will get all temperatures for every 15 seconds elapsed since the start of the experiment. Timestamps are given relative to the start time, and thus might get converted (especially by Sheets or Excel) into a date and time (12AM, some date way in the past). Make sure when importing this file that these fields get let alone, they should be read as HOURS:MINUTES:SECONDS since start time.

The final table shows all data tagged for each event and note. Any custom notes will appear here, as well as all standard events. This table is sorted by timestamp by default. Each record is tagged with which lizard is being referred to, the timestamp that the note or event was made, the ambient and lizard's temperature at that moment, and any text associated with the record.

=== Issues ===

If the pretty interface isn't working properly, the console version should suffice, and offers all the same capabilities.

Also, the original interface programmer may be reached at jexmax@gmail.com for fixes or help. All code is available at https://github.com/scnerd/Lizards and can be downloaded, fixed, and compiled by itself.

== Code Notes ==

=== Source Code ===

The program is split into a library and two applications. The library (Lizards.dll) handles all communication and state maintenance, and is usable by other .NET applications. The two applications merely provide skins over this. Issues in communication should thus be limited to the Lizards library.

In order to open the Lizards solution, the coder must have Visual Studio 2010, 2012, or 2013 (with C#, so some VS Express versions will not work), as well as .NET 4.0 Client Profile available. To load the Installer project successfully, you must have downloaded and installed the Microsoft Visual Studio 20XX Installer Projects, which stopped being included by default in VS 2012. It should have been available in 2010, and can be installed in 2013 using the following link: https://visualstudiogallery.msdn.microsoft.com/9abe329c-9bba-44a1-be59-0fbf6151054d

The code is largely commented, besides some of the helper classes that just perform small, modular tasks. See the comments and code headers for help if you need to recode anything.

=== Communication ===

The communication between the Arduino and the PC takes place entirely as 16-bit integers.

The following commands can be sent from the PC to the Arduino

HOLD:  0x0001 0xTARG 0xHEAT
START: 0x0002 0xRAMP 0xUPTO
STOP:  0x0003
Where
   TARG: The temperature to hold the environment at while in the "holding" state
   HEAT: The temperature to maintain in the heating chamber until stopped
   RAMP: The temperature change per minute to maintain while in the "ramping" state
   UPTO: The temperature at which to stop ramping, the max the environment should ever reach
   
   NOTE: ALL TEMPERATURES SENT TO THE ARDUINO ARE REVERSE-SCALED USING THE "AMBIENT" THERMOMETER SETTINGS. Thus, if the ambient thermometer reports 16 integer values per degree Celsius, then a ramp temperature of 1 degree per minute would be transmitted as 16, and a target of 50 C would be reported as 50*16=320.
   NOTE: It is assumed that the heating chamber and the environment use the same thermometer type. Arduino-side scaling will be needed if this is not true.
   
The Arduino responds to the PC using the following packet form

0xDEAD 0xBEEF 0xHEAT 0xENVI 0xLIZ1 0xLIZ2 ... 0xLIZN 0xDEAD 0xBEEF
Where
   DEAD: 0xDEAD, exactly like it sounds like
   BEEF: 0xBEEF, exactly like it sounds like. These two values are used as unique packet border values to ensure that data doesn't get written to the wrong variables
   HEAT: The temperature in the heating chamber
   ENVI: The temperature in the lizard environment
   LIZN: The temperature of lizard number N - note that these are the only temperatures scaled using the lizard thermometer settings