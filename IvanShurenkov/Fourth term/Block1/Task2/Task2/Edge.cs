namespace Graph
{
    [Serializable]
    class Edge
    {
        public int src { get; }
        public int dest { get; }
        public int weight { get; }
        public int id { get; }

        public Edge(int src, int dest, int weight, int id)
        {
            this.src = src;
            this.dest = dest;
            this.weight = weight;
            this.id = id;
        }
    }
}