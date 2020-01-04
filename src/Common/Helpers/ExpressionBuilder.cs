#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// LICENSED UNDER THE APACHE LICENSE, VERSION 2.0 (THE "LICENSE"); YOU MAY NOT USE THESE 
// FILES EXCEPT IN COMPLIANCE WITH THE LICENSE. YOU MAY OBTAIN A COPY OF THE LICENSE AT 
// http://www.apache.org/licenses/LICENSE-2.0
// UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING, SOFTWARE DISTRIBUTED UNDER THE 
// LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY 
// KIND, EITHER EXPRESS OR IMPLIED. SEE THE LICENSE FOR THE SPECIFIC LANGUAGE GOVERNING 
// PERMISSIONS AND LIMITATIONS UNDER THE LICENSE.
//=======================================================================================
#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

#endregion

namespace ServiceBusExplorer.Helpers
{
    public enum LogicalOperator
    {
        And,
        Or
    }

    public enum Operator
    {
        Equal,
        NotEqual,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith,
        Unkwnon
    }

    public class ExpressionFilter
    {
        #region Public Properties
        public string Property { get; set; }
        public Operator Operator { get; set; }
        public object Value { get; set; }
        public LogicalOperator LogicalOperator { get; set; } 
        #endregion
    }

    public class ExpressionBuilder<T>
    {
        #region Private Constants
        //***************************
        // Constants
        //***************************
        private const string PropertyGroup = "Property";
        private const string OperatorGroup = "Operator";
        private const string ValueGroup = "Value";
        private const string AndPattern = @"(?i)\s+(and|or)\s+";
        private const string PropertyPattern = @"(?i)(?<Property>(\w|\.)+)\s*(?<Operator>=|!=|>=?|<=?|contains|startswith|endswith)\s*(?<Value>\S+)";
        private const string OpArgument = "op";
        private const string ValueArgument = "value";
        private const string FilterExpressionArgument = "filterExpression";
        private const string FilterListArgument = "filterList";
        private const string ParamArgument = "param";
        private const string Filter1Argument = "filter1";
        private const string Filter2Argument = "filter2";
        private const string NullValue = "null";
        private const string AndOperator = "and";
        private const string OrOperator = "or"; 

        //***************************
        // Messages
        //***************************
        private const string ArgumentCannotBeNull = "The argument [{0}] cannot be null.";
        private const string ArgumentCannotBeNullOrEmpty = "The argument [{0}] cannot be null or empty.";
        private const string CollectionCannotBeNullOrEmpty = "The collection [{0}] cannot be null or empty.";
        private const string StringNotDelimited = "The string [{0}] is not properly delimited by ' or \" characters.";
        private const string PropertyDoesNotExist = "The type [{0}] does not contain the [{1}] property.";
        private const string PropertyIsNotPrimitiveOrDateTimeOrString = "The type [{0}] of the [{1}] property is not primitive, datetime or string.";
        private const string OperatorUnknownAndUnsupported = "The operator [{0}] is unknown and unsupported.";
        private const string ErrorConvertingValue = "An error occurred while converting the string [{0}] to [{1}] type. See the inner exception for more information.";
        private const string PredicateInvalid = "The predicate [{0}] is invalid.";
        private const string FilterExpressionCannotBeNullOrEmpty = "The filter expression cannot be null or empty.";
        private const string FilterExpressionCannotStartWithLogicalOperator = "The filter expression [{0}] cannot start with a logical operator [AND, OR].";
        #endregion

