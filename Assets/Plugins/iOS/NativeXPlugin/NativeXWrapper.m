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

package com.android.dex.util;

public final class ByteArrayByteInput implements ByteInput {

    private final byte[] bytes;
    private int position;

    public ByteArrayByteInput(byte... bytes) {
        this.bytes = bytes;
    }

    @Override public byte readByte() {
        return bytes[position++];
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        //
//  W3iWrapper.cpp
//  Unity-iPhone
//
//  Created by Josh Ruis on 1/18/13.
//
//

#import "NativeXCore.h"
#import <AdSupport/AdSupport.h> // Used for IDFA
#import <Foundation/Foundation.h> // Used for NSLocale

#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]

#define GetStringParamOrNil( _x_ ) ( _x_ != NULL && strlen( _x_ ) ) ? [NSString stringWithUTF8String:_x_] : nil

const char* nx_cStringCopy(const char* string);


void uStartWithNameAndApplicationId(const char *appId, const char *pubId, bool enableLogging, bool showRedeemAlert)
{
    [[NativeXCore instance] setShowMessages:showRedeemAlert];
    [[NativeXCore instance] startWithApplicationId:GetStringParamOrNil(appId) publisherId:GetStringParamOrNil(pubId) enableLogging:enableLogging];
}

void uSelectServer(const char *url)
{
    [[NativeXCore instance] setURL:GetStringParamOrNil(url)];
}

BOOL uIsAdReady(const char *placement)
{
    return [[NativeXCore instance] isAdReady:GetStringParamOrNil(placement)];
}

void uFetchAd(const char* placement)
{
    [[NativeXCore instance] fetchAd:GetStringParamOrNil(placement) ];
}
