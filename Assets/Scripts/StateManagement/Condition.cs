namespace StateManagement
{
    public delegate bool Condition<in T>(T context);
}