// TODO: workaround for https://github.com/RSuter/NSwag/issues/1386
// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Mvc
{
    public class RoutePrefixAttribute : RouteAttribute
    {
        public RoutePrefixAttribute(string template)
            : base(template)
        {
        }
    }
}