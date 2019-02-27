using System;
using System.Collections.Generic;
using System.Linq;
using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace ApiGenerator
{

    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://raw.githubusercontent.com/OpenBankingUK/read-write-api-specs/v3.1.0/dist/account-info-swagger.json";
            var document = SwaggerDocument.FromUrlAsync(url).Result;

            var requiredPathItemKeys = new List<string>
            {
                "/accounts",
                "/accounts/{AccountId}",
                "/accounts/{AccountId}/balances",
                "/accounts/{AccountId}/transactions",
                "/accounts/{AccountId}/statements",
                "/accounts/{AccountId}/statements/{StatementId}"
            };

            var pathItemKeys = document.Paths.Keys.OrderBy(k => k).ToList();
            for (var i = pathItemKeys.Count - 1; i >= 0; i--)
            {
                var pathKey = pathItemKeys[i];
                if (!requiredPathItemKeys.Contains(pathKey))
                {
                    document.Paths.Remove(pathKey);
                }
            }


        }
    }
}
