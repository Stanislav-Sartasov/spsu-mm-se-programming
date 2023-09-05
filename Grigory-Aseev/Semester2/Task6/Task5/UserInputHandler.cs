using Sites;

namespace Task6
{
    public static class UserInputHandler
    {
        public static (Type?, bool) ReadType(string usersInput)
        {
            usersInput = usersInput.Trim();
            usersInput = usersInput.ToLower();

            if (usersInput.Equals("exit"))
            {
                return (null, true);
            }

            Type? type = usersInput switch
            {
                "openweathermap" => typeof(OpenWeatherMap),
                "tomorrowio" => typeof(TomorrowIO),
                "stormglassio" => typeof(StormGlassIO),
                _ => null
            };

            return (type, false);
        }
    }
}
