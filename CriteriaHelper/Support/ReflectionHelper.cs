using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace CriteriaHelper.Support
{
    internal class ReflectionHelper<T>
    {
        public string PropertyAsString(Expression<Func<T, object>> exp)
        {
            string expBody = ((LambdaExpression)exp).Body.ToString();
            return expBody;
        }
        
        public string PropertyAsString(Expression<Func<T, object>> exp, string alias)
        {
            Regex regex = new Regex(@"^[a-zA-Z]+", RegexOptions.None);
            string expression = this.PropertyAsString(exp);
            if (expression.Contains("Convert("))
                expression = expression.Replace("Convert(", "").Replace(")","");
            return regex.Replace(expression, alias);
        }
    }
}
