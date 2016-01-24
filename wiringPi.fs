\ Gforth interface for the wiringPi library.
\ 
\ On Raspbian, you need "sudo apt-get install gforth libtool-bin"
\ and then follow instructions for installing wiringPi library at
\ <http://wiringpi.com/download-and-install/>.

marker -wiringPi

\ Update this identifier when the API changes, so that Gforth
\ will build a new cached library.
c-library wiringPi_2_31_A

s" wiringPi" add-lib

\c #include <wiringPi.h>
\c #include <wiringPiI2C.h>
\c #include <wiringPiSPI.h>
\c #include <wiringSerial.h>
\c #include <wiringShift.h>

\ wiringPi modes

0 constant WPI_MODE_PINS
1 constant WPI_MODE_GPIO
2 constant WPI_MODE_GPIO_SYS
3 constant WPI_MODE_PHYS
4 constant WPI_MODE_PIFACE
-1 constant WPI_MODE_UNINITIALIZED

\ Pin modes

0 constant INPUT
1 constant OUTPUT
2 constant PWM_OUTPUT
3 constant GPIO_CLOCK
4 constant SOFT_PWM_OUTPUT
5 constant SOFT_TONE_OUTPUT
6 constant PWM_TONE_OUTPUT

0 constant LOW
1 constant HIGH

\ Pull up/down/none

0 constant PUD_OFF
1 constant PUD_DOWN
2 constant PUD_UP

\ PWM

0 constant PWM_MODE_MS
1 constant PWM_MODE_BAL

\ Interrupt levels

0 constant INT_EDGE_SETUP
1 constant INT_EDGE_FALLING
2 constant INT_EDGE_RISING
3 constant INT_EDGE_BOTH

\ Pi model types and version numbers
\      Intended for the GPIO program Use at your own risk.

0 constant PI_MODEL_A
1 constant PI_MODEL_B
2 constant PI_MODEL_AP
3 constant PI_MODEL_BP
4 constant PI_MODEL_2
5 constant PI_ALPHA
6 constant PI_MODEL_CM
7 constant PI_MODEL_07
8 constant PI_MODEL_08
9 constant PI_MODEL_ZERO

0 constant PI_VERSION_1
1 constant PI_VERSION_1_1
2 constant PI_VERSION_1_2
3 constant PI_VERSION_2

0 constant PI_MAKER_SONY
1 constant PI_MAKER_EGOMAN
2 constant PI_MAKER_MBEST
3 constant PI_MAKER_UNKNOWN

\ TODO: Provide Forth definitions for piModelNames, piRevisionNames,
\ piMakerNames, and piMemorySize arrays.

\ TODO: Provide Forth definitions for the wiringPiNodes list and
\ to access fields of a wiringPiNodeStruct.

\ Functions

\ Core wiringPi functions

c-function wiringPiFindNode wiringPiFindNode n -- a
c-function wiringPiNewNode wiringPiNewNode n n -- a

c-function wiringPiSetup wiringPiSetup -- n
c-function wiringPiSetupSys wiringPiSetupSys -- n
c-function wiringPiSetupGpio wiringPiSetupGpio -- n
c-function wiringPiSetupPhys wiringPiSetupPhys -- n

c-function pinModeAlt pinModeAlt n n -- void
c-function pinMode pinMode n n -- void
c-function pullUpDnControl pullUpDnControl n n -- void
c-function digitalRead digitalRead n -- n
c-function digitalWrite digitalWrite n n -- void
c-function pwmWrite pwmWrite n n -- void
c-function analogRead analogRead n -- n
c-function analogWrite analogWrite n n -- void

\ On-Board Raspberry Pi hardware specific stuff

c-function piBoardRev piBoardRev -- n
c-function piBoardId piBoardId a a a a a -- void
c-function wpiPinToGpio wpiPinToGpio n -- n
c-function physPinToGpio physPinToGpio n -- n
c-function setPadDrive setPadDrive n n -- void
c-function getAlt getAlt n -- n
c-function pwmToneWrite pwmToneWrite n n -- void
c-function digitalWriteByte digitalWriteByte n -- void
c-function pwmSetMode pwmSetMode n -- void
c-function pwmSetRange pwmSetRange n -- void
c-function pwmSetClock pwmSetClock n -- void
c-function gpioClockSet gpioClockSet n n -- void

\ Interrupts
\      (Also Pi hardware specific)

c-function waitForInterrupt waitForInterrupt n n -- n
c-function wiringPiISR wiringPiISR n n func -- n

\ Threads

c-function piThreadCreate piThreadCreate func -- n
c-function piLock piLock n -- void
c-function piUnlock piUnlock n -- void

\ Scheduling priority

c-function piHiPri piHiPri n -- n

\ Extras from arduino land

c-function delay delay n -- void
c-function delayMicroseconds delayMicroseconds n -- void
c-function millis millis -- n
c-function micros micros -- n

\ wiringPiI2C.h

c-function wiringPiI2CRead wiringPiI2CRead n -- n
c-function wiringPiI2CReadReg8 wiringPiI2CReadReg8 n n -- n
c-function wiringPiI2CReadReg16 wiringPiI2CReadReg16 n n -- n
c-function wiringPiI2CWrite wiringPiI2CWrite n n -- n
c-function wiringPiI2CWriteReg8 wiringPiI2CWriteReg8 n n n -- n
c-function wiringPiI2CWriteReg16 wiringPiI2CWriteReg16 n n n -- n
c-function wiringPiI2CSetupInterface wiringPiI2CSetupInterface a n -- n
c-function wiringPiI2CSetup wiringPiI2CSetup n -- n

\ wiringPiSPI.h

c-function wiringPiSPIGetFd wiringPiSPIGetFd n -- n
c-function wiringPiSPIDataRW wiringPiSPIDataRW n a n -- n
c-function wiringPiSPISetupMode wiringPiSPISetupMode n n n -- n
c-function wiringPiSPISetup wiringPiSPISetup n n -- n

\ wiringSerial.h

c-function serialOpen serialOpen a n -- n
c-function serialClose serialClose n -- void
c-function serialFlush serialFlush n -- void
c-function serialPutchar serialPutchar n n -- void
c-function serialPuts serialPuts n a -- void
c-function serialDataAvail serialDataAvail n -- n
c-function serialGetchar serialGetchar n -- n

\ wiringShift.h

c-function shiftIn shiftIn n n n -- n
c-function shiftOut shiftOut n n n n -- void

end-c-library

