using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Diagnostics;
using System.Threading.Tasks;
using Markdig;

using ArticleManager.Utils;

namespace ArticleManager.TagHelpers
{
    [HtmlTargetElement("markdown")]
    public class MarkdownTagHelper : TagHelper
    {
        [HtmlAttributeName("normalize-whitespace")]
        public bool NormalizeWhitespace { get; set; } = true;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            TagHelperContent tagContent = await output.GetChildContentAsync(NullHtmlEncoder.Default);
            string content = tagContent.GetContent(NullHtmlEncoder.Default);
            // var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();


            if (string.IsNullOrEmpty(content)) return;
            char[] charsToTrim = { '\r', '\n' };
            content = content.Trim(charsToTrim);

            string markdown = MarkdownUtils.NormalizeWhiteSpaceText(content);
            string html = Markdig.Markdown.ToHtml(markdown);
            output.Content.SetHtmlContent(html);
            output.TagName = null;
        }
    }
}