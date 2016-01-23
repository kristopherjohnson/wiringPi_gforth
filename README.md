[wiringPi.fs](https://github.com/kristopherjohnson/wiringPi_gforth/blob/master/wiringPi.fs) defines an interface to the functions in the [wiringPi](http://wiringpi.com) GPIO interface library, allowing those functions to be used in [Gforth](https://www.gnu.org/software/gforth/) programs.


## Installation

You can install Gforth on a [Raspbian](https://www.raspbian.org) system with `apt-get`:

    sudo apt-get install gforth

Gforth's C interface requires a C compiler toolchain, which is usually already installed on a Raspbian system. It also requires [libtool](http://www.gnu.org/software/libtool/), which is usually not installed by default, so you can install it like this:

    sudo apt-get install libtool-bin

To install the wiringPi library, follow the instructions here: <http://wiringpi.com/download-and-install/>


## Usage

Access to the GPIO hardware requires root access, so you must invoke Gforth with `sudo gforth` when using wiringPi.

To call wiringPi functions, push the arguments onto the stack and then invoke the corresponding Gforth word. For example, if you would write this in C:

```c
pinMode(18, OUTPUT);
digitalWrite(18, HIGH);
```

Then you would write this in Gforth:

```forth
18 OUTPUT pinMode
18 HIGH digitalWrite
```

When translating wiringPi code from C to Gforth, remember that result codes cannot be ignored. For example, the [wiringPiSetupXXX()](http://wiringpi.com/reference/setup/) functions each return a status code (always 0), which you need to `DROP` to keep your Forth stack balanced.

To load the interface into Gforth and use it interactively, launch Gforth like this:

    sudo gforth wiringPi.fs

To use wiringPi within a Gforth program, use `require wiringPi.fs` or one of the [other ways](https://www.complang.tuwien.ac.at/forth/gforth/Docs-html/Forth-source-files.html) to read Forth source files.

Here is a complete example of a Gforth program that uses wiringPi to make an LED blink:

```forth
\ Blink an LED connected to GPIO pin 18
\ 
\ Must be run with root permissions. For example: "sudo gforth blink.fs"

\ Load the wiringPi Gforth interface.
require wiringPi.fs

\ Initialize the wiringPi library.
wiringPiSetupGpio drop

\ Declare what GPIO pin we are using, and set it to output mode.
18 constant ledPin
ledPin OUTPUT pinMode

\ Define words to turn the LED on/off, and to wait a bit.
: ledOn   ledPin HIGH digitalWrite ;
: ledOff  ledPin LOW digitalWrite ;
: pause   500 delay ;

\ Our program is a simple loop that never ends.
: blink   begin ledOn pause ledOff pause again ;

\ Start our program.
blink
```

The first time you use `wiringPi.fs`, Gforth will build a library in your `~/.gforth/libcc-named/.libs/` directory. Subsequently, Gforth will reuse that cached library. Note that Gforth will continue to use that cached library even if you make your own changes to `wiringPi.fs`, so if you do make changes, you need to delete the libraries in that directory or change the name for the `c-library` declaration in `wiringPi.fs`.

