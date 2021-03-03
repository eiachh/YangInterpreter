using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// If-Feature Statement RFC 6020 7.18.2. 
    /// 
    /// <summary>
    /// The "if-feature" statement makes its parent statement conditional.
    /// The argument is the name of a feature, as defined by a "feature"
    /// statement.The parent statement is implemented by servers that
    /// support this feature.If a prefix is present on the feature name, it
    /// refers to a feature defined in the module that was imported with that
    /// prefix, or the local module if the prefix matches the local module’s
    /// prefix. Otherwise, a feature with the matching name MUST be defined
    /// in the current module or an included submodule.
    /// </summary>
    public class IfFeatureStatement : ChildlessStatement
    {
        public IfFeatureStatement() : base("if-feature") { }
        public IfFeatureStatement(string Argument) : base("if-feature", Argument) { }
    }
}
