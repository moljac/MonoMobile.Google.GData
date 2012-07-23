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
#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.Net;
using System.Collections;
using System.IO;
using Microsoft.Win32;

#endregion

// <summary>Contains the MediaSources currently implemented</summary>
namespace Google.GData.Client
{
	/// <summary>
	/// placeholder for a media object to be uploaded
	/// the base class only defines some primitives like content type
	/// </summary>
	public abstract partial class MediaSource
	{
		/// <summary>
		/// tries to get a contenttype for a filename by using the classesRoot
		/// in the registry. Will FAIL if that filetype is not registered with a
		/// contenttype
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns>NULL or the registered contenttype</returns>
		public static string GetContentTypeForFileName(string fileName)
		{
			// TODO: registr
			// string ext = System.IO.Path.GetExtension(fileName).ToLower();
			// 
			// using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(ext)) {
			//     if (registryKey != null && registryKey.GetValue("Content Type") != null) {
			//         return registryKey.GetValue("Content Type").ToString();
			//     }
			// }
			return null;
		}
	}
}