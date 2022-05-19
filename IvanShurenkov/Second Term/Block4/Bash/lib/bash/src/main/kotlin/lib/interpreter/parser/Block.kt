package lib.interpreter.parser

class Block(var string: String) {
    var type: Type = if (string[0] == '$')
        Type.VARIABLE
    else if (string[0] == '\'' && string[string.length - 1] == '\'' ||
        string[0] == '\"' && string[string.length - 1] == '\"'
    ) {
        Type.STRING
    } else if (string == "|")
        Type.SEQUENCE
    else if (string == "=")
        Type.EQUAL
    else
        Type.SUBSTRING

    init {
        if (type == Type.STRING) {
            //"Add special symbols"
            type = Type.SUBSTRING
            string = string.substring(1 until string.length - 1)
        }
    }

}