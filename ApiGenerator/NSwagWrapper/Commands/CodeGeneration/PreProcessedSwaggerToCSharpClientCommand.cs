//-----------------------------------------------------------------------
// <copyright file="SwaggerToCSharpClientCommand.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NConsole;
using NSwag.CodeGeneration;
using NSwag.CodeGeneration.CSharp;

#pragma warning disable 1591

namespace ApiGenerator.NSwagWrapper.Commands.CodeGeneration
{
    [Command(Name = "swagger2csclient", Description = "Generates CSharp client code from a Swagger specification (with pre-processing).")]
    public class PreProcessedSwaggerToCSharpClientCommand : NSwag.Commands.CodeGeneration.SwaggerToCSharpClientCommand
    {
        private static IProcessSwaggerDocuments _preProcessor;

        public static void SetDocumentPreProcessor(IProcessSwaggerDocuments processor) => _preProcessor = processor;

        public override async Task<object> RunAsync(CommandLineProcessor processor, IConsoleHost host)
        {
            var result = await RunAsync();
            foreach (var pair in result)
                await TryWriteFileOutputAsync(pair.Key, host, () => pair.Value).ConfigureAwait(false);
            return result;
        }

        public new async Task<Dictionary<string, string>> RunAsync()
        {
            return await Task.Run(async () =>
            {
                var document = await GetInputSwaggerDocument().ConfigureAwait(false);
                _preProcessor?.ApplyProcessing(document);
                var clientGenerator = new SwaggerToCSharpClientGenerator(document, Settings);

                if (GenerateContractsOutput)
                {
                    var result = new Dictionary<string, string>();
                    GenerateContracts(result, clientGenerator);
                    GenerateImplementation(result, clientGenerator);
                    return result;
                }
                else
                {
                    return new Dictionary<string, string>
                    {
                        { OutputFilePath ?? "Full", clientGenerator.GenerateFile(ClientGeneratorOutputType.Full) }
                    };
                }
            });
        }

        private void GenerateImplementation(Dictionary<string, string> result, SwaggerToCSharpClientGenerator clientGenerator)
        {
            var savedAdditionalNamespaceUsages = Settings.AdditionalNamespaceUsages?.ToArray();
            Settings.AdditionalNamespaceUsages =
                Settings.AdditionalNamespaceUsages?.Concat(new[] { ContractsNamespace }).ToArray() ?? new[] { ContractsNamespace };
            result[OutputFilePath ?? "Implementation"] = clientGenerator.GenerateFile(ClientGeneratorOutputType.Implementation);
            Settings.AdditionalNamespaceUsages = savedAdditionalNamespaceUsages;
        }

        private void GenerateContracts(Dictionary<string, string> result, SwaggerToCSharpClientGenerator clientGenerator)
        {
            var savedNamespace = Settings.CSharpGeneratorSettings.Namespace;
            Settings.CSharpGeneratorSettings.Namespace = ContractsNamespace;
            result[ContractsOutputFilePath ?? "Contracts"] = clientGenerator.GenerateFile(ClientGeneratorOutputType.Contracts);
            Settings.CSharpGeneratorSettings.Namespace = savedNamespace;
        }
    }
}
