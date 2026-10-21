/* ====================================================================
   SPDX-License-Identifier: Apache-2.0

   Independent implementation of the documented java.util.StringTokenizer
   contract. Replaces an earlier iText-derived file (MPL-1.1 / LGPL) so that
   the whole library is available under the Apache License, Version 2.0.
==================================================================== */

using System;

namespace NPOI.Util.Collections
{
    public class StringTokenizer
    {
        private const string DefaultDelimiters = " \t\n\r\f";

        private readonly string source;
        private readonly bool returnDelimiters;
        private string delimiters;
        private int position;

        public StringTokenizer(string str) : this(str, DefaultDelimiters, false)
        {
        }

        public StringTokenizer(string str, string delim) : this(str, delim, false)
        {
        }

        public StringTokenizer(string str, string delim, bool retDelims)
        {
            source = str ?? throw new ArgumentNullException(nameof(str));
            delimiters = delim;
            returnDelimiters = retDelims;
        }

        public bool HasMoreTokens()
        {
            return SkipDelimiters(position) < source.Length;
        }

        public string NextToken(string delim)
        {
            delimiters = delim;
            return NextToken();
        }

        public string NextToken()
        {
            int start = SkipDelimiters(position);
            if (start >= source.Length)
            {
                throw new IndexOutOfRangeException("No more tokens.");
            }

            position = ScanToken(start);
            return source.Substring(start, position - start);
        }

        public int CountTokens()
        {
            int count = 0;
            int index = SkipDelimiters(position);
            while (index < source.Length)
            {
                index = SkipDelimiters(ScanToken(index));
                count++;
            }

            return count;
        }

        private int SkipDelimiters(int index)
        {
            if (returnDelimiters)
            {
                return index;
            }

            while (index < source.Length && IsDelimiter(source[index]))
            {
                index++;
            }

            return index;
        }

        private int ScanToken(int index)
        {
            if (returnDelimiters && IsDelimiter(source[index]))
            {
                return index + 1;
            }

            while (index < source.Length && !IsDelimiter(source[index]))
            {
                index++;
            }

            return index;
        }

        private bool IsDelimiter(char c)
        {
            return delimiters.Contains(c);
        }
    }
}
