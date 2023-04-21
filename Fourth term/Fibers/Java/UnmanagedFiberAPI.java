package com.lab;

import com.sun.jna.Library;
import com.sun.jna.Native;

public class UnmanagedFiberAPI {

    public interface Kernel32 extends Library {
        int ConvertThreadToFiber(int lpParameter);

        void SwitchToFiber(int lpFiber);

        void DeleteFiber(int lpFiber);

        int CreateFiber(int dwStackSize, EventCallbackInterface lpStartAddress, int lpParameter);
    }

    public static Kernel32 kernel32 = (Kernel32) Native.loadLibrary("kernel32", Kernel32.class);

}