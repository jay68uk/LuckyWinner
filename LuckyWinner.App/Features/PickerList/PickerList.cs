namespace LuckyWinner.App.Features.PickerList;

public sealed class PickerList
{
  private readonly Dictionary<Guid, string> _entries = [];

  public IReadOnlyDictionary<Guid, string> Entries => _entries.AsReadOnly();
  public bool IsModified { get; private set; }

  private PickerList(Dictionary<Guid, string>? listData = null)
  {
    if (listData is not null)
    {
      _entries = listData;
    }
  }

  public static PickerList Initialise()
  {
    return new PickerList();
  }

  public static PickerList Initialise(Dictionary<Guid, string> listData)
  {
    return new PickerList(listData);
  }

  public void AddEntry(Guid key,string value)
  {
    _entries.Add(key, value);
    IsModified = true;
  }

  public void DeleteEntry(Guid key)
  {
    _entries.Remove(key);
    IsModified = true;
  }

  public void SetModifiedStatus(bool status)
  {
    IsModified = status;
  }
}