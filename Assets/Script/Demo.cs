/*
 * Copyright (C) 2011 The Android Open Source Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package com.android.server;

import static com.android.server.NativeDaemonConnector.appendEscaped;
import static com.android.server.NativeDaemonConnector.makeCommand;

import android.test.AndroidTestCase;
import android.test.suitebuilder.annotation.MediumTest;

import com.android.server.NativeDaemonConnector.SensitiveArg;

/**
 * Tests for {@link NativeDaemonConnector}.
 */
@MediumTest
public class NativeDaemonConnectorTest extends AndroidTestCase {
    private static final String TAG = "NativeDaemonConnectorTest";

    public void testArgumentNormal() throws Exception {
        final StringBuilder builder = new StringBuilder();

        builder.setLength(0);
        appendEscaped(builder, "");
        assertEquals("", builder.toString());

        builder.setLength(0);
        appendEscaped(builder, "foo");
        assertEquals("foo", builder.toString());

        builder.setLength(0);
        appendEscaped(builder, "foo\"bar");
        assertEquals("foo\\\"bar", builder.toString());

        builder.setLength(0);
        appendEscaped(builder, "foo\\bar\\\"baz");
        assertEquals("foo\\\\bar\\\\\\\"baz", builder.toString());
    }

    public void testArgumentWithSpaces() throws Exception {
        final StringBuilder builder = new StringBuilder();

        builder.setLength(0);
        appendEscaped(builder, "foo bar");
        assertEquals("\"foo bar\"", builder.toString());

        builder.setLength(0);
        appendEscaped(builder, "foo\"bar\\baz foo");
        assertEquals("\"foo\\\"bar\\\\baz foo\"", builder.toString());
    }

    public void testArgumentWithUtf() throws Exception {
        final StringBuilder builder = new StringBuilder();

        builder.setLength(0);
        appendEscaped(builder, "caf\u00E9 c\u00F6ffee");
        assertEquals("\"caf\u00E9 c\u00F6ffee\"", builder.toString());
    }

    public void testSensitiveArgs() throws Exception {
        final StringBuilder rawBuilder = new StringBuilder();
        final StringBuilder logBuilder = new StringBuilder();

        rawBuilder.setLength(0);
        logBuilder.setLength(0);
        makeCommand(rawBuilder, logBuilder, 1, "foo", "bar", "baz");
        assertEquals("1 foo bar baz\0", rawBuilder.toString());
        assertEquals("1 foo bar baz", logBuilder.toString());

        rawBuilder.setLength(0);
        logBuilder.setLength(0);
        makeCommand(rawBuilder, logBuilder, 1, "foo", new SensitiveArg("bar"), "baz");
        assertEquals("1 foo bar baz\0", rawBuilder.toString());
        assertEquals("1 foo [scrubbed] baz", logBuilder.toString());

        rawBuilder.setLength(0);
        logBuilder.setLength(0);
        makeCommand(rawBuilder, logBuilder, 1, "foo", new SensitiveArg("foo bar"), "baz baz",
                new SensitiveArg("wat"));
        assertEquals("1 foo \"foo bar\" \"baz baz\" wat\0", rawBuilder.toString());
        assertEquals("1 foo [scrubbed] \"baz baz\" [scrubbed]", logBuilder.toString());
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  ﻿using UnityEngine;
using System.Collections;
using Parse;
using System;
using System.Threading.Tasks;
using Facebook;
using Facebook.MiniJSON;
using System.Collections.Generic;
using UnityEngine.UI;

public class Demo : MonoBehaviour {
	public GameObject ScoreEntryPanel;
	public GameObject ScoreScrollList;
	String rstext = "";
	void Start () {
		//FB.Init(OnInitComplete, OnHideUnity);
		//StartCoroutine( GetScore());
	}


	private IEnumerator GetScore()
	{
		ParseCloud.CallFunctionAsync<string>("GetScore", new Dictionary<string, object>()).ContinueWith(t =>
		{
			rstext = t.Result;
			//QueryScore();
		});
		yield break;
	}
	
	void Update () {
	}

	private void CallFBInit()
	{
		FB.Init(OnInitComplete, OnHideUnity);
	}
	
	private void OnInitComplete()
	{
		//Check when login complete
		//Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		//Debug.Log("Is game showing? " + isGameShown);
	}

	// Login before use function
	public void CallFBLogin()
	{
		FB.Logout ();
		FB.Login("user_about_me, user_relationships, user_birthday, user_location", LoginCallback);
	}

	void LoginCallback(FBResult result)
	{
		if(FB.IsLoggedIn) {
			StartCoroutine("ParseLogin");
		} else {
			//Debug.Log ("FBLoginCallback: User canceled login");
		}
	}

	private IEnumerator ParseLogin() {
		if (FB.IsLoggedIn) {
			var loginTask = ParseFacebookUtils.LogInAsync(FB.UserId,FB.AccessToken, DateTime.Now);
			while (!loginTask.IsCompleted) yield re