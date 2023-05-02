namespace ChatParticipants
{
    public static class Constants
    {
        public static string ServerIP { get; } = "127.0.0.1";

        public static int ServerPort { get; } = 5000;

        public static int MessageBufferSize { get; } = 1024;

        public static int MinUserNameLength { get; } = 2;

        public static int MaxUserNameLength { get; } = 20;

        public static int MinAvailableIPAddress { get; } = 1024;

        public static int MaxAvailableIPAddress { get; } = 49151;

        public static int MaxMessageLength { get; } = 200;
    }
}
