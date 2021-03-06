﻿using EPSILab.SolarSystem.Jupiter.ReadersService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace EPSILab.SolarSystem.Jupiter
{
    /// <summary>
    /// Default page
    /// </summary>
    public partial class Default : Page
    {
        #region Attributes

        /// <summary>
        /// Webservice proxy for ads
        /// </summary>
        private readonly ISlideReader _webserviceSlides = new SlideReaderClient();

        /// <summary>
        /// Webservice proxy for news
        /// </summary>
        private readonly INewsReader _webserviceNews = new NewsReaderClient();

        /// <summary>
        /// Used for news carousel
        /// </summary>
        private int _idNews = 1;

        #endregion

        #region Properties

        /// <summary>
        /// Used for news carousel
        /// </summary>
        protected int IdNews
        {
            get { return _idNews; }
            set { _idNews = value; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Raised when the page is loaded
        /// </summary>
        /// <param name="sender">Element which raised the event.</param>
        /// <param name="e">Event arguments</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get ads
            IEnumerable<Slide> slides = _webserviceSlides.GetSlides();

            if (!slides.Any())
                repeater_TemporaryNews.Visible = false;
            else
            {
                repeater_TemporaryNews.DataSource = slides;
                repeater_TemporaryNews.DataBind();
            }

            // Get 20 last news
            IEnumerable<News> news = _webserviceNews.GetListNewsLimited(0, 20);

            repeaterNews.DataSource = news;
            repeaterNews.DataBind();
        }

        #endregion
    }
}