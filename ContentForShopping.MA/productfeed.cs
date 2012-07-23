﻿/* Copyright (c) 2006 Google Inc.
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
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.ContentForShopping {
    /// <summary>
    /// Feed API customization class for defining Product feed.
    /// </summary>
    public class ProductFeed : AbstractFeed {

        /// <summary>
        /// StartToken from feed if present.
        /// </summary>
        public string StartToken {
            get {
                if (this.NextChunk != null) {
                    Uri nextUri = new Uri(this.NextChunk);

                    char[] delimiters = { '?', '&' };
                    string source = HttpUtility.UrlDecode(nextUri.Query);
                    TokenCollection tokens = new TokenCollection(source, delimiters);
                    foreach (String token in tokens) {
                        if (token.Length > 0) {
                            char[] otherDelimiters = { '=' };
                            String[] parameters = token.Split(otherDelimiters, 2);

                            if (parameters[0] == "start-token") {
                                return parameters[1];
                            }
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uriBase">The uri for the product feed.</param>
        /// <param name="iService">The ContentForShopping service.</param>
        public ProductFeed(Uri uriBase, IService iService)
            : base(uriBase, iService) {
            this.AddExtension(new BatchErrors());
        }

        /// <summary>
        /// returns a new entry for this feed
        /// </summary>
        /// <returns>AtomEntry</returns>
        public override AtomEntry CreateFeedEntry() {
            return new ProductEntry();
        }
    }
}
