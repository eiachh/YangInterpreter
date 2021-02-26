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
        private static SearchScheme InnerBlockParser = new SearchScheme(new Regex("\\s*{\\s*(?<argument>[^}]*\\s*})\\s*$"), null);
        private static SearchScheme InnerEndlineParser = new SearchScheme(new Regex(@".(?<argument>})\s*$"), null, TokenTypes.NodeEndingBracket);

        internal static void Init()
        {
            InterpreterSearchSchemeList = new List<SearchScheme>
            {
                new SearchScheme(new Regex(@"^\s*module (?<argument>[a-z0-9A-Z-]*) {\s*$",                              RegexOptions.IgnoreCase),typeof(Module)),
                new SearchScheme(new Regex(@"^\s*grouping (?<argument>[a-z0-9A-Z-]*) {\s*$",                            RegexOptions.IgnoreCase),typeof(Grouping)),
                new SearchScheme(new Regex(@"^\s*uses (?<argument>[a-z0-9A-Z-]*);\s*$",                                 RegexOptions.IgnoreCase),typeof(Uses)                      ),
                new SearchScheme(new Regex(@"^\s*choice (?<argument>[a-z0-9A-Z-]*) {\s*$",                              RegexOptions.IgnoreCase),typeof(Choices)                   ),
                new SearchScheme(new Regex(@"^\s*case (?<argument>[a-z0-9A-Z-]*) {\s*$",                                RegexOptions.IgnoreCase),typeof(ChoiceCase)               ),
                new SearchScheme(new Regex("^\\s*prefix \"?(?<argument>[^;\"]*)\"?;\\s*$",                             RegexOptions.IgnoreCase),typeof(Prefix)                   ),
                new SearchScheme(new Regex(@"^\s*container (?<argument>[a-z0-9A-Z-]*) {\s*$",                           RegexOptions.IgnoreCase),typeof(Container)                ),
                new SearchScheme(new Regex(@"^\s*leaf\s*(?<argument>[a-z0-9A-Z-]*)\s*{\s*$",                            RegexOptions.IgnoreCase),typeof(Leaf)                     ),
                new SearchScheme(new Regex(@"^\s*leaf-list (?<argument>[a-z0-9A-Z-]*) {\s*$",                           RegexOptions.IgnoreCase),typeof(LeafList)                 ),
                new SearchScheme(new Regex(@"^\s*list (?<argument>[a-z0-9A-Z-]*) {\s*$",                                RegexOptions.IgnoreCase),typeof(ListNode)                      ),

                new SearchScheme(new Regex("^\\s*pattern\\s*\"(?<argument>[^{\\s\"]+)\"(;|(?<bracket>.*{.*)*)$",        RegexOptions.IgnoreCase),typeof(Pattern)),
                new SearchScheme(new Regex(@"^\s*revision\s*(?<argument>[a-z0-9A-Z-]+)(;|(?<bracket>.*{.*)*)$",         RegexOptions.IgnoreCase),typeof(Revision)                 ,true),
                new SearchScheme(new Regex(@"^\s*status\s*(?<argument>[a-z0-9A-Z-]*)\s*;\s*$",                          RegexOptions.IgnoreCase),typeof(StatusStatement)                    ,true),
                new SearchScheme(new Regex(@"^\s*value\s*(?<argument>[a-z0-9A-Z-]*)\s*;\s*$",                           RegexOptions.IgnoreCase),typeof(ValueStatement)                      ,true),
                new SearchScheme(new Regex("^\\s*require-instance\\s*\"(?<argument>[^;\"]*)\";\\s*$",                  RegexOptions.IgnoreCase),typeof(RequireInstanceStatement)                      ,true),
                new SearchScheme(new Regex("^\\s*base\\s*\"(?<argument>[^;\"]*)\";\\s*$",                  RegexOptions.IgnoreCase),typeof(BaseStatement)                      ,true),
                new SearchScheme(new Regex(@"^\s*position\s*(?<argument>[0-9])*;\s*$",                                  RegexOptions.IgnoreCase),typeof(Position)                 ),

                new SearchScheme(new Regex(@"^\s*type enumeration\s*{\s*$",                                             RegexOptions.IgnoreCase),typeof(EnumTypeStatement)                  ),
                new SearchScheme(new Regex(@"^\s*type bits\s*{\s*$",                                                    RegexOptions.IgnoreCase),typeof(BitsTypeStatement)                  ),
                new SearchScheme(new Regex(@"^\s*type union\s*{\s*$",                                                    RegexOptions.IgnoreCase),typeof(UnionTypeStatement)                  ),
                new SearchScheme(new Regex(@"^\s*type\s*empty;\s*$",                                                    RegexOptions.IgnoreCase),typeof(EmptyTypeStatement)                 ,true),
                new SearchScheme(new Regex(@"^\s*type\s*boolean;\s*$",                                                    RegexOptions.IgnoreCase),typeof(BooleanTypeStatement)                 ,true),
                new SearchScheme(new Regex(@"^\s*type\s*leafref\s*{\s*$",                                               RegexOptions.IgnoreCase),typeof(LeafRefTypeStatement)               ),
                new SearchScheme(new Regex(@"^\s*type\s*instance-identifier(;\s*|(?<bracket>.*{.*)*)$",                                               RegexOptions.IgnoreCase),typeof(InstanceIdentifierTypeStatement),true               ),
                new SearchScheme(new Regex(@"^\s*type\s*binary(;\s*|(?<bracket>.*{.*)*)$",                                         RegexOptions.IgnoreCase),typeof(BinaryTypeStatement),true                  ),
                new SearchScheme(new Regex(@"^\s*type\s*identityref(;\s*|(?<bracket>.*{.*)*)$",                                         RegexOptions.IgnoreCase),typeof(IdentityrefTypeStatement),true                  ),

                new SearchScheme(new Regex(@"^\s*type\s*int64(;\s*|(?<bracket>.*{.*)*)$",                                         RegexOptions.IgnoreCase),typeof(Int64TypeStatement),true                  ),
                new SearchScheme(new Regex(@"^\s*type\s*int32(;\s*|(?<bracket>.*{.*)*)$",                                        RegexOptions.IgnoreCase),typeof(Int32TypeStatement),true                  ),
                new SearchScheme(new Regex(@"^\s*type\s*int16(;\s*|(?<bracket>.*{.*)*)$",                                        RegexOptions.IgnoreCase),typeof(Int16TypeStatement),true                 ),
                new SearchScheme(new Regex(@"^\s*type\s*int8(;\s*|(?<bracket>.*{.*)*)$",                                            RegexOptions.IgnoreCase),typeof(Int8TypeStatement),true                 ),
                new SearchScheme(new Regex(@"^\s*type\s*uint64(;\s*|(?<bracket>.*{.*)*)$",                                          RegexOptions.IgnoreCase),typeof(UInt64TypeStatement),true                 ),
                new SearchScheme(new Regex(@"^\s*type\s*uint32(;\s*|(?<bracket>.*{.*)*)$",                                         RegexOptions.IgnoreCase),typeof(UInt32TypeStatement),true                  ),
                new SearchScheme(new Regex(@"^\s*type\s*uint16(;\s*|(?<bracket>.*{.*)*)$",                                       RegexOptions.IgnoreCase),typeof(UInt16TypeStatement),true                 ),
                new SearchScheme(new Regex(@"^\s*type\s*uint8(;\s*|(?<bracket>.*{.*)*)$",                                        RegexOptions.IgnoreCase), typeof(UInt8TypeStatement),true               ),

                new SearchScheme(new Regex(@"^\s*type\s*decimal64(;\s*|(?<bracket>.*{.*)*)$",                           RegexOptions.IgnoreCase),typeof(Decimal64TypeStatement),true              ),
                new SearchScheme(new Regex(@"^\s*type string(;\s*|(?<bracket>.*{.*)*)$",                                RegexOptions.IgnoreCase),typeof(StringTypeStatement)               ),

                new SearchScheme(new Regex(@"^\s*bit\s*(?<argument>[a-z0-9A-Z-]+)(;\s*|(?<bracket>.*{.*)*)$",           RegexOptions.IgnoreCase),typeof(Bit)                 ),
                new SearchScheme(new Regex(@"^\s*enum (?<argument>[a-z0-9A-Z-]+)(;\s*|(?<bracket>.*{.*)*)$",            RegexOptions.IgnoreCase),typeof(EnumStatement)                 ,true),

                new SearchScheme(new Regex("^\\s*length\\s*\"(?<argument>[^;\"]*)\";(?<bracket>.*{.*)*$",               RegexOptions.IgnoreCase),typeof(Length)      ,true),
                new SearchScheme(new Regex("^\\s*length\\s*\"(?<argument>[^;\"]*)\\s*$",                                RegexOptions.IgnoreCase),typeof(Length), TokenTypes.SameLineStart),
                new SearchScheme(new Regex("^\\s*length\\s*$",                                                          RegexOptions.IgnoreCase),typeof(Length),TokenTypes.NextLineStart),

                new SearchScheme(new Regex("^\\s*range\\s*\"(?<argument>[^;\"]*)\";(?<bracket>.*{.*)*$",                RegexOptions.IgnoreCase),typeof(RangeStatement),true),
                new SearchScheme(new Regex("^\\s*range\\s*\"(?<argument>[^;\"]*)\\s*$",                                 RegexOptions.IgnoreCase),typeof(RangeStatement),TokenTypes.SameLineStart),
                new SearchScheme(new Regex("^\\s*range\\s*$",                                                           RegexOptions.IgnoreCase),typeof(RangeStatement),TokenTypes.NextLineStart),

                new SearchScheme(new Regex("^\\s*error-message\\s*\"(?<argument>[^;\"]*)\";\\s*$",                      RegexOptions.IgnoreCase),typeof(ErrorMessageStatement),true),
                new SearchScheme(new Regex("^\\s*error-message\\s*\"(?<argument>[^;\"]*)\\s*$",                         RegexOptions.IgnoreCase),typeof(ErrorMessageStatement),TokenTypes.SameLineStart),
                new SearchScheme(new Regex("^\\s*error-message\\s*$",                                                   RegexOptions.IgnoreCase),typeof(ErrorMessageStatement),TokenTypes.NextLineStart),
                new SearchScheme(new Regex("^\\s*error-app-tag\\s*\"(?<argument>[^;\"]*)\";\\s*$",                      RegexOptions.IgnoreCase),typeof(ErrorAppTagStatement),true),
                new SearchScheme(new Regex("^\\s*error-app-tag\\s*\"(?<argument>[^;\"]*)\\s*$",                         RegexOptions.IgnoreCase),typeof(ErrorAppTagStatement),TokenTypes.SameLineStart),
                new SearchScheme(new Regex("^\\s*error-app-tag\\s*$",                                                   RegexOptions.IgnoreCase),typeof(ErrorAppTagStatement),TokenTypes.NextLineStart),
                new SearchScheme(new Regex("^\\s*path \"(?<argument>[^;\"]*)\";\\s*$",                                  RegexOptions.IgnoreCase),typeof(PathStatement),true          ),
                new SearchScheme(new Regex("^\\s*path \"(?<argument>[^;\"]*)\\s*$",                                     RegexOptions.IgnoreCase),typeof(PathStatement),TokenTypes.SameLineStart),
                new SearchScheme(new Regex("^\\s*path\\s*$",                                                            RegexOptions.IgnoreCase),typeof(PathStatement),TokenTypes.NextLineStart),
                new SearchScheme(new Regex("^\\s*contact \"(?<argument>[^;\"]*)\";\\s*$",                               RegexOptions.IgnoreCase),typeof(Contact),true       ),
                new SearchScheme(new Regex("^\\s*contact \"(?<argument>[^;\"]*)\\s*$",                                  RegexOptions.IgnoreCase),typeof(Contact),TokenTypes.SameLineStart),
                new SearchScheme(new Regex("^\\s*contact\\s*$",                                                         RegexOptions.IgnoreCase),typeof(Contact),TokenTypes.NextLineStart),
                new SearchScheme(new Regex("^\\s*namespace \"(?<argument>[^;\"]*)\";\\s*$",                             RegexOptions.IgnoreCase),typeof(NamespaceStatement),TokenTypes.SameLineStart    ),
                new SearchScheme(new Regex("^\\s*namespace \"(?<argument>[^;\"]*)\\s*$",                                RegexOptions.IgnoreCase),typeof(NamespaceStatement),TokenTypes.SameLineStart),
                new SearchScheme(new Regex("^\\s*namespace\\s*$",                                                       RegexOptions.IgnoreCase),typeof(NamespaceStatement),TokenTypes.NextLineStart),
                new SearchScheme(new Regex("^\\s*organization \"(?<argument>[^;\"]*)\";\\s*$",                          RegexOptions.IgnoreCase),typeof(Organization)),
                new SearchScheme(new Regex("^\\s*organization \"(?<argument>[^;\"]*)\\s*$",                             RegexOptions.IgnoreCase),typeof(Organization),TokenTypes.SameLineStart),
                new SearchScheme(new Regex("^\\s*organization\\s*$",                                                    RegexOptions.IgnoreCase),typeof(Organization),TokenTypes.NextLineStart),
                new SearchScheme(new Regex("^\\s*reference \"(?<argument>[^;\"]*)\";\\s*$",                             RegexOptions.IgnoreCase),typeof(Reference)     ),
                new SearchScheme(new Regex("^\\s*reference \"(?<argument>[^;\"]*)\\s*$",                                RegexOptions.IgnoreCase),typeof(Reference),TokenTypes.SameLineStart),
                new SearchScheme(new Regex("^\\s*reference\\s*$",                                                       RegexOptions.IgnoreCase),typeof(Reference),TokenTypes.NextLineStart),

                new SearchScheme(new Regex("^\\s*description \"(?<argument>[^;\"]*)\";\\s*$",                           RegexOptions.IgnoreCase),typeof(Description)),
                new SearchScheme(new Regex("^\\s*description \"(?<argument>[^;\"]*)\\s*$",                              RegexOptions.IgnoreCase),typeof(Description),TokenTypes.SameLineStart),
                new SearchScheme(new Regex("^\\s*description\\s*$",                                                     RegexOptions.IgnoreCase),typeof(Description),TokenTypes.NextLineStart),

                new SearchScheme(new Regex("^\\s*\"(?<argument>[^;]*)\";\\s*$"),                                        null,TokenTypes.ValueForPreviousLine                               ),
                new SearchScheme(new Regex("^\\s*\"(?<argument>[^\"]*)\\s*$"),                                          null,TokenTypes.ValueForPreviousLineBeg),
                new SearchScheme(new Regex("^\\s*(?<argument>[^;]*)\";(?<bracket>.*{.*)*\\s*$"),                        null,TokenTypes.ValueForPreviousLineEnd),

                new SearchScheme(new Regex(@"^\s*yang-version (?<argument>[a-z0-9A-Z-]*);\s*$"),                        typeof(YangVersionStatement)                                       ),
                new SearchScheme(new Regex("^\\s*import\\s*(?<argument>[a-z0-9A-Z-]*)\\s*{\\s*$"),                      typeof(Import)                                            ),

                new SearchScheme(new Regex(@"^\s* *$"),                                                                 null,TokenTypes.Skip),
                new SearchScheme(new Regex(@"^\s*}\s*$"),                                                               null,TokenTypes.NodeEndingBracket),

                //Multiline of value or error based on previous state
                new SearchScheme(new Regex("(?s)^(?!.*[\";])[\\s\\t]*(?<argument>.*)$"),                                null, TokenTypes.ValueForPreviousLineMultiline),
            };
        }

        /// <summary>
        /// Creates a YangInterpreter.Interpreter.Token for the given row.
        /// </summary>
        /// <param name="row">A row of a yang file</param>
        /// <returns></returns>
        internal static Token GetTokenForRow(string row, Token currentToken)
        {
            Token MatchResultToken = new Token( "", null, TokenTypes.Empty, false);

            row = InnerBlockTryParse(MatchResultToken, row);
            foreach (var scheme in InterpreterSearchSchemeList)
            {
                Match match = scheme.Reg.Match(row);
                if (match.Success)
                {
                    //MatchResultToken.TokenType = scheme.TokenType;
                    MatchResultToken.TokenAsSingleLine = scheme.TokenAsSingleLine;
                    MatchResultToken.TokenAsType = scheme.TokenAsType;
                    MatchResultToken.IsChildlessContainer = scheme.IsChildlessContainer;

                    if (match.Groups["argument"].Value != "")
                        MatchResultToken.TokenArgument = match.Groups["argument"].Value;

                    if (match.Groups["bracket"].Value != "")
                    {
                        //if (currentToken != null)
                          //  MatchResultToken.TokenAsSingleLine = currentToken.TokenAsSingleLine;
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
                MatchResult.InnerBlock = match.Groups["argument"].Value;
                return row.Replace(match.Groups["argument"].Value, "");
            }

            match = InnerEndlineParser.Reg.Match(row);
            if (match.Success)
            {
                MatchResult.InnerBlock = match.Groups["argument"].Value;
                return InnerEndlineParser.Reg.Replace(row, "", 1);
            }
            return row;
        }
    }
}
