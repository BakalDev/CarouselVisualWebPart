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
		/// Configurables
		/// </summary>
		private static string listUrl = "http://vmsptpro1dba01/Community/";
		private static string listName = "News";
		private static int itemsLimit = 5;
		private static int articleLengthLimit = 100; // characters
		
		/// <summary>
		/// Map properties to existing list columns and create repeater required columns for view manipulation
		/// </summary>
		public class NewsItem
		{
			// Main Properties of news item
			public string Title { get; set; }
			public int ID { get; set; }
			public string Article { get; set; }
			public string NewsType { get; set; }
			public string linkToArticle { get; set; }

			// News Types - Linked to how repeater calls css classes
			public bool isDiscount { get; set; }
			public bool isBenefit { get; set; }
			public bool isAdvert { get; set; }
			public bool isEvent { get; set; }
			public bool isFundraising { get; set; }
			public bool isOther { get; set; }
		}

		/// <summary>
		/// Calls ReturnListItems method to populate the repeater control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			PopulateRepeater(ReturnListItems());
		}

		/// <summary>
		/// Main method of project, modify SP query to change ordering
		/// </summary>
		/// <returns>List of NewsItem class</returns>
		protected List<NewsItem> ReturnListItems()
		{
			List<NewsItem> newsItems = new List<NewsItem>();

			try
			{
				using (SPSite oSPsite = new SPSite(listUrl))
				{
					using (SPWeb oSpWeb = oSPsite.OpenWeb())
					{
						

						SPList newsList = oSpWeb.Lists[listName]; // access list by name, in this case 'news'
						SPQuery q = new SPQuery();
						q.Query = "<OrderBy><FieldRef Name='Created' Ascending='FALSE' /></OrderBy>";

						SPListItemCollection items = newsList.GetItems(q);

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
			catch (Exception)
			{
				// fail silently for now				
			}

			// Only return specified amount of items.
			if (newsItems.Count > itemsLimit)
			{
				newsItems.RemoveRange(itemsLimit, newsItems.Count - itemsLimit);
			}

			return newsItems;
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
			if (article.Length > articleLengthLimit)
			{
				// get first 100 characters
				article = article.Substring(0, articleLengthLimit);

				// now check if last character is a space - just tidies it up a bit.
				if (article.EndsWith(" "))
					article = article.Substring(0, article.Length - 1);
				if (article.EndsWith(","))
					article = article.Substring(0, article.Length - 1);

				// add ellipses
				article = string.Format("{0}{1}", article, "...");
			}

			return article;
		}


	}
}
