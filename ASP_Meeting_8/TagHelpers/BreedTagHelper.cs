using ASP_Meeting_8.Data.Entities;
using ASP_Meeting_8.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Drawing.Drawing2D;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ASP_Meeting_8.TagHelpers
{
    public class BreedTagHelper : TagHelper
    {
        public IEnumerable<string> Breeds { get; set; } = default!;
        public string CurrentBreed { get; set; } = default!;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "btn btn-group");
            output.Attributes.SetAttribute("style", "padding: 0");
            StringBuilder sb = new StringBuilder();
            if(CurrentBreed == string.Empty)
            {
                sb.Append($"<a href=\"List/\" class=\"btn btn-sm btn-outline-primary\">All</a>");
            }
            else
            {
                sb.Append($"<a href=\"List/\" class=\"btn btn-sm btn-outline-secondary\">All</a>");
            }
            foreach (var breed in Breeds)
            {
                if(breed == CurrentBreed)
                {
                    sb.Append($"<a href=\"List/{breed}\" class=\"btn btn-sm btn-outline-primary\">{breed}</a>");
                }
                else
                {
                    sb.Append($"<a href=\"List/{breed}\" class=\"btn btn-sm btn-outline-secondary\">{breed}</a>");
                }
               
            }
            output.Content.SetHtmlContent( sb.ToString() );
        }
    }

    public class BreedIndexTagHelper : TagHelper
    {
        //[ViewContext]
        //[HtmlAttributeNotBound]
        //public ViewContext ViewContext { get; set; } = default!;
        public List<string> BreedsData { get; set; } = default!;
        public string CurrentBreedId { get; set; } = default!;
        public string AspController { get; set; } = default!;
        public string AspAction { get; set; } = default!;

        //private readonly IUrlHelperFactory helperFactory;
        private readonly LinkGenerator linkGenerator;

        public BreedIndexTagHelper(IUrlHelperFactory helperFactory, LinkGenerator linkGenerator)
        {
            //this.helperFactory = helperFactory;
            this.linkGenerator = linkGenerator;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //IUrlHelper helper = helperFactory.GetUrlHelper(ViewContext);

            output.TagName = "div";
            output.Attributes.SetAttribute("class", "btn btn-group");
            output.Attributes.SetAttribute("style", "padding: 0");
            //string? href = helper.Action(AspAction, AspController, new { breedName = string.Empty });
            string? href = linkGenerator.GetPathByAction( AspAction, AspController, new { breedName = string.Empty });
            StringBuilder sb = new();
            if (!string.IsNullOrEmpty( CurrentBreedId))
            {
                sb.Append($"<a href=\"{href}\" class=\"btn btn-sm btn-outline-secondary\">All</a>");
            }
            else
            {
                sb.Append($"<a href=\"{href}\" class=\"btn btn-sm btn-outline-primary\">All</a>");
            }
            foreach (var item in BreedsData)
            {
                //href = helper.Action(AspAction, AspController, new { breedName = item });
                href = linkGenerator.GetPathByAction(AspAction, AspController, new { breedName = item });
                if (item == CurrentBreedId)
                {
                    sb.Append($"<a href=\"{href}\" class=\"btn btn-sm btn-outline-primary\">{item}</a>");
                }
                else
                {
                    sb.Append($"<a href=\"{href}\" class=\"btn btn-sm btn-outline-secondary\">{item}</a>");

                }
            }
            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}