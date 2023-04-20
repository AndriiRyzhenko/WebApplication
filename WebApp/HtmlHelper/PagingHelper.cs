using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.HtmlHelper;
public static class PagingHelper
{
    public static IHtmlContent PageLinks(this IHtmlHelper<FoodListViewModel> html, PagingInfo pagingInfo, Func<int, string> pageUrl)
    {
        var result = new HtmlContentBuilder();

        for (int i = 1; i <= pagingInfo.TotalPages; i++)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(i));
            tag.InnerHtml.AppendHtml(i.ToString());
            if (i == pagingInfo.CurrentPage)
            {
                tag.AddCssClass("selected");
                tag.AddCssClass("btn-primary");
            }
            tag.AddCssClass("btn btn-default");
            result.AppendHtml(tag);
        }

        return result;
    }
}