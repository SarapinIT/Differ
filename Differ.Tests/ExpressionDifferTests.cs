using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Differ.Tests
{
  [TestClass]
  public class ExpressionDifferTests
  {
    [TestMethod]
    public void Compare_SameObj_EmptyCollection()
    {
      var differ = new ExpressionDiffer<ProperertySet>();

      var obj = new ProperertySet
      {
        DoubleValue = 80.2d,
        StringValue = "string!",
        IntValue = 100500
      };
      var diff = differ.Compare(obj, obj);

      Assert.AreEqual(0, diff.Count());
    }

    [TestMethod]
    public void Compare_ChangedObj_Diff()
    {
      var differ = new ExpressionDiffer<ProperertySet>();

      var obj1 = new ProperertySet
      {
        DoubleValue = 80.2d,
        StringValue = "string!",
        IntValue = 100500
      };

      var obj2 = new ProperertySet
      {
        DoubleValue = obj1.DoubleValue,
        StringValue = obj1.StringValue,
        IntValue = obj1.IntValue
      };

      obj2.StringValue = "new";

      var diff = differ.Compare(obj1, obj2).ToArray();

      Assert.AreEqual(1, diff.Length);

      var diffItem = diff[0];

      Assert.AreEqual("StringValue", diffItem.PropertyName);
      Assert.AreEqual("string!", diffItem.OldValue);
      Assert.AreEqual("new", diffItem.NewValue);
    }
  }
}