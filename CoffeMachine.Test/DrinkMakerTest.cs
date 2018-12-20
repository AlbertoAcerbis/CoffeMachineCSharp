using CoffeMachine.Shared.Enums;
using CoffeMachine.Shared.JsonModel;
using CoffeMachine.Shared.ValueObject;
using Xunit;

namespace CoffeMachine.Test
{
    public class DrinkMakerTest
    {
        [Fact]
        public void Should_Prepare_Drink()
        {
            var coffeMachine = new CoffeeMachine();


        }

        [Theory]
        [InlineData("Coffee", 0, 0.1,false, "M:Not enough money. Please insert 0.5")]
        [InlineData("Coffee", 0, 1.0, false,"C::")]
        [InlineData("Thea", 0, 0.1, false,"M:Not enough money. Please insert 0.3")]
        [InlineData("Thea", 1, 1.0, false,"T:1:0")]
        [InlineData("Hot Chocolate",0, 0.1,false, "M:Not enough money. Please insert 0.4")]
        [InlineData("Hot Chocolate", 2, 1.0, false,"H:2:0")]
        [InlineData("Orange", 0, 1.0,false, "O::")]
        [InlineData("Coffee", 0, 1.0, true,"Cc::")]
        public void Should_Translate_CommandPocoInto_String(string drinkType, int sugarQuantity, double money,bool isCold,  string expected)
        {
            var drinkTypeEnum = DrinkType.FromName(drinkType);

            var userCommand = new DrinkCommand
            {
                DrinkType = drinkTypeEnum,
                Sugar = new Sugar(sugarQuantity),
                Money = new Money(money),
                IsCold =  new IsCold(isCold),
            };

            var coffeeMachine = new CoffeeMachine();

            var drinkMakerCommand = coffeeMachine.GetDrinkMakerComand(userCommand);

            Assert.Equal(expected, drinkMakerCommand);
        }

        [Fact]
        public void Should_Throw_Exception_With_WrongDrinkType()
        {

        }


        [Fact]
        public void DoesReportMatch()
        {
            var coffeMachine = new CoffeeMachine();

            var test1 = GenerateTestCommand("Coffee", 1, 1.0, false);

            coffeMachine.GetDrinkMakerComand(test1);

            var expected = "Coffee: 1; Cassa: 0.6";

            Assert.Equal(expected, coffeMachine.GetReport());
            
        }


        private DrinkCommand GenerateTestCommand(string drinkType, int sugarQuantity, double money, bool isCold)
        {
            var drinkTypeEnum = DrinkType.FromName(drinkType);

            var userCommand = new DrinkCommand
            {
                DrinkType = drinkTypeEnum,
                Sugar = new Sugar(sugarQuantity),
                Money = new Money(money),
                IsCold = new IsCold(isCold),
            };

            return userCommand;
        }


    }
}
