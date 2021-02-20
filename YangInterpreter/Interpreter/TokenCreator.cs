using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using YangInterpreter.Statements;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Statements.Types;

namespace YangInterpreter.Interpreter
{
    /// <summary>
    /// Creates a token for the given data. Core of the interpreter.
    /// </summary>
    internal static class TokenCreator
    {
        private static List<SearchScheme> InterpreterSearchSchemeList;
        private static SearchScheme InnerBlockParser = new SearchScheme(new Regex("\\s*{\\s*(?<value>[^}]*\\s*})\\s*$"), TokenTypes.Empty, null);
        private static SearchScheme InnerEndlineParser = new SearchScheme(new Regex(@".(?<value>})\s*$"), TokenTypes.NodeEndingBracket, null);

        internal static void Init()
        {
            InterpreterSearchSchemeList = new List<SearchScheme>
            {
                new SearchScheme(new Regex(@"^\s*module (?<name>[a-z0-9A-Z-]*) {\s*$",         RegexOptions.IgnoreCase),TokenTypes.Module,                     typeof(Module)),
                new SearchScheme(new Regex(@"^\s*grouping (?<name>[a-z0-9A-Z-]*) {\s*$",       RegexOptions.IgnoreCase),TokenTypes.Grouping,                   typeof(Grouping)),
                new SearchScheme(new Regex(@"^\s*uses (?<name>[a-z0-9A-Z-]*);\s*$",            RegexOptions.IgnoreCase),TokenTypes.Uses,                       typeof(Uses)),
                new SearchScheme(new Regex(@"^\s*choice (?<name>[a-z0-9A-Z-]*) {\s*$",         RegexOptions.IgnoreCase),TokenTypes.Choice,                     typeof(Choices)),
                new SearchScheme(new Regex(@"^\s*case (?<name>[a-z0-9A-Z-]*) {\s*$",           RegexOptions.IgnoreCase),TokenTypes.ChoiceCase,                 typeof(ChoiceCase)),
                new SearchScheme(new Regex("^\\s*prefix \"?(?<value>[^;^\"]*)\"?;\\s*$",       RegexOptions.IgnoreCase),TokenTypes.Prefix,                     typeof(Prefix)),
                new SearchScheme(new Regex(@"^\s*container (?<name>[a-z0-9A-Z-]*) {\s*$",      RegexOptions.IgnoreCase),TokenTypes.Container,                  typeof(Container)),
                new SearchScheme(new Regex(@"^\s*leaf\s*(?<name>[a-z0-9A-Z-]*)\s*{\s*$",       RegexOptions.IgnoreCase),TokenTypes.Leaf,                       typeof(Leaf)),
                new SearchScheme(new Regex(@"^\s*leaf-list (?<name>[a-z0-9A-Z-]*) {\s*$",      RegexOptions.IgnoreCase),TokenTypes.LeafList,                   typeof(LeafList)),
                new SearchScheme(new Regex(@"^\s*list (?<name>[a-z0-9A-Z-]*) {\s*$",           RegexOptions.IgnoreCase),TokenTypes.List,                       typeof(ListNode)),

                new SearchScheme(new Regex("^\\s*pattern\\s*\"(?<name>[^{\\s\"]+)\"(;|(?<bracket>.*{.*)*)$",  RegexOptions.IgnoreCase),TokenTypes.Pattern,                    typeof(Pattern)),
                new SearchScheme(new Regex(@"^\s*revision\s*(?<name>[a-z0-9A-Z-]+)(;|(?<bracket>.*{.*)*)$",      RegexOptions.IgnoreCase),TokenTypes.Revision,                   typeof(Revision),true),
                new SearchScheme(new Regex(@"^\s*status\s*(?<name>[a-z0-9A-Z-]*)\s*;\s*$",     RegexOptions.IgnoreCase),TokenTypes.Status,                     typeof(StatusStatement),true),
                new SearchScheme(new Regex(@"^\s*value\s*(?<name>[a-z0-9A-Z-]*)\s*;\s*$",      RegexOptions.IgnoreCase),TokenTypes.Value,                      typeof(ValueStatement),true),
                new SearchScheme(new Regex("^\\s*length \"?(?<value>[^;\"]*)\"?;\\s*$",         RegexOptions.IgnoreCase),TokenTypes.Length,                     typeof(Length),true),

                new SearchScheme(new Regex(@"^\s*position\s*(?<value>[0-9])*;\s*$",             RegexOptions.IgnoreCase),TokenTypes.Position,                   typeof(Position)),

                new SearchScheme(new Regex(@"^\s*type enumeration\s*{\s*$",               RegexOptions.IgnoreCase),TokenTypes.TypeEnum,                   typeof(EnumTypeStatement)),
                new SearchScheme(new Regex(@"^\s*type bits\s*{\s*$",                    RegexOptions.IgnoreCase),TokenTypes.TypeBits,                   typeof(BitsTypeStatement)),
                new SearchScheme(new Regex(@"^\s*type\s*empty;\s*$",                    RegexOptions.IgnoreCase),TokenTypes.TypeEmpty,                  typeof(EmptyTypeStatement),true),
                new SearchScheme(new Regex(@"^\s*type\s*leafref\s*{\s*$",               RegexOptions.IgnoreCase),TokenTypes.TypeLeafRef,                typeof(LeafRefTypeStatement)),

                new SearchScheme(new Regex(@"^\s*type\s*int64(?<bracket>.*{.*)*$",                 RegexOptions.IgnoreCase),TokenTypes.TypeInt64,                  typeof(Int64TypeStatement)),
                new SearchScheme(new Regex(@"^\s*type\s*int32(?<bracket>.*{.*)*$",                 RegexOptions.IgnoreCase),TokenTypes.TypeInt32,                  typeof(Int32TypeStatement)),
                new SearchScheme(new Regex(@"^\s*type\s*int16(?<bracket>.*{.*)*$",                 RegexOptions.IgnoreCase),TokenTypes.TypeInt16,                  typeof(Int16TypeStatement)),
                new SearchScheme(new Regex(@"^\s*type\s*int8(?<bracket>.*{.*)*$",                  RegexOptions.IgnoreCase),TokenTypes.TypeInt8,                   typeof(Int8TypeStatement)),
                new SearchScheme(new Regex(@"^\s*type\s*uint64(?<bracket>.*{.*)*$",                RegexOptions.IgnoreCase),TokenTypes.TypeUInt64,                 typeof(UInt64TypeStatement)),
                new SearchScheme(new Regex(@"^\s*type\s*uint32(?<bracket>.*{.*)*$",                RegexOptions.IgnoreCase),TokenTypes.TypeUInt32,                typeof(UInt32TypeStatement)),
                new SearchScheme(new Regex(@"^\s*type\s*uint16(?<bracket>.*{.*)*$",                RegexOptions.IgnoreCase),TokenTypes.TypeUInt16,                 typeof(UInt16TypeStatement)),
                new SearchScheme(new Regex(@"^\s*type\s*uint8(?<bracket>.*{.*)*$",                 RegexOptions.IgnoreCase),TokenTypes.TypeUInt8,                 typeof(UInt8TypeStatement)),

                new SearchScheme(new Regex(@"^\s*type\s*decimal64(;\s*|(?<bracket>.*{.*)*)$",             RegexOptions.IgnoreCase),TokenTypes.TypeDecimal64,              typeof(Decimal64TypeStatement)),

                new SearchScheme(new Regex(@"^\s*type string(;\s*|(?<bracket>.*{.*)*)$",                  RegexOptions.IgnoreCase),TokenTypes.TypeString,                 typeof(StringTypeStatement)),

                new SearchScheme(new Regex(@"^\s*bit\s*(?<name>[a-z0-9A-Z-]+)(;\s*|(?<bracket>.*{.*)*)$",        RegexOptions.IgnoreCase),TokenTypes.SimpleBit,                  typeof(Bit)),
                new SearchScheme(new Regex(@"^\s*enum (?<name>[a-z0-9A-Z-]+)(;\s*|(?<bracket>.*{.*)*)$",         RegexOptions.IgnoreCase),TokenTypes.SimpleEnum,                 typeof(EnumStatement),true),

                new SearchScheme(new Regex("^\\s*range\\s*\"(?<name>[^;\"]*)\";(?<bracket>.*{.*)*$",        RegexOptions.IgnoreCase),TokenTypes.RangeSameLineStart,        typeof(RangeStatement),true),
                new SearchScheme(new Regex("^\\s*range\\s*\"(?<name>[^;\"]*)\\s*$",           RegexOptions.IgnoreCase),TokenTypes.RangeMultiLine,             typeof(RangeStatement), TokenTypes.RangeSameLineStart),
                new SearchScheme(new Regex("^\\s*range\\s*$",                           RegexOptions.IgnoreCase),TokenTypes.RangeMultiLine,             typeof(RangeStatement), TokenTypes.RangeNextLineStart),
                new SearchScheme(new Regex("^\\s*error-message\\s*\"(?<value>[^;\"]*)\";\\s*$",RegexOptions.IgnoreCase),TokenTypes.ErrorMessageSameLineStart,  typeof(ErrorMessageStatement)),
                new SearchScheme(new Regex("^\\s*error-message\\s*\"(?<value>[^;\"]*)\\s*$",   RegexOptions.IgnoreCase),TokenTypes.ErrorMessageMultiLine,      typeof(ErrorMessageStatement), TokenTypes.ErrorMessageSameLineStart),
                new SearchScheme(new Regex("^\\s*error-message\\s*$",                   RegexOptions.IgnoreCase),TokenTypes.ErrorMessageMultiLine,     typeof(ErrorMessageStatement), TokenTypes.ErrorMessageNextLineStart),
                new SearchScheme(new Regex("^\\s*error-app-tag\\s*\"(?<value>[^;\"]*)\";\\s*$",RegexOptions.IgnoreCase),TokenTypes.ErrorAppTagSameLineStart,   typeof(ErrorAppTagStatement)),
                new SearchScheme(new Regex("^\\s*error-app-tag\\s*\"(?<value>[^;\"]*)\\s*$",   RegexOptions.IgnoreCase),TokenTypes.ErrorAppTagMultiLine,       typeof(ErrorAppTagStatement), TokenTypes.ErrorAppTagSameLineStart),
                new SearchScheme(new Regex("^\\s*error-app-tag\\s*$",                   RegexOptions.IgnoreCase),TokenTypes.ErrorAppTagMultiLine,       typeof(ErrorAppTagStatement), TokenTypes.ErrorAppTagNextLineStart),
                new SearchScheme(new Regex("^\\s*path \"(?<value>[^;\"]*)\";\\s*$",            RegexOptions.IgnoreCase),TokenTypes.PathSameLineStart,          typeof(PathStatement)),
                new SearchScheme(new Regex("^\\s*path \"(?<value>[^;\"]*)\\s*$",               RegexOptions.IgnoreCase),TokenTypes.PathMultiLine,               typeof(PathStatement), TokenTypes.PathSameLineStart),
                new SearchScheme(new Regex("^\\s*path\\s*$",                            RegexOptions.IgnoreCase),TokenTypes.PathMultiLine,               typeof(PathStatement), TokenTypes.PathNextLineStart),
                new SearchScheme(new Regex("^\\s*contact \"(?<value>[^;\"]*)\";\\s*$",         RegexOptions.IgnoreCase),TokenTypes.ContactSameLineStart,       typeof(Contact)),
                new SearchScheme(new Regex("^\\s*contact \"(?<value>[^;\"]*)\\s*$",            RegexOptions.IgnoreCase),TokenTypes.ContactMultiLine,          typeof(Contact), TokenTypes.ContactSameLineStart),
                new SearchScheme(new Regex("^\\s*contact\\s*$",                         RegexOptions.IgnoreCase),TokenTypes.ContactMultiLine,            typeof(Contact), TokenTypes.ContactNextLineStart),
                new SearchScheme(new Regex("^\\s*namespace \"(?<value>[^;\"]*)\";\\s*$",       RegexOptions.IgnoreCase),TokenTypes.NamespaceSameLineStart,     typeof(NamespaceStatement)),
                new SearchScheme(new Regex("^\\s*namespace \"(?<value>[^;\"]*)\\s*$",          RegexOptions.IgnoreCase),TokenTypes.NamespaceMultiLine,          typeof(NamespaceStatement),TokenTypes.NamespaceSameLineStart),
                new SearchScheme(new Regex("^\\s*namespace\\s*$",                       RegexOptions.IgnoreCase),TokenTypes.NamespaceMultiLine,          typeof(NamespaceStatement),TokenTypes.NamespaceNextLineStart),
                new SearchScheme(new Regex("^\\s*organization \"(?<value>[^;\"]*)\";\\s*$",    RegexOptions.IgnoreCase),TokenTypes.OrganizationSameLineStart,   typeof(Organization)),
                new SearchScheme(new Regex("^\\s*organization \"(?<value>[^;\"]*)\\s*$",       RegexOptions.IgnoreCase),TokenTypes.OrganizationMultiLine,       typeof(Organization),TokenTypes.OrganizationSameLineStart),
                new SearchScheme(new Regex("^\\s*organization\\s*$",                    RegexOptions.IgnoreCase),TokenTypes.OrganizationMultiLine,       typeof(Organization),TokenTypes.OrganizationNextLineStart),
                new SearchScheme(new Regex("^\\s*reference \"(?<value>[^;\"]*)\";\\s*$",       RegexOptions.IgnoreCase),TokenTypes.ReferenceSameLineStart,      typeof(Reference)),
                new SearchScheme(new Regex("^\\s*reference \"(?<value>[^;\"]*)\\s*$",          RegexOptions.IgnoreCase),TokenTypes.ReferenceMultiline,          typeof(Reference),TokenTypes.ReferenceSameLineStart),
                new SearchScheme(new Regex("^\\s*reference\\s*$",                       RegexOptions.IgnoreCase),TokenTypes.ReferenceMultiline,         typeof(Reference),TokenTypes.ReferenceNextLineStart),

                new SearchScheme(new Regex("^\\s*description \"(?<value>[^;\"]*)\";\\s*$",     RegexOptions.IgnoreCase),TokenTypes.DescriptionSameLineStart,    typeof(Description)),
                new SearchScheme(new Regex("^\\s*description \"(?<value>[^;\"]*)\\s*$",        RegexOptions.IgnoreCase),TokenTypes.DescriptionMultiLine,        typeof(Description),TokenTypes.DescriptionSameLineStart),
                new SearchScheme(new Regex("^\\s*description\\s*$",                     RegexOptions.IgnoreCase),TokenTypes.DescriptionMultiLine,        typeof(Description),TokenTypes.DescriptionNextLineStart),

                new SearchScheme(new Regex("^\\s*\"(?<value>[^;]*)\";\\s*$"),                   TokenTypes.ValueForPreviousLine,                                null),
                new SearchScheme(new Regex("^\\s*\"(?<value>[^\"]*)\\s*$"),                     TokenTypes.ValueForPreviousLineBeg,                             null,TokenTypes.Empty),
                new SearchScheme(new Regex("^\\s*(?<value>[^;]*)\";(?<bracket>.*{.*)*\\s*$"),                     TokenTypes.ValueForPreviousLineEnd,                             null,TokenTypes.Empty),

                new SearchScheme(new Regex(@"^\s*yang-version (?<value>[a-z0-9A-Z-]*);\s*$"),   TokenTypes.YangVersion,                                         typeof(YangVersionStatement)),
                new SearchScheme(new Regex("^\\s*import\\s*(?<name>[a-z0-9A-Z-]*)\\s*{\\s*$"), TokenTypes.Import,                                             typeof(Import)),

                new SearchScheme(new Regex(@"^\s* *$"),                                 TokenTypes.Skip,null),
                new SearchScheme(new Regex(@"^\s*}\s*$"),                               TokenTypes.NodeEndingBracket,null),

                //Multiline of value or error based on previous state
                new SearchScheme(new Regex("(?s)^(?!.*[\";])[\\s\\t]*(?<value>.*)$"),         TokenTypes.ValueForPreviousLineMultiline, null, TokenTypes.ValueForPreviousLineBeg),
            };
        }

