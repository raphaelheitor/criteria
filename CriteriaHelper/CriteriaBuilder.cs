using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;
using CriteriaHelper.Support;
using Castle.ActiveRecord.Queries;
using System.Linq.Expressions;
using Castle.Core;

namespace CriteriaHelper
{
    public class CriteriaBuilder<T>
    {
        private readonly string alias = "this_";
        private ReflectionHelper<T> helper;
        private DetachedCriteria detachedCriteria;
        private ActiveRecordCriteriaQuery activeRecordCriteria;
        private ProjectionList projectionList;
        private Disjunction disjunction;

        public CriteriaBuilder()
        {
            this.helper = new ReflectionHelper<T>();
            this.detachedCriteria = DetachedCriteria.For<T>(this.alias);
            this.projectionList = Projections.ProjectionList();   
        }

        private void initializeDisjunction(){
            this.disjunction = new Disjunction();
        }
        /// <summary>
        /// Parameter if between a DatePart(year, month, day) for DateTime object.
        /// </summary>
        /// <param name="exp">field</param>
        /// <param name="lower">lower value of parameter query</param>
        /// <param name="higher">higher value of parameter query</param>
        /// <param name="partOfDate">Enum DatePart(day, month or year)</param>
        /// <returns></returns>
        public CriteriaBuilder<T> BetweenDatePart(Expression<Func<T, object>> exp, object lower, object higher, DatePart partOfDate)
        {
            this.detachedCriteria.Add(Restrictions.Between(
                Projections.SqlFunction(partOfDate.ToString(), NHibernateUtil.DateTime, Projections.Property(expressionToStringWithAlias(exp))), lower, higher));
            return this;
        }
        public CriteriaBuilder<T> Between(Expression<Func<T, object>> exp, object lower, object higher)
        {
            this.detachedCriteria.Add(NHibernate.Criterion.Expression.Between(expressionToStringWithAlias(exp), lower, higher));
            return this;
        }
        public CriteriaBuilder<T> BetweenAllowingNull(Expression<Func<T, object>> exp, object lower, object higher)
        {
            if (IsNotNull(lower) && IsNotNull(higher))
                this.detachedCriteria.Add(Restrictions.Between(expressionToStringWithAlias(exp), lower, higher));
            return this;
        }
        public CriteriaBuilder<T> Count(Expression<Func<T, object>> exp)
        {
            this.projectionList.Add(Projections.Count(expressionToStringWithAlias(exp)));
            return this;
        }
        public CriteriaBuilder<T> DisjunctionEq(Expression<Func<T, object>> exp, object value)
        {
            if (this.disjunction == null)
                initializeDisjunction();
            this.disjunction.Add(Restrictions.Eq(expressionToStringWithAlias(exp), value));
            return this;
        }
        public CriteriaBuilder<T> DisjunctionEqAllowingNull(Expression<Func<T, object>> exp, object value)
        {
            if (this.disjunction == null)
                initializeDisjunction();
            if (IsNotNull(value))
                this.disjunction.Add(Restrictions.Eq(expressionToStringWithAlias(exp), value));
            return this;
        }
        public CriteriaBuilder<T> DisjunctionLike(Expression<Func<T, object>> exp, object value)
        {
            if (this.disjunction == null)
                initializeDisjunction();
            this.disjunction.Add(Restrictions.Like(expressionToStringWithAlias(exp), value.ToString(), MatchMode.Anywhere));
            return this;
        }
        public CriteriaBuilder<T> DisjunctionLikeAllowingNull(Expression<Func<T, object>> exp, object value)
        {
            if (this.disjunction == null)
                initializeDisjunction();
            if (IsNotNull(value))
                this.disjunction.Add(Restrictions.Like(expressionToStringWithAlias(exp), value.ToString(), MatchMode.Anywhere));
            return this;
        }
        public CriteriaBuilder<T> Eq(Expression<Func<T, object>> exp, object value)
        {
            this.detachedCriteria.Add(Restrictions.Eq(expressionToStringWithAlias(exp), value));
            return this;
        }
        /// <summary>
        /// Parameter if equal a DatePart(year, month, day) for DateTime object.
        /// </summary>
        /// <param name="exp">field</param>
        /// <param name="value">value of parameter query</param>
        /// <param name="partOfDate">Enum DatePart(day, month or year)</param>
        /// <returns></returns>
        public CriteriaBuilder<T> EqDatePartAllowingNull(Expression<Func<T, object>> exp, object value, DatePart partOfDate)
        {
            if (IsNotNull(value))
                this.detachedCriteria.Add(Restrictions.Eq(
                    Projections.SqlFunction(partOfDate.ToString(), NHibernateUtil.DateTime, Projections.Property(expressionToStringWithAlias(exp))), value));
            return this;
        }
        public CriteriaBuilder<T> EqAllowingNull(Expression<Func<T, object>> exp, object value)
        {
            if (IsNotNull(value))
                this.detachedCriteria.Add(Restrictions.Eq(expressionToStringWithAlias(exp), value));
            return this;
        }
        public CriteriaBuilder<T> EqJoinAllowingNull(string propertyRelationship, Expression<Func<T, object>> exp, object value)
        {
            if (IsNotNull(value))
            {
                this.detachedCriteria.CreateAlias(propertyRelationship, propertyRelationship);
                this.detachedCriteria.SetFetchMode(expressionToStringWithAlias(exp), FetchMode.Join);
                string newExpression = expressionToStringWithAlias(exp).Remove(0, 6);
                this.detachedCriteria.Add(Restrictions.Eq(newExpression, value));
            }
            return this;
        }
        /// <summary>
        /// Parameter if greater than a DatePart(year, month, day) for DateTime object.
        /// </summary>
        /// <param name="exp">field</param>
        /// <param name="value">value of parameter query</param>
        /// <param name="partOfDate">Enum DatePart(day, month or year)</param>
        /// <returns></returns>
        public CriteriaBuilder<T> GreaterThanDatePartAllowingNull(Expression<Func<T, object>> exp, object value, DatePart partOfDate)
        {
            if (IsNotNull(value))
                this.detachedCriteria.Add(Restrictions.Gt(
                    Projections.SqlFunction(partOfDate.ToString(), NHibernateUtil.DateTime, Projections.Property(expressionToStringWithAlias(exp))), value));
            return this;
        }
        public CriteriaBuilder<T> GreaterThan(Expression<Func<T, object>> exp, object value)
        {
            this.detachedCriteria.Add(Restrictions.Gt(expressionToStringWithAlias(exp), value));
            return this;
        }
        public CriteriaBuilder<T> GreaterThanAllowingNull(Expression<Func<T, object>> exp, object value)
        {
            if(IsNotNull(value))
                this.detachedCriteria.Add(Restrictions.Gt(expressionToStringWithAlias(exp), value));
            return this;
        }
        /// <summary>
        /// Parameter if greater than or equal a DatePart(year, month, day) for DateTime object.
        /// </summary>
        /// <param name="exp">field</param>
        /// <param name="value">value of parameter query</param>
        /// <param name="partOfDate">Enum DatePart(day, month or year)</param>
        /// <returns></returns>
        public CriteriaBuilder<T> GreaterThanOrEqualDatePartAllowingNull(Expression<Func<T, object>> exp, object value, DatePart partOfDate)
        {
            if (IsNotNull(value))
                this.detachedCriteria.Add(Restrictions.Ge(
                    Projections.SqlFunction(partOfDate.ToString(), NHibernateUtil.DateTime, Projections.Property(expressionToStringWithAlias(exp))), value));
            return this;
        }
        public CriteriaBuilder<T> GreaterThanOrEqual(Expression<Func<T, object>> exp, object value)
        {
            this.detachedCriteria.Add(Restrictions.Ge(expressionToStringWithAlias(exp), value));
            return this;
        }
        public CriteriaBuilder<T> GreaterThanOrEqualAllowingNull(Expression<Func<T, object>> exp, object value)
        {
            if(IsNotNull(value))
                this.detachedCriteria.Add(Restrictions.Ge(expressionToStringWithAlias(exp), value));
            return this;
        }

