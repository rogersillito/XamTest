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
            //var url = "https://raw.githubusercontent.com/OpenBankingUK/read-write-api-specs/v3.1.0/dist/account-info-swagger.json";
            //var document = SwaggerDocument.FromUrlAsync(url).Result;

            OpenBankingToNmslSwaggerDocumentPreProcessor.ApplyToCommands();

            Console.Write("NSwag command line tool for .NET Core (with pre-processing!) " + RuntimeUtilities.CurrentRuntime + ", ");
            var processor = new NSwagCommandProcessor(new ConsoleHost());
            return processor.Process(args);
        }
    }
}
