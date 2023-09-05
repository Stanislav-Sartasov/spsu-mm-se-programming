namespace DeansOffice
{
    public class ReferenceMarkPair<T>
    {
        public T Reference { get; set; }
        public bool Mark { get; set; }

        public ReferenceMarkPair(T reference, bool mark)
        {
            Reference = reference;
            Mark = mark;
        }
    }
}
