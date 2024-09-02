using System.Collections.ObjectModel;
using FluentAssertions;
using LuckWinner.WinnerPicker.UnitTests.Fixtures;
using LuckWinner.WinnerPicker.UnitTests.TestDoubles;

namespace LuckWinner.WinnerPicker.UnitTests;

[Collection("WinnerManager Collection")]
public class WinnerManagerTests(WinnerManagerFixture fixture)
{
  private readonly TestRandom _randomFixedSecondItem = new TestRandom(1);

  [Fact]
  public void Should_ReturnWinnerManager_WhenInitialiseIsCalledWithData()
  {
    var winnerManager = WinnerManager.Initialise(fixture.ListData, _randomFixedSecondItem);
    
    winnerManager.Entries.Should().HaveCount(3);
  }

  [Fact]
  public void Should_AddWinnerToWinners_WhenWinnerIsPicked()
  {
    var winnerManager = WinnerManager.Initialise(fixture.ListData, _randomFixedSecondItem);
    var winner = fixture.SecondItemKeyInEntries;

    winnerManager.AddWinner(winner);

    winnerManager.Winners.Should().HaveCount(1);
  }
  
  [Fact]
  public void Should_AddDuplicateWinnerToWinners_WhenSameWinnerIsPicked()
  {
    var winnerManager = WinnerManager.Initialise(fixture.ListData, _randomFixedSecondItem);
    var winner = fixture.SecondItemKeyInEntries;

    winnerManager.AddWinner(winner);
    winnerManager.AddWinner(winner);

    winnerManager.Winners.Should().HaveCount(2);
  }

  [Fact]
  public void Should_RemoveWinner_FromEntries_WhenInvoked()
  {
    var winnerManager = WinnerManager.Initialise(fixture.ListData, _randomFixedSecondItem);
    var winner = fixture.SecondItemKeyInEntries;
    
    winnerManager.RemoveWinnerFromEntries(winner);
    
    winnerManager.Entries.Should().HaveCount(2);
  }

  [Fact]
  public void Should_PickWinner_WhenInvoked()
  {
    var winnerManager = WinnerManager.Initialise(fixture.ListData, _randomFixedSecondItem);

    var winner = winnerManager.PickWinner();
    
    winner.Should().BeEquivalentTo(fixture.SecondItemInEntries);
  }
}

public class WinnerManager
{
  private readonly Dictionary<Guid,string> _entries;
  private readonly List<string> _winners = [];
  private Random _random;

  private WinnerManager(Dictionary<Guid, string> data, Random random)
  {
    _entries = data;
    _random = random;
  }

  public ReadOnlyDictionary<Guid, string> Entries => _entries.AsReadOnly();
  public IReadOnlyList<string> Winners => _winners.AsReadOnly();

  public static WinnerManager Initialise(Dictionary<Guid, string> data, Random random)
  {
    return new WinnerManager(data, random);
  }

  public void AddWinner(Guid winner)
  {
    _winners.Add(_entries[winner]);
  }

  public void RemoveWinnerFromEntries(Guid winner)
  {
    _entries.Remove(winner);
  }

  public KeyValuePair<Guid, string> PickWinner()
  {
    var randomIndex = _random.Next(_entries.Count);
    
    return _entries.ElementAt(randomIndex);
  }
}