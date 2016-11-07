using System.Collections.Generic;

namespace Differ
{
  public sealed class DummyDiffer
  {
    public ChangeSet[] Compare(ProperertySet old, ProperertySet newOne)
    {
      var result = new List<ChangeSet>();

      if (!ReferenceEquals(old, newOne))
      {
        if (old.DoubleValue != newOne.DoubleValue)
          result.Add(new ChangeSet(
            nameof(ProperertySet.DoubleValue),
            old.DoubleValue.ToString(),
            newOne.DoubleValue.ToString())
          );

        if (old.IntValue != newOne.IntValue)
          result.Add(new ChangeSet(
            nameof(ProperertySet.IntValue),
            old.IntValue.ToString(),
            newOne.IntValue.ToString())
          );

        if (old.StringValue != newOne.StringValue)
          result.Add(new ChangeSet(
            nameof(ProperertySet.StringValue),
            old.StringValue.ToString(),
            newOne.StringValue.ToString())
          );
      }

      return result.ToArray();
    }
  }
}
