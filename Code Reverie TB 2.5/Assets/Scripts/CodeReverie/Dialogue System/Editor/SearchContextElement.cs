namespace CodeReverie
{
    public struct SearchContextElement
    {
        public object target { get; private set; }
        public string category { get; private set; }

        public SearchContextElement(object target, string name)
        {
            this.target = target;
            this.category = name;
        }
        
    }
}