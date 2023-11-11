using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetsAdoption.Domain.Models;
public class Picture : ModelBase
{
    public Pet Pet { get; set; } = new Pet();
    public byte[] File { get; set; } = Array.Empty<byte>();
    public string FileName { get; set; } = string.Empty;
    public bool IsCover { get; set; }
}
