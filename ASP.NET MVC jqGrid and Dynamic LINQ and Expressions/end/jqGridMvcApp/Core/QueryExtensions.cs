using System;
using System.Linq;
using System.Linq.Expressions;

namespace jqGridMvcApp.Core
{
    public static class QueryExtensions
    {
         public static IQueryable<T> WhereFilter<T>(this IQueryable<T> list, T filterValues) where T: class 
        {
            // получаем все свойства filterValues значения которых != null
            var props = filterValues.GetType().GetProperties()
                        .Where(propertyInfo => propertyInfo.GetValue(filterValues, null) != null)
                        .ToList();

            // создаем предикат и используем его для фильтрации коллекции
            foreach (var prop in props)
            {
                var predicate = CreatePredicate<T>(prop.Name, prop.GetValue(filterValues, null));

                list = list.Where(predicate);
            }
            return list;
        }

        public static Expression<Func<T, bool>> CreatePredicate<T>(string propertyName, object propertyValue)
        {
            var argument = Expression.Parameter(typeof (T),"x");
            var property = Expression.Convert(Expression.Property(argument, propertyName),propertyValue.GetType());
            var value = Expression.Constant(propertyValue);
            var body = Expression.Equal(property, value);

            return Expression.Lambda<Func<T, bool>>(body,argument);

        }
    }
}