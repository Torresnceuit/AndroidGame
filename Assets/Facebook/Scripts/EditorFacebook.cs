/*
 *  Licensed to the Apache Software Foundation (ASF) under one or more
 *  contributor license agreements.  See the NOTICE file distributed with
 *  this work for additional information regarding copyright ownership.
 *  The ASF licenses this file to You under the Apache License, Version 2.0
 *  (the "License"); you may not use this file except in compliance with
 *  the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

package org.apache.harmony.tests.java.io;

import java.io.ByteArrayOutputStream;
import java.io.OutputStreamWriter;
import java.io.UnsupportedEncodingException;

public class UnsupportedEncodingExceptionTest extends junit.framework.TestCase {

    /**
     * java.io.UnsupportedEncodingException#UnsupportedEncodingException()
     */
    public void test_Constructor() {
        // Test for method java.io.UnsupportedEncodingException()
        try {
            new OutputStreamWriter(new ByteArrayOutputStream(), "BogusEncoding");
        } catch (UnsupportedEncodingException e) {
            return;
        }

        fail("Failed to generate expected exception");
    }

    /**
     * java.io.UnsupportedEncodingException#UnsupportedEncodingException(java.lang.String)
     */
    public void test_ConstructorLjava_lang_String() {
        // Test for method
        // java.io.UnsupportedEncodingException(java.lang.String)
        try {
            new OutputStreamWriter(new ByteArrayOutputStream(), "BogusEncoding");
        } catch (UnsupportedEncodingException e) {
            return;
        }

        fail("Failed to generate expected exception");
    }

    /**
     * Sets up the fixture, for example, open a network connection. This method
     * is called before a test is executed.
     */
    protected void setUp() {
    }

    /**
     * Tears down the fixture, for example, close a network connection. This
     * method is called after a test is executed.
     */
    protected void tearDown() {
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Facebook
{
    class EditorFacebook : AbstractFacebook, IFacebook
    {
        private AbstractFacebook fb;
        private FacebookDelegate loginCallback;

        public override int DialogMode
        {
            get { return 0; }
            set { ; }
        }

        public override bool LimitEventUsage
        {
            get
            {
                return limitEventUsage;
            }
            set
            {
                limitEventUsage = value;
            }
        }

        #region Init
        protected override void OnAwake()
        {
            // bootstrap the canvas facebook for native dialogs
            StartCoroutine(FB.RemoteFacebookLoader.LoadFacebookClass("CanvasFacebook", OnDllLoaded));
        }

        public override void Init(
            InitDelegate onInitComplete,
            string appId,
            bool cookie = false,
            bool logging = true,
            bool status = true,
            bool xfbml = false,
            string channelUrl = "",
            string authResponse = null,
            bool frictionlessRequests = false,
            Facebook.HideUnityDelegate hideUnityDelegate = null)
        {
            StartCoroutine(OnInit(onInitComplete, appId, cookie, logging, status, xfbml, channelUrl, authResponse, frictionlessRequests, hideUnityDelegate));
        }

        private IEnumerator OnInit(
            InitDelegate onInitComplete,
            string appId,
            bool cookie = false,
            bool logging = true,
            bool status = true,
            bool xfbml = false,
            string channelUrl = "",
            string authResponse = null,
            bool frictionlessRequests = false,
            Facebook.HideUnityDelegate hideUnityDelegate = null)
        {
            // wait until the native dialogs are loaded
            while (fb == null)
            {
                yield return null;
            }
            fb.Init(onInitComplete, appId, cookie, logging, status, xfbml, channelUrl, authResponse, frictionlessRequests, hideUnityDelegate);

            this.isInitialized = true;
            if (onInitComplete != null)
            {
                onInitComplete();
            }
        }

        private void OnDllLoaded(IFacebook fb)
        {
            this.fb = (AbstractFacebook) fb;
        }
        #endregion

        public override void Login(string scope = "", FacebookDelegate callback = null)
        {
            AddAuthDelegate(callback);
            FBComponentFactory.GetComponent<EditorFacebookAccessToken>();
        }

        public override void Logout()
        {
            isLoggedIn = false;
            userId = "";
            accessToken = "";
            fb.UserId = "";
            fb.AccessToken = "";
        }

        public override void AppRequest(
            string message,
            OGActionType actionType,
            string objectId,
            string[] to = null,
            List<object> filters = null,
            string[] excludeIds = null,
            int? maxRecipients = null,
            string data = "",
            string title = "",
            FacebookDelegate callback = null)
        {
            fb.AppRequest(message, actionType, objectId, to, filters, excludeIds, maxRecipients, data, title, callback);
        }

        public override void FeedRequest(
            string toId = "",
            string link = "",
            string linkName = "",
            string linkCaption = "",
            string linkDescription = "",
            string picture = "",
            string mediaSource = "",
            string actionName = "",
            string actionLink = "",
            string reference = "",
            Dictionary<string, string[]> properties = null,
            FacebookDelegate callback = null)
        {
            fb.FeedRequest(toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference, properties, callback);
        }

        public override void Pay(
            string product,
            string action = "purchaseitem",
            int quantity = 1,
            int? quantityMin = null,
            int? quantityMax = null,
            string requestId = null,
            string pricepointId = null,
            string testCurrency = null,
            FacebookDelegate callback = null)
        {
            FbDebug.Info("Pay method only works with Facebook Canvas.  Does nothing in the Unity Editor, iOS or Android");
        }

        public override void GameGroupCreate(
            string 