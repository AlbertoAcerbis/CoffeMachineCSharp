namespace CoffeMachine.Shared.ValueObject
{
    public sealed class Quantity
    {
        public readonly int Value;

        public Quantity(int value)
        {
            this.Value = value;
        }
    }
}