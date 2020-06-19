using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Html.Forms;
using ScrapySharp.Network;
using System;
using System.Linq;

namespace WebScrapingMercadoLibre
{
    class MercadolibreSearch
    {
        WebPage homePage;
        Search search;
        ScrapingBrowser browser;
        public MercadolibreSearch(Search _search)
        {
            search = _search;
            browser = new ScrapingBrowser();
            homePage = browser.NavigateToPage(new Uri("https://www.mercadolibre.com.ar/"));
        }

        public WebPage getPage()
        {
            HtmlNode node = homePage.Html.CssSelect("form.nav-search").FirstOrDefault();
            PageWebForm form = new PageWebForm(node, browser);
            form["as_word"] = search.name;
            form.Method = HttpVerb.Get;
            WebPage page = form.Submit();
            return setCondition(page, (search.param.Equals("ORD_PRICE")));
        }
        private WebPage setCondition(WebPage page, bool orderByPrice)
        {
            HtmlNode a = page.Html.CssSelect("dl.filters__group.filters__ITEM_CONDITION a.qcat-truncate").FirstOrDefault();
            string url = a.Attributes["href"].Value;
            if(url.Contains("_ITEM"))
                url = url.Replace("_ITEM", "_DisplayType_LF_ITEM");
            if (orderByPrice)
                url = url.Replace("_ITEM", "_OrderId_PRICE_ITEM");

            return browser.NavigateToPage(new Uri(url));
        }

    }
}
