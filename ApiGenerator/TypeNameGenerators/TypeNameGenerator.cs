using NJsonSchema;
using NSwag;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ApiGenerator.TypeNameGenerators
{
    //public class ParentPathFallbackBasedTypeNameGenerator : DefaultTypeNameGenerator
    public class TypeNameGenerator : MyDefaultTypeNameGenerator
    {
        public override string Generate(JsonSchema4 schema, string typeNameHint, IEnumerable<string> reservedTypeNames)
        {
            var typeName = base.Generate(schema, typeNameHint, reservedTypeNames);

            if (!AnonymousTypeNamePattern.IsMatch(typeName))
                return typeName;

            var nameFromParents = GenerateNameFromParents(schema);

            Debug.WriteLine(nameFromParents);

            return nameFromParents;
        }

        protected virtual string GenerateNameFromParents(dynamic schema)
        {
            string TraverseParents(dynamic schema1)
            {
                Type type = schema1.GetType();
                var name = "";

                if (type == typeof(JsonProperty))
                    name += Generate(schema1, schema1.Name);
                if (type == typeof(JsonSchema4) && schema1.Title != null)
                    name += Generate(schema1, null);

                if (type == typeof(OpenApiComponents) || schema1.Parent == null)
                    return name;

                var parentNames = TraverseParents(schema1.Parent) + "_" + name;
                return parentNames;
            }
            var generatedName = TraverseParents(schema).Trim('_');
            generatedName = SnailCaseCleanupPattern.Replace(generatedName, "_");
            return generatedName;
        }

        private static readonly Regex SnailCaseCleanupPattern = new Regex("_+", RegexOptions.Compiled);
        private static readonly Regex AnonymousTypeNamePattern = new Regex("^Anonymous[0-9]+$", RegexOptions.Compiled);
    }
}