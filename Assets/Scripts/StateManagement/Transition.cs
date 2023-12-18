namespace StateManagement
{
    public class Transition<T> where T : class
    {
        private readonly Condition<T> _condition;

        public Transition(IState from, IState to, Condition<T> condition)
        {
            From = from;
            To = to;
            _condition = condition;
        }

        public IState From { get; }
        public IState To { get; }

        public bool Statement(T context)
        {
            return _condition(context);
        }
    }
}