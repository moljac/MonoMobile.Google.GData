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

#define TRACE

using System;
using System.Xml;
using System.IO;
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.AccessControl {
    /// <summary>
    /// Entry API customization class for defining entries in an AccessControl feed.
    /// </summary>
    public class AclEntry : BaseAclEntry {
        /// <summary>
        /// Constructs a new AccessControlEntry instance with the appropriate category
        /// to indicate that it is an AccessControlEntry.
        /// </summary>
        public AclEntry()
            : base() {
            this.AddExtension(new AclRole());
        }

        /// <summary>
        /// property accessor for the AclRole
        /// </summary>
        public AclRole Role {
            get {
                return FindExtension(AclNameTable.XmlAclRoleElement,
                    AclNameTable.gAclNamespace) as AclRole;
            }
            set {
                ReplaceExtension(AclNameTable.XmlAclRoleElement,
                    AclNameTable.gAclNamespace, value);
            }
        }
    }
}

