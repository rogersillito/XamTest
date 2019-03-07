using NJsonSchema;
using NSwag;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using NSwag.Collections;

namespace ApiGenerator.TypeNameGenerators
{
    public class ParentSchemaFallbackBasedTypeNameGenerator : DefaultTypeNameGenerator
    {
        //TODO: replace the virtual Generate method versions with the commented override to run that naming implementation

        // Favour NUMERICALLY-INCREMENTED naming when there are duplicates
        // BUT loses some top-level names as referred to in Open Banking Spec
        // (e.g. OBBCAData1 -> BCA: https://openbanking.atlassian.net/wiki/spaces/DZ/pages/937820288/Products+v3.1)
        public override string Generate(JsonSchema4 schema, string typeNameHint, IEnumerable<string> reservedTypeNames)
        //public virtual string GenerateV3(JsonSchema4 schema, string typeNameHint, IEnumerable<string> reservedTypeNames)
        {
            var providedTypeNameHint = typeNameHint;
            typeNameHint = GenerateNameFromFirstNamedParent(schema);
            var typeName = base.Generate(schema, typeNameHint ?? providedTypeNameHint, reservedTypeNames);

            Debug.WriteLine($"{typeName}\t{typeNameHint}\t{providedTypeNameHint}");
            return typeName;
        }

        // Favour PARENT-CONTEXTUALIZED, CONCATENATED naming when there are duplicates
        //public override string Generate(JsonSchema4 schema, string typeNameHint, IEnumerable<string> reservedTypeNames)
        public virtual string Generatev2(JsonSchema4 schema, string typeNameHint, IEnumerable<string> reservedTypeNames)
        {
            var typeName = base.Generate(schema, typeNameHint, reservedTypeNames);

            if (AnonymousTypeNamePattern.IsMatch(typeName))
            {
                typeNameHint = GenerateNameFromFirstNamedParent(schema);
                typeName = base.Generate(schema, typeNameHint, reservedTypeNames);
            }
            Debug.WriteLine($"{typeName}\t{typeNameHint}");
            return typeName;
        }

        // This gives us very long, SNAKE-CASED type names...
        //public override string Generate(JsonSchema4 schema, string typeNameHint, IEnumerable<string> reservedTypeNames)
        public virtual string GenerateV1(JsonSchema4 schema, string typeNameHint, IEnumerable<string> reservedTypeNames)
        {
            var typeName = base.Generate(schema, typeNameHint, reservedTypeNames);

             if (!AnonymousTypeNamePattern.IsMatch(typeName))
                return typeName;

             var nameFromParents = GenerateNameFromParents(schema);

             Debug.WriteLine(nameFromParents);

             return nameFromParents;
        }

        private bool _componentsSearchComplete;
        private IDictionary<string, JsonSchema4> _schemas;
        private string GetSchemaKey(dynamic schema)
        {
            OpenApiComponents TraverseParents(dynamic schema1)
            {
                Type type = schema1.GetType();
                if (typeof(JsonSchema4).IsAssignableFrom(type) && schema1.Parent != null)
                    return TraverseParents(schema1.Parent);
                if (type == typeof(OpenApiComponents) || schema1.Parent == null)
                    return schema1;
                return null;
            }
            string LookupKey() => _schemas
                    .Where(m => m.Value.Equals(schema))
                    .Select(m => Generate(schema, m.Key))
                    .FirstOrDefault();
            if (_componentsSearchComplete) return LookupKey();
            _schemas = TraverseParents(schema)?.Schemas ?? new Dictionary<string, JsonSchema4>();;
            _componentsSearchComplete = true;
            return LookupKey();
        }

        string GenerateNameFromFirstNamedParent(dynamic schema)
        {
            Type type = schema.GetType();

            if (type == typeof(JsonProperty))
                return Generate(schema, schema.Name);
            if (type == typeof(JsonSchema4) && schema.Title != null)
                return Generate(schema, GetSchemaKey(schema));
            if (type == typeof(OpenApiComponents) || schema.Parent == null)
                return null; // should not happen

            var nameFromParents = GenerateNameFromFirstNamedParent(schema.Parent);
            return nameFromParents;
        }

        // gives us very long, snake-cased types...
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
            generatedName = SnakeCaseCleanupPattern.Replace(generatedName, "_");
            return generatedName;
        }
        private static readonly Regex SnakeCaseCleanupPattern = new Regex("_+", RegexOptions.Compiled);

        private static readonly Regex AnonymousTypeNamePattern = new Regex("^Anonymous[0-9]+$", RegexOptions.Compiled);
    }
}
