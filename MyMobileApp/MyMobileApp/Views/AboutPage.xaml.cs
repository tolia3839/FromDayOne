using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
namespace MyMobileApp.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            
            // Получение текста из буфера обмена
            string clipboardText = Clipboard.GetTextAsync().Result;
            // clipboardText содержит текст из буфера обмена
            
            // Создаем элементы управления для ввода логина и пароля
            Entry loginEntry = new Entry
            {
                Placeholder = "Логин",
                Keyboard = Keyboard.Default
            };

            Entry passwordEntry = new Entry
            {
                Placeholder = "Пароль",
                Keyboard = Keyboard.Default,
                IsPassword = true // Это скроет введенный текст для безопасного ввода пароля
            };

            // Создает кнопку для входа
            Button loginButton = new Button
            {
                Text = "Войти"
            };

            // Создаем обработчик события для кнопки
            loginButton.Clicked += async (sender, e) =>
            {
                string login = loginEntry.Text;
                string password = passwordEntry.Text;

                //HttpClient для отправки запроса на сервер
                using (HttpClient client = new HttpClient())
                {
                    // URL сервера, на который вы хотите отправить данные
                    string serverUrl = "https://localhost:PORT/"; // Меняем на реальный URL

                    // Создать данные для отправки (например, в формате JSON)
                    var requestData = new { Login = login, Password = password };

                    // Преобразуем данные в JSON и отправляем на сервер
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(serverUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Обработка успешного ответа от сервера
                        Console.WriteLine("Да");
                    }
                    else
                    {
                        // Обработка ошибочного ответа от сервера
                        Console.WriteLine($"Нет");
                    }
                }
            };

            // Создаем макет страницы и добавляем в него элементы управления
            StackLayout layout = new StackLayout
            {
                Padding = new Thickness(20),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = { loginEntry, passwordEntry, loginButton }
            };

            Content = layout;
        }
    }
}