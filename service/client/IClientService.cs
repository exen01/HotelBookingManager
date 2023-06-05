using HotelBookingManager.domain.dto;
using System.Collections.Generic;

namespace HotelBookingManager.service.client
{
    public interface IClientService
    {
        void AddClient(Client client);
        void UpdateClient(Client client);
        void DeleteClientById(int id);
        List<Client> GetAllClients();
        Client GetClientById(int id);
    }
}
