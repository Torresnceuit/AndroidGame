oneStateListener.LISTEN_SERVICE_STATE);
        }
    }

    protected synchronized void removePhoneStateListener() {
        if (mPhoneStateListener != null) {
            mTelephonyManager.listen(mPhoneStateListener, PhoneStateListener.LISTEN_NONE);
            mPhoneStateListener = null;
        }
    }

    protected boolean isGeoCoderImplemented() {
        return Geocoder.isPresent();
    }

    @Override
    public String toString() {
        long currentTime = SystemClock.elapsedRealtime();
        long currentSessionLength = 0;
        StringBuilder sb = new StringBuilder();
        sb.append("ComprehensiveCountryDetector{");
        // The detector hasn't stopped yet --> still running
        if (mStopTime == 0) {
            currentSessionLength = currentTime - mStartTime;
            sb.append("timeRunning=" + currentSessionLength + ", ");
        } else {
            // Otherwise, it has already stopped, so take the last session
            sb.append("lastRunTimeLength=" + (mStopTime - mStartTime) + ", ");
        }
        sb.append("totalCountServiceStateChanges=" + mTotalCountServiceStateChanges + ", ");
        sb.append("currentCountServiceStateChanges=" + mCountServiceStateChanges + ", ");
        sb.append("totalTime=" + (mTotalTime + currentSessionLength) + ", ");
        sb.append("currentTime=" + currentTime + ", ");
        sb.append("countries=");
        for (Country country : mDebugLogs) {
            sb.append("\n   " + country.toString());
        }
        sb.append("}");
        return sb.toString();
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   /*
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

#import <CoreLocation/CoreLocation.h>
#import <UIKit/UIKit.h>

#import "FBCacheDescriptor.h"
#import "FBGraphObjectPickerViewController.h"
#import "FBGraphPlace.h"

@protocol FBPlacePickerDelegate;

/*!
 @class FBPlacePickerViewController

 @abstract
 The `FBPlacePickerViewController` class creates a controller object that manages
 the user interface for displaying and selecting nearby places.

 @discussion
 When the `FBPlacePickerViewController` view loads it creates a `UITableView` object
 where the places near a given location will be displayed. You can access this view
 through the `tableView` property.

 The place data can be pre-fetched and cached prior to using the view controller. The
 cache is setup using an <FBCacheDescriptor> object that can trigger the
 data fetch. Any place data requests will first check the cache and use that data.
 If the place picker is being displayed cached data will initially be shown before
 a fresh copy is retrieved.

 The `delegate` property may be set to an object that conforms to the <FBPlacePickerDelegate>
 protocol. The `delegate` object will receive updates related to place selection and
 data changes. The delegate can also be used to filter the places to display in the
 picker.
 */
@interface FBPlacePickerViewController : FBGraphObjectPickerViewController

/*!
 @abstract
 The coordinates to use for place discovery.
 */
@property (nonatomic) CLLocationCoordinate2D locationCoordinate;

/*!
 @abstract
 The radius to use for place discovery.
 */
@property (nonatomic) NSInteger radiusInMeters;

/*!
 @abstract
 The maximum number of places to fetch.
 */
@property (nonatomic) NSInteger resultsLimit;

/*!
 @abstract
 The search words used to narrow down the results returned.
 */
@property (nonatomic, copy) NSString *searchText;

/*!
 @abstract
 The plac