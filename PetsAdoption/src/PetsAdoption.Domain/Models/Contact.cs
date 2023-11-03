using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetsAdoption.Domain.Models;
public class Contact : ModelBase
{
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public List<Pet> Pets { get; set; } = new List<Pet>();
}
