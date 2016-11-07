using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Differ
{
  class Program
  {
    static void Main(string[] args)
    {
      var differ = new ExpressionDiffer<ProperertySet>();

      var old = new ProperertySet
      {
        DoubleValue = .2d,
        IntValue = 231,
        StringValue = "dsdadsa"
      };

      var newOne = new ProperertySet
      {
        DoubleValue = .2d,
        IntValue = 231,
        StringValue = "dsdadsa1"
      };

      Console.WriteLine(object.Equals(.2d, .2d));
      var result = differ.Compare(old, newOne);

      Console.ReadKey();
    }
  }
}
