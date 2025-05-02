using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tixora.Models;
using Tixora.Repositories.Interfaces;
using Tixora.Services.Interfaces;


namespace Tixora.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<List<Ticket>> GetAll()
        {
            try
            {
                return await _ticketRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the tickets.", ex);
            }
        }

        public async Task<Ticket> GetById(int id)
        {
            try
            {
                return await _ticketRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the ticket.", ex);
            }
        }

        public async Task Add(Ticket ticket)
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

        public async Task Update(Ticket ticket)
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

        public async Task<bool> Delete(int id)
        {
            try
            {
                var result =await _ticketRepository.Delete(id);
                if (result)
                {
                    _ticketRepository.SaveAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the ticket.", ex);
            }
        }
       
        public IEnumerable<Ticket> GetTicketsByUser(string username)
        {
            try
            {
                return _ticketRepository.GetTicketsByUser(username);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching tickets for user {username}.", ex);
            }
        }

        public async Task<decimal> GetTicketPriceAsync(int ticketId)
        {
            Ticket ticket = await _ticketRepository.GetById(ticketId);
            return ticket.Price;
        }
    }
}



     
 