﻿using System;
using System.Linq.Expressions;
using LinqSpecs.ExpressionCombining;

namespace LinqSpecs
{
	/// <summary>
	/// The and specification.
	/// </summary>
	/// <typeparam name="T">
	/// </typeparam>
	[Serializable]
	class AndSpecification<T> : Specification<T>
	{
		private readonly Specification<T> spec1;

		private readonly Specification<T> spec2;

		public AndSpecification(Specification<T> spec1, Specification<T> spec2)
		{
			this.spec1 = spec1;
			this.spec2 = spec2;
		}

		protected override object[] Parameters
		{
			get
			{
				return new[] {spec1, spec2};
			}
		}

		public override Expression<Func<T, bool>> IsSatisfiedBy()
		{
			var expr1 = spec1.IsSatisfiedBy();
			var expr2 = spec2.IsSatisfiedBy();

            // combines the expressions without the need for Expression.Invoke which fails on EntityFramework
		    return expr1.AndAlso(expr2);

            //ParameterExpression param = expr1.Parameters[0];
            //if (ReferenceEquals(param, expr2.Parameters[0]))
            //{
            //    return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, expr2.Body), param);
            //}

            //return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, Expression.Invoke(expr2, param)),
            //        param);
		}
	}
}