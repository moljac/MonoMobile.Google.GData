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

using Google.GData.Client;

namespace Google.GData.WebmasterTools
{
    public class MessagesQuery : FeedQuery
    {
        /// <summary>
        ///  service url, http and https 
        /// </summary>
        public const string HttpsFeedUrl = "https://www.google.com/webmasters/tools/feeds/messages/";

        /// <summary>
        /// default constructor, does nothing 
        /// </summary>
        public MessagesQuery()
            : base(HttpsFeedUrl)
        {
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public MessagesQuery(string queryUri)
        : base(queryUri)
        {
        }
    }
}
