namespace LuckWinner.WinnerPicker.UnitTests.Fixtures;

[CollectionDefinition("WinnerManager Collection")]
public class WinnerManagerCollection : ICollectionFixture<WinnerManagerFixture>
{
  
}

public class WinnerManagerFixture : IDisposable
{
  private readonly Dictionary<Guid, string> _originalListData = new()
  { 
    {Guid.Parse("31CD038C-6A2F-4F01-B9AF-B8DF2B1A19FE"), "Item A"},
    {Guid.Parse("90C0EBBB-0FFB-4D82-8237-3BB58056000B"), "Item B"},
    {Guid.Parse("2D51A2C7-A894-4994-A538-26D4D731E483"), "Item C"}};
  
  public Dictionary<Guid, string> ListData => _originalListData.ToDictionary(entry => entry.Key, entry => entry.Value);

  public Guid SecondItemKeyInEntries => SecondItemInEntries.Key;
  
  public KeyValuePair<Guid, string> SecondItemInEntries => ListData.ElementAt(1);

  public void Dispose()
  {
    
  }
}