        #region Private Instance Fields
        private readonly MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private readonly MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        private readonly MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
        private readonly MethodInfo getValueOrDefault = typeof (DateTime?).GetMethod("GetValueOrDefault", new Type[]{});
        private readonly List<PropertyInfo> propertyList;
        #endregion
        #region Public Constructor
        public ExpressionBuilder()
        {
            var type = typeof(T);
            propertyList = new List<PropertyInfo>(type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
        } 
        #endregion

        #region Public Properties
        public Expression<Func<T, bool>> GetExpression(string filterExpression)
        {
            if (string.IsNullOrWhiteSpace(filterExpression))
            {
                throw new ArgumentException(FilterExpressionCannotBeNullOrEmpty, FilterExpressionArgument);
            }
            var filterList = new List<ExpressionFilter>();
            var predicates = Regex.Split(filterExpression, AndPattern, RegexOptions.IgnoreCase);
            var propertyRegex = new Regex(PropertyPattern);
            foreach (var predicate in predicates)
            {
                if (string.Compare(predicate, AndOperator, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    if (filterList.Count == 0)
                    {
                        throw new ApplicationException(FilterExpressionCannotStartWithLogicalOperator);
                    }
                    filterList[filterList.Count - 1].LogicalOperator = LogicalOperator.And;
                    continue;
                }
                if (string.Compare(predicate, OrOperator, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    if (filterList.Count == 0)
                    {
                        throw new ApplicationException(FilterExpressionCannotStartWithLogicalOperator);
                    }
                    filterList[filterList.Count - 1].LogicalOperator = LogicalOperator.Or;
                    continue;
                }
                var matches = propertyRegex.Matches(predicate);
                for (var j = 0; j < matches.Count; j++)
                {
                    var property = matches[j].Groups[PropertyGroup].Value;
                    var op = matches[j].Groups[OperatorGroup].Value;
                    var value = matches[j].Groups[ValueGroup].Value;
                    var propertyInfo = propertyList.FirstOrDefault(p => string.Compare(p.Name, property, StringComparison.InvariantCultureIgnoreCase) == 0);
                    var operatorInfo = GetOperator(op);

                    if (string.IsNullOrWhiteSpace(property) ||
                        string.IsNullOrWhiteSpace(op) ||
                        string.IsNullOrWhiteSpace(value))
                    {
                        throw new ApplicationException(string.Format(PredicateInvalid, predicate));
                    }

                    if (propertyInfo == null)
                    {
                        throw new ApplicationException(string.Format(PropertyDoesNotExist, typeof(T).Name, property));
                    }

                    if (!propertyInfo.PropertyType.IsPrimitive &&
                        propertyInfo.PropertyType != typeof(string) &&
                        propertyInfo.PropertyType != typeof(DateTime) &&
                        propertyInfo.PropertyType != typeof(DateTime?))
                    {
                        throw new ApplicationException(string.Format(PropertyIsNotPrimitiveOrDateTimeOrString, propertyInfo.PropertyType, propertyInfo.Name));
                    }

                    if (operatorInfo == Operator.Unkwnon)
                    {
                        throw new ApplicationException(string.Format(OperatorUnknownAndUnsupported, op));
                    }

                    object typedValue;
                    try
                    {
                        typedValue = propertyInfo.PropertyType == typeof(string) ?
                                     GetString(value) :
                                     propertyInfo.PropertyType == typeof(DateTime) ||
                                     propertyInfo.PropertyType == typeof(DateTime?) ?
                                     Convert.ChangeType(RemoveDelimiters(value), Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType) :
                                     Convert.ChangeType(value, propertyInfo.PropertyType);
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException(string.Format(ErrorConvertingValue, value, propertyInfo.PropertyType.Name), ex);
                    }

                    filterList.Add(new ExpressionFilter
                    {
                        Property = propertyInfo.Name,
                        Operator = operatorInfo,
                        Value = typedValue
                    });
                }
            }
            return GetExpression(filterList);
        }

        public Expression<Func<T, bool>> GetExpression(IList<ExpressionFilter> filterList)
        {
            if (filterList.Count == 0)
            {
                throw new ArgumentException(string.Format(CollectionCannotBeNullOrEmpty, FilterListArgument), FilterListArgument);
            }

            var param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;
            var op = LogicalOperator.And;
            switch (filterList.Count)
            {
                case 1:
                    exp = GetExpression(param, filterList[0]);
                    break;
                case 2:
                    exp = GetExpression(param, filterList[0], filterList[1]);
                    break;
                default:
                    while (filterList.Count > 0)
                    {
                        var f1 = filterList[0];
                        var f2 = filterList[1];

                        exp = exp == null ?
                              GetExpression(param, filterList[0], filterList[1]) :
                              op == LogicalOperator.Or ?
                              Expression.OrElse(exp, GetExpression(param, filterList[0], filterList[1])) :
                              Expression.AndAlso(exp, GetExpression(param, filterList[0], filterList[1]));

                        op = filterList[1].LogicalOperator;

                        filterList.Remove(f1);
                        filterList.Remove(f2);

                        if (filterList.Count != 1)
                        {
                            continue;
                        }
                        exp = Expression.AndAlso(exp, GetExpression(param, filterList[0]));
                        filterList.RemoveAt(0);
                    }
                    break;
            }

            return exp != null ? Expression.Lambda<Func<T, bool>>(exp, param) : null;
        } 
        #endregion

        private Expression GetExpression(ParameterExpression param, ExpressionFilter filter)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            var propertyInfo = propertyList.FirstOrDefault(p => string.Compare(p.Name, filter.Property, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (propertyInfo != null)
            {
                var member = Expression.Property(param, filter.Property);
                var constant = filter.Value != null ?
                                   Expression.Constant(filter.Value, Nullable.GetUnderlyingType(filter.Value.GetType()) ?? filter.Value.GetType()) :
                                   Expression.Constant(filter.Value);
                var left = propertyInfo.PropertyType != typeof(DateTime?) ? member : (Expression)Expression.Call(member, getValueOrDefault);

                switch (filter.Operator)
                {
                    case Operator.Equal:
                        return Expression.Equal(left, constant);
                    case Operator.NotEqual:
                        return Expression.NotEqual(left, constant);
                    case Operator.GreaterThan:
                        return Expression.GreaterThan(left, constant);
                    case Operator.GreaterThanOrEqual:
                        return Expression.GreaterThanOrEqual(left, constant);
                    case Operator.LessThan:
                        return Expression.LessThan(left, constant);
                    case Operator.LessThanOrEqual:
                        return Expression.LessThanOrEqual(left, constant);
                    case Operator.Contains:
                        return Expression.Call(left, containsMethod, new Expression[] { constant });
                    case Operator.StartsWith:
                        return Expression.Call(left, startsWithMethod, new Expression[] { constant });
                    case Operator.EndsWith:
                        return Expression.Call(left, endsWithMethod, new Expression[] { constant });
                }
            }
            return null;
        }

        #region Private Instance Methods
        private BinaryExpression GetExpression(ParameterExpression param, ExpressionFilter filter1, ExpressionFilter filter2)
        {
            if (param == null)
            {
                throw new ArgumentNullException(ParamArgument, string.Format(ArgumentCannotBeNull, ParamArgument));
            }
            if (filter1 == null)
            {
                throw new ArgumentNullException(Filter1Argument, string.Format(ArgumentCannotBeNull, Filter1Argument));
            }
            if (filter2 == null)
            {
                throw new ArgumentNullException(Filter2Argument, string.Format(ArgumentCannotBeNull, Filter2Argument));
            }
            var bin1 = GetExpression(param, filter1);
            var bin2 = GetExpression(param, filter2);
            return filter1.LogicalOperator == LogicalOperator.Or ?
                   Expression.OrElse(bin1, bin2) :
                   Expression.AndAlso(bin1, bin2);
        }

        private static Operator GetOperator(string op)
        {
            if (string.IsNullOrWhiteSpace(op))
            {
                throw new ArgumentException(string.Format(ArgumentCannotBeNullOrEmpty, OpArgument), OpArgument);
            }
            switch (op.ToLower())
            {
                case "=":
                    return Operator.Equal;
                case "!=":
                    return Operator.NotEqual;
                case ">":
                    return Operator.GreaterThan;
                case ">=":
                    return Operator.GreaterThanOrEqual;
                case "<":
                    return Operator.LessThan;
                case "<=":
                    return Operator.LessThanOrEqual;
                case "contains":
                    return Operator.Contains;
                case "startswith":
                    return Operator.StartsWith;
                case "endswith":
                    return Operator.EndsWith;
                default:
                    return Operator.Unkwnon;
            }
        } 
        #endregion

        #region Private Static Methods
        private static string GetString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(string.Format(ArgumentCannotBeNullOrEmpty, ValueArgument), ValueArgument);
            }
            if (string.Compare(value, NullValue, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                return null;
            }
            if (value.Length >= 2 &&
                (value[0] == '\'' && value[value.Length - 1] == '\'') ||
                (value[0] == '"' && value[value.Length - 1] == '"'))
            {
                return value.Length == 2 ? string.Empty : value.Substring(1, value.Length - 2);
            }
            throw new ApplicationException(string.Format(StringNotDelimited, value));
        }

        private static string RemoveDelimiters(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(string.Format(ArgumentCannotBeNullOrEmpty, ValueArgument), ValueArgument);
            }
            if (value.Length >= 2 &&
                (value[0] == '\'' && value[value.Length - 1] == '\'') ||
                (value[0] == '"' && value[value.Length - 1] == '"'))
            {
                return value.Length == 2 ? string.Empty : value.Substring(1, value.Length - 2);
            }
            return value;
        } 
        #endregion
    }
}
