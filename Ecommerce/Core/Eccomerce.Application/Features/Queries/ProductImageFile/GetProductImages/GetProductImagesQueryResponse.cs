using System.Diagnostics;

namespace Ecomerce.Application.Features.Queries.ProductImageFile.GetProductImages;

public class GetProductImagesQueryResponse
{
    public string Path { get; set; }
    public string FileName { get; set; }
    public string Storage { get; set; }
    public string Id { get; set; }
}