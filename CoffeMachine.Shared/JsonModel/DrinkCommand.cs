using CoffeMachine.Shared.Enums;
using CoffeMachine.Shared.ValueObject;

namespace CoffeMachine.Shared.JsonModel
{
    public class DrinkCommand
    {
        public DrinkType DrinkType { get; set; }
        public Sugar Sugar { get; set; }
        public Stick Stick => Sugar.Value > 0 
            ? new Stick(true) 
            : new Stick(false);

        public Money Money { get; set; }

        public IsCold IsCold { get; set; }
    }
}