using ClinicManagment.Data;
using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagment.Services;

public interface ITelegramBotService
{
    Task<TelegramBotClient> CreateBotAsync(string token);
    Task HandleUpdateAsync(Update update);
}

public class TelegramBotService : ITelegramBotService
{
    private readonly ClinicDbContext _context;
    private TelegramBotClient _botClient;

    public TelegramBotService(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<TelegramBotClient> CreateBotAsync(string token)
    {
        _botClient = new TelegramBotClient(token);
        await _botClient.SetWebhookAsync($"{GetNgrokUrl()}/api/telegram/webhook");
        return _botClient;
    }

    public async Task HandleUpdateAsync(Update update)
    {
        if (update.Message is null)
            return;

        // Handle commands
        if (update.Message.Text.StartsWith("/"))
        {
            await HandleCommandAsync(update.Message);
            return;
        }

        // Handle regular messages
        await HandleMessageAsync(update.Message);
    }

    private async Task HandleCommandAsync(Message message)
    {
        var command = message.Text.Split(' ')[0];
        switch (command)
        {
            case "/start":
                await SendWelcomeMessage(message.Chat.Id);
                break;
            case "/book":
                await HandleBookingRequest(message);
                break;
            case "/cancel":
                await HandleCancellation(message);
                break;
            case "/schedule":
                await ShowAvailableSlots(message.Chat.Id);
                break;
        }
    }

    private async Task HandleBookingRequest(Message message)
    {
        // Implement booking logic
    }

    private async Task ShowAvailableSlots(long chatId)
    {
        // Implement available slots display
    }

    private async Task SendWelcomeMessage(long chatId)
    {
        var welcomeMessage = 
            "Welcome to our Clinic Bot!\n\n" +
            "Available commands:\n" +
            "/book - Book an appointment\n" +
            "/schedule - View available slots\n" +
            "/cancel - Cancel your appointment";

        await _botClient.SendTextMessageAsync(chatId, welcomeMessage);
    }

    private async Task HandleMessageAsync(Message message)
    {
        var response = "Please use one of the available commands:\n" +
                      "/book - Book an appointment\n" +
                      "/schedule - View available slots\n" +
                      "/cancel - Cancel your appointment";

        await _botClient.SendTextMessageAsync(message.Chat.Id, response);
    }

    private async Task HandleCancellation(Message message)
    {
        // Extract appointment ID if provided
        var parts = message.Text.Split(' ');
        if (parts.Length < 2)
        {
            await _botClient.SendTextMessageAsync(
                message.Chat.Id, 
                "Please provide your appointment ID: /cancel <appointment_id>"
            );
            return;
        }

        if (int.TryParse(parts[1], out int appointmentId))
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
                await _botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Your appointment has been cancelled successfully."
                );
            }
            else 
            {
                await _botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Appointment not found."
                );
            }
        }
    }

    private string GetNgrokUrl()
    {
        // Implement ngrok URL retrieval
        return "https://your-ngrok-url.ngrok.io";
    }

    // ... Other implementation methods
}
