namespace LuckWinner.WinnerPicker.UnitTests.TestDoubles;

public class TestRandom : Random
{
  private readonly int _fixedValue;

  public TestRandom(int fixedValue)
  {
    _fixedValue = fixedValue;
  }

  public override int Next(int maxValue)
  {
    return _fixedValue;
  }
}
