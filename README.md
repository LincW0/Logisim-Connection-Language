# Logisim-Connection-Language

 A better way of describing Logisim connections.

 Current version: Beta2.0.1

 The command-line compiler is using C# now!

 Support IDE made for LCL.

 Notice: The compilation result is slightly different from Beta1.4, but it's better!

 Notice: This version is not compatible with code made for versions lower than Beta0.7.1

 Notice: The IDE is still using Beta0.7.2 compiler, so you will need to download compiler.exe for the lastest version.

 Bug notice: Any IDE version before Beta1.3 contains bugs.

 For the command line compiler, open /Compiler/C#/Compiler.sln with Visual Studio.

 For the graphic IDE, open /IDE/IDE/IDE.pro with Qt Creator.

 Both are now available on the "release" link on the right.

 "IMPT" feature comming soon! (This will be supported in Stable3.0)

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

### Beta1.1 update

 First official release of the IDE.

### Beta1.2 update

 Added icon & released on github.

### Beta1.3 update

 Removed setup.exe.

 Changed releasing format to zip.

 Actually fixed the bug of not able to compile or run when working in a different directory.

 Bug notice: Any IDE version before Beta1.3 contains bugs.

 Removed Herobrine.

### Beta1.4 update

 Removed the following keywords:

 "AND" "OR" "XOR" "NOT" "IN" "OUT"

 This is compatible with versions before Beta1.4, but anything that start with these keywords will be reconized as comment.

### Beta2.0 update

 Rewrote the command-line compiler code in C#.

 This has several advantages:

- Better Code Structures.
- More documents/comments.
- Easy to modify.
- Added exception handling.

 Also added syntax error detecting. (Only a few of the syntax errors, though)

#### Beta 2.0.1 update

 Bug fix: Fixed the bug of not able to use another LCL file path.

 Changed the way of passing file name, preparing for Stable3.0 update.
