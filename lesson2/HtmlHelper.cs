using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace lesson2
{
    internal class HtmlHelper
    {
        private readonly static HtmlHelper _Instance = new HtmlHelper();
        public static HtmlHelper Instance => _Instance;
        public string[] HtmlTags { get; }
        public string[] HtmlVoidTags { get; }
        private HtmlHelper()
        {
            string jsonTags = File.ReadAllText("seed/HtmlTags.json");
            HtmlTags = JsonSerializer.Deserialize<string[]>(jsonTags) ?? Array.Empty<string>();
            string jsonVoidTags = File.ReadAllText("seed/HtmlVoidTags.json");
            HtmlVoidTags = JsonSerializer.Deserialize<string[]>(jsonVoidTags)
                           ?? Array.Empty<string>();
        }
    }
}
