<%@ Assembly Name="CarouselCommunity, Version=1.0.2.1, Culture=neutral, PublicKeyToken=3014057125c677d1" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
	Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
	Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VisualWebPart1UserControl.ascx.cs"
	Inherits="CarouselCommunity.VisualWebPart1.VisualWebPart1UserControl" %>
<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
<script type="text/javascript" src="http://vmsptpro1dba01/Community/Style%20Library/Carousel/jquery.bxslider.js"></script>
<link href="http://vmsptpro1dba01/Community/Style Library/Carousel/jquery.bxslider.css"
	rel="stylesheet" />
<asp:Repeater ID="Repeater1" runat="server">
	<HeaderTemplate>
		<ul class="bxslider">
	</HeaderTemplate>
	<ItemTemplate>
		<li id="Li1" class="poster-item orange" runat="server" visible='<%# Eval("isDiscount") %>' 
		onclick='<%# String.Format("javascript:carouselLink(\"{0}\")", Eval("linktoArticle").ToString()) %>'>
			<div class="poster-link-wrapper">				
				<span class="orange-poster-title"><%# Eval("Title") %></span><br />
				<span class="orange-poster-desc"><%# Eval("Article")%></span> 
				<span class="orange-poster-newsType">Discount</span>				
			</div>
		</li>
		<li id="Li2" class="poster-item purple" runat="server" visible='<%# Eval("isBenefit") %>' 
		onclick='<%# String.Format("javascript:carouselLink(\"{0}\")", Eval("linktoArticle").ToString()) %>'>
			<div class="poster-link-wrapper">				
				<span class="purple-poster-title"><%# Eval("Title") %></span><br />
				<span class="purple-poster-desc"><%# Eval("Article")%></span>
				<span class="purple-poster-newsType">Benefit</span> 
			</div>
		</li>
		<li id="Li3" class="poster-item green" runat="server" visible='<%# Eval("isAdvert") %>' 
		onclick='<%# String.Format("javascript:carouselLink(\"{0}\")", Eval("linktoArticle").ToString()) %>'>
			<div class="poster-link-wrapper">				
				<span class="green-poster-title"><%# Eval("Title") %></span><br />
				<span class="green-poster-desc"><%# Eval("Article")%></span>
				<span class="green-poster-newsType">Advert</span> 
			</div>
		</li>
		<li id="Li4" class="poster-item blue" runat="server" visible='<%# Eval("isEvent") %>' 
		onclick='<%# String.Format("javascript:carouselLink(\"{0}\")", Eval("linktoArticle").ToString()) %>'>
			<div class="poster-link-wrapper">
				<span class="blue-poster-title"><%# Eval("Title") %></span><br />
				<span class="blue-poster-desc"><%# Eval("Article")%></span>
				<span class="blue-poster-newsType">Event</span> 
			</div>
		</li>
		<li id="Li5" class="poster-item salmon" runat="server" visible='<%# Eval("isFundraising") %>' 
		onclick='<%# String.Format("javascript:carouselLink(\"{0}\")", Eval("linktoArticle").ToString()) %>'>
			<div class="poster-link-wrapper">				
				<span class="salmon-poster-title"><%# Eval("Title") %></span><br />
				<span class="salmon-poster-desc"><%# Eval("Article")%></span>
				<span class="salmon-poster-newsType">Fundraising</span> 
			</div>
		</li>
		<li id="Li6" class="poster-item brown" runat="server" visible='<%# Eval("isOther") %>' 
		onclick='<%# String.Format("javascript:carouselLink(\"{0}\")", Eval("linktoArticle").ToString()) %>'>
			<div class="poster-link-wrapper">
				<span class="brown-poster-title"><%# Eval("Title") %></span><br />
				<span class="brown-poster-desc"><%# Eval("Article")%></span>
				<span class="brown-poster-newsType"></span> 
			</div>
		</li>
	</ItemTemplate>
	<FooterTemplate>
		</ul>
	</FooterTemplate>
</asp:Repeater>
<script type="text/javascript">
	// redirect to article on carousel click
	function carouselLink(link) {
		window.location.href = link;
	}

	
	// initialise the carousel
	$(document).ready(function () {
		$('.bxslider').bxSlider();
	});
	$(window).resize(function () {
		$('.bxslider').bxSlider();
	});

</script>
