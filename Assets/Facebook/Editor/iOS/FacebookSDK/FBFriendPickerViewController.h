   return "ACTION_SELECT";
            case ACTION_CLEAR_SELECTION:
                return "ACTION_CLEAR_SELECTION";
            case ACTION_CLICK:
                return "ACTION_CLICK";
            case ACTION_LONG_CLICK:
                return "ACTION_LONG_CLICK";
            case ACTION_ACCESSIBILITY_FOCUS:
                return "ACTION_ACCESSIBILITY_FOCUS";
            case ACTION_CLEAR_ACCESSIBILITY_FOCUS:
                return "ACTION_CLEAR_ACCESSIBILITY_FOCUS";
            case ACTION_NEXT_AT_MOVEMENT_GRANULARITY:
                return "ACTION_NEXT_AT_MOVEMENT_GRANULARITY";
            case ACTION_PREVIOUS_AT_MOVEMENT_GRANULARITY:
                return "ACTION_PREVIOUS_AT_MOVEMENT_GRANULARITY";
            case ACTION_NEXT_HTML_ELEMENT:
                return "ACTION_NEXT_HTML_ELEMENT";
            case ACTION_PREVIOUS_HTML_ELEMENT:
                return "ACTION_PREVIOUS_HTML_ELEMENT";
            case ACTION_SCROLL_FORWARD:
                return "ACTION_SCROLL_FORWARD";
            case ACTION_SCROLL_BACKWARD:
                return "ACTION_SCROLL_BACKWARD";
            case ACTION_CUT:
                return "ACTION_CUT";
            case ACTION_COPY:
                return "ACTION_COPY";
            case ACTION_PASTE:
                return "ACTION_PASTE";
            case ACTION_SET_SELECTION:
                return "ACTION_SET_SELECTION";
            default:
                return"ACTION_UNKNOWN";
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              /*
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

#import <UIKit/UIKit.h>

#import "FBCacheDescriptor.h"
#import "FBGraphUser.h"
#import "FBPeoplePickerViewController.h"

@protocol FBFriendPickerDelegate;


/*!
 @class

 @abstract
 The `FBFriendPickerViewController` class creates a controller object that manages
 the user interface for displaying and selecting Facebook friends.

 @discussion
 When the `FBFriendPickerViewController` view loads it creates a `UITableView` object
 where the friends will be displayed. You can access this view through the `tableView`
 property. The friend display can be sorted by first name or last name. Friends'
 names can be displayed with the first name first or the last name first.

 The friend data can be pre-fetched and cached prior to using the view controller. The
 cache is setup using an <FBCacheDescriptor> object that can trigger the
 data fetch. Any friend data requests will first check the cache and use that data.
 If the friend picker is being displayed cached data will initially be shown before
 a fresh copy is retrieved.

 The `delegate` property may be set to an object that conforms to the <FBFriendPickerDelegate>
 protocol. The `delegate` object will receive updates related to friend selection and
 data changes. The delegate can also be used to filter the friends to display in the
 picker.
 */
@interface FBFriendPickerViewController : FBPeoplePickerViewController

/*!
 @abstract
 The list of friends that are currently selected in the veiw.
 The items in the array are <FBGraphUser> objects.

 @discussi