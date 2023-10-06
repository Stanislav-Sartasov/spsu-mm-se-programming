@file:Suppress("FunctionName")

package fibers

import com.sun.jna.Library;
import com.sun.jna.Native;

object UnmanagedFiberAPI{

    public var kernel32 = Native.loadLibrary("kernel32", Kernel32::class.java) as Kernel32

    interface Kernel32: Library {

        fun ConvertThreadToFiber(lpParameter: Int): Int

        fun SwitchToFiber(lpFiber: Int)

        fun DeleteFiber(lpFiber: Int)

        fun CreateFiber(dwStackSize: Int, lpStartAddress: EventCallbackInterface, lpParameter: Int): Int
    }

    var count = 0
}