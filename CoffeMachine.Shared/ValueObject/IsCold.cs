namespace CoffeMachine.Shared.ValueObject
{
    public sealed class IsCold
    {
        public readonly bool Value;

        public IsCold(bool value)
        {
            Value = value;
        }
    }
}
