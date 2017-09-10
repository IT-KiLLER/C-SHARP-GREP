# [C#] GREP 0.9
A simple grip application, written in c#. Grep is a command-line utility for searching plain-text in the terminal output or files.

## Parameters 
    --L, --SHOWLINES     Displays lines

    --C, --CASE          Case-sentivensive

    --W, --WORD          Optional to use this, otherwise the first parameter is
                      the search word.

    --S, --SUBFOLDERS    Look for files in all subfolders.

    --F, --FILE          This is optional if you want to search in a file.

    --HELP              Display this help screen
## Example
ipconfig /all | **grep IPv4 --C**

type myfile.txt | **grep Hey --L**


**grep *_hello_* --F myfile.txt --L**

**grep --W *_Hello_* --F myfile.txt --C**

**grep --W *_Hello_* --F \*.txt --S --C**

## Download
### [Click here to get to the release](https://github.com/IT-KiLLER/C-SHARP-GREP/releases)    [Source code (zip)](https://github.com/IT-KiLLER/C-SHARP-GREP/archive/master.zip)
Please feel free to contact me if you have any questions. [contact information here.](https://github.com/IT-KiLLER/HOW-TO-CONTACT-ME)

## Note
More features will come later.

## Change log
- **0.9** - 2017-09-10
  - PRE-Release.
