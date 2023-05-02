﻿using System.Net;

namespace ChatParticipants
{
    public static class Validator
    {
        public static bool ValidateUserName(string userName, out string errorMessage)
        {
            errorMessage = "";
            int length = userName.Length;
            if (length >= Constants.MinUserNameLength && length <= Constants.MaxUserNameLength)
            {
                return true;
            }

            errorMessage = $"Incorrect name: the length must be from {Constants.MinUserNameLength} to {Constants.MaxUserNameLength}";
            return false;
        }

        public static bool ValidateIP(string ip, out string errorMessage, out IPAddress parsedIP)
        {
            errorMessage = "";
            bool success = IPAddress.TryParse(ip, out parsedIP);
            if (!success)
            {
                errorMessage = "Incorrect IP adress";
            }

            return success;
        }

        public static bool ValidatePort(string port, out string errorMessage, out int intPort)
        {
            errorMessage = "";
            bool success = int.TryParse(port, out intPort);
            if (!success || intPort < Constants.MinAvailableIPAddress || intPort > Constants.MaxAvailableIPAddress)
            {
                errorMessage = $"Incorrect port: must be an integer from {Constants.MinAvailableIPAddress} to {Constants.MaxAvailableIPAddress}";
                return false;
            }

            return true;
        }

        public static bool ValidateMessage(string message, out string errorMessage)
        {
            errorMessage = "";
            int length = message.Length;
            if (length >= 1 && length <= Constants.MaxMessageLength)
            {
                return true;
            }

            errorMessage = $"Incorrect message: the length must be from 1 to {Constants.MaxMessageLength}";
            return false;
        }
    }
}
