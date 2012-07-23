/* Copyright (c) 2007 Google Inc.
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

using System;
using System.Text;
using System.Xml;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps.Migration
{
    /// <summary>
    /// Feed API customization class for defining the Google Apps
    /// Domain Migration API's mail item feed.
    /// </summary>
    public class MailItemFeed : AbstractFeed
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uriBase">The uri for the migration feed.</param>
        /// <param name="iService">The migration service.</param>
        public MailItemFeed(Uri uriBase, IService iService)
            : base(uriBase, iService)
        {
            GAppsExtensions.AddMailItemExtensions(this);
        }

        /// <summary>
        /// Overridden.  Creates a new <code>MailItemEntry</code>.
        /// </summary>
        /// <returns>the new <code>MailItemEntry</code>.</returns>
        public override AtomEntry CreateFeedEntry()
        {
            return new MailItemEntry();
        }

        /// <summary>
        /// Gets called after we handled the custom entry, to handle other
        /// potential parsing tasks
        /// </summary>
        /// <param name="e"></param>
        /// <param name="parser"></param>
        protected override void HandleExtensionElements(ExtensionElementEventArgs e, AtomFeedParser parser)
        {
            base.HandleExtensionElements(e, parser);
        }
    }
}
