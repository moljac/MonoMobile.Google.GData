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
using Google.GData.Client;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps.GoogleMailSettings
{
    /// <summary>
    /// Base service for accessing Google Mail Settings item feeds from the
    /// Google Apps Google Mail Settings API.
    /// </summary>
	public class GoogleMailSettingsService : AppsPropertyService
    {
        /// <summary>
        /// URL suffixes for the Google Mail Settings tasks
        /// </summary>
        public const string labelFeedUriSuffix = "/label";
        public const string filterFeedUriSuffix = "/filter";
        public const string sendasFeedUriSuffix = "/sendas";
        public const string forwardingFeedUriSuffix = "/forwarding";
        public const string webclipFeedUriSuffix = "/webclip";
        public const string popFeedUriSuffix = "/pop";
        public const string imapFeedUriSuffix = "/imap";
        public const string vacationFeedUriSuffix = "/vacation";
        public const string signatureFeedUriSuffix = "/signature";
        public const string languageFeedUriSuffix = "/language";
        public const string generalFeedUriSuffix = "/general";
        public const string delegationFeedUriSuffix = "/delegation";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="domain">The hosted domain in which the Google Mail Settings are
        /// being set up</param>
        /// <param name="applicationName">The name of the client application 
        /// using this service.</param>
        public GoogleMailSettingsService(string domain, string applicationName)
            : base(AppsNameTable.GAppsService, applicationName)
        {
            this.domain = domain;
            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewGoogleMailSettingsItemEntry);
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed);
            // You can set factory.methodOverride = true if you are behind a 
            // proxy that filters out HTTP methods such as PUT and DELETE.
        }

        private string domain;

        /// <summary>
        /// Accessor for Domain property.
        /// </summary>
        public string Domain
        {
            get { return domain; }
        }

        /// <summary>
        /// Creates a new Google Mail label for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="label">the new Google Mail label</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// creation</returns>
		public AppsExtendedEntry CreateLabel(string userName, string label)
        {
            Uri labelUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
               + domain + "/" + userName + labelFeedUriSuffix);
			AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.Properties.Add(
                new PropertyElement(AppsGoogleMailSettingsNameTable.label,
                label));
			return base.Insert(labelUri, entry) as AppsExtendedEntry;
        }

        /// <summary>
        /// Deletes a Google Mail label for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="label">The name of the label to be deleted</param>
        public void DeleteLabel(string userName, string label) {
            string uri = AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + labelFeedUriSuffix + "/" +  HttpUtility.UrlEncode(label);
            base.Delete(new Uri(uri));
        }

		/// <summary>
		/// Retrieves all labels and their settings in Google Mail for the given userName
		/// </summary>
		/// <param name="userName">The user for whom this should be done</param>
		/// <returns>Feed containing all labels</returns>
		public AppsExtendedFeed RetrieveLabels(string userName) {
			string uri = AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
			    + domain + "/" + userName + labelFeedUriSuffix;
			return QueryExtendedFeed(new Uri(uri), true);
		}

        /// <summary>
        /// Creates a new Google Mail filter for the given userName.
        /// Method overloaded for backward compatibility.
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="from">come-from email address to be filtered</param>
        /// <param name="to">send-to email address to be filtered</param>
        /// <param name="subject">a string the email must have on the subject line to be filtered</param>
        /// <param name="hasTheWords">a string the email can have anywhere in its subject or body</param>
        /// <param name="doesNotHaveTheWords">a string the email cannot have anywhere in its subject or body</param>
        /// <param name="hasAttachment">a boolean representing whether or not the emails contains an attachment</param>
        /// <param name="label">the name of the label to apply if the message matches the filter criteria</param>
        /// <param name="shouldMarkAsRead">Whether to automatically mark the message as read
        /// if it matches the filter criteria</param>
        /// <param name="shouldArchive">Whether to automatically move the message to Archived state
        /// if it matches the filter criteria</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the creation</returns>
        public AppsExtendedEntry CreateFilter(string userName, string from, string to,
            string subject, string hasTheWords, string doesNotHaveTheWords, string hasAttachment,
            string label, string shouldMarkAsRead, string shouldArchive) {
            return CreateFilter(userName, from, to, subject, hasTheWords, doesNotHaveTheWords, hasAttachment,
            label, shouldMarkAsRead, shouldArchive, null, null, null, null);
        }

        /// <summary>
        /// Creates a new Google Mail filter for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="from">come-from email address to be filtered</param>
        /// <param name="to">send-to email address to be filtered</param>
        /// <param name="subject">a string the email must have on the subject line to be filtered</param>
        /// <param name="hasTheWords">a string the email can have anywhere in its subject or body</param>
        /// <param name="doesNotHaveTheWords">a string the email cannot have anywhere in its subject or body</param>
        /// <param name="hasAttachment">a boolean representing whether or not the emails contains an attachment</param>
        /// <param name="label">the name of the label to apply if the message matches the filter criteria</param>
        /// <param name="shouldMarkAsRead">Whether to automatically mark the message as read
        /// if it matches the filter criteria</param>
        /// <param name="shouldArchive">Whether to automatically move the message to Archived state
        /// if it matches the filter criteria</param>
        /// <param name="shouldStar">Whether to automatically star the message
        /// if it matches the filter criteria</param>
        /// <param name="neverSpam">Whether to automatically move the message to Spam state
        /// if it matches the filter criteria</param>
        /// <param name="forwardTo">Whether to automatically forward the message to the given 
        /// verified email address if it matches the filter criteria</param>
        /// <param name="shouldTrash">Whether to automatically move the message to Trash state
        /// if it matches the filter criteria</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the creation</returns>
		public AppsExtendedEntry CreateFilter(string userName, string from, string to,
            string subject, string hasTheWords, string doesNotHaveTheWords, string hasAttachment,
            string label, string shouldMarkAsRead, string shouldArchive,
            string shouldStar, string neverSpam, string forwardTo, string shouldTrash)
        {
            Uri filterUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + filterFeedUriSuffix);
			AppsExtendedEntry entry = new AppsExtendedEntry();
            if (!string.IsNullOrEmpty(from))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.from, from));
            if (!string.IsNullOrEmpty(to))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.to, to));
            if (!string.IsNullOrEmpty(subject))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.subject, subject));
            if (!string.IsNullOrEmpty(hasTheWords))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.hasTheWord, hasTheWords));
            if (!string.IsNullOrEmpty(doesNotHaveTheWords))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.doesNotHaveTheWord,
                    doesNotHaveTheWords));
            if (!string.IsNullOrEmpty(hasAttachment))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.hasAttachment, hasAttachment));
            if (!string.IsNullOrEmpty(label))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.label, label));
            if (!string.IsNullOrEmpty(shouldMarkAsRead))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.shouldMarkAsRead,
                    shouldMarkAsRead));
            if (!string.IsNullOrEmpty(shouldArchive))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.shouldArchive, shouldArchive));
            if (!string.IsNullOrEmpty(shouldStar))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.shouldStar, shouldStar));
            if (!string.IsNullOrEmpty(neverSpam))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.neverSpam, neverSpam));
            if (!string.IsNullOrEmpty(forwardTo))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.forwardTo, forwardTo));
            if (!string.IsNullOrEmpty(shouldTrash))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.shouldTrash, shouldTrash));
            return base.Insert(filterUri, entry) as AppsExtendedEntry;
        }

        /// <summary>
        /// Creates a new Google Mail senda-as alias for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="name">The name which emails sent using the alias are from</param>
        /// <param name="address">The email address which emails sent using the alias are from</param>
        /// <param name="replyTo">If set, this address will be included as the reply-to addres for the alias</param>
        /// <param name="makeDefault">Whether the new alias would be the default email address</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// creation</returns>
		public AppsExtendedEntry CreateSendAs(string userName, string name, string address, string replyTo,
            string makeDefault)
        {
            Uri sendasUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + sendasFeedUriSuffix);
			AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.name, name));
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.address, address));
            if (!string.IsNullOrEmpty(replyTo))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.replyTo, replyTo));
            if (!string.IsNullOrEmpty(makeDefault))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.makeDefault, makeDefault));
            return base.Insert(sendasUri, entry) as AppsExtendedEntry;
        }

		/// <summary>
		/// Retrieves all send-as alias settings for the given userName
		/// </summary>
		/// <param name="userName">The user for whom this should be done</param>
		/// <returns>Feed containing all send-as aliases</returns>
		public AppsExtendedFeed RetrieveSendAs(string userName) {
			string uri = AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
				+ domain + "/" + userName + sendasFeedUriSuffix;
			return QueryExtendedFeed(new Uri(uri), true);
		}

        /// <summary>
        /// Enables/disables Google Mail's web clip
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="enable">Whether to enable web clip</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the creation</returns>
        public AppsExtendedEntry UpdateWebclip(string userName, string enable) {
            Uri webclipUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + webclipFeedUriSuffix);
            AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.EditUri = webclipUri;
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.enable, enable));
            return base.Update((AtomEntry)entry) as AppsExtendedEntry;
        }

        /// <summary>
        /// Updates Google Mail's forwarding rule for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="enable">Whether to enable forwarding of incoming mail</param>
        /// <param name="forwardTo">The email will be forwarded to this address</param>
        /// <param name="action">What Google Mail should do with its copy of the email after forwarding it on</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// creation</returns>
		public AppsExtendedEntry UpdateForwarding(string userName, string enable, string forwardTo, string action)
        {
            Uri forwardingUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + forwardingFeedUriSuffix);
			AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.EditUri = forwardingUri;
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.enable, enable));
            if (!string.IsNullOrEmpty(forwardTo))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.forwardTo, forwardTo));
            if (!string.IsNullOrEmpty(action))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.action, action));
			return base.Update((AtomEntry)entry) as AppsExtendedEntry;
        }

		/// <summary>
		/// Retrieves Google Mail's forwarding rule for the given userName
		/// </summary>
		/// <param name="userName">The user for whom this should be done</param>
		/// <returns>Entry containing forwarding settings</returns>
		public AppsExtendedEntry RetrieveForwarding(string userName) {
			string uri = AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
				+ domain + "/" + userName + forwardingFeedUriSuffix;
			return Get(uri) as AppsExtendedEntry;
		}

        /// <summary>
        /// Updates Google Mail's POP settings for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="enable">Whether to enable POP3 access</param>
        /// <param name="enableFor">Whether to enable POP3 for all mail or mail from now on</param>
        /// <param name="action">What Google Mail should do with its copy of the email after 
        /// it is retrieved using POP3</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// creation</returns>
		public AppsExtendedEntry UpdatePop(string userName, string enable, string enableFor, string action)
        {
            Uri popUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + popFeedUriSuffix);
			AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.EditUri = popUri;
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.enable, enable));
            if (!string.IsNullOrEmpty(enableFor))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.enableFor, enableFor));
            if (!string.IsNullOrEmpty(action))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.action, action));
			return base.Update((AtomEntry)entry) as AppsExtendedEntry;
        }

		/// <summary>
		/// Retrieves Google Mail's POP settings for the given userName
		/// </summary>
		/// <param name="userName">The user for whom this should be done</param>
		/// <returns>Entry containing POP settings</returns>
		public AppsExtendedEntry RetrievePop(string userName) {
			string uri = AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
				+ domain + "/" + userName + popFeedUriSuffix;
			return Get(uri) as AppsExtendedEntry;
		}

        /// <summary>
        /// Updates Google Mail's IMAP settings for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="enable">Whether to enable IMAP access</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// creation</returns>
		public AppsExtendedEntry UpdateImap(string userName, string enable)
        {
            Uri imapUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + imapFeedUriSuffix);
			AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.EditUri = imapUri;
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.enable, enable));
			return base.Update((AtomEntry)entry) as AppsExtendedEntry;
        }

		/// <summary>
		/// Retrieves Google Mail's IMAP settings for the given userName
		/// </summary>
		/// <param name="userName">The user for whom this should be done</param>
		/// <returns>Entry containing IMAP settings</returns>
		public AppsExtendedEntry RetrieveImap(string userName) {
			string uri = AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
				+ domain + "/" + userName + imapFeedUriSuffix;
			return Get(uri) as AppsExtendedEntry;
		}

        /// <summary>
        /// Updates Google Mail's vacation-responder settings for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should</param>
        /// <param name="enable">Wheter to enable the vacation responder</param>
        /// <param name="subject">The subject line of the vacacion responder autoresponse</param>
        /// <param name="message">The message body of the vacation responder autoresponse</param>
        /// <param name="contactsOnly">Wheter to only send the autoresponse to known contacts</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// creation</returns>
		public AppsExtendedEntry UpdateVacation(string userName, string enable, string subject,
            string message, string contactsOnly, string domainOnly, string startDate, string endDate)
        {
            Uri vacationUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + vacationFeedUriSuffix);
			AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.EditUri = vacationUri;
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.enable, enable));
            if (!string.IsNullOrEmpty(subject))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.subject, subject));
            if (!string.IsNullOrEmpty(message))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.message, message));
            if (!string.IsNullOrEmpty(contactsOnly))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.contactsOnly, contactsOnly));
            if (!string.IsNullOrEmpty(domainOnly))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.domainOnly, domainOnly));
            if (!string.IsNullOrEmpty(startDate))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.startDate, startDate));
            if (!string.IsNullOrEmpty(endDate))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.endDate, endDate));
			return base.Update((AtomEntry)entry) as AppsExtendedEntry;
        }

		/// <summary>
		/// Retrieves Google Mail's vacation-responder settings for the given userName
		/// </summary>
		/// <param name="userName">The user for whom this should be done</param>
		/// <returns>Entry containing vacation-responder settings</returns>
		public AppsExtendedEntry RetrieveVacation(string userName) {
			string uri = AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
				+ domain + "/" + userName + vacationFeedUriSuffix;
			return Get(uri) as AppsExtendedEntry;
		}

        /// <summary>
        /// Updates Google Mail's signature for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="signature">The signature to be appended to outgoing messages</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// creation</returns>
		public AppsExtendedEntry UpdateSignature(string userName, string signature)
        {
            Uri signatureUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + signatureFeedUriSuffix);
			AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.EditUri = signatureUri;
            if (signature == null)
                signature = String.Empty;
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.signature, signature));
			return base.Update((AtomEntry)entry) as AppsExtendedEntry;
        }

		/// <summary>
		/// Retrieves Google Mail's signature for the given userName
		/// </summary>
		/// <param name="userName">The user for whom this should be done</param>
		/// <returns>Entry containing signature</returns>
		public AppsExtendedEntry RetrieveSignature(string userName) {
			string uri = AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
				+ domain + "/" + userName + signatureFeedUriSuffix;
			return Get(uri) as AppsExtendedEntry;
		}

        /// <summary>
        /// Updates Google Mail's display language for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="language">Google Mail's display language</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// creation</returns>
		public AppsExtendedEntry UpdateLanguage(string userName, string language)
        {
            Uri languageUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + languageFeedUriSuffix);
			AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.EditUri = languageUri;
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.language, language));
			return base.Update((AtomEntry)entry) as AppsExtendedEntry;
        }

        /// <summary>
        /// Updates Google Mail's general settings for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="label">the new Google Mail label</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// creation</returns>
		public AppsExtendedEntry UpdateGeneralSettings(string userName, string pageSize, string shortcuts,
            string arrows, string snippets, string unicode)
        {
            Uri generalUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + generalFeedUriSuffix);
			AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.EditUri = generalUri;
            if (!string.IsNullOrEmpty(pageSize))
                entry.Properties.Add(
                   new PropertyElement(
                   AppsGoogleMailSettingsNameTable.pageSize, pageSize));
            if (!string.IsNullOrEmpty(shortcuts))
                entry.Properties.Add(
                   new PropertyElement(
                   AppsGoogleMailSettingsNameTable.shortcuts, shortcuts));
            if (!string.IsNullOrEmpty(arrows))
                entry.Properties.Add(
                   new PropertyElement(
                   AppsGoogleMailSettingsNameTable.arrows, arrows));
            if (!string.IsNullOrEmpty(snippets))
                entry.Properties.Add(
                   new PropertyElement(
                   AppsGoogleMailSettingsNameTable.snippets, snippets));
            if (!string.IsNullOrEmpty(unicode))
                entry.Properties.Add(
                   new PropertyElement(
                   AppsGoogleMailSettingsNameTable.unicode, unicode));
			return base.Update((AtomEntry)entry) as AppsExtendedEntry;
        }

        /// <summary>
        /// Creates a new Google Mail delegation for the given userName
        /// </summary>
        /// <param name="userName">The user that grants mailbox access to another user</param>
        /// <param name="delegationId">Email address of the user receiving access</param>
        /// <returns>a <code>AppsExtendedEntry</code> containing the results of the
        /// creation</returns>
        public AppsExtendedEntry CreateDelegate(string userName, string delegationId) {
            Uri delegationUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
               + domain + "/" + userName + delegationFeedUriSuffix);
            AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.Properties.Add(
                new PropertyElement(AppsGoogleMailSettingsNameTable.address,
                delegationId));
            return base.Insert(delegationUri, entry) as AppsExtendedEntry;
        }

        /// <summary>
        /// Retrieves all Google Mail delegates for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <returns>Feed containing all delegates</returns>
        public AppsExtendedFeed RetrieveDelegates(string userName) {
            string uri = AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + delegationFeedUriSuffix;
            return QueryExtendedFeed(new Uri(uri), true);
        }

        /// <summary>
        /// Deletes a Google Mail delegate for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="delegationId">Email address of the user we want to revoke access to</param>
        public void DeleteDelegate(string userName, string delegationId) {
            string uri = AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + delegationFeedUriSuffix + "/" + delegationId;
            base.Delete(new Uri(uri));
        }

        /// <summary>
        /// Event handler. Called when a new Google Mail Settings entry is parsed.
        /// </summary>
        /// <param name="sender">the object that's sending the evet</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        protected void OnParsedNewGoogleMailSettingsItemEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry)
            {
				e.Entry = new AppsExtendedEntry();
            }
        }

        /// <summary>
        /// Overridden so that new feeds are returned as <code>GoogleMailSettingsFeed</code>s
        /// instead of base <code>AtomFeed</code>s.
        /// </summary>
        /// <param name="sender"> the object which sent the event</param>
        /// <param name="e">FeedParserEventArguments, holds the FeedEntry</param> 
        protected void OnNewFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new Google Mail Settings Item Feed");
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
			e.Feed = new AppsExtendedFeed(e.Uri, e.Service);
        }
    }
}
