using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;

namespace Differ.Benchmarks
{
  [Config(typeof(Config))]
  public class Program
  {
    static void Main(string[] args)
    {
      var summary = BenchmarkRunner.Run<Program>();

      Console.ReadKey();
    }

    private class Config : ManualConfig
    {
      public Config()
      {
        Add(Job.Clr.WithWarmupCount(5));
        Add(Job.Clr.WithLaunchCount(50));
      }
    }

    #region Act
    public DummyDiffer dummy = new DummyDiffer();
    public ReflectionDiffer<ProperertySet> reflDiffer = new ReflectionDiffer<ProperertySet>();
    public ExpressionDiffer<ProperertySet> expDiffer = new ExpressionDiffer<ProperertySet>();
    public ProperertySet old = new ProperertySet
    {
      DoubleValue = 0.05d,
      IntValue = 100500,
      StringValue = "uiiiiiiiiiiii"
    };
    public ProperertySet newOne = new ProperertySet
    {
      DoubleValue = 0.08d,
      IntValue = 1005001,
      StringValue = "uiiiiiiiiUUiii"
    };
    #endregion

    [Benchmark(Baseline = true)]
    public ChangeSet[] Dummy() => dummy.Compare(old, newOne);

    [Benchmark]
    public ChangeSet[] Reflection() => reflDiffer.Compare(old, newOne);

    [Benchmark]
    public ChangeSet[] Expression() => expDiffer.Compare(old, newOne);
  }
}
