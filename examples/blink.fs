#! /usr/bin/env gforth

\ Blink an LED connected to GPIO pin 18
\ 
\ Must be run with root permissions. For example: "sudo gforth blink.fs"
\ or "sudo ./blink.fs" (if blink.fs is marked executable)
\ 
\ If the environment variable BLINK_DEBUG is set, then instead of
\ starting the program, we just load all the definitions and then
\ go into interactive mode.  Invoke the program as
\ "sudo BLINK_DEBUG=1 gforth blink.fs" to get this behavior.


\ Forget and reload definitions if this file is re-included.
[ifdef] -blink
    -blink
[endif]
marker -blink


\ Load the wiringPi Gforth interface.
require ../wiringPi.fs

\ Initialize the wiringPi library.
wiringPiSetupGpio drop

\ Declare which GPIO pin we are using, and set it to output mode.
18 constant ledPin
ledPin OUTPUT pinMode

\ Define words to turn the LED on/off, and to wait a bit.
: ledOn   ledPin HIGH digitalWrite ;
: ledOff  ledPin LOW digitalWrite ;
: pause   500 delay ;

\ Our program is a simple loop that never ends.
: blink   begin  ledOn pause ledOff pause  again ;


: blink-debug? ( -- f ) s" BLINK_DEBUG" getenv nip ;

blink-debug? [if]
    ." blink: Entering interactive mode." cr
[else]
    ." blink: The LED should start blinking." cr
    blink
[then]

