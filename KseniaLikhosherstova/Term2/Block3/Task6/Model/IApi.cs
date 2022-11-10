namespace Model
{
    public interface IApi
    {
        string ApiName { get; }
        WeatherInfo GetData();
    }  
}
