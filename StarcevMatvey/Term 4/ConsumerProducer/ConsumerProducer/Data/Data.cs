namespace ConsumerProducer
{
    public class Data<T>
    {
        public T Inf { get; set; }

        public Data (T data)
        {
            Inf = data;
        }
    }
}
