using HotelBookingManager.dao.client;
using HotelBookingManager.domain.dto;
using System.Collections.Generic;

namespace HotelBookingManager.service.client
{
    class ClientServiceImpl : IClientService
    {
        private readonly IClientDao clientDao;

        public ClientServiceImpl(IClientDao clientDao)
        {
            this.clientDao = clientDao;
        }

        public void AddClient(Client client)
        {
            clientDao.AddClient(client);
        }

        public void DeleteClientById(int id)
        {
            clientDao.DeleteClientById(id);
        }

        public List<Client> GetAllClients()
        {
            return clientDao.GetAllClients();
        }

        public Client GetClientById(int id)
        {
            var result = clientDao.GetClientById(id);
            if (result == null)
            {
                result = new Client();
            }
            return result;
        }

        public void UpdateClient(Client client)
        {
            clientDao.UpdateClient(client);
        }
    }
}
