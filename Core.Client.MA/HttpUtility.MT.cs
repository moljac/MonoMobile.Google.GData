// 
// System.Web.HttpUtility
// http://github.com/mono/mono/tree/master/mcs/class/System.Web/System.Web.Util/
//
// Authors:
//   Patrik Torstensson (Patrik.Torstensson@labs2.com)
//   Wictor Wilén (decode/encode functions) (wictor@ibizkit.se)
//   Tim Coleman (tim@timcoleman.com)
//   Gonzalo Paniagua Javier (gonzalo@ximian.com)
//
// Copyright (C) 2005 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System;
//using System.Web;
using Google.GData.Client.Util;

#if NET_2_0
using System.Collections.Generic;
#endif

namespace Google.GData.Client
{

	// #if !MONOTOUCH
	// CAS - no InheritanceDemand here as the class is sealed
	// [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	// #endif	// CAS - no InheritanceDemand here as the class is sealed
	//[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed partial class HttpUtility
	{
	}
}