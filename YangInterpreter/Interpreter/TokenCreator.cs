using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace YangInterpreter.Interpreter
{
    /// <summary>
    /// Creates a token for the given data. Core of the interpreter.
    /// </summary>
    internal static class TokenCreator
    {
        private static List<SearchScheme> InterpreterSearchSchemeList;

        internal static void Init()
        {
            InterpreterSearchSchemeList = new List<SearchScheme>
            {
                //                                              name, value, OPtional Token as single line if multiline token
                //new SearchScheme(new Regex(@"^\s*$"),TokenTypes,-1,-1),

                new SearchScheme(new Regex(@"^\s*module ([a-z0-9A-Z-]*) {$",RegexOptions.IgnoreCase),TokenTypes.Module,1,-1),
                new SearchScheme(new Regex(@"^\s*grouping ([a-z0-9A-Z-]*) {$"),TokenTypes.Grouping,1,-1),
                new SearchScheme(new Regex(@"^\s*uses ([a-z0-9A-Z-]*);$"),TokenTypes.Uses,1,-1),
                new SearchScheme(new Regex(@"^\s*choice ([a-z0-9A-Z-]*) {$"),TokenTypes.Choice,1,-1),
                new SearchScheme(new Regex(@"^\s*case ([a-z0-9A-Z-]*) {$"),TokenTypes.ChoiceCase,1,-1),
                new SearchScheme(new Regex("^\\s*namespace \"([^\"]*)\";$",RegexOptions.IgnoreCase),TokenTypes.Namespace,-1,1),
                new SearchScheme(new Regex("^\\s*namespace\\s*$",RegexOptions.IgnoreCase),TokenTypes.NamespaceMultiline,-1,-1,TokenTypes.Namespace),
                new SearchScheme(new Regex("^\\s*prefix \"([^\"]*)\";$",RegexOptions.IgnoreCase),TokenTypes.Prefix,-1,1),
                new SearchScheme(new Regex("^\\s*prefix ([^;]*);$",RegexOptions.IgnoreCase),TokenTypes.Prefix,-1,1),
                new SearchScheme(new Regex("^\\s*organization \"([^\"]*)\";$",RegexOptions.IgnoreCase),TokenTypes.Organization,-1,1),
                new SearchScheme(new Regex("^\\s*organization\\s*$",RegexOptions.IgnoreCase),TokenTypes.OrganizationMultiline,-1,1,TokenTypes.Organization),
                new SearchScheme(new Regex("^\\s*contact \"([^\"]*)\";$",RegexOptions.IgnoreCase),TokenTypes.Contact,-1,1),
                new SearchScheme(new Regex("^\\s*contact\\s*$",RegexOptions.IgnoreCase),TokenTypes.ContactMultiline,-1,1,TokenTypes.Contact),
                new SearchScheme(new Regex("^\\s*reference \"([^\"]*)\";$",RegexOptions.IgnoreCase),TokenTypes.Reference,-1,1),
                new SearchScheme(new Regex("^\\s*reference\\s*$",RegexOptions.IgnoreCase),TokenTypes.ReferenceMultiline,-1,1,TokenTypes.Reference),
                new SearchScheme(new Regex(@"^\s*container ([a-z0-9A-Z-]*) {$",RegexOptions.IgnoreCase),TokenTypes.Container,1,-1),
                new SearchScheme(new Regex(@"^\s*leaf ([a-z0-9A-Z-]*) {$",RegexOptions.IgnoreCase),TokenTypes.Leaf,1,-1),
                new SearchScheme(new Regex(@"^\s*leaf-list ([a-z0-9A-Z-]*) {$",RegexOptions.IgnoreCase),TokenTypes.LeafList,1,-1),
                new SearchScheme(new Regex(@"^\s*list ([a-z0-9A-Z-]*) {$",RegexOptions.IgnoreCase),TokenTypes.List,1,-1),
                new SearchScheme(new Regex("^\\s*key \"([^\"]*)\";$",RegexOptions.IgnoreCase),TokenTypes.Key,-1,1),
                new SearchScheme(new Regex(@"^\s*type enumeration {$",RegexOptions.IgnoreCase),TokenTypes.TypeEnum,1,-1),
                 new SearchScheme(new Regex(@"^\s*type empty;$",RegexOptions.IgnoreCase),TokenTypes.TypeEmpty,1,-1),
                new SearchScheme(new Regex(@"^\s*enum ([a-z0-9A-Z-]*);$",RegexOptions.IgnoreCase),TokenTypes.SimpleEnum,-1,1),
                new SearchScheme(new Regex(@"^\s*revision ([a-z0-9A-Z-]*)\s*{$",RegexOptions.IgnoreCase),TokenTypes.Revision, 1,-1),
                new SearchScheme(new Regex("^\\s*description \"([^\"]*)\";$",RegexOptions.IgnoreCase),TokenTypes.Description,-1,1),
                new SearchScheme(new Regex("^\\s*description\\s*$",RegexOptions.IgnoreCase),TokenTypes.DescriptionWithValueNextLine,-1,-1,TokenTypes.Description),
                new SearchScheme(new Regex("^\\s*\"([^\"]*)\";\\s*$"),TokenTypes.ValueForPreviousLine,-1,1),
                new SearchScheme(new Regex("^\\s*\"([^\"]*)$"),TokenTypes.ValueForPreviousLineBeg,-1,1,TokenTypes.Empty),
                new SearchScheme(new Regex("^\\s*([^\"]*)\";$"),TokenTypes.ValueForPreviousLineEnd,-1,1,TokenTypes.Empty),
                new SearchScheme(new Regex(@"^\s*config (true|false);$"),TokenTypes.ConfigStatement,-1, 1),
                new SearchScheme(new Regex(@"^\s*typedef ([a-z0-9A-Z-]*) {$"),TokenTypes.Typedef,1,-1),
                new SearchScheme(new Regex("^\\s*range \"([0-9]* ?.. ?[0-9]*)\";$"),TokenTypes.Range,-1,1),
                new SearchScheme(new Regex(@"^\s*yang-version ([a-z0-9A-Z-]*);$"),TokenTypes.YangVersion,-1,1),
                new SearchScheme(new Regex("^\\s*import ([a-z0-9A-Z-]*) { prefix\\s*\"([^\"]*)\";\\s*}$"),TokenTypes.Import,1,2),

                new SearchScheme(new Regex(@"^\s* *$"),TokenTypes.Skip,-1,-1),
                new SearchScheme(new Regex(@"^\s*}$"),TokenTypes.NodeEndingBracket,-1,-1),

                //Multiline of value or error based on previous state
                new SearchScheme(new Regex("([^\"]*)"),TokenTypes.ValueForPreviousLineMultiline,-1,1,TokenTypes.ValueForPreviousLineBeg),
            };
        }

        /// <summary>
        /// Creates a YangInterpreter.Interpreter.Token for the given row.
        /// </summary>
        /// <param name="row">A row of a yang file</param>
        /// <returns></returns>
        internal static Token GetTokenForRow(string row)
        {
            foreach (var scheme in InterpreterSearchSchemeList)
            {
                Match match = scheme.Reg.Match(row);
                if (match.Success)
                {
                    Token MatchResultToken = new Token(scheme.TokenType, "", "", scheme.TokenAsSingleLine);
                    //if ()
                    {

                    }
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
    }
}
