using System;
using System.Text;
using CoffeMachine.Shared.JsonModel;

namespace CoffeMachine
{
    internal class CommandTranslator
    {
        internal static CommandTranslationResult GetComand(DrinkCommand userComand)
        {
            var commandBuilder = new StringBuilder();

            var moneyDifference = CheckEnoughMoney(userComand.DrinkType.Price, userComand.Money.Value);

            if (moneyDifference < 0)
            {
                return new CommandTranslationResult
                {
                    CommandResult = $"M:Not enough money. Please insert {Math.Abs(moneyDifference)}",
                    IsValid = false
                };
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

            return new CommandTranslationResult
            {
                CommandResult = commandBuilder.ToString(),
                IsValid = true
            };
        }

        private static double CheckEnoughMoney(double price, double money)
        {
            return money - price;
        }
    }
}