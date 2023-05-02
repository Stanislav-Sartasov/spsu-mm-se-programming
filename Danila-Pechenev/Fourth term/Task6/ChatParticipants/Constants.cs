namespace ChatParticipants
{
    public static class Constants
    {
        public static string ServerIP { get; } = "127.0.0.1";

        public static int ServerPort { get; } = 5000;

        public static int MessageBufferSize { get; } = 1024;

        public static int MinUserNameLength { get; } = 2;

        public static int MaxUserNameLength { get; } = 20;

        public static string MinAvailableIPAddress { get; } = "127.0.0.1";

        public static string MaxAvailableIPAddress { get; } = "127.255.255.254";

        public static int MinAvailablePort { get; } = 1024;

        public static int MaxAvailablePort { get; } = 49151;

        public static int MaxMessageLength { get; } = 200;
    }
}
