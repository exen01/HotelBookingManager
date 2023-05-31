using HotelBookingManager.domain.dto;
using System.Collections.Generic;

namespace HotelBookingManager.dao.client
{
    internal interface IClientDao
    {
        void AddClient(Client client);
        void DeleteClientById(int id);
        void UpdateClient(Client client);
        List<Client> GetAllClients();
        Client? GetClientById(int id);
    }
}
