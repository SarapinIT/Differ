using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Differ
{
  public sealed class ExpressionDiffer<T>
  {
    Action<List<ChangeSet>, T, T> _compiled;

    public ExpressionDiffer()
    {
      var type = typeof(T);

      var propertyComparators = new List<Expression>();
      var propertyInfos = type.GetProperties();

      var listParam = Expression.Parameter(typeof(List<ChangeSet>));
      var oldParam = Expression.Parameter(typeof(T));
      var newParam = Expression.Parameter(typeof(T));

      if (propertyInfos.Length == 0) throw new ArgumentException("type have not properties");

      foreach (var propertyInfo in propertyInfos)
      {
        var nameConst = Expression.Constant(propertyInfo.Name, typeof(string));

        var oldVal = Expression.Property(oldParam, propertyInfo.Name);
        var newVal = Expression.Property(newParam, propertyInfo.Name);

        var convertedOld = Expression.Convert(oldVal, typeof(object));
        var convertedNew = Expression.Convert(newVal, typeof(object));

        var eq = Expression.NotEqual(oldVal, newVal);

        var ctorInfo = typeof(ChangeSet).GetConstructor(new[] { typeof(string), typeof(int), typeof(int) });
        var changeset = Expression.New(ctorInfo, new Expression[] { nameConst, convertedOld, convertedNew });
        var addToList = Expression.Call(listParam, typeof(List<ChangeSet>).GetMethod("Add"), changeset);

        var condition = Expression.IfThen(eq, addToList);

        propertyComparators.Add(condition);
      }

      var block = Expression.Block(propertyComparators.ToArray());
      var lambda = Expression.Lambda<Action<List<ChangeSet>, T, T>>(block, new[] { listParam, oldParam, newParam });
      _compiled = lambda.Compile();
    }

    public ChangeSet[] Compare(T old, T newOne)
    {
      var result = new List<ChangeSet>();
      if(!ReferenceEquals(old, newOne))
      {
        _compiled(result, old, newOne);
      }
      return result.ToArray();
    }
  }
}
