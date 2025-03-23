using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Exceptions;
using Shouldly;

namespace Confab.Modules.Attendances.Tests.Unit.Entities;

public class AttendableEvent_Attend_Tests
{
    private Attendance Act(Participant participant) => _attendableEvent.Attend(participant);
    
    [Fact]
    public void given_no_slots_attend_should_fail()
    {
        // Arrange 
        var participant = GetParticipant();
        // Act
        var exception = Record.Exception(() => Act(participant));
        //Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<NoFreeSlotsException>();
        _attendableEvent.Slots.ShouldBeEmpty();
    }

    [Fact]
    public void given_existing_slot_with_same_participant_attend_should_fail()
    {
        var participant = GetParticipant();
        _attendableEvent.AddSlots(new Slot(Guid.NewGuid(), participant.Id));
        // Act
        var exception = Record.Exception(() => Act(participant));
        //Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<AlreadyParticipatingInEventException>();
    }

    [Fact]
    public void given_no_free_slots_attend_should_fail()
    {
        var participant1 = GetParticipant();
        var participant2 = GetParticipant();
        _attendableEvent.AddSlots(new Slot(Guid.NewGuid(), participant1.Id));
        
        var exception = Record.Exception(() => Act(participant2));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<NoFreeSlotsException>();
    }

    [Fact]
    public void given_free_slots_attend_should_success()
    {
        var participant = GetParticipant();
        var slot = new Slot(Guid.NewGuid());
        _attendableEvent.AddSlots(slot);
        
        var attendance = Act(participant);
        attendance.ShouldNotBeNull();
        
        attendance.ParticipantId.ShouldBe(participant.Id);
        attendance.SlotId.ShouldBe(slot.Id);
    }

    private readonly Guid _conferenceId = Guid.NewGuid();
    private readonly AttendableEvent _attendableEvent;
    
    //setup
    public AttendableEvent_Attend_Tests()
    {
        _attendableEvent = new AttendableEvent(Guid.NewGuid(), _conferenceId,
            new DateTime(2025, 3, 25, 9, 0, 0),
            new DateTime(2025, 3, 25, 10, 0, 0));
    }
    
    private Participant GetParticipant() => new(Guid.NewGuid(), _conferenceId, Guid.NewGuid());
}