/*
 * Copyright (C) 2014 The Android Open Source Project
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

package com.android.ex.camera2.utils;

import android.hardware.camera2.CameraCaptureSession;
import android.hardware.camera2.CameraCaptureSession.CaptureCallback;
import android.hardware.camera2.CaptureFailure;
import android.hardware.camera2.CaptureRequest;
import android.hardware.camera2.CaptureResult;
import android.hardware.camera2.TotalCaptureResult;
import android.os.Handler;

/**
 * Proxy that forwards all updates to another {@link CaptureCallback}, invoking
 * its callbacks on a separate {@link Handler}.
 */
public class Camera2CaptureCallbackForwarder extends CaptureCallback {
    private CaptureCallback mListener;
    private Handler mHandler;

    public Camera2CaptureCallbackForwarder(CaptureCallback listener, Handler handler) {
        mListener = listener;
        mHandler = handler;
    }

    @Override
    public void onCaptureCompleted(final CameraCaptureSession session, final CaptureRequest request,
                                   final TotalCaptureResult result) {
        mHandler.post(new Runnable() {
            @Override
            public void run() {
                mListener.onCaptureCompleted(session, request, result);
            }});
    }

    @Override
    public void onCaptureFailed(final CameraCaptureSession session, final CaptureRequest request,
                                final CaptureFailure failure) {
        mHandler.post(new Runnable() {
            @Override
            public void run() {
                mListener.onCaptureFailed(session, request, failure);
            }});
    }

    @Override
    public void onCaptureProgressed(final CameraCaptureSession session,
                                    final CaptureRequest request,
                                    final CaptureResult partialResult) {
        mHandler.post(new Runnable() {
            @Override
            public void run() {
                mListener.onCaptureProgressed(session, request, partialResult);
            }});
    }

    @Override
    public void onCaptureSequenceAborted(final CameraCaptureSession session, final int sequenceId) {
        mHandler.post(new Runnable() {
            @Override
            public void run() {
                mListener.onCaptureSequenceAborted(session, sequenceId);
            }});
    }

    @Override
    public void onCaptureSequenceCompleted(final CameraCaptureSession session, final int sequenceId,
                                           final long frameNumber) {
        mHandler.post(new Runnable() {
            @Override
            public void run() {
                mListener.onCaptureSequenceCompleted(session, sequenceId, frameNumber);
            }});
    }

    @Override
    public void onCaptureStarted(final CameraCaptureSession session, final CaptureRequest request,
                                 final long timestamp, final long frameNumber) {
        mHandler.post(new Runnable() {
            @Override
            public void run() {
                mListener.onCaptureStarted(session, request, timestamp, frameNumber);
            }});
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                        ﻿using UnityEngine;
using System.Collections;
using Parse;
using System;
using System.Threading.Tasks;
using Facebook;
using Facebook.MiniJSON;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Net;

public class LoginIcon : MonoBehaviour {
	
	
	public GameState gamestate;
	public GhostRunnerResources resources;
	public Notification notification;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	private void CallFBInit()
	{
		FB.Init(OnInitComplete, OnHideUnity);
	}
	
	private void OnInitComplete()
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}
	
	public void LogIn()
	{
		FB.Login("public_profile,email,user_friends", LoginCallback);
	}

	void SharedCallback(FBResult result)
	{
		//Screen.orientation = ScreenOrientation.LandscapeLeft;
		if(result.Error != null)
		{
			//Debug.LogError("OnActionShared: Error: " + result.Error);
		}
		
		if (result == null || result.Error != null)
		{
			//Do something request failed
			
		}
		else
		{
			var responseObject = Facebook.MiniJSON.Json.Deserialize(result.Text) as System.Collections.Generic.Dictionary<string, object>;
			object obj = 0;
			if (responseObject == null || responseObject.Count <= 0 || responseObject.TryGetValue("cancelled", out obj))
			{
				//Debug.LogWarning("Request cancelled");
				//Do something when user cancelled
			}
			else if (responseObject.TryGetValue("id", out obj) || responseObject.TryGetValue("post_id", out obj))
			{
				//Debug.LogWarning("Request Send");
				//Do something it is succeeded
			}
		}
		
	}
	
	private IEnumerator ParseLogin() 
	{
		if (FB.IsLoggedIn) {
			var loginTask = ParseFacebookUtils.LogInAsync(FB.UserId,FB.AccessToken, DateTime.Now);
			while (!loginTask.IsCompleted) yield return null;
			if (loginTask.IsFaulted || loginTask.IsCanceled) {
				// Handle error
			} else {
				Debug.Log("Yeah");
				FB.API("/me", HttpMethod.GET, FBAPICallback);
			}
		}
	}
	
	private void FBAPICallback(FBResult result)
	{
		if (!String.IsNullOrEmpty(result.Error)) {
			// Handle error
		} else {
			// Got user profile info
			var resultObject = Json.Deserialize(result.Text) as Dictionary<string, object>;
			var userProfile = new Dictionary<string, string>();
			
			userProfile["facebookId"] = getDataValueForKey(resultObject, "id");
			userProfile["name"] = getDataValueForKey(resultObject, "name");
			object location;
			if (resultObject.TryGetValue("location", out location)) {
				userProfile["location"] = (string)(((Dictionary<string, object>)location)["name"]);
			}
			userProfile["locale"] = getDataValueForKey(resultObject, "locale");
			userProfile["gender"]