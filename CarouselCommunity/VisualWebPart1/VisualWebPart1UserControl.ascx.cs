using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using Microsoft.SharePoint;

namespace CarouselCommunity.VisualWebPart1
{
	public partial class VisualWebPart1UserControl : UserControl
	{
		/// <summary>
		/// Map to existing list columns and create repeater required columns for view manipulation
		/// </summary>
		public class NewsItem
		{
			public string Title { get; set; }
			public int ID { get; set; }
			public string Article { get; set; }
			public string NewsType { get; set; }
			public string linkToArticle { get; set; }
			public bool isDiscount { get; set; }
			public bool isBenefit { get; set; }
			public bool isAdvert { get; set; }
			public bool isEvent { get; set; }
			public bool isFundraising { get; set; }
			public bool isOther { get; set; }
		}


		protected void Page_Load(object sender, EventArgs e)
		{

			List<NewsItem> newsItems = new List<NewsItem>();

			try
			{
				using (SPSite oSPsite = new SPSite("http://vmsptpro1dba01/Community/"))
				{
					using (SPWeb oSpWeb = oSPsite.OpenWeb())
					{
						SPList newsList = oSpWeb.Lists["News"]; // access list by name, in this case 'news'
						SPQuery q = new SPQuery();
						q.Query = "<OrderBy><FieldRef Name='Created' Ascending='FALSE' /></OrderBy>";

						SPListItemCollection items = newsList.GetItems(q);

						for (int i = 0; i < 6; i++)
						{
							foreach (SPListItem item in items)
							{

								var newsItem = new NewsItem();
								newsItem.Title = item.Title;
								newsItem.ID = item.ID;
								newsItem.NewsType = Convert.ToString(item["News_x0020_Type"]); // custom list column
								newsItem.linkToArticle = Convert.ToString(item["CarouselLink"]); // custom list column
								newsItem.Article = Convert.ToString(item["Article"]); // custom list column
								newsItem.Article = SetArticle(newsItem.Article);

								// set values for repeater formatting on news type
								newsItem.isDiscount = newsItem.NewsType == "Discount" ? true : false;
								newsItem.isBenefit = newsItem.NewsType == "Benefit" ? true : false;
								newsItem.isAdvert = newsItem.NewsType == "Advert" ? true : false;
								newsItem.isEvent = newsItem.NewsType == "Event" ? true : false;
								newsItem.isFundraising = newsItem.NewsType == "Fundraising" ? true : false;

								// if none of those are true then it must be 'other'
								if (!newsItem.isDiscount && !newsItem.isBenefit && !newsItem.isAdvert &&
									!newsItem.isEvent && !newsItem.isFundraising)
									newsItem.isOther = true;

								// add populated news item to list for repeater binding
								newsItems.Add(newsItem);
							}
						}
					}
				}

				// Only keep first 5 items
				if (newsItems.Count > 5)
				{
					newsItems.RemoveRange(5, newsItems.Count - 5);
				}

			}
			catch (Exception)
			{
				// fail silently for now				
			}

			PopulateRepeater(newsItems);
		}

		/// <summary>
		/// Moved binding to seperate method as I expect it will need further transforming pre binding soon.
		/// </summary>
		/// <param name="newsItems"></param>
		void PopulateRepeater(List<NewsItem> newsItems)
		{
			Repeater1.DataSource = newsItems;
			Repeater1.DataBind();
		}

		/// <summary>
		/// Checks the article length and manipulates it to return a 100 char limited string with elipses
		/// </summary>
		/// <param name="article"></param>
		/// <returns></returns>
		protected string SetArticle(string article)
		{
			// trim description
			if (article.Length > 100)
			{
				// get first 100 characters
				article = article.Substring(0, 100);

				// now check if last character is a space - just tidies it up a bit.
				if (article.EndsWith(" ") || article.EndsWith(","))
				{
					article = article.Substring(0, article.Length - 1);
				}

				// add ellipses
				article = string.Format("{0}{1}", article, "...");
			}

			return article;
		}


	}
}
