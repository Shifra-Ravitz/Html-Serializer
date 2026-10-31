using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lesson2
{
    internal class Selector
    {
        public string? TagName { get; set; }
        public string? Id { get; set; }
        public List<string> Classes { get; set; } = new();
        public Selector? Parent { get; set; }
        public Selector? Child { get; set; }
        public static Selector Parse(string query)
        {
            string[]parts = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Selector root = new Selector();
            Selector current = root;
            for (int i= 0;i < parts.Length;i++)
            {
                var matches = Regex.Matches(parts[i], @"([#.]?)([^#.]+)");

                foreach (Match match in matches)
                {
                    string separator = match.Groups[1].Value;
                    string value = match.Groups[2].Value;

                    if (separator == "#")
                        current.Id = value;

                    else if (separator == ".")
                        current.Classes.Add(value);

                    else if (HtmlHelper.Instance.HtmlTags.Contains(value))
                        current.TagName = value;

                }
                if (i < parts.Length - 1)
                {
                    Selector child = new Selector();
                    current.Child = child;
                    child.Parent = current;
                    current = child;
                }
            }
            return root;
        }
    }
}
