using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SimpleCaptcha.Helpers
{
    public class CaptchaTagHelper : TagHelper
    {
        public string ReloadIconUrl { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.TagMode = TagMode.StartTagOnly;
            output.Attributes.SetAttribute("src", "/?ImageCaptcha");
            output.Attributes.SetAttribute("id", "simpleCaptcha");
            output.Attributes.SetAttribute("class", "simple-captcha");
            output.PostElement.SetHtmlContent($"<img class=\"my-img\" style=\"padding: 5px; cursor: pointer; \" alt=\"Изменить капчу\" src={ReloadIconUrl} />");
            output.PostElement.AppendHtml("<script>$('.my-img').click(function () {$('.simple-captcha').attr('src', '/?ImageCaptcha');})</script>");
        }
    }
}
