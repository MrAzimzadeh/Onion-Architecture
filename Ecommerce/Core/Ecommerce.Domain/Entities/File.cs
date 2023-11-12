using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Entities.Common;

namespace Ecommerce.Domain.Entities;

public class File : BaseEntity
{
    public string FileName { get; set; }
    public string Path { get; set; }
    public string Storage { get; set; }

    [NotMapped] public override DateTime UpdateDate { get; set; }
}