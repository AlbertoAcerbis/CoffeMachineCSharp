using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CoffeMachine.Shared.Enums
{
    //https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
    //https://www.planetgeek.ch/2009/07/01/enums-are-evil/
    public abstract class Enumeration : IComparable
    {
        public string Name { get; }
        public string Code { get; }
        public int Id { get; }
        public double Price { get; }

        public bool CanBeCold { get; }

        protected Enumeration(int id, string code, string name, double price, bool canBeCold = false)
        {
            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.Price = price;
            this.CanBeCold = canBeCold;
        }

        public override string ToString() => this.Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
                return false;

            var typeMatches = this.GetType() == obj.GetType();
            var valueMatches = this.Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => this.Id.GetHashCode();

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        public static T FromCode<T>(string code) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(code, "code", item => item.Code == code);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        public int CompareTo(object other) => this.Id.CompareTo(((Enumeration)other).Id);
    }
}