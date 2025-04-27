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

        public List<Ticket> GetAll()
        {
            try
            {
                return _ticketRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the tickets.", ex);
            }
        }

        public Ticket GetById(int id)
        {
            try
            {
                return _ticketRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the ticket.", ex);
            }
        }

        public void Add(Ticket ticket)
        {
            try
            {
                _ticketRepository.Add(ticket);
                _ticketRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the ticket.", ex);
            }
        }

        public void Update(Ticket ticket)
        {
            try
            {
                _ticketRepository.Update(ticket);
                _ticketRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the ticket.", ex);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var result = _ticketRepository.Delete(id);
                if (result)
                {
                    _ticketRepository.Save();
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


    }
}



     
 