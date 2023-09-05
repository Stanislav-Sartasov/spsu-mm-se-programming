package lib.interpreter.parser

object Parser {
    var blocks = emptyList<Block>()
        private set

    fun run(line: String): List<Block> {
        return parseInBlocks(line)
    }

    fun parseInBlocks(line: String): List<Block> {
        blocks = emptyList<Block>()
        var sep = ""
        var subStr = ""

        for (i in line) {
            if (sep == "") {
                if (i == ' ') {
                    addSubString(subStr)
                    subStr = ""
                } else if (i == '|' || i == '=') {
                    addSubString(subStr)
                    subStr = ""
                    addSubString("$i")
                } else if (i == '$') {
                    addSubString(subStr)
                    subStr = "$"
                } else if (i == '\'' || i == '"') {
                    addSubString(subStr)
                    subStr = "$i"
                    sep = "$i"
                } else {
                    subStr += i
                }
            } else {
                if (i.toString() == sep) {
                    addSubString("$subStr$i")
                    subStr = ""
                    sep = ""
                } else {
                    subStr += i
                }
            }
        }
        addSubString(subStr)
        return blocks
    }

    fun addSubString(subStr: String) {
        if (subStr != "")
            blocks += Block(subStr)
    }
}