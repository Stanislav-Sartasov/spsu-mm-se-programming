package fibers

import com.sun.jna.Callback


fun interface EventCallbackInterface : Callback {

    fun callback(param: Int): Int
}
