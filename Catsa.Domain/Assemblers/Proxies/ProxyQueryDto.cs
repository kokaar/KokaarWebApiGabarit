﻿namespace Catsa.Domain.Assemblers.Proxies
{
    public class ProxyQueryDto : BaseDto<int>
    {
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
