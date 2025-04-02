using ClinicManagment.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ClinicManagment.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class TelegramController : ControllerBase
{
    private readonly ITelegramBotService _botService;

    public TelegramController(ITelegramBotService botService)
    {
        _botService = botService;
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> Post([FromBody] Update update)
    {
        await _botService.HandleUpdateAsync(update);
        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterBot([FromBody] string token)
    {
        var bot = await _botService.CreateBotAsync(token);
        return Ok(new { botInfo = bot.GetMeAsync().Result });
    }
}
