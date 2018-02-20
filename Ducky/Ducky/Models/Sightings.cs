using Newtonsoft.Json;
using System;

namespace Ducky
{
    public class Sigtings
    {
        public string Id { get; set; }
        public string Species { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}
