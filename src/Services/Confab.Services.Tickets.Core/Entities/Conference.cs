namespace Confab.Services.Tickets.Core.Entities;

public class Conference
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? ParticipantsLimit { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public IList<TicketSale> TicketsSales { get; set; }
}