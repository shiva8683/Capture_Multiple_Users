using System;
using System.Web;
using System.Web.Mvc;

namespace Test_1_Capture_Multiple_Users.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString CreateBreadCrumb(this HtmlHelper htmlHelper, string pagetitle, params (string actionUrl, string title)[] breadcrumbItems)
        {
            TagBuilder containerDiv = new TagBuilder("div");
            containerDiv.AddCssClass("container");

            TagBuilder rowDiv = new TagBuilder("div");
            rowDiv.AddCssClass("row");


            TagBuilder colDiv = new TagBuilder("div");
            colDiv.AddCssClass("col");

            TagBuilder h1 = new TagBuilder("h1");
            h1.SetInnerText(pagetitle);
            colDiv.InnerHtml = h1.ToString(TagRenderMode.Normal);

            TagBuilder colautofloatrightalignselfendDiv = new TagBuilder("div");
            colautofloatrightalignselfendDiv.AddCssClass("col-auto float-right align-self-end");


            TagBuilder nav = new TagBuilder("nav");
            nav.MergeAttribute("aria-label", "breadcrumb");

            TagBuilder ol = new TagBuilder("ol");
            ol.AddCssClass("breadcrumb");


            TagBuilder HomeItem = new TagBuilder("li");
            HomeItem.AddCssClass("breadcrumb-item");

            TagBuilder HomeAnchorTag = new TagBuilder("a");
            HomeAnchorTag.MergeAttribute("href", "Users/Index");

            TagBuilder fontAwesomeHomeIcon = new TagBuilder("i");
            fontAwesomeHomeIcon.AddCssClass("fas fa-home");

            HomeAnchorTag.InnerHtml = fontAwesomeHomeIcon.ToString(TagRenderMode.Normal);
            HomeItem.InnerHtml = HomeAnchorTag.ToString(TagRenderMode.Normal);

            ol.InnerHtml = HomeItem.ToString(TagRenderMode.Normal);
            if (breadcrumbItems != null)
            {
                foreach ((string actionUrl, string title) in breadcrumbItems)
                {
                    TagBuilder itemCrumb = new TagBuilder("li");
                    itemCrumb.AddCssClass("breadcrumb-item");

                    TagBuilder itemUrl = new TagBuilder("a");
                    if (!string.IsNullOrWhiteSpace(actionUrl))
                        itemUrl.MergeAttribute("href", actionUrl);
                    itemUrl.SetInnerText(title);
                    itemCrumb.InnerHtml = itemUrl.ToString(TagRenderMode.Normal);

                    ol.InnerHtml += itemCrumb.ToString(TagRenderMode.Normal);
                }
            }


            nav.InnerHtml = ol.ToString(TagRenderMode.Normal);
            colautofloatrightalignselfendDiv.InnerHtml = nav.ToString(TagRenderMode.Normal);
            rowDiv.InnerHtml = colDiv.ToString(TagRenderMode.Normal) + colautofloatrightalignselfendDiv.ToString(TagRenderMode.Normal);
            containerDiv.InnerHtml = rowDiv.ToString(TagRenderMode.Normal);

            string completeRenderedString = containerDiv.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(completeRenderedString);
        }

        public static string AbsoluteRoutedPath(this HttpContext context, string relativePath)
        {
            string absolutePath = $"{context.Request.Url.Scheme}://{context.Request.Url.Authority}/{relativePath.TrimStart('/')}";
            return absolutePath;
        }


    }
}