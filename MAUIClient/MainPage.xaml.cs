using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;

namespace MAUIClient
{
    public partial class MainPage : ContentPage
    {

    private HubConnection hubConnection;
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();

    public MainPage()
    {
        InitializeComponent();
        
        BindingContext = this;

        hubConnection = new HubConnectionBuilder()
        .WithUrl("https://localhost:7192/chathub")
        .WithAutomaticReconnect()
            .Build();

        hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            var messageObj = new Message(user,message);
            Messages.Add(messageObj);
        });

        ConnectToHub();
    }

    private async void ConnectToHub()
    {
        try
        {
            await hubConnection.StartAsync();
        }
        catch (Exception ex)
        {
                ;// Handle connection errors here
        }
    }

    private async void OnSendMessage(object sender, EventArgs e)
    {
        try
        {
            await hubConnection.InvokeAsync("SendMessage", UserEntry.Text, MessageEntry.Text);
            MessageEntry.Text = string.Empty;
        }
        catch (Exception ex)
        {
                ;// Handle message sending errors here
        }
    }

}
    //write a poco object to hold the message
    public class Message
    {
        public Message(string user, string message)
        {
            User=user;
            Text=message;
        }

        public string User { get; set; }
        public string Text { get; set; }
    }
}