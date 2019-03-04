using System;
using System.Collections.Generic;
using NSwag.Commands;
using System.Linq;
using System.Threading.Tasks;
using NSwag.Commands.SwaggerGeneration;
using NSwag.Commands.SwaggerGeneration.AspNetCore;
using NSwag.Commands.SwaggerGeneration.WebApi;

namespace ApiGenerator.NSwagWrapper.Commands
{
    public class NSwagDocumentWithPreProcessing: NSwagDocument
    {
        private static IProcessSwaggerDocuments _preProcessor;

        public static void SetDocumentPreProcessor(IProcessSwaggerDocuments processor) => _preProcessor = processor;
 
        /// <summary>Loads an existing NSwagDocument with environment variable expansions and variables.</summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="variables">The variables.</param>
        /// <returns>The document.</returns>
        public new static async Task<NSwagDocumentWithPreProcessing> LoadWithTransformationsAsync(string filePath, string variables)
        {
            return await LoadAsync<NSwagDocumentWithPreProcessing>(filePath, variables, true, new Dictionary<Type, Type>
            {
                { typeof(AspNetCoreToSwaggerCommand), typeof(AspNetCoreToSwaggerCommand) },
                { typeof(WebApiToSwaggerCommand), typeof(WebApiToSwaggerCommand) },
                { typeof(TypesToSwaggerCommand), typeof(TypesToSwaggerCommand) }
            });
        }

        public override async Task<SwaggerDocumentExecutionResult> ExecuteAsync()
        {
            var document = await GenerateSwaggerDocumentAsync();
            _preProcessor?.ApplyProcessing(document);
            foreach (var codeGenerator in CodeGenerators.Items.Where(c => !string.IsNullOrEmpty(c.OutputFilePath)))
            {
                codeGenerator.Input = document;
                await codeGenerator.RunAsync(null, null);
                codeGenerator.Input = null;
            }

            return new SwaggerDocumentExecutionResult(null, null, true);
        }
    }
}