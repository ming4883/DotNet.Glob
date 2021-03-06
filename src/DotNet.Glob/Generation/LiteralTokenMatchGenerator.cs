using System.Text;
using DotNet.Globbing.Token;
using System;
using System.Collections.Generic;

namespace DotNet.Globbing.Generation
{
    internal class LiteralTokenMatchGenerator : IMatchGenerator
    {
        private LiteralToken token;
        private Random _random;

        public LiteralTokenMatchGenerator(LiteralToken token, Random _random)
        {
            this.token = token;
            this._random = _random;
        }

        public void AppendMatch(StringBuilder builder)
        {
            builder.Append(token.Value);
        }

        public void AppendNonMatch(StringBuilder builder)
        {
            // append a literal of the same size, but with one or many of the characters changed.

            bool sameSize = true;

            if (sameSize)
            {
                var changedList = new List<char>(token.Value.Length);
                changedList.AddRange(token.Value.ToCharArray());

                int numberOfCharsToChange = _random.Next(1, token.Value.Length);
                int changed = 0;
                //   var removedList = new List<int>(numberOfCharsToChange);

                int chanceToChange = token.Value.Length / numberOfCharsToChange;

                for (int i = 0; i < token.Value.Length; i++)
                {
                    var currentChar = token.Value[i];
                    if (i >= (token.Value.Length - 1) - changed)
                    {
                        // we must change this char.  
                        changed = changed + 1;
                        builder.AppendRandomLiteralCharacterNotBetween(_random, currentChar, currentChar);
                        continue;
                    }

                    // randomly decide whether to change char.
                    int probablityOfChanging = _random.Next(1, 10);
                    if (probablityOfChanging >= chanceToChange)
                    {
                        // we are randomly deciding to change this char.  
                        changed = changed + 1;
                        builder.AppendRandomLiteralCharacterNotBetween(_random, currentChar, currentChar);
                        continue;
                    }

                }
            }

        }
    }
}