        /// <summary>
        /// Creates a YangInterpreter.Interpreter.Token for the given row.
        /// </summary>
        /// <param name="row">A row of a yang file</param>
        /// <returns></returns>
        internal static Token GetTokenForRow(string row, Token currentToken)
        {
            Token MatchResultToken;

            MatchResultToken = new Token(TokenTypes.Empty, "", "", null, TokenTypes.Empty, false);


            row = InnerBlockTryParse(MatchResultToken, row);
            foreach (var scheme in InterpreterSearchSchemeList)
            {
                Match match = scheme.Reg.Match(row);
                if (match.Success)
                {
                    MatchResultToken.TokenType = scheme.TokenType;
                    MatchResultToken.TokenAsSingleLine = scheme.TokenAsSingleLine;
                    MatchResultToken.TokenAsType = scheme.TokenAsType;
                    MatchResultToken.IsChildlessContainer = scheme.IsChildlessContainer;

                    if (match.Groups["name"].Value != "")
                        MatchResultToken.TokenName = match.Groups["name"].Value;
                    if (match.Groups["value"].Value != "")
                        MatchResultToken.TokenValue = match.Groups["value"].Value;

                    if (match.Groups["bracket"].Value != "")
                    {
                        if (currentToken != null)
                            MatchResultToken.TokenAsSingleLine = currentToken.TokenAsSingleLine;
                        MatchResultToken.IsChildlessContainer = false;
                    }
                    return MatchResultToken;
                }
            }
            return null;
        }

        private static string InnerBlockTryParse(Token MatchResult, string row)
        {
            Match match = InnerBlockParser.Reg.Match(row);
            if (match.Success)
            {
                MatchResult.InnerBlock = match.Groups["value"].Value;
                return row.Replace(match.Groups["value"].Value, "");
            }

            match = InnerEndlineParser.Reg.Match(row);
            if (match.Success)
            {
                MatchResult.InnerBlock = match.Groups["value"].Value;
                return InnerEndlineParser.Reg.Replace(row, "", 1);
            }
            return row;
        }
    }
}
