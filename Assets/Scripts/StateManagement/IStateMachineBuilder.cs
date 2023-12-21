namespace StateManagement
{
    public interface IStateMachineBuilder<T> where T : class
    {
        void BuildStates(StateMachine<T> stateMachine);

        void BuildTransitions(StateMachine<T> stateMachine);
    }
}