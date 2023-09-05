namespace AbstractOperators
{
    public abstract class ARunner
    {
        protected ALogger logger;

        public ARunner(ALogger logger)
        {
            this.logger = logger;
        }

        public abstract List<String> Start(string name, string args);
    }
}