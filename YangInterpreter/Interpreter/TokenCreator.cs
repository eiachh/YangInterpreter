using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
        private static bool IsArgBeggining = false;
        private static bool IsArgEnding = false;
        //private static bool IsMultilineToken = false;
        private static string QuoteType = string.Empty;

        private static List<SearchScheme> InterpreterSearchSchemeList;
        private static SearchScheme InnerBlockParser = new SearchScheme(new Regex("\\s*{\\s*(?<argument>[^}]*\\s*})\\s*$"), null);
        private static SearchScheme InnerEndlineParser = new SearchScheme(new Regex(@".(?<argument>})\s*$"), null, TokenTypes.NodeEndingBracket);
        private static string ArgumentParser;
        internal static void Init()
        {
            Assembly assem = typeof(TokenCreator).Assembly;
            foreach (string resourceName in assem.GetManifestResourceNames())
            {
                if(resourceName.Contains("ArgumentParserManifest"))
                {
                    Stream stream = assem.GetManifestResourceStream(resourceName);
                    StreamReader r = new StreamReader(stream);
                    ArgumentParser = r.ReadLine();
                }
            }
            

            InterpreterSearchSchemeList = new List<SearchScheme>
            {
                new SearchScheme(new Regex(@"^\s*}\s*$"),                                                               null,TokenTypes.NodeEndingBracket),

                new SearchScheme(new Regex(@"^\s*(?<statementName>yang-version)\s+"+ArgumentParser),                        typeof(YangVersionStatement)                                       ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>import)\s+"+ArgumentParser),                      typeof(Import)                                            ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>module)\s+"+ArgumentParser),typeof(Module)),
                new SearchScheme(new Regex(@"^\s*(?<statementName>grouping)\s+"+ArgumentParser),typeof(Grouping)),
                new SearchScheme(new Regex(@"^\s*(?<statementName>uses)\s+"+ArgumentParser),typeof(Uses)                      ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>choice)\s+"+ArgumentParser),typeof(Choices)                   ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>case)\s+"+ArgumentParser),typeof(ChoiceCaseStatement)               ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>prefix)\s*"+ArgumentParser),typeof(Prefix)                   ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>container)\s+"+ArgumentParser),typeof(Container)                ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>leaf)\s+"+ArgumentParser),typeof(Leaf)                     ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>leaf-list)\s+"+ArgumentParser),typeof(LeafList)                 ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>list)\s+"+ArgumentParser),typeof(ListStatement)                      ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>anyxml)\s+"+ArgumentParser),typeof(AnyXmlStatement)),
                
                new SearchScheme(new Regex(@"^\s*(?<statementName>pattern)\s+"+ArgumentParser),typeof(Pattern)),
                new SearchScheme(new Regex(@"^\s*(?<statementName>revision)\s+"+ArgumentParser),typeof(Revision)                 ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>status)\s+"+ArgumentParser),typeof(StatusStatement)),
                new SearchScheme(new Regex(@"^\s*(?<statementName>value)\s+"+ArgumentParser),typeof(ValueStatement)                      ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>require-instance)\s+"+ArgumentParser),typeof(RequireInstanceStatement)                      ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>base)\s+"+ArgumentParser),typeof(BaseStatement)                      ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>position)\s+"+ArgumentParser),typeof(Position)                 ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>config)\s+"+ArgumentParser),typeof(ConfigStatement)                 ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>if-feature)\s+"+ArgumentParser),typeof(IfFeatureStatement)                 ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>mandatory)\s+"+ArgumentParser),typeof(MandatoryStatement)                 ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>must)\s+"+ArgumentParser),typeof(MustStatement)                 ),
                

                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s+enumeration)\s*"+ArgumentParser),typeof(EnumTypeStatement)                  ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type bits)\s*"+ArgumentParser),typeof(BitsTypeStatement)                  ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s+union)\s*"+ArgumentParser),typeof(UnionTypeStatement)                  ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s+empty)\s*"+ArgumentParser),typeof(EmptyTypeStatement)                 ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s+boolean)\s*"+ArgumentParser),typeof(BooleanTypeStatement)                 ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s*leafref)\s*"+ArgumentParser),typeof(LeafRefTypeStatement)               ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s*instance-identifier)\s*"+ArgumentParser),typeof(InstanceIdentifierTypeStatement)               ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s*binary)\s*"+ArgumentParser),typeof(BinaryTypeStatement)                  ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s*identityref)\s*"+ArgumentParser),typeof(IdentityrefTypeStatement)                  ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s*int64)\s*"+ArgumentParser),typeof(Int64TypeStatement)                  ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s*int32)\s*"+ArgumentParser),typeof(Int32TypeStatement)                  ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s*int16)\s*"+ArgumentParser),typeof(Int16TypeStatement)                 ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s*int8)\s*"+ArgumentParser),typeof(Int8TypeStatement)                 ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s*uint64)\s*"+ArgumentParser),typeof(UInt64TypeStatement)                 ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s*uint32)\s*"+ArgumentParser),typeof(UInt32TypeStatement)                  ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s*uint16)\s*"+ArgumentParser),typeof(UInt16TypeStatement)                 ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s*uint8)\s*"+ArgumentParser), typeof(UInt8TypeStatement)               ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type\s*decimal64)\s*"+ArgumentParser),typeof(Decimal64TypeStatement)              ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>type string)\s*"+ArgumentParser),typeof(StringTypeStatement)               ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>bit)\s+"+ArgumentParser),typeof(Bit)                 ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>enum)\s+"+ArgumentParser),typeof(EnumStatement)                 ),

                new SearchScheme(new Regex(@"^\s*(?<statementName>when\s+"+ArgumentParser+@"|when)\s*"),typeof(WhenStatement)                 ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>length\s+"+ArgumentParser+@"|length)\s*"),typeof(Length)      ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>range\s+"+ArgumentParser+@"|range)\s*"),typeof(RangeStatement)),
                new SearchScheme(new Regex(@"^\s*(?<statementName>error-message\s+"+ArgumentParser+@"|error-message)\s*"),typeof(ErrorMessageStatement)),
                new SearchScheme(new Regex(@"^\s*(?<statementName>error-app-tag\s+"+ArgumentParser+@"|error-app-tag)\s*"),typeof(ErrorAppTagStatement)),
                new SearchScheme(new Regex(@"^\s*(?<statementName>path\s+"+ArgumentParser+@"|path)\s*"),typeof(PathStatement)          ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>contact\s+"+ArgumentParser+@"|contact)\s*"),typeof(Contact)       ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>namespace\s+"+ArgumentParser+@"|namespace)\s*"),typeof(NamespaceStatement)    ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>organization\s+"+ArgumentParser+@"|organization)\s*"),typeof(Organization)),
                new SearchScheme(new Regex(@"^\s*(?<statementName>reference\s+"+ArgumentParser+@"|reference)\s*"),typeof(Reference)     ),
                new SearchScheme(new Regex(@"^\s*(?<statementName>description\s+"+ArgumentParser+@"|description)\s*"),typeof(Description)),

                
                
                new SearchScheme(new Regex(@"^\s* *$"),                                                                 null,TokenTypes.Skip),

                new SearchScheme(new Regex("(?s)^(?!.*(?:[\";']))[\\s\\t]*(?<nonQuoted>.*)$"),                                null, TokenTypes.ValueForPreviousLineMultiline),
                new SearchScheme(new Regex(@"^\s*"+ArgumentParser),                                        null,TokenTypes.ValueForPreviousLine                               ),
            };
        }

        /// <summary>
        /// Creates a YangInterpreter.Interpreter.Token for the given row.
        /// </summary>
        /// <param name="row">A row of a yang file</param>
        /// <returns></returns>
        internal static Token GetTokenForRow(string row, Token previousTokenPartOfMultiline)
        {
            Token MatchResultToken = new Token( "", null, TokenTypes.Empty, false);

            row = InnerBlockTryParse(MatchResultToken, row);
            foreach (var scheme in InterpreterSearchSchemeList)
            {
                Match match = scheme.Reg.Match(row);
                if (match.Success)
                {
                    if (row.Contains("+ \"b\" +"))
                    {
                        var sddsds = 2;
                    }

                    if(previousTokenPartOfMultiline != null)
                    {
                        if (previousTokenPartOfMultiline.MultilinePreviousQuote != "")
                            MatchResultToken.MultilinePreviousQuote = previousTokenPartOfMultiline.MultilinePreviousQuote;
                        if (!IsAllowedInMultiline(scheme))
                            continue;
                    }    
                    MatchResultToken.TokenTypeSpecialInfo = scheme.TokenAsSingleLine;
                    MatchResultToken.TokenAsType = scheme.TokenAsType;
                    MatchResultToken.IsChildlessContainer = scheme.IsChildlessContainer;
                    MatchResultToken.TokenArgument = GetArgumentFromMatch(match);

                    var EndingValue = match.Groups["ending"].Value.Trim();

                    if (IsArgBeggining || (EndingValue == "+" && match.Groups["statementName"].Value != ""))
                    {
                        IsArgBeggining = false;
                        MatchResultToken.MultilinePreviousQuote = QuoteType;
                        if (match.Groups["statementName"].Value != "")
                            MatchResultToken.TokenTypeSpecialInfo = TokenTypes.SameLineStart;
                        else
                            MatchResultToken.TokenTypeSpecialInfo = TokenTypes.ValueForPreviousLineBeg;
                    }
                    else if (IsArgEnding)
                    {
                        IsArgEnding = false;

                        //Invalid formatting check
                        if (EndingValue == "" || (previousTokenPartOfMultiline != null && previousTokenPartOfMultiline.MultilinePreviousQuote != QuoteType))
                            return null;
                        MatchResultToken.TokenTypeSpecialInfo = TokenTypes.ValueForPreviousLineEnd;
                    }
                    else if (MatchResultToken.TokenArgument != "" && previousTokenPartOfMultiline!= null && string.IsNullOrEmpty(previousTokenPartOfMultiline.MultilinePreviousQuote))
                    {
                        if (EndingValue == "")
                            return null;
                    }
                    else if (MatchResultToken.TokenArgument != "" && previousTokenPartOfMultiline != null && !string.IsNullOrEmpty(previousTokenPartOfMultiline.MultilinePreviousQuote) && string.IsNullOrEmpty(EndingValue))
                    {
                        MatchResultToken.TokenArgument += Environment.NewLine;
                        MatchResultToken.TokenTypeSpecialInfo = TokenTypes.ValueForPreviousLineMultiline;
                    }
                    else if(MatchResultToken.TokenArgument != "" && previousTokenPartOfMultiline != null && !string.IsNullOrEmpty(previousTokenPartOfMultiline.MultilinePreviousQuote) && EndingValue == "+")
                        MatchResultToken.TokenTypeSpecialInfo = TokenTypes.ValueForPreviousLineMultiline;

                    if (EndingValue == "{")                       
                        MatchResultToken.IsChildlessContainer = false;
                    if (EndingValue == ";")
                        MatchResultToken.IsChildlessContainer = true;

                    if (match.Groups["statementName"].Value != "" && EndingValue == "" && MatchResultToken.TokenArgument == "")
                        MatchResultToken.TokenTypeSpecialInfo = TokenTypes.NextLineStart;
                    else if (match.Groups["statementName"].Value != "" && EndingValue == "" && MatchResultToken.TokenArgument != "")
                    {
                        MatchResultToken.TokenTypeSpecialInfo = TokenTypes.SameLineStart;
                        //MatchResultToken.TokenArgument += Environment.NewLine;
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

        private static string GetArgumentFromMatch(Match match)
        {
            string arg = GetArgumentBegginingFromMatch(match);

            if (match.Groups["nonQuoted"].Value != "")
                arg += match.Groups["nonQuoted"].Value;

            if (match.Groups["quotedContent"].Value != "")
                arg += match.Groups["quotedContent"].Value;

            else if (match.Groups["singleQuotedContent"].Value != "")
                arg += match.Groups["singleQuotedContent"].Value;

            arg += GetArgumentEndingFromMatch(match);

            if (match.Groups["remainingArgs"].Value != "")
                arg += GetArgumentFromRemainingArgs(match);

            

            return arg;
        }

        private static string GetArgumentBegginingFromMatch(Match match)
        {
            string arg = string.Empty;

            if (match.Groups["halfSingleQuoteBegginingContent"].Value != "")
                arg += match.Groups["halfSingleQuoteBegginingContent"].Value+Environment.NewLine;

            if (match.Groups["halfNormalQuoteBegginingContent"].Value != "")
                arg += match.Groups["halfNormalQuoteBegginingContent"].Value+Environment.NewLine;

            if(match.Groups["quoteType1"].Value != "")
                 QuoteType = match.Groups["quoteType1"].Value;
            if (match.Groups["quoteType2"].Value != "")
                QuoteType = match.Groups["quoteType2"].Value;
            if (!string.IsNullOrEmpty(arg))
                IsArgBeggining = true;
            return arg;
        }

        private static string GetArgumentEndingFromMatch(Match match)
        {
            string arg = string.Empty;

            if (match.Groups["normalQuoteEnd"].Value != "")
                arg += match.Groups["normalQuoteEnd"].Value;

            if (match.Groups["singleQuoteEnd"].Value != "")
                arg += match.Groups["singleQuoteEnd"].Value;

            if (match.Groups["quoteType3"].Value != "")
                QuoteType = match.Groups["quoteType3"].Value;
            if (match.Groups["quoteType4"].Value != "")
                QuoteType = match.Groups["quoteType4"].Value;
            if (!string.IsNullOrEmpty(arg))
                IsArgEnding = true;
            return arg;
        }

        private static string GetArgumentFromRemainingArgs(Match match)
        {
            string remainingArg = match.Groups["remainingArgs"].Value.Trim();
            if (!remainingArg.StartsWith("+"))
                throw new Exception("The argument did not contain ++ sign! " + match.Groups[0].Value);
            remainingArg = remainingArg.Remove(0, 1);
            Match innerMatch = new Regex(ArgumentParser).Match(remainingArg);

            return GetArgumentFromMatch(innerMatch);
        }

        private static bool IsAllowedInMultiline(SearchScheme scheme)
        {
            return (scheme.TokenAsSingleLine == TokenTypes.ValueForPreviousLine || scheme.TokenAsSingleLine == TokenTypes.ValueForPreviousLineBeg
                    || scheme.TokenAsSingleLine == TokenTypes.ValueForPreviousLineEnd || scheme.TokenAsSingleLine == TokenTypes.ValueForPreviousLineMultiline
                    || scheme.TokenAsSingleLine == TokenTypes.ValueForPreviousLineEnd || scheme.TokenAsSingleLine == TokenTypes.Skip);
        }
    }
}
