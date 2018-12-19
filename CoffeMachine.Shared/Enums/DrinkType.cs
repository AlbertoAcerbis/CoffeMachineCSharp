using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeMachine.Shared.Enums
{
    public class DrinkType : Enumeration
    {

        public static DrinkType Thea = new DrinkType(1, "T", "Thea", 0.4);
        public static DrinkType HotChocolate = new DrinkType(2, "H", "Hot Chocolate", 0.5);
        public static DrinkType Coffee = new DrinkType(3, "C", "Coffee", 0.6, true);
        public static DrinkType Orange = new DrinkType(4, "O", "Orange",0.6);

        public static IEnumerable<DrinkType> List() => new[] { Thea, HotChocolate, Coffee, Orange };

        public DrinkType(int id, string code, string name, double price, bool canBeCold = false) : base(id, code, name, price, canBeCold)
        {
        }

        public static DrinkType FromName(string name)
        {
            var drinkType = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (drinkType == null)
                throw new Exception($"Possible values for DrinkType: {string.Join(",", List().Select(s => s.Name))}");

            return drinkType;
        }

        public static DrinkType FromCode(string code)
        {
            var drinkType = List().SingleOrDefault(s => string.Equals(s.Code, code, StringComparison.CurrentCultureIgnoreCase));

            if (drinkType == null)
                throw new Exception($"Possible values for DrinkType: {string.Join(",", List().Select(s => s.Code))}");

            return drinkType;
        }

        public static DrinkType From(int id)
        {
            var drinkType = List().SingleOrDefault(s => s.Id == id);

            if (drinkType == null)
                throw new Exception($"Possible values for DrinkType: {string.Join(",", List().Select(s => s.Name))}");

            return drinkType;
        }
    }
}