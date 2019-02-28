//-----------------------------------------------------------------------
// <copyright file="SwaggerToCSharpControllerCommand.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NConsole;
using NSwag.CodeGeneration.CSharp;

#pragma warning disable 1591

namespace ApiGenerator.NSwagWrapper.Commands.CodeGeneration
{
    [Command(Name = "swagger2cscontroller", Description = "Generates CSharp Web API controller code from a Swagger specification (with pre-processing).")]
    public class PreProcessedSwaggerToCSharpControllerCommand : NSwag.Commands.CodeGeneration.SwaggerToCSharpControllerCommand
    {
        private static IProcessSwaggerDocuments _preProcessor;

        public static void SetDocumentPreProcessor(IProcessSwaggerDocuments processor) => _preProcessor = processor;

        public override async Task<object> RunAsync(CommandLineProcessor processor, IConsoleHost host)
        {
            var code = await RunAsync();
            await TryWriteFileOutputAsync(host, () => code).ConfigureAwait(false);
            return code;
        }

        public new async Task<string> RunAsync()
        {
            return await Task.Run(async () =>
            {
                var document = await GetInputSwaggerDocument().ConfigureAwait(false);
                _preProcessor?.ApplyProcessing(document);
                var clientGenerator = new SwaggerToCSharpControllerGenerator(document, Settings);
                return clientGenerator.GenerateFile();
            });
        }
    }
}
