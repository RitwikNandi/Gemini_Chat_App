using MaIN.Core;
using MaIN.Core.Hub;
using MaIN.Domain.Configuration;
using Microsoft.AspNetCore.Components.Web;

var geminiApiKey = Environment.GetEnvironmentVariable("Gemini_API_Key");

if (string.IsNullOrEmpty(geminiApiKey))
{
    Console.WriteLine("Error: The GEMINI_API_KEY environment variable is not set.");
    return;
}

MaINBootstrapper.Initialize(configureSettings: (options) =>
{
    options.BackendType = BackendType.Gemini;
    options.GeminiKey = geminiApiKey;
});

var chat = AIHub.Chat()
    .WithModel("gemini-2.0-flash");

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("Welcome to Console_Chat... \nPowered by Gemini... \nPresented by Sleepy_Monkey 🐵... aka Ritwik Nandi... \nType 'exit' to end the conversation.\n");
Console.ResetColor();


while (true)
{
    Console.Write("_You:");
    Console.ForegroundColor = ConsoleColor.Yellow;
    var question = Console.ReadLine();
    Console.ResetColor();


    if (question.ToLower() == "exit")
    {
        Console.WriteLine("\nGoodbye");
        break;
    }

    int currentCursorLeft = Console.CursorLeft;
    int currentCursorTop = Console.CursorTop;
    Console.Write("Gemini Thinking...\n");
    Thread.Sleep(1000);

    var response = await chat
        .WithMessage(question)
        .CompleteAsync(interactive: false);

    Console.SetCursorPosition(currentCursorLeft, currentCursorTop);
    Console.Write(new string(' ', "Gemini Thinking...".Length));
    Console.SetCursorPosition(currentCursorLeft, currentCursorTop);



    Console.Write("_Gemini: ");
    Console.ForegroundColor = ConsoleColor.Green;

    foreach (char c in response.Message.Content)
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true);
            if (key.KeyChar == ' ')
            {
                break;
            }
        }

        Console.Write(c);
        Thread.Sleep(1);
    }
    Console.ResetColor();
    Console.WriteLine();
}
