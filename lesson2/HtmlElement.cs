using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson2
{
    internal class HtmlElement
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public List<HtmlAttribute> Attributes { get; set; } = new();
        public List<string> Classes { get; set; } = new();
        public string InnerHtml { get; set; }
        public HtmlElement?  Parent { get; set; }
        public List<HtmlElement> Children { get; set; } = new();
        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                HtmlElement current = queue.Dequeue();

                yield return current;

                foreach (HtmlElement child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement? current = this.Parent;

            while (current != null)
            {
                yield return current;
                current = current.Parent;
            }
        }


    }
}
