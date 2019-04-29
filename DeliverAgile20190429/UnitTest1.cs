using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeliverAgile20190429
{
    //MicroObjects FizzBuzz

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ShouldReturnString1ForInput1()
        {
            var input = new Input(1);
            var fizzBuzz = new FizzBuzz();
            string result = fizzBuzz.Convert(input);
            result.Should().Be("1");
        }

        [TestMethod]
        public void ShouldReturnString2ForInput2()
        {
            Input input = new Input(2);
            IFizzBuzz fizzBuzz = new FizzBuzz();
            string result = fizzBuzz.Convert(input);
            result.Should().Be("2");
        }

        [TestMethod]
        public void ShouldReturnFizzForInput3()
        {
            Input input = new Input(3);
            IFizzBuzz fizzBuzz = new FizzBuzz();
            string result = fizzBuzz.Convert(input);
            result.Should().Be("Fizz");
        }

        [TestMethod]
        public void ShouldReturnFizzForInput6()
        {
            Input input = new Input(6);
            IFizzBuzz fizzBuzz = new FizzBuzz();
            string result = fizzBuzz.Convert(input);
            result.Should().Be("Fizz");
        }

        [TestMethod]
        public void ShouldReturnBuzzForInput5()
        {
            Input input = new Input(5);
            IFizzBuzz fizzBuzz = new FizzBuzz();
            string result = fizzBuzz.Convert(input);
            result.Should().Be("Buzz");
        }

        [TestMethod]
        public void ShouldReturnBuzzForInput10()
        {
            Input input = new Input(10);
            IFizzBuzz fizzBuzz = new FizzBuzz();
            string result = fizzBuzz.Convert(input);
            result.Should().Be("Buzz");
        }

        [TestMethod]
        public void ShouldReturnBuzzForInput15()
        {
            Input input = new Input(15);
            IFizzBuzz fizzBuzz = new FizzBuzz();
            string result = fizzBuzz.Convert(input);
            result.Should().Be("FizzBuzz");
        }

        [TestMethod]
        public void ShouldReturnBuzzForInput30()
        {
            Input input = new Input(30);
            IFizzBuzz fizzBuzz = new FizzBuzz();
            string result = fizzBuzz.Convert(input);
            result.Should().Be("FizzBuzz");
        }
    }

    public sealed class FizzBuzz : IFizzBuzz
    {
        private readonly IFizzBuzzAction _action;

        public FizzBuzz() : this(
            new FizzBuzzAction(
                new BuzzAction(
                    new FizzAction(
                        new ToStringAction()))))
        {}

        private FizzBuzz(IFizzBuzzAction action) => _action = action;

        public Result Convert(Input input) => _action.Act(input);
    }
    

    public sealed class FizzBuzzAction : BaseAction
    {
        private const int ThreeAndFive = 3*5;
        public FizzBuzzAction(IFizzBuzzAction nextAction) : base(nextAction, ThreeAndFive, new FizzBuzzResult()) { }
    }
    public sealed class BuzzAction : BaseAction
    {
        private const int BuzzValue = 5;
        public BuzzAction(IFizzBuzzAction nextAction) : base(nextAction, BuzzValue, new BuzzResult()) { }
    }
    public sealed class FizzAction : BaseAction
    {
        private const int FizzValue = 3;
        public FizzAction(IFizzBuzzAction nextAction) : base(nextAction, FizzValue, new FizzResult()) { }
    }

    public abstract class BaseAction : IFizzBuzzAction
    {
        private readonly IFizzBuzzAction _nextAction;
        private readonly int _value;
        private readonly Result _result;

        protected BaseAction(IFizzBuzzAction nextAction, int value, Result result)
        {
            _nextAction = nextAction;
            _value = value;
            _result = result;
        }

        public Result Act(Input input)
        {
            if (input % _value == 0) return _result;
            return _nextAction.Act(input);
        }
    }
    public sealed class ToStringAction : IFizzBuzzAction
    {
        public Result Act(Input input) => new ToStringResult(input);
    }
    public interface IFizzBuzzAction
    {
        Result Act(Input input);
    }
    public interface IFizzBuzz
    {
        Result Convert(Input input);
    }
    public sealed class Input
    {
        public static implicit operator int(Input origin) => origin._rawValue;
        private readonly int _rawValue;

        public Input(int origin) => _rawValue = origin;
    }
    public abstract class Result
    {
        public static implicit operator string(Result origin) => origin.RawValue();
        protected abstract string RawValue();
    }
    public sealed class ToStringResult : Result
    {
        private readonly Input _origin;
        public ToStringResult(Input origin) => _origin = origin;

        protected override string RawValue() => ((int)_origin).ToString();
    }
    public sealed class FizzResult : Result
    {
        protected override string RawValue() => "Fizz";
    }
    public sealed class FizzBuzzResult : Result
    {
        protected override string RawValue() => "FizzBuzz";
    }
    public sealed class BuzzResult : Result
    {
        protected override string RawValue() => "Buzz";
    }

}
