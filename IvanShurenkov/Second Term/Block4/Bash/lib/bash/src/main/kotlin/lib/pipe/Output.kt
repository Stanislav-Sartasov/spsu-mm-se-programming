package lib.pipe

class Output(var output: String? = null, var error: String? = null, var exit: Boolean = false) {
    fun addOut(out: String?) {
        if (out == null)
            return
        output = (if (output != null) output else "") + out + System.lineSeparator()
    }

    fun addErr(err: String?) {
        if (err == null)
            return
        error = (if (error != null) error else "") + err + System.lineSeparator()
    }
}
