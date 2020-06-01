using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities;

namespace Axon.Business.Abstractions.Models
{
    public class TechnologyDTO : IdentifiedEntityDTO<Technology>
    {
        public string Name { get; set; }
        public string DocumentationURL { get; set; }
    }
}
