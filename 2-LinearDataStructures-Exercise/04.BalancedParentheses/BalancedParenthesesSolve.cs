namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            if (parentheses.Length % 2 != 0)
            {
                return false;
            }

            int one = 0;
            int two = 0;
            int three = 0;


            for (int i = 0; i < parentheses.Length; i++)
            {
                switch (parentheses[i])
                {
                    case '(': one++; break;
                    case '{': two++; break;
                    case '[': three++; break;
                    case ')': one--; break;
                    case '}': two--; break;
                    case ']': three--; break;
                }
            }

            if (one == 0 && two == 0 && three == 0)
            {
                return true;
            }

            return false;
        }
    }
}
