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
        private static SearchScheme InnerBlockParser = new SearchScheme(new Regex("\\s*{\\s*([^}]*\\s*})\\s*$"), TokenTypes.Empty, -1, 1, null);
        private static SearchScheme InnerEndlineParser = new SearchScheme(new Regex(@".(})\s*$"), TokenTypes.NodeEndingBracket, -1, -1, null);

        internal static void Init()
        {
            InterpreterSearchSchemeList = new List<SearchScheme>
            {
                //                                              name, value, OPtional Token as single line if multiline token
                //new SearchScheme(new Regex(@"^\s*$"),TokenTypes,-1,-1),

                new SearchScheme(new Regex(@"^\s*module ([a-z0-9A-Z-]*) {\s*$",         RegexOptions.IgnoreCase),TokenTypes.Module,                     1,-1,typeof(Module)),
                new SearchScheme(new Regex(@"^\s*grouping ([a-z0-9A-Z-]*) {\s*$"),      TokenTypes.Grouping,                                            1,-1,typeof(Grouping)),
                new SearchScheme(new Regex(@"^\s*uses ([a-z0-9A-Z-]*);\s*$"),           TokenTypes.Uses,                                                1,-1, typeof(Uses)),
                new SearchScheme(new Regex(@"^\s*choice ([a-z0-9A-Z-]*) {\s*$"),        TokenTypes.Choice,                                              1,-1, typeof(Choices)),
                new SearchScheme(new Regex(@"^\s*case ([a-z0-9A-Z-]*) {\s*$"),          TokenTypes.ChoiceCase,                                          1,-1, typeof(ChoiceCase)),
                new SearchScheme(new Regex("^\\s*prefix \"?([^;|^\"]*)\"?;\\s*$",       RegexOptions.IgnoreCase),TokenTypes.Prefix,                     -1,1, typeof(Prefix)),
                new SearchScheme(new Regex(@"^\s*container ([a-z0-9A-Z-]*) {\s*$",      RegexOptions.IgnoreCase),TokenTypes.Container,                  1,-1, typeof(Container)),
                new SearchScheme(new Regex(@"^\s*leaf\s*([a-z0-9A-Z-]*)\s*{\s*$",       RegexOptions.IgnoreCase),TokenTypes.Leaf,                       1,-1, typeof(Leaf)),
                new SearchScheme(new Regex(@"^\s*leaf-list ([a-z0-9A-Z-]*) {\s*$",      RegexOptions.IgnoreCase),TokenTypes.LeafList,                   1,-1, typeof(LeafList)),
                new SearchScheme(new Regex(@"^\s*list ([a-z0-9A-Z-]*) {\s*$",           RegexOptions.IgnoreCase),TokenTypes.List,                       1,-1, typeof(ListNode)),

                new SearchScheme(new Regex("^\\s*pattern\\s*\"([^{|\\s|\"]*)\"\\s*{$",         RegexOptions.IgnoreCase),TokenTypes.Pattern,                    1,-1, typeof(Pattern)),
                new SearchScheme(new Regex("^\\s*pattern\\s*\"([^\"]*)\";\\s*$",          RegexOptions.IgnoreCase),TokenTypes.Pattern,                    1,-1, typeof(Pattern),true),
                new SearchScheme(new Regex(@"^\s*revision\s*([a-z0-9A-Z-]*)\s*{$",      RegexOptions.IgnoreCase),TokenTypes.Revision,                   1,-1, typeof(Revision)),
                new SearchScheme(new Regex(@"^\s*revision\s*([a-z0-9A-Z-]*);\s*$",      RegexOptions.IgnoreCase),TokenTypes.Revision,                   1,-1, typeof(Revision),true),
                
                new SearchScheme(new Regex(@"^\s*status\s*([a-z0-9A-Z-]*)\s*;\s*$",     RegexOptions.IgnoreCase),TokenTypes.Status,                     1,-1, typeof(StatusStatement),true),
                new SearchScheme(new Regex(@"^\s*value\s*([a-z0-9A-Z-]*)\s*;\s*$",      RegexOptions.IgnoreCase),TokenTypes.Value,                      1,-1, typeof(ValueStatement),true),
                new SearchScheme(new Regex("^\\s*length \"?([^;|^\"]*)\"?;\\s*$",       RegexOptions.IgnoreCase),TokenTypes.Length,                     -1,1, typeof(Length),true),

                new SearchScheme(new Regex(@"^\s*position\s*([0-9])*;\s*$",             RegexOptions.IgnoreCase),TokenTypes.Position,                   -1,1, typeof(Position)),

                new SearchScheme(new Regex(@"^\s*type enumeration {\s*$",               RegexOptions.IgnoreCase),TokenTypes.TypeEnum,                   1,-1, typeof(EnumTypeStatement)),
                new SearchScheme(new Regex(@"^\s*type bits\s*{\s*$",                    RegexOptions.IgnoreCase),TokenTypes.TypeBits,                   1,-1, typeof(BitsTypeStatement)),
                new SearchScheme(new Regex(@"^\s*type\s*empty;\s*$",                    RegexOptions.IgnoreCase),TokenTypes.TypeEmpty,                  1,-1, typeof(EmptyTypeStatement),true),
                new SearchScheme(new Regex(@"^\s*type\s*leafref\s*{\s*$",               RegexOptions.IgnoreCase),TokenTypes.TypeLeafRef,                1,-1, typeof(LeafRefTypeStatement)),

                new SearchScheme(new Regex(@"^\s*type string\s*{\s*$",                  RegexOptions.IgnoreCase),TokenTypes.TypeString,                 1,-1, typeof(StringTypeStatement)),
                new SearchScheme(new Regex(@"^\s*type string;\s*$",                     RegexOptions.IgnoreCase),TokenTypes.TypeString,                 1,-1, typeof(StringTypeStatement),true),

                new SearchScheme(new Regex(@"^\s*bit\s*([a-z0-9A-Z-]*)\s*{\s*$",        RegexOptions.IgnoreCase),TokenTypes.SimpleBit,                  1,-1, typeof(Bit)),
                new SearchScheme(new Regex(@"^\s*enum ([a-z0-9A-Z-]*);\s*$",            RegexOptions.IgnoreCase),TokenTypes.SimpleEnum,                 1,-1, typeof(EnumStatement),true),
                new SearchScheme(new Regex(@"^\s*enum ([a-z0-9A-Z-]*)\s*{\s*$",         RegexOptions.IgnoreCase),TokenTypes.SimpleEnum,                 1,-1, typeof(EnumStatement)),

                new SearchScheme(new Regex("^\\s*path \"([^;]*)\";\\s*$",               RegexOptions.IgnoreCase),TokenTypes.PathSameLineStart,          -1,1, typeof(PathStatement)),
                new SearchScheme(new Regex("^\\s*path \"([^;]*)\\s*$",                  RegexOptions.IgnoreCase),TokenTypes.PathMultiLine,              -1,1, typeof(PathStatement), TokenTypes.PathSameLineStart),
                new SearchScheme(new Regex("^\\s*path\\s*$",                            RegexOptions.IgnoreCase),TokenTypes.PathMultiLine,              -1,-1, typeof(PathStatement), TokenTypes.PathNextLineStart),
                new SearchScheme(new Regex("^\\s*contact \"([^;]*)\";\\s*$",            RegexOptions.IgnoreCase),TokenTypes.ContactSameLineStart,       -1,1, typeof(Contact)),
                new SearchScheme(new Regex("^\\s*contact \"([^;]*)\\s*$",               RegexOptions.IgnoreCase),TokenTypes.ContactMultiLine,           -1,1, typeof(Contact), TokenTypes.ContactSameLineStart),
                new SearchScheme(new Regex("^\\s*contact\\s*$",                         RegexOptions.IgnoreCase),TokenTypes.ContactMultiLine,           -1,-1, typeof(Contact), TokenTypes.ContactNextLineStart),
                new SearchScheme(new Regex("^\\s*namespace \"([^;]*)\";\\s*$",          RegexOptions.IgnoreCase),TokenTypes.NamespaceSameLineStart,     -1,1, typeof(NamespaceStatement)),
                new SearchScheme(new Regex("^\\s*namespace \"([^;]*)\\s*$",             RegexOptions.IgnoreCase),TokenTypes.NamespaceMultiLine,         -1,1, typeof(NamespaceStatement),TokenTypes.NamespaceSameLineStart),
                new SearchScheme(new Regex("^\\s*namespace\\s*$",                       RegexOptions.IgnoreCase),TokenTypes.NamespaceMultiLine,         -1,-1, typeof(NamespaceStatement),TokenTypes.NamespaceNextLineStart),
                new SearchScheme(new Regex("^\\s*organization \"([^;]*)\";\\s*$",       RegexOptions.IgnoreCase),TokenTypes.OrganizationSameLineStart,  -1,1, typeof(Organization)),
                new SearchScheme(new Regex("^\\s*organization \"([^;]*)\\s*$",          RegexOptions.IgnoreCase),TokenTypes.OrganizationMultiLine,      -1,1, typeof(Organization),TokenTypes.OrganizationSameLineStart),
                new SearchScheme(new Regex("^\\s*organization\\s*$",                    RegexOptions.IgnoreCase),TokenTypes.OrganizationMultiLine,      -1,-1, typeof(Organization),TokenTypes.OrganizationNextLineStart),
                new SearchScheme(new Regex("^\\s*reference \"([^;]*)\";\\s*$",          RegexOptions.IgnoreCase),TokenTypes.ReferenceSameLineStart,     -1,1, typeof(Reference)),
                new SearchScheme(new Regex("^\\s*reference \"([^;]*)\\s*$",             RegexOptions.IgnoreCase),TokenTypes.ReferenceMultiline,         -1,1, typeof(Reference),TokenTypes.ReferenceSameLineStart),
                new SearchScheme(new Regex("^\\s*reference\\s*$",                       RegexOptions.IgnoreCase),TokenTypes.ReferenceMultiline,         -1,1, typeof(Reference),TokenTypes.ReferenceNextLineStart),

                new SearchScheme(new Regex("^\\s*description \"([^;]*)\";\\s*$",        RegexOptions.IgnoreCase),TokenTypes.DescriptionSameLineStart,   -1,1, typeof(Description)),
                new SearchScheme(new Regex("^\\s*description \"([^;]*)\\s*$",           RegexOptions.IgnoreCase),TokenTypes.DescriptionMultiLine,       -1,1, typeof(Description),TokenTypes.DescriptionSameLineStart),
                new SearchScheme(new Regex("^\\s*description\\s*$",                     RegexOptions.IgnoreCase),TokenTypes.DescriptionMultiLine,       -1,-1, typeof(Description),TokenTypes.DescriptionNextLineStart),
                
                new SearchScheme(new Regex("^\\s*\"([^;]*)\";\\s*$"),                   TokenTypes.ValueForPreviousLine,                                -1,1,null),
                new SearchScheme(new Regex("^\\s*\"([^\"]*)\\s*$"),                     TokenTypes.ValueForPreviousLineBeg,                             -1,1,null,TokenTypes.Empty),
                new SearchScheme(new Regex("^\\s*([^;]*)\";\\s*$"),                     TokenTypes.ValueForPreviousLineEnd,                             -1,1,null,TokenTypes.Empty),

                new SearchScheme(new Regex(@"^\s*yang-version ([a-z0-9A-Z-]*);\s*$"),   TokenTypes.YangVersion,                                         -1,1, typeof(YangVersionStatement)),
                new SearchScheme(new Regex("^\\s*import\\s*([a-z0-9A-Z-]*)\\s*{\\s*$"), TokenTypes.Import,                                              1,-1, typeof(Import)),

                new SearchScheme(new Regex(@"^\s* *$"),                                 TokenTypes.Skip,-1,-1,null),
                new SearchScheme(new Regex(@"^\s*}\s*$"),                               TokenTypes.NodeEndingBracket,-1,-1,null),

                //Multiline of value or error based on previous state
                new SearchScheme(new Regex("(?s)^(?!.*[\"|;])[\\s|\\t]*(.*)$"),         TokenTypes.ValueForPreviousLineMultiline,-1,1, null, TokenTypes.ValueForPreviousLineBeg),
            };
        }

        /// <summary>
        /// Creates a YangInterpreter.Interpreter.Token for the given row.
        /// </summary>
        /// <param name="row">A row of a yang file</param>
        /// <returns></returns>
        internal static Token GetTokenForRow(string row)
        {
            Token MatchResultToken = new Token(TokenTypes.Empty, "", "", null, TokenTypes.Empty,false);
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
                    if (scheme.IndexOfTokenName != -1)
                    {
                        MatchResultToken.TokenName = match.Groups[scheme.IndexOfTokenName].Value;
                    }
                    if (scheme.IndexOfTokenValue != -1)
                    {
                        MatchResultToken.TokenValue = match.Groups[scheme.IndexOfTokenValue].Value;
                    }
                    return MatchResultToken;
                }
            }
            /*InterpretationErrorHandler interpreterr = new InterpretationErrorHandler("unrecognisable line.",
                                                                                         "The interpreter could not convert the following line into any valid token:\r\n" + row.Trim(),
                                                                                         LineNumber,
                                                                                         InterpreterTracer);*/
            return null;
        }

        private static string InnerBlockTryParse(Token MatchResult,string row)
        {
            Match match = InnerBlockParser.Reg.Match(row);
            if (match.Success)
            {
                MatchResult.InnerBlock = match.Groups[1].Value;
                return row.Replace(match.Groups[1].Value, "");
            }

            match = InnerEndlineParser.Reg.Match(row);
            if (match.Success)
            {
                MatchResult.InnerBlock = match.Groups[1].Value;
                return InnerEndlineParser.Reg.Replace(row, "", 1);
            }
            return row;
        }
    }
}
