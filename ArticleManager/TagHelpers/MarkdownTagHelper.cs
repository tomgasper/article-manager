using Microsoft.AspNetCore.Razor.TagHelpers;
using ArticleManager.Utils;
using Ganss.Xss;

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

            if (string.IsNullOrEmpty(content)) return;

            char[] charsToTrim = { '\r', '\n' };
            content = content.Trim(charsToTrim);

            string markdown = NormalizeWhitespace ? MarkdownUtils.NormalizeWhiteSpaceText(content) : content;
            string html = Markdig.Markdown.ToHtml(markdown);

            // Sanitize for safety
            var sanitizer = new HtmlSanitizer();
            var sanitized = sanitizer.Sanitize(html);

            output.Content.SetHtmlContent(sanitized);
            output.TagName = null;
        }
    }
}