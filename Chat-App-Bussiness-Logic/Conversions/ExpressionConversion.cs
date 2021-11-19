using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Bussiness_Logic.Conversions
{
    public class ExpressionConversion
    {
        public static string ReturnStringExpressionParameter<T>(Expression<Func<T, bool>> id)
        {
            return Expression.Lambda(((BinaryExpression)id.Body).Right).Compile().DynamicInvoke() as string;
        }
        public static int ReturnIntegerExpressionParameter<T>(Expression<Func<T, bool>> id)
        {
            return (int)Expression.Lambda(((BinaryExpression)id.Body).Right).Compile().DynamicInvoke();
        }
    }
}
