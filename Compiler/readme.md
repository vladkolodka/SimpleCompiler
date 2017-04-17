# HTCL compiler reference

You will find complete compiler reference below.

## Message codes

#### Format

`<file name> | <code> | [<parameters>] | <message description>`

>Parameters separated by a space

#### Success messages

| Code     | Parameters    | Message |
| --------|---------|-------|
| 1 | token class name, token identifier, line number in code, token value | Token found. |

#### Error messages

>Error codes are negative.

| Code     | Parameters    | Message |
| --------|---------|-------|
| -1 | file name | File not found. |
| -2 | *no parameters* | You must specify the name of the file. |
| -3 | symbol, line number in code | Unexpected symbol. |