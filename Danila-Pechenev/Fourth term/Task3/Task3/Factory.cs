using System.Runtime.Serialization.Formatters;

namespace Task3;

public class Factory
{
    List<Participant> participants;
    List<int> numbers;
    private Semaphore semaphore;

    public Factory(int producersNumber, int consumersNumber, int maxObjectsNumberMax, int pauseBetweenActionsMax)
    {
        var random = new Random();
        semaphore = new Semaphore(0, 1);
        numbers = new List<int>();

        participants = new List<Participant>();
        for (int i = 0; i < producersNumber; i++)
        {
            participants.Add(new Producer(semaphore, random.Next(1, maxObjectsNumberMax + 1), random.Next(1, pauseBetweenActionsMax + 1), numbers));
        }

        for (int i = 0; i < consumersNumber; i++)
        {
            participants.Add(new Consumer(semaphore, random.Next(1, maxObjectsNumberMax + 1), random.Next(1, pauseBetweenActionsMax + 1), numbers));
        }
    }

    public void Start()
    {
        Console.WriteLine("The factory starts working.");
        foreach (var participant in participants)
        {
            participant.StartAction();
        }
    }

    public void Stop()
    {
        foreach (var participant in participants)
        {
            participant.StopAction();
        }

        Console.WriteLine("The factory has finished working.");
    }
}
