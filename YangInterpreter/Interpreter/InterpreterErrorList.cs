using System;

namespace YangInterpreter.Interpreter
{
    [Serializable()]
    public class InvalidYangVersion : System.Exception
    {
        public InvalidYangVersion() : base() { }
        public InvalidYangVersion(string message) : base(message) { }
        public InvalidYangVersion(string message, System.Exception inner) : base(message, inner) { }
        protected InvalidYangVersion(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable()]
    public class InterpreterParseFail : System.Exception
    {
        public InterpreterParseFail() : base() { }
        public InterpreterParseFail(string message) : base(message) { }
        public InterpreterParseFail(string message, System.Exception inner) : base(message, inner) { }
        protected InterpreterParseFail(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable()]
    public class ImproperValue : System.Exception
    {
        public ImproperValue() : base() { }
        public ImproperValue(string message) : base(message) { }
        public ImproperValue(string message, System.Exception inner) : base(message, inner) { }
        protected ImproperValue(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable()]
    public class StatementEndIsMissing : System.Exception
    {
        public StatementEndIsMissing() : base() { }
        public StatementEndIsMissing(string message) : base(message) { }
        public StatementEndIsMissing(string message, System.Exception inner) : base(message, inner) { }
        protected StatementEndIsMissing(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
