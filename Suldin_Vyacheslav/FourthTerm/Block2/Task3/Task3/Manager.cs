using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Manager<T>
    {
        private static List<Participant<T>> actors = new List<Participant<T>>();

        public Manager(int nC, int nP, List<T> buffer)
        {
            for (int i = 0; i < nC; i++)
            {
                actors.Add(new Consumer<T>(buffer));
            }
            for (int i = 0; i < nP; i++)
            {
                actors.Add(new Producer<T>(buffer));
            }
        }

        public void Manage()
        {
            foreach (var actor in actors)
            {
                actor.Start();
            }
        }

        public void ShoutDown()
        {
            foreach (var actor in actors)
            {
                actor.Stop();
            }
        }
    }
}
