



using lesson2;
using System;
using System.Text.RegularExpressions;

var html = await Load("https://hebrewbooks.org/beis");
Console.WriteLine(html);
var cleanHtml = html;
var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s=> !string.IsNullOrWhiteSpace(s));
HtmlElement root = new HtmlElement{Name = "root"};
HtmlElement currentElement = root;
HtmlElement temp;
HtmlAttribute att;

Console.WriteLine();
string[] parts;
string tagName;
foreach (var line in htmlLines)
{
    if (line.StartsWith("/"))
    {
        if (currentElement.Parent != null)
            currentElement = currentElement.Parent;
    }
    else
    {
        parts = Regex.Split(line, @"\s+", RegexOptions.None);
        tagName = parts[0];
        if (HtmlHelper.Instance.HtmlTags.Contains(tagName))
        {
            temp = new HtmlElement { Name = tagName };
            temp.Parent = currentElement;
            currentElement.Children.Add(temp);
            bool isSelfClosing = line.TrimEnd().EndsWith("/");
            var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(line);
            foreach (Match attribute in attributes)
            {
                string name = attribute.Groups[1].Value;
                string value = attribute.Groups[2].Value;
                att = new HtmlAttribute
                {
                    Name = name,
                    Value = value
                };
                temp.Attributes.Add(att);
                if (name == "id")
                    temp.Id = value;

                if (name == "class")
                    temp.Classes = value.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            if (!HtmlHelper.Instance.HtmlVoidTags.Contains(tagName) && !isSelfClosing)
                currentElement = temp;
        }
        else
        {
            currentElement.InnerHtml += line;
        }
    }
 


}
async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
void Test(string query)
{
    var selector = Selector.Parse(query);
    var results = root.FindElements(selector);

    Console.WriteLine(query + "  =>  " + results.Count);

    foreach (var el in results)
    {
        Console.WriteLine("    " + el.Name + " | id: " + el.Id + " | class: " + string.Join(" ", el.Classes) + " | text: " + el.InnerHtml);
    }
}
Test("#cf-wrapper .cf-section");



