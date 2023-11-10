
namespace Ecommerce.Application.Abstractions.Storeg;

public interface IStorageService : IStorage
{
    public string StorageName { get;  }
}