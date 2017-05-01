# Space Engineers : Assembler Resources Status

## Description :
This lightweight script allows you to display on lcd screens information on the Assemblers, Refineries status in ship / station.

This script is at the beginning and for now it has only this elements:
* Time clock
* Display status isProducing Assembler (on/off) and detail information of type block and number.
* Display status isProducing Refinery (on/off) and detail information of type block and number.
* Change color for selected lights (linked to assembler or refinery status)

This script is set to display the desired values on any lcd screen which contains this specials tag names in name LCD:
* __CALL SCRIPT in LCD or LIGHT [obligatory] : __
```[SE]```
* Time clock: (only LCD)
```CLOCK```
* Assembler: 
```ASSEMBLER```
* Refinery: 
```REFINERY```

### Example:
In all LCDs or Lights where do you need to see the script data (or status for light), you need to change Name LCD/LIGHT by :
* example 1:
```[ARS] LCD ;clock;refinery;assembler;```
* example 2:
```[ARS] Light ;assembler;```

* __It's important to separate the different module by a semicolon__ ```;```
* __In name lcd, you can call module (clock, assembler...) in lower case or upper case (it's your choice)__

## LICENSE

[APACHE LICENSE VERSION 2.0](LICENSE)