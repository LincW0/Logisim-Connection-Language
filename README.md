# Logisim-Connection-Language

 A better way of describing Logisim connections.

 Current version: Beta1.3

 Now supporting IDE made for LCL! (Now released!)

 Notice: This version is not compatible with code made for versions lower than Beta0.7.1

 Bug notice: Any IDE version before Beta1.3 contains bugs.

 For the command line compiler, compile /Compiler/compiler.cpp with gcc. // We are now developing Beta2.0, so the compiler.cpp isn't exactly what you would expect. For now, just click the "release" link to get the compiler.

 For the graphic IDE, open /IDE/IDE/IDE.pro with Qt Creator.

 Both are now available on the "release" link on the right.

 This does not support the "IMPT" feature addressed in /Formats/LCLFormat.txt (This will be supported in Beta2.0)

## Update log

### Beta0.5 update

 This is the first version, supported features:

 IN OUT AND XOR OR NOT CNCT

 This version doesn't place output components.

#### Beta0.5.1 update

 Bug fix: fixed the bug of NOT Gate malfunctioning.

### Beta0.6 update

 Added feature: Output components are now placed according to the source code.

 This version doesn't arrange outputs' order.

#### Beta0.6.1 update

 Rearranged the directories.

#### Beta0.6.2 update

 Bug notice: Every version before Beta0.6.2 (including Beta0.6.2) have bugs, stop using them!

#### Beta0.6.3 update

 False alarm: Beta0.6.2 doesn't have any bug.

### Beta0.7 update

 Added Feature: Output components are now fully organized.

#### Beta0.7.1 update

 Changed LCL Format.

 Added one parameter in order to perform more complicated functions.

 Example:

 Former: CNCT input[0],output[0][0];

 New: CNCT input[0][0],output[0][0];

 This is a preparation for Beta2.0

 Also changed headers from .hpp to .h

#### Beta0.7.2 update

 Changed headers from .h back to .hpp

### Beta1.0 update

 Added a IDE made for LCL.

 The IDE is a Qt application.

#### Beta1.0.1 update

 Fixed the bug of unable to open a file by clicking on it.

 Fixed the bug of unable to run (or compile & run) programs.

#### Beta1.0.2 update

 Fixed the bug of not able to compile or run when working in a different directory.

#### Beta1.1 update

 First official release of the IDE.

#### Beta1.2 update

 Added icon & released on github.

#### Beta1.3 update

 Removed setup.exe.

 Changed releasing format to zip.

 Actually fixed the bug of not able to compile or run when working in a different directory.

 Bug notice: Any IDE version before Beta1.3 contains bugs.

 Removed Herobrine.
