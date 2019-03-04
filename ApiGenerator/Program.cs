using System;
using NConsole;
using NSwag.Commands;
using NSwagCommandProcessor = ApiGenerator.NSwagWrapper.Commands.NSwagCommandProcessor;

namespace ApiGenerator
{
    class Program
    {
        static int Main(string[] args)
        {
            OpenBankingToNmslSwaggerDocumentPreProcessor.ApplyToCommands();

            Console.Write("NSwag command line tool for .NET Core (with pre-processing!) " + RuntimeUtilities.CurrentRuntime + ", ");
            var processor = new NSwagCommandProcessor(new ConsoleHost());
            return processor.Process(args);
        }
    }
}
