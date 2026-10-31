using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lesson2
{
    internal static class HtmlElementExtensions
    {
            public static HashSet<HtmlElement> FindElements(this HtmlElement element, Selector selector)
            {
                var result = new HashSet<HtmlElement>();
                FindElementsRecursive(element, selector, result);
                return result;
            }

            private static void FindElementsRecursive(HtmlElement element, Selector selector, HashSet<HtmlElement> result)
            {
                var descendants = element.Descendants().Where(d => d != element);

                var filtered = descendants.Where(d => IsMatch(d, selector));

                if (selector.Child == null)
                {
                    foreach (var el in filtered)
                        result.Add(el);   
                    return;
                }

                foreach (var el in filtered)
                    FindElementsRecursive(el, selector.Child, result);
            }

        private static bool IsMatch(HtmlElement element,Selector selector)
        {
            if (selector.TagName != null && element.Name != selector.TagName)
                return false;

            if (selector.Id != null && element.Id != selector.Id)
                return false;

            foreach (string className in selector.Classes)
            {
                if (!element.Classes.Contains(className))
                    return false;
            }

            return true;
        }
    }
    
}
