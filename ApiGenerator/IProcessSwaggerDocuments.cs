using NSwag;

namespace ApiGenerator
{
    public interface IProcessSwaggerDocuments
    {
        void ApplyProcessing(SwaggerDocument document);
    }
}