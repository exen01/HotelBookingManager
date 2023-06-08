using HotelBookingManager.domain.dto;
using HotelBookingManager.domain.exception;
using HotelBookingManager.service.client;
using HotelBookingManager.util.validation;
using System.Windows;
using System.Windows.Media;

namespace HotelBookingManager.GUI
{
    /// <summary>
    /// Interaction logic for ClientProfileWindow.xaml
    /// </summary>
    public partial class ClientCreateWindow : Window
    {
        private Client client;
        private IClientService clientService;

        public ClientCreateWindow(bool isNewClient, Client client, IClientService clientService)
        {
            InitializeComponent();

            this.clientService = clientService;
            this.client = client;

            if (isNewClient)
            {
                saveButtonText.Text = "Добавить";
            }
            else
            {
                Title = string.Format("Редактирование клиента {0}", client.Name);
                InitializeWindow();
            }
        }

        private void InitializeWindow()
        {
            clientNameInput.Text = client.Name;
            clientAddressInput.Text = client.Address;
            clientEmailInput.Text = client.Email;
            clientPhoneInput.Text = client.PhoneNumber;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            client.Name = clientNameInput.Text;
            client.Address = clientAddressInput.Text;
            client.Email = clientEmailInput.Text;
            client.PhoneNumber = clientPhoneInput.Text;

            if (ValidateClient(client))
            {
                DialogResult = true;
                Close();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool ValidateClient(Client client)
        {
            bool result = false;

            try
            {
                result = ValidationUtils.ValidateClient(client);
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.UserMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                clientNameInput.BorderBrush = new SolidColorBrush(Color.FromArgb(0x89, 0x00, 0x00, 0x00));
                clientAddressInput.BorderBrush = new SolidColorBrush(Color.FromArgb(0x89, 0x00, 0x00, 0x00));

                if (ex.Code == domain.constatnt.ExceptionCode.WRONG_CLIENT_NAME)
                {
                    clientNameInput.BorderBrush = Brushes.Red;
                }
                else if (ex.Code == domain.constatnt.ExceptionCode.WRONG_CLIENT_ADDRESS)
                {
                    clientAddressInput.BorderBrush = Brushes.Red;
                }
            }

            return result;
        }
    }
}
