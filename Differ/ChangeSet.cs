namespace Differ
{
  public class ChangeSet
  {
    public ChangeSet()
    {

    }
    public ChangeSet(string propertyName, object oldValue, object newValue)
    {
      PropertyName = propertyName;
      OldValue = oldValue;
      NewValue = newValue;
    }
    public string PropertyName { get; }
    public object OldValue { get; }
    public object NewValue { get; }
  }
}