using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PetsAdoption.Domain.Models;

public class Pet : ModelBase
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Contact? Contact { get; set; }
    public List<Picture> Pictures { get; set; } = new List<Picture>();
}
