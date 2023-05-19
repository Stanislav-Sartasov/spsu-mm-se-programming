using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.AtomicTypes;

public class AtomicNode<T>
{
    public T Value;
    public int Key;
    public IAtomicMarkableReference<AtomicNode<T>> CurrentMarkedAndNext = new AtomicMarkableReference<AtomicNode<T>>(null, false);
}