using MaIN.Core;
using MaIN.Core.Hub;
using MaIN.Domain.Configuration;

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

Console.WriteLine("Welcome to Console_Chat... \nPowered by Gemini... \nType 'exit' to end the conversation.\n");

while (true)
{
    Console.Write("_You:");
    var question = Console.ReadLine();

    if(question.ToLower() == "exit")
    {
        Console.WriteLine("\nGoodbye");
        break;
    }

    Console.WriteLine("Gemini Thinking...");

    var response = await chat
        .WithMessage(question)
        .CompleteAsync(interactive: true);

    //Console.WriteLine("_Gemini: ");
    //Console.WriteLine(response.Message.Content);
    ////Console.WriteLine();
}
