namespace CodeReverie
{
    public class StatsContainer
    {
        public Stat stat;
        public float statValue;
        
        public StatsContainer(Stat stat, float value)
        {
            this.stat = stat;
            StatValue = value;
        }
        
        
        public float StatValue
        {
            get { return statValue; }
            set { statValue = value; }
        }
        
    }
}