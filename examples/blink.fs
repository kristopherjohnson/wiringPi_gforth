\ Blink an LED connected to GPIO pin 18
\ 
\ Must be run with root permissions. For example: "sudo gforth blink.fs"

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

\ Start our program.
blink

