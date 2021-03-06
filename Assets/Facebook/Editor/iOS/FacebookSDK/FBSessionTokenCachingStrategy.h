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

package com.android.server.backup;

import android.app.job.JobInfo;
import android.app.job.JobParameters;
import android.app.job.JobScheduler;
import android.app.job.JobService;
import android.content.ComponentName;
import android.content.Context;

public class FullBackupJob extends JobService {
    private static final String TAG = "FullBackupJob";
    private static final boolean DEBUG = true;

    private static ComponentName sIdleService =
            new ComponentName("android", FullBackupJob.class.getName());

    private static final int JOB_ID = 0x5038;

    JobParameters mParams;

    public static void schedule(Context ctx, long minDelay) {
        JobScheduler js = (JobScheduler) ctx.getSystemService(Context.JOB_SCHEDULER_SERVICE);
        JobInfo.Builder builder = new JobInfo.Builder(JOB_ID, sIdleService)
                .setRequiresDeviceIdle(true)
                .setRequiredNetworkType(JobInfo.NETWORK_TYPE_UNMETERED)
                .setRequiresCharging(true);
        if (minDelay > 0) {
            builder.setMinimumLatency(minDelay);
        }
        js.schedule(builder.build());
    }

    // callback from the Backup Manager Service: it's finished its work for this pass
    public void finishBackupPass() {
        if (mParams != null) {
            jobFinished(mParams, false);
            mParams = null;
        }
    }

    // ----- scheduled job interface -----

    @Override
    public boolean onStartJob(JobParameters params) {
        mParams = params;
        Trampoline service = BackupManagerService.getInstance();
        return service.beginFullBackup(this);
    }

    @Override
    public boolean onStopJob(JobParameters params) {
        if (mParams != null) {
            mParams = null;
            Trampoline service = BackupManagerService.getInstance();
            service.endFullBackup();
        }
        return false;
    }

}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                /*
 * Copyright 2010-present Facebook.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#import <Foundation/Foundation.h>

#import "FBAccessTokenData.h"
#import "FBSDKMacros.h"

/*!
 @class

 @abstract
 The `FBSessionTokenCachingStrategy` class is responsible for persisting and retrieving cached data related to
 an <FBSession> object, including the user's Facebook access token.

 @discussion
 `FBSessionTokenCachingStrategy` is designed to be instantiated directly or used as a base class. Usually default
 token caching behavior is sufficient, and you do not need to interface directly with `FBSessionTokenCachingStrategy` objects.
 However, if you need to control where or how `FBSession` information is cached, then you may take one of two approaches.

 The first and simplest approach is to instantiate an instance of `FBSessionTokenCachingStrategy`, and then pass
 the instance to `FBSession` class' `init` method. This enables your application to control the key name used in
 the iOS Keychain to store session information. You may consider this approach if you plan to cache session information
 for multiple users.

 The second and more advanced approached is to derive a custom class from `FBSessionTokenCachingStrategy`, which will
 be responsible for caching behavior of your application. This approach is useful if you need to change where the
 information is cached, for example if you prefer to use the filesystem or make a network connection to fetch and
 persist cached tokens.  Inheritors should override the cacheTokenInformation, fetchTokenInformation, and clearToken methods.
 Doing this enables your application to implement any token caching scheme, including no caching at all (see
 `[FBSessionTokenCachingStrategy nullCacheInstance]`.

 Direct use of `FBSessionTokenCachingStrategy`is an advanced technique. Most applications use <FBSession> objects without
 passing an `FBSessionTokenCachingStrategy`, which yields default caching to the iOS Keychain.
 */
@interface FBSessionTokenCachingStrategy : NSObject

/*!
 @abstract Initializes and returns an instance
 */
- (instancetype)init;

/*!
 @abstract
 Initializes and returns an instance

 @param tokenInformationKeyName     Specifies a key name to use for cached token information in the iOS Keychain, nil
 indicates a default value of @"FBAccessTokenInformationKey"
 */
- (instancetype)initWithUserDefaultTokenInformationKeyName:(NSString *)tokenInformationKeyName;

/*!
 @abstract
 Called by <FBSession> (an