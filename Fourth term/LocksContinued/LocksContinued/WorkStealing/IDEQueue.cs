using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocksContinued.WorkStealing
{
    public interface IDEQueue<T>
    {
        void PushBottom(T value);

        T PopTop();
        T PopBottom();

        bool IsEmpty();
    }
}
