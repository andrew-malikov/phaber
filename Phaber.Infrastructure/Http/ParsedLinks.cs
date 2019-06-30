using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Phaber.Infrastructure.Http {
    public class ParsedLinks {
        public readonly Dictionary<string, Uri> Values;

        private static Regex TypeExpression = new Regex(
            $"rel=\"({@"\w+"})\"",
            RegexOptions.Compiled
        );
        private static Regex LinkExpression = new Regex(
            "<(.+)>",
            RegexOptions.Compiled
        );

        public ParsedLinks(string data) {
            var links = new Dictionary<string, Uri>();

            foreach (var rawLink in data.Split(',')) {
                var matchedType = TypeExpression.Match(rawLink);
                if (!matchedType.Success) break;

                var matchedLink = LinkExpression.Match(rawLink);
                if (!matchedType.Success) break;

                links.Add(
                    matchedType.Groups[1].Value,
                    new Uri(matchedLink.Groups[1].Value)
                );
            }

            Values = links;
        }
    }
}