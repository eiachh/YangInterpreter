using System;
using System.Collections.Generic;
using System.Text;

namespace YangInterpreter.Interpreter
{
    [Serializable()]
    public class OverflownContainer : System.Exception
    {
        public OverflownContainer() : base() { }
        public OverflownContainer(string message) : base(message) { }
        public OverflownContainer(string message, System.Exception inner) : base(message, inner) { }
        protected OverflownContainer(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable()]
    public class TypebindingError : System.Exception
    {
        public TypebindingError() : base() { }
        public TypebindingError(string message) : base(message) { }
        public TypebindingError(string message, System.Exception inner) : base(message, inner) { }
        protected TypebindingError(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable()]
    public class TypeMissmatch : System.Exception
    {
        public TypeMissmatch() : base() { }
        public TypeMissmatch(string message) : base(message) { }
        public TypeMissmatch(string message, System.Exception inner) : base(message, inner) { }
        protected TypeMissmatch(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

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
}
