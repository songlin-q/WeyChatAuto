using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WeyChatAuto.Common
{
    public static class ExpressHelper
    {
        /// <summary>
        /// 创建insert语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static string CreateInsertSql<T>(T mod)
        {
            var sql = "Insert into {0}({1}) Values({2})";

            string table = string.Empty;//表名
            List<string> columns = new List<string>();//列名
            List<string> values = new List<string>();//值


            Type type = typeof(T);
            table = type.Name;

            foreach (PropertyInfo item in type.GetProperties())
            {
                string name = item.Name;
                columns.Add(name);
                object value = item.GetValue(mod, null);
                string v_str = string.Empty;
                if (value == null)
                {
                    v_str = "NULL";
                }
                else if (value is string)
                {
                    v_str = string.Format("'{0}'", value.ToString());
                }
                else if (value is DateTime)
                {
                    DateTime time = (DateTime)value;
                    v_str = string.Format("'{0}'", time.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    v_str = value.ToString();
                }
                values.Add(v_str);
            }


            sql=string.Format(sql, table, string.Join(",", columns), string.Join(",", values));

            return sql;

        }



        /// <summary>
        /// 处理wherelambda表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string DealExpress(Expression exp)
        {
            if (exp is LambdaExpression)
            {
                LambdaExpression _exp = exp as LambdaExpression;

                return DealExpress(_exp.Body);
            }
            if (exp is BinaryExpression)
            {
                return DealBinaryExpression(exp as BinaryExpression);
            }
            if (exp is MemberExpression)
            {
                return DealMemberExpression(exp as MemberExpression);
            }
            if (exp is ConstantExpression)
            {
                return DealConstantExpression(exp as ConstantExpression);
            }
            if (exp is UnaryExpression)
            {
                return DealUnaryExpression(exp as UnaryExpression);
            }
            return "";

        }
        public static string DealUnaryExpression(UnaryExpression exp)
        {
            return DealExpress(exp.Operand);
        }
        /// <summary>
        /// 处理一元表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string DealConstantExpression(ConstantExpression exp)
        {
            object vaule = exp.Value;
            string v_str = string.Empty;
            if (vaule == null)
            {
                return "NULL";
            }
            if (vaule is string)
            {
                v_str = string.Format("'{0}'", vaule.ToString());
            }
            else if (vaule is DateTime)
            {
                DateTime time = (DateTime)vaule;
                v_str = string.Format("'{0}'", time.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                v_str = vaule.ToString();
            }
            return v_str;
        }
        /// <summary>
        /// 处理成员表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string DealMemberExpression(MemberExpression exp)
        {
            return exp.Member.Name;
        }
        /// <summary>
        /// 处理二元表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private static string DealBinaryExpression(BinaryExpression exp)
        {

            string left = DealExpress(exp.Left);
            string right = DealExpress(exp.Right);
            string oper = GetOperStr(exp.NodeType);

            if (string.IsNullOrEmpty(right))
            {
                if (oper == "=")
                {
                    oper = " is";
                }
                else
                {
                    oper = " is not";
                }

            }

            return left + oper + right;
        }

        /// <summary>
        /// 获取操作符字符串
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetOperStr(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.OrElse:
                    return "OR";
                case ExpressionType.Or:
                    return "||";
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.And:
                    return "&&";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Add:
                    return "+";
                case ExpressionType.Subtract:
                    return "-";
                case ExpressionType.Multiply:
                    return "*";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Modulo:
                    return "%";
                case ExpressionType.Equal:
                    return "=";
            }

            return "";
        }



    }
}
