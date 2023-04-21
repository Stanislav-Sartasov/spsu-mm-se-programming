@file:Suppress("FunctionName")

package fibers

import com.sun.jna.Library
import com.sun.jna.Native


object UnmanagedFiberAPI {

    val kernel32: Kernel32 by lazy { Native.load("kernel32", Kernel32::class.java) }


    interface Kernel32 : Library {

        fun ConvertThreadToFiber(lpParameter: Long): Long

        fun SwitchToFiber(lpFiber: Long)

        fun DeleteFiber(lpFiber: Long)

        fun CreateFiber(dwStackSize: Long, lpStartAddress: EventCallbackInterface?, lpParameter: Long): Long
    }


    var cnt: Long = 0
}
