a/UnityExtensions/Unity/GUISystem/UnityEngine.UI.dll</HintPath>
 </Reference>
 <Reference Include="UnityEditor.iOS.Extensions.Xcode">
 <HintPath>C:/Program Files/Unity/Editor/Data/PlaybackEngines/iossupport/UnityEditor.iOS.Extensions.Xcode.dll</HintPath>
 </Reference>
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. 
	   Other similar extension points exist, see Microsoft.Common.targets.
  <Target Name="BeforeBuild">
  </Target>
  <Target Name="AfterBuild">
  </Target>
  -->
  
</Project>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           /*
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

// core
#import "FBAccessTokenData.h"
#import "FBAppCall.h"
#import "FBAppEvents.h"
#import "FBCacheDescriptor.h"
#import "FBDialogs.h"
#import "FBError.h"
#import "FBErrorUtility.h"
#import "FBFrictionlessRecipientCache.h"
#import "FBFriendPickerViewController.h"
#import "FBGraphLocation.h"
#import "FBGraphObject.h"           // + design summary for graph component-group
#import "FBGraphPlace.h"
#import "FBGraphUser.h"
#import "FBInsights.h"
#import "FBLikeControl.h"
#import "FBLoginView.h"
#import "FBNativeDialogs.h"         // deprecated, use FBDialogs.h
#import "FBOpenGraphAction.h"
#import "FBOpenGraphActionShareDialogParams.h"
#import "FBOpenGraphObject.h"
#import "FBPlacePickerViewController.h"
#import "FBProfilePictureView.h"
#import "FBRequest.h"
#import "FBSession.h"
#import "FBSessionTokenCachingStrategy.h"
#import "FBSettings.h"
#import "FBShareDialogParams.h"
#import "FBShareDialogPhotoParams.h"
#import "FBTaggableFriendPickerViewController.h"
#import "FBUserSettingsViewController.h"
#import "FBWebDialogs.h"
#import "NSError+FBError.h"

/*!
 @header

 @abstract  Library header, import this to import all of the public types
            in the Facebook SD