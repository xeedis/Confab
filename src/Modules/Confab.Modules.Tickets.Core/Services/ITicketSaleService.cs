using Confab.Modules.Tickets.Core.DTO;

namespace Confab.Modules.Tickets.Core.Services;

public interface ITicketSaleService
{
    Task AddAsync(TicketSaleDto dto);
    Task<IEnumerable<TicketSaleInfoDto>> GetAllAsync(Guid conferenceId);
    Task<TicketSaleInfoDto> GetCurrentAsync(Guid conferenceId);

}