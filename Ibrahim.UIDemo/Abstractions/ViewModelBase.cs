using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibrahim.UI.Abstractions
{
    public abstract class ViewModelBase
    {
        public Guid WindowId { get; set; } = new Guid();
    }
}
