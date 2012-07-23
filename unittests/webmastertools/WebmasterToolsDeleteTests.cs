/* Copyright (c) 2006 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

/* 
 * Created by Morten Christensen, http://blog.sitereactor.dk | http://twitter.com/sitereactor
 */

using System;
using Google.GData.Client;
using Google.GData.Client.LiveTests;
using Google.GData.Client.UnitTests;
using Google.GData.WebmasterTools;
using NUnit.Framework;

namespace unittests.webmastertools
{
    public class WebmasterToolsDeleteTests : BaseLiveTestClass
    {
        private Google.GData.Client.UnitTests.TestContext TestContext1;

        protected override void ReadConfigFile()
        {
            base.ReadConfigFile();

            if (unitTestConfiguration.Contains("userName"))
            {
                this.userName = (string)unitTestConfiguration["userName"];
                Tracing.TraceInfo("Read userName value: " + this.userName);
            }
            if (unitTestConfiguration.Contains("passWord"))
            {
                this.passWord = (string)unitTestConfiguration["passWord"];
                Tracing.TraceInfo("Read passWord value: " + this.passWord);
            }
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public Google.GData.Client.UnitTests.TestContext TestContext
        {
            get { return TestContext1; }
            set { TestContext1 = value; }
        }

        [Test]
        public void DeleteSite()
        {
            WebmasterToolsService service = new WebmasterToolsService(this.ApplicationName);
            service.Credentials = new GDataCredentials(this.userName, this.passWord);

            SitesQuery feedQuery = new SitesQuery();
            SitesFeed beforeFeed = service.Query(feedQuery);

            int before = beforeFeed.Entries.Count;

            Uri deleteUrl = SitesQuery.CreateCustomUri("http://www.example.com");
            service.Delete(deleteUrl);

            SitesFeed afterFeed = service.Query(feedQuery);
            int after = afterFeed.Entries.Count;

            Assert.Greater(before, after);
        }

        [Test]
        public void DeleteSitemap()
        {}

        [Test]
        public void DeleteMessage()
        { }
    }
}
