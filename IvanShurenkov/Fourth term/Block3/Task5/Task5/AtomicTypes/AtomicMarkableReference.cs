using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.AtomicTypes;

[Serializable]
public class AtomicMarkableReference<T> : IAtomicMarkableReference<T>
{
    [Serializable]
    private class ReferencePair<T>
    {
        private readonly T _reference;
        private readonly bool _mark;

        internal ReferencePair(T reference, bool mark)
        {
            _reference = reference;
            _mark = mark;
        }

        public T Reference
        {
            get { return _reference; }
        }

        public bool Mark
        {
            get { return _mark; }
        }
    }

    private readonly AtomicReference<ReferencePair<T>> _atomicReference;

    public AtomicMarkableReference(T initialValue, bool initialMark)
    {
        _atomicReference = new AtomicReference<ReferencePair<T>>(new ReferencePair<T>(initialValue, initialMark));
    }

    private ReferencePair<T> Pair
    {
        get { return _atomicReference.Reference; }
    }

    public T GetReference()
    {
        return Pair.Reference;
    }

    public bool Mark
    {
        get { return Pair.Mark; }
    }

    public bool CompareAndSet(T expectedReference, T newReference, bool expectedMark, bool newMark)
    {
        ReferencePair<T> current = Pair;

        return expectedReference.Equals(current.Reference) && expectedMark == current.Mark &&
            ((newReference.Equals(current.Reference) && newMark == current.Mark) ||
            _atomicReference.CompareAndSet(current, new ReferencePair<T>(newReference, newMark)));
    }

/*    public bool AttemptMark(T expectedReference, bool newMark)
    {
        ReferencePair<T> curr = Pair;

        return expectedReference.Equals(curr.Reference) &&
            (newMark == curr.Mark ||
            _atomicReference.CompareAndSet(curr, new ReferencePair<T>(expectedReference, newMark)));
    }
*/
    public T Get(out bool marked)
    {
        ReferencePair<T> pair = Pair;
        marked = pair.Mark;
        return pair.Reference;
    }
/*
    public void Set(T newReference, bool newMark)
    {
        ReferencePair<T> curr = Pair;
        if (!newReference.Equals(curr.Reference) || newMark != curr.Mark)
        {
            _atomicReference.Reference = new ReferencePair<T>(newReference, newMark);
        }
    }
*/
}
