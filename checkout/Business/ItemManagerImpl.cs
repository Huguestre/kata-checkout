using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkout.Business
{
    public class ItemManagerImpl : IItemManager
    {
        private readonly ILogger _log;

        public ItemManagerImpl(ILogger logger)
        {
            _log = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
