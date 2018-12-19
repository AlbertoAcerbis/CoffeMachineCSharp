using System;
using System.Text;
using CoffeMachine.Shared.JsonModel;

namespace CoffeMachine
{
    public class CommandTranslator
    {
        public static string GetDrinkMakerComand(DrinkCommand userComand)
        {
            var commandBuilder = new StringBuilder();

            var moneyDifference = CheckEnoughMoney(userComand.DrinkType.Price, userComand.Money.Value);

            if (moneyDifference < 0)
            {
                return $"M:Not enough money. Please insert {Math.Abs(moneyDifference)}";
            }

            commandBuilder.AppendFormat("{0}", userComand.DrinkType.Code);
            if (userComand.IsCold.Value && userComand.DrinkType.CanBeCold)
            {
                commandBuilder.Append("c");
            }
            if (userComand.Sugar.Value > 0)
            {
                commandBuilder.AppendFormat(":{0}", userComand.Sugar.Value);
                commandBuilder.Append(":0");
            }
            else
            {
                commandBuilder.Append("::");
            }

            return commandBuilder.ToString();
        }

        private static double CheckEnoughMoney(double price, double money)
        {
            return money - price;
        }
    }
}