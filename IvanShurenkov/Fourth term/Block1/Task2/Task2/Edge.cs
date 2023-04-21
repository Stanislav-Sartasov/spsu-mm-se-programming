namespace Graph
{
    [Serializable]
    class Edge
    {
        public int Src { get; }
        public int Dest { get; }
        public int Weight { get; }
        public int Id { get; }

        public Edge(int src, int dest, int weight, int id)
        {
            this.Src = src;
            this.Dest = dest;
            this.Weight = weight;
            this.Id = id;
        }
    }
}