        public CriteriaBuilder<T> IsNotNull(Expression<Func<T, object>> exp)
        {
            this.detachedCriteria.Add(Restrictions.IsNotNull(expressionToStringWithAlias(exp)));
            return this;
        }
        /// <summary>
        /// Parameter if less than a DatePart(year, month, day) for DateTime object.
        /// </summary>
        /// <param name="exp">field</param>
        /// <param name="value">value of parameter query</param>
        /// <param name="partOfDate">Enum DatePart(day, month or year)</param>
        /// <returns></returns>
        public CriteriaBuilder<T> LessThanDatePartAllowingNull(Expression<Func<T, object>> exp, object value, DatePart partOfDate)
        {
            if (IsNotNull(value))
                this.detachedCriteria.Add(Restrictions.Lt(
                    Projections.SqlFunction(partOfDate.ToString(), NHibernateUtil.DateTime, Projections.Property(expressionToStringWithAlias(exp))), value));
            return this;
        }
        public CriteriaBuilder<T> LessThan(Expression<Func<T, object>> exp, object value)
        {
            this.detachedCriteria.Add(Restrictions.Lt(expressionToStringWithAlias(exp), value));
            return this;
        }
        public CriteriaBuilder<T> LessThanAllowingNull(Expression<Func<T, object>> exp, object value)
        {
            if(IsNotNull(value))
                this.detachedCriteria.Add(Restrictions.Lt(expressionToStringWithAlias(exp), value));
            return this;
        }
        /// <summary>
        /// Parameter if less than or equal a DatePart(year, month, day) for DateTime object.
        /// </summary>
        /// <param name="exp">field</param>
        /// <param name="value">value of parameter query</param>
        /// <param name="partOfDate">Enum DatePart(day, month or year)</param>
        /// <returns></returns>
        public CriteriaBuilder<T> LessThanOrEqualDatePartAllowingNull(Expression<Func<T, object>> exp, object value, DatePart partOfDate)
        {
            if (IsNotNull(value))
                this.detachedCriteria.Add(Restrictions.Le(
                    Projections.SqlFunction(partOfDate.ToString(), NHibernateUtil.DateTime, Projections.Property(expressionToStringWithAlias(exp))), value));
            return this;
        }
        public CriteriaBuilder<T> LessThanOrEqual(Expression<Func<T, object>> exp, object value)
        {
            this.detachedCriteria.Add(Restrictions.Le(expressionToStringWithAlias(exp), value));
            return this;
        }
        public CriteriaBuilder<T> LessThanOrEqualAllowingNull(Expression<Func<T, object>> exp, object value)
        {
            if(IsNotNull(value))
                this.detachedCriteria.Add(Restrictions.Le(expressionToStringWithAlias(exp), value));
            return this;
        }
        public CriteriaBuilder<T> Like(Expression<Func<T, object>> exp, object value)
        {
            this.detachedCriteria.Add(Restrictions.Like(expressionToStringWithAlias(exp), value.ToString(), MatchMode.Anywhere));
            return this;
        }
        public CriteriaBuilder<T> LikeAllowingNull(Expression<Func<T, object>> exp, object value)
        {
            if (IsNotNull(value))
                this.detachedCriteria.Add(Restrictions.Like(expressionToStringWithAlias(exp), value.ToString(), MatchMode.Anywhere));
            return this;
        }
        public CriteriaBuilder<T> OrderByAsc(Expression<Func<T, object>> exp)
        {
            this.detachedCriteria.AddOrder(Order.Asc(expressionToStringWithAlias(exp)));
            return this;
        }
        public CriteriaBuilder<T> OrderByDesc(Expression<Func<T, object>> exp)
        {
            this.detachedCriteria.AddOrder(Order.Desc(expressionToStringWithAlias(exp)));
            return this;
        }
        public CriteriaBuilder<T> OrEq(Expression<Func<T, object>> firstField, object firstValue, Expression<Func<T, object>> lastField, object lastValue)
        {

            this.detachedCriteria.Add(Restrictions.Or(Restrictions.Eq(expressionToStringWithAlias(firstField), firstValue), 
                                                      Restrictions.Eq(expressionToStringWithAlias(lastField), lastValue))); 
            return this;
        }
        public CriteriaBuilder<T> OrEqLike(Expression<Func<T, object>> eqField, object eqValue, Expression<Func<T, object>> likeField, object likeValue)
        {
            this.detachedCriteria.Add(Restrictions.Or(Restrictions.Eq(expressionToStringWithAlias(eqField), eqValue),
                                                      Restrictions.Like(expressionToStringWithAlias(likeField), likeValue.ToString(), MatchMode.Anywhere)));
            return this;
        }
        public CriteriaBuilder<T> OrLike(Expression<Func<T, object>> firstField, object firstValue, Expression<Func<T, object>> lastField, object lastValue)
        {
            this.detachedCriteria.Add(Restrictions.Or(Restrictions.Like(expressionToStringWithAlias(firstField), firstValue.ToString(), MatchMode.Anywhere),
                                                      Restrictions.Like(expressionToStringWithAlias(lastField), lastValue.ToString(), MatchMode.Anywhere)));
            return this;
        }
        public CriteriaBuilder<T> Sum(Expression<Func<T, object>> exp)
        {
            this.projectionList.Add(Projections.Sum(expressionToStringWithAlias(exp)));
            return this;
        }

