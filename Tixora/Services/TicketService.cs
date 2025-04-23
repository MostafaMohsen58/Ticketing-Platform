using System.Collections.Generic;
using System.Threading.Tasks;
using Tixora.Models;
using Tixora.Repositories.Interfaces;
using Tixora.Services.Interfaces;



namespace Tixora.Services
{
    public class TicketService : ITicketService
    {
        ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            try
            {
                return await _ticketRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the tickets.", ex);
            }
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            try
            {
                return await _ticketRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the ticket.", ex);
            }
        }

        public async Task AddAsync(Ticket ticket)
        {
            try
            {
                await _ticketRepository.AddAsync(ticket);
                await _ticketRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the ticket.", ex);
            }
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            try
            {
                await _ticketRepository.UpdateAsync(ticket);
                await _ticketRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the ticket.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var result = await _ticketRepository.DeleteAsync(id);
                if (result)
                {
                    await _ticketRepository.SaveAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the ticket.", ex);
            }
        }
    }
}
