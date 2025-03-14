namespace Confab.Modules.Attendances.Application.Clients.DTO;

public class AgendaTrackDto
{
    public Guid Id { get; set; }
    public Guid ConferenceId { get; set; }
    public string Name { get; set; }
    public IEnumerable<AgendaSlotDto> Slots { get; set; }
}