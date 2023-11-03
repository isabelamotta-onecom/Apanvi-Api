using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetsAdoption.Domain.Models;
public class User : ModelBase
{
    public string Name { get; set; } = string.Empty;
    public Roles Role { get; set; }
}
