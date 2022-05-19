using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopWebApp.Models;
using System;
using System.Text.Encodings.Web;

namespace OnlineShopWebApp.Helpers
{
    public static class PagingHelper
    {
        public static HtmlString PageLinks(this IHtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            var writer = new System.IO.StringWriter();
            int beginPage = pageInfo.PageNumber;
            while (beginPage % 10 != 1)
            {
                beginPage--;
            }
            int lastPage = pageInfo.TotalPages - beginPage < 9 ? pageInfo.TotalPages : beginPage + 9;
            for (int i = beginPage - 2; i <= lastPage + 2; i++)
            {
                var tag = new TagBuilder("a");
                if (i.Equals(beginPage - 2))
                {
                    tag.MergeAttribute("href", pageUrl(1));
                    if (Constants.Theme.Equals(Theme.Dark))
                    {
                        tag.AddCssClass("begin-last-page-border-box-glow-dark btn-primary-glow-dark-modal");
                    }
                    else
                    {
                        tag.AddCssClass("begin-last-page-border");
                    }
                    tag.InnerHtml.SetContent("<<");
                    tag.Attributes.Add("title", "К начальной странице");
                    if (pageInfo.PageNumber.Equals(1))
                    {
                        tag.AddCssClass("border-box-none");
                        tag.AddCssClass("disabled");
                    }
                }
                if (i.Equals(beginPage - 1))
                {
                    tag.MergeAttribute("href", pageUrl(pageInfo.PageNumber - 1));
                    tag.InnerHtml.SetContent("<");
                    tag.Attributes.Add("title", "К предыдущей странице");
                    if (pageInfo.PageNumber.Equals(1))
                    {
                        tag.AddCssClass("disabled");
                    }
                }
                if (i.Equals(lastPage + 1))
                {
                    tag.MergeAttribute("href", pageUrl(pageInfo.PageNumber + 1));
                    tag.InnerHtml.SetContent(">");
                    tag.Attributes.Add("title", "К следующей странице");
                    if (pageInfo.PageNumber.Equals(pageInfo.TotalPages))
                    {
                        tag.AddCssClass("disabled");
                    }
                }
                if (i.Equals(lastPage + 2))
                {
                    tag.MergeAttribute("href", pageUrl(pageInfo.TotalPages));
                    if (Constants.Theme.Equals(Theme.Dark))
                    {
                        tag.AddCssClass("begin-last-page-border-box-glow-dark btn-primary-glow-dark-modal");
                    }
                    else
                    {
                        tag.AddCssClass("begin-last-page-border");
                    }
                    tag.InnerHtml.SetContent(">>");
                    tag.Attributes.Add("title", "К последней странице");
                    if (pageInfo.PageNumber.Equals(pageInfo.TotalPages))
                    {
                        tag.AddCssClass("border-box-none");
                        tag.AddCssClass("disabled");
                    }
                }
                if (!i.Equals(beginPage - 2) && !i.Equals(beginPage - 1) && !i.Equals(lastPage + 1) && !i.Equals(lastPage + 2))
                {
                    tag.MergeAttribute("href", pageUrl(i));
                    tag.InnerHtml.SetContent(i.ToString());
                    if (i.Equals(pageInfo.PageNumber))
                    {
                        tag.AddCssClass("btn-primary");
                    }
                }
                tag.Attributes.Add("style", "font-size: x-small; margin: 2px");
                tag.AddCssClass("btn");
                if (Constants.Theme.Equals(Theme.Dark))
                {
                    tag.AddCssClass("glow-reference-pagination-dark");
                }
                tag.WriteTo(writer, HtmlEncoder.Default);
            }
            return new HtmlString(writer.ToString());
        }
    }
}