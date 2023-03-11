using System.Runtime.InteropServices;

namespace Fibers
{
    internal static class UnmanagedFiberAPI
    {
        public delegate uint LPFIBER_START_ROUTINE(uint param);

        [DllImport("Kernel32.dll")]
        public static extern uint ConvertThreadToFiber(uint lpParameter);

        [DllImport("Kernel32.dll")]
        public static extern void SwitchToFiber(uint lpFiber);

        [DllImport("Kernel32.dll")]
        public static extern void DeleteFiber(uint lpFiber);

        [DllImport("Kernel32.dll")]
        public static extern uint CreateFiber(uint dwStackSize, LPFIBER_START_ROUTINE lpStartAddress, uint lpParameter);
    }
}
