using FluentAssertions;
using LuckWinner.WinnerPicker.UnitTests.Fixtures;
using LuckyWinner.App.Features.PickerList;

namespace LuckWinner.WinnerPicker.UnitTests;

[Collection("PickerList Collection")]
public class PickerListTests(PickerListFixture fixture)
{
  [Fact]
  public void Should_ReturnPickerList_WhenInitialiseIsCalled()
  {
    var pickerList = PickerList.Initialise();
    
    pickerList.Entries.Should().HaveCount(0);
    pickerList.IsModified.Should().BeFalse();
  }
  
  [Fact]
  public void Should_ReturnPickerList_WhenInitialiseIsCalledWithData()
  {
    var pickerList = PickerList.Initialise(fixture.ListData);
    
    pickerList.Entries.Should().HaveCount(3);
    pickerList.IsModified.Should().BeFalse();
  }
  
  [Fact]
  public void Should_AddEntryToList_WhenInputValid()
  {
    var pickerList = PickerList.Initialise();

    pickerList.AddEntry(Guid.NewGuid(), "Item D");
    
    pickerList.Entries.Should().HaveCount(1);
    pickerList.IsModified.Should().BeTrue();
  }
  
  [Fact]
  public void Should_DeleteEntryFromList_WhenEntryIsFound()
  {
    var key = Guid.Parse(PickerListFixture.SecondItemInList);
    var pickerList = PickerList.Initialise(fixture.ListData);

    pickerList.DeleteEntry(key);
    
    pickerList.Entries.Should().HaveCount(2);
    pickerList.IsModified.Should().BeTrue();
  }

  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public void Should_UpdateIsModifiedStatus(bool status)
  {
    var pickerList = PickerList.Initialise(fixture.ListData);

    pickerList.SetModifiedStatus(status);
    pickerList.IsModified.Should().Be(status);
  }
}