using NJsonSchema;
using NSwag;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApiGenerator.TypeNameGenerators
{
    //public class ParentPathFallbackBasedTypeNameGenerator : DefaultTypeNameGenerator
    public class TypeNameGenerator : ITypeNameGenerator
    {
        // TODO: Expose as options to UI and cmd line?

        /// <summary>Gets or sets the reserved names.</summary>
        public IEnumerable<string> ReservedTypeNames { get; set; } = new List<string> { "object" };

        /// <summary>Gets the name mappings.</summary>
        public IDictionary<string, string> TypeNameMappings { get; } = new Dictionary<string, string>();

        /// <summary>Generates the type name for the given schema respecting the reserved type names.</summary>
        /// <param name="schema">The schema.</param>
        /// <param name="typeNameHint">The type name hint.</param>
        /// <param name="reservedTypeNames">The reserved type names.</param>
        /// <returns>The type name.</returns>
        private string Base_Generate(JsonSchema4 schema, string typeNameHint, IEnumerable<string> reservedTypeNames)
        {
            if (string.IsNullOrEmpty(typeNameHint) && !string.IsNullOrEmpty(schema.DocumentPath))
                typeNameHint = schema.DocumentPath.Replace("\\", "/").Split('/').Last();

            typeNameHint = (typeNameHint ?? "")
                .Replace("[", " Of ")
                .Replace("]", " ")
                .Replace("<", " Of ")
                .Replace(">", " ")
                .Replace(",", " And ")
                .Replace("  ", " ");

            var parts = typeNameHint.Split(' ');
            typeNameHint = string.Join(string.Empty, parts.Select(p => Generate(schema, p)));

            var typeName = Generate(schema, typeNameHint);
            if (string.IsNullOrEmpty(typeName) || reservedTypeNames.Contains(typeName))
                typeName = GenerateAnonymousTypeName(typeNameHint, reservedTypeNames);

            return typeName;
        }

        /// <summary>Generates the type name for the given schema.</summary>
        /// <param name="schema">The schema.</param>
        /// <param name="typeNameHint">The type name hint.</param>
        /// <returns>The type name.</returns>
        protected virtual string Generate(JsonSchema4 schema, string typeNameHint)
        {
            if (string.IsNullOrEmpty(typeNameHint) &&
                string.IsNullOrEmpty(schema.Title) == false &&
                Regex.IsMatch(schema.Title, "^[a-zA-Z0-9_]*$"))
            {
                typeNameHint = schema.Title;
            }

            var lastSegment = typeNameHint?.Split('.').Last();
            return ConversionUtilities.ConvertToUpperCamelCase(lastSegment ?? "Anonymous", true);
        }

        private string GenerateAnonymousTypeName(string typeNameHint, IEnumerable<string> reservedTypeNames)
        {
            if (!string.IsNullOrEmpty(typeNameHint))
            {
                if (TypeNameMappings.ContainsKey(typeNameHint))
                    typeNameHint = TypeNameMappings[typeNameHint];

                typeNameHint = typeNameHint.Split('.').Last();

                if (!reservedTypeNames.Contains(typeNameHint) && !ReservedTypeNames.Contains(typeNameHint))
                    return typeNameHint;

                var count = 1;
                do
                {
                    count++;
                } while (reservedTypeNames.Contains(typeNameHint + count));

                return typeNameHint + count;
            }

            return GenerateAnonymousTypeName("Anonymous", reservedTypeNames);
        }


        // Favour NUMERICALLY-INCREMENTED naming when there are duplicates
        public virtual string GenerateV2(JsonSchema4 schema, string typeNameHint, IEnumerable<string> reservedTypeNames)
        {
            var providedTypeNameHint = typeNameHint;
            typeNameHint = GenerateNameFromFirstNamedParent(schema);
            var typeName = Base_Generate(schema, typeNameHint ?? providedTypeNameHint, reservedTypeNames);

            Debug.WriteLine($"{typeName}\t{typeNameHint}\t{providedTypeNameHint}");
            return typeName;
        }

        // Favour PARENT-CONTEXTUALIZED, CONCATENATED naming when there are duplicates
        public virtual string Generate(JsonSchema4 schema, string typeNameHint, IEnumerable<string> reservedTypeNames)
        {
            var typeName = Base_Generate(schema, typeNameHint, reservedTypeNames);

            if (AnonymousTypeNamePattern.IsMatch(typeName))
            {
                typeNameHint = GenerateNameFromFirstNamedParent(schema);
                typeName = Base_Generate(schema, typeNameHint, reservedTypeNames);
            }
            Debug.WriteLine($"{typeName}\t{typeNameHint}");
            return typeName;
        }

        string GenerateNameFromFirstNamedParent(dynamic schema)
        {
            Type type = schema.GetType();

            if (type == typeof(JsonProperty))
                return Generate(schema, schema.Name);
            if (type == typeof(JsonSchema4) && schema.Title != null)
                return Generate(schema, null);
            if (type == typeof(OpenApiComponents) || schema.Parent == null)
                return null; // Recursive stop

            var nameFromParents = GenerateNameFromFirstNamedParent(schema.Parent);
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