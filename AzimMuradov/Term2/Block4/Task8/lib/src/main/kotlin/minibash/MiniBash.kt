package minibash

import java.io.InputStream
import java.io.PrintStream

interface MiniBash {

    fun run(
        inputStream: InputStream,
        outputStream: PrintStream,
        errorsStream: PrintStream,
    )
}
