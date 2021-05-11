using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.Common.API.CQRS
{
    public class CommandResult
    {
        public bool Success { get; set; }

        public List<string> Errors { get; set; } = new();
    }
}
