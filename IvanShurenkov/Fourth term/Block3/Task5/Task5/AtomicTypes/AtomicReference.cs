using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.AtomicTypes;

[Serializable]
public class AtomicReference<T>
{
    private T _reference;

    public AtomicReference(T initialValue)
    {
        _reference = initialValue;
    }

    public T Reference
    {
        get
        {
            lock (this)
            {
                return _reference;
            }
        }
        set
        {
            lock (this)
            {
                _reference = value;
            }
        }
    }

    public bool CompareAndSet(T expectedValue, T newValue)
    {
        lock (this)
        {
            if (!typeof(T).IsValueType)
            {
                if (_reference == null)
                {
                    if (expectedValue == null)
                    {
                        _reference = newValue;
                        return true;
                    }
                    return false;
                }
            }
            if (_reference.Equals(expectedValue))
            {
                _reference = newValue;
                return true;
            }
            return false;
        }
    }
}
