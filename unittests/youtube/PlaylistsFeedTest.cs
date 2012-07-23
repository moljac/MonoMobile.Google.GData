﻿using Google.GData.YouTube;
using NUnit.Framework;
using System;
using Google.GData.Client;

namespace Google.GData.Client.UnitTests.YouTube
{
    
    
    /// <summary>
    ///This is a test class for PlaylistsFeedTest and is intended
    ///to contain all PlaylistsFeedTest Unit Tests
    ///</summary>
    [TestFixture][Category("YouTube")]
    public class PlaylistsFeedTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CreateFeedEntry
        ///</summary>
        [Test]
        public void CreateFeedEntryTest()
        {
            Uri uriBase = null; // TODO: Initialize to an appropriate value
            IService iService = null; // TODO: Initialize to an appropriate value
            PlaylistsFeed target = new PlaylistsFeed(uriBase, iService); // TODO: Initialize to an appropriate value
            PlaylistsEntry entry = target.CreateFeedEntry() as PlaylistsEntry;
            Assert.IsNotNull(entry);
        }

        /// <summary>
        ///A test for PlaylistsFeed Constructor
        ///</summary>
        [Test]
        public void PlaylistsFeedConstructorTest()
        {
            Uri uriBase = null; // TODO: Initialize to an appropriate value
            IService iService = null; // TODO: Initialize to an appropriate value
            PlaylistsFeed target = new PlaylistsFeed(uriBase, iService);
            Assert.IsNotNull(target);
        }
    }
}
