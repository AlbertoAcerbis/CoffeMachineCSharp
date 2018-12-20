using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoffeMachine.Shared.JsonModel;

namespace CoffeMachine
{
    public class CoffeeMachine
    {
        private IList<DrinkCommand> commandHistory;

        public string GetDrinkMakerComand(DrinkCommand userComand)
        {

            var uc = CommandTranslator.GetComand(userComand);
            if(uc.IsValid)
                this.commandHistory.Add(userComand);

            return uc.CommandResult;

        }


        public string GetReport()
        {
            string result = "";

            //"Coffee: 1; Cassa: 0.6";

            result += "" + this.commandHistory.GroupBy(x => x.DrinkType.Code).FirstOrDefault().Key +
                      this.commandHistory.GroupBy(x => x.DrinkType.Code).Count().ToString();
            result += "Cassa: " + this.commandHistory.Sum(x => x.DrinkType.Price);
         

            return result;
        }

    }
}
