using System.Collections.Generic;

namespace Differ
{
  public sealed class ReflectionDiffer<T> 
    where T : class
  {
    public ChangeSet[] Compare(T old, T newOne)
    {
      var result = new List<ChangeSet>();

      if (!ReferenceEquals(old, newOne))
      {
        var type = typeof(T);
        var propertyInfos = type.GetProperties();
        foreach (var propertyInfo in propertyInfos)
        {
          var oldValue = propertyInfo.GetValue(old);
          var newValue = propertyInfo.GetValue(newOne);

          if (!Equals(oldValue, newValue))
          {
            result.Add(new ChangeSet(propertyInfo.Name, oldValue.ToString(), newValue.ToString()));
          }
        }
      }

      return result.ToArray();
    }
  }
}