        public CriteriaBuilder<T> Range(int initialResult, int maxResult)
        {
            this.detachedCriteria.SetFirstResult(initialResult).SetMaxResults(maxResult);
            return this;
        }

        public CriteriaBuilder<T> Paging(int pageIndex, int pageSize)
        {
            this.detachedCriteria.SetFirstResult((pageIndex * pageSize)).SetMaxResults(pageSize);
            return this;
        }

        public DetachedCriteria GetDetachedCriteria()
        {
            if (this.disjunction != null)
                this.detachedCriteria.Add(this.disjunction);
            return this.detachedCriteria;
        }

        public ActiveRecordCriteriaQuery GetActiveRecordCriteria()
        {
            this.detachedCriteria.SetProjection(this.projectionList);
            if (this.disjunction != null)
                this.detachedCriteria.Add(this.disjunction);
            return this.activeRecordCriteria = new ActiveRecordCriteriaQuery(typeof(T), this.detachedCriteria);
        }

        private bool IsNotNull(object value)
        {
            return (value != null && value != "" && value != (object)-1) ? true : false;
        }

        private string expressionToStringWithAlias(Expression<Func<T, object>> exp)
        {
            return this.helper.PropertyAsString(exp, this.alias);
        }
    }
    public enum DatePart
    {
        Year,
        Month,
        Day
    }
}
