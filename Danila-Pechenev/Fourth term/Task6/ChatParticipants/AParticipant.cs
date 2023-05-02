using System;

namespace ChatParticipants
{
    public abstract class AParticipant : IDisposable
    {
        public abstract string IP { get; protected set; }

        public abstract int Port { get; protected set; }

        public abstract void Start();

        public abstract void Dispose();

        protected abstract void Listen();
    }
}
