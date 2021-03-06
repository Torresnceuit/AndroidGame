EEH���n��=��mp�4jX�him�����H)JD%)   �]u�\}���-n��*[�-EKZ�d�JQ%D�$�F�5�s�F�(�F�(�F�(�F�(�F�(�F�(�F�(�F�(�F�(�F�(�F�(�F�(�F�(�F�(�F�(�F�(�F�(���ؿ�$���MEn��V�m�~	��\(�����[.�ꅶ�MWڮttw
_��E#�ӊz�Z�'�)��r�h|(u?���7���g-�;r�b��{�-鱯>����f���j�f�ҡ�u�j��x��m��-�� f��辮���������;����W�-���[����Si*���ѹ�+klTQ��7�n�ٕ�[~ߺ_�⟱�k�cn���6�յ�5��i�S� V����]���q/����vn�R�&���t�ݻ�\Ov�T&=��K���n%*ү�O���ˬp0���m �ǃ�ݫ���K�
)Rܷa���~`�d@�#<�� O���>� �8�g�L� � H�w����~Ѿ��QIҫoL>$6�}��ծ�����b^���'��`��fƷZ.w�2��m�ۛ|�[�ۨ�z���:��O{�����onKNӮ�1�M��hUd��nm�й׽m�������紶�n���^/��o�5u�~
������Wj���,{?��n�r�i6Oj�i���V��MM�rܸ�xm)t��е]�v���!��h���j��m��t�ųf�*�ߨ�*��Kv�q�Z��m��Yq�+]�0	�D�$G8���p|�0G�bf|�3'�\�t}5� o�K���k�tWit��u�j��]J�OM��(��O�{OO���}�Uo6)��A����u��>U��V7�?����3`�Zwg�V.�{��o���t�`�]zm�ZkӔU��-�}�OvWQw������S��VgzG\_���Ų����K��K[yM�-��VT����3��(?l
�Pq���V��	C�ۈZ�_}%�R��/Z�C[.Z�wj����a��M5��#��֗�K��%�Ox�$�<\�~����ן���.����Weض��'���+������m��.��
a������S[KeRֳt�l<�Z���� �l�K�� �ޛ�*�;�� �#��Pt��d���p�^nUW�^����r����nm�s�n;��(-��u�զ�j�[SKn��:q��t{hZ�I���Ʊ��Z6���v���-ߊ�z���K=������k�jk���ϭ�����qꪇ�^s�J��4vTժ�Z�*��\?j�L�����m��.���!�����h�Y���𻰋;����â�T��~�����߸��mĥ����ݴ6ͳ!b���ѣF�*5F�4QF�4QF�4QF�4QF�4QF�4QF�4QF�4QF�4QF�4QF�4QF�4QF�4QF�4QF�4Q_��                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          /*
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

#import "FBSession.h"

#import "FBSDKMacros.h"

#if defined(DEBUG) && !defined(SAFE_TO_USE_FBTESTSESSION)
#define SAFE_TO_USE_FBTESTSESSION
#endif

#if !defined(SAFE_TO_USE_FBTESTSESSION)
#pragma message ("warning: using FBTestSession, which is designed for unit-testing uses only, in non-DEBUG code -- ensure this is what you really want")
#endif

/*!
 Consider using this tag to pass to sessionWithSharedUserWithPermissions:uniqueUserTag: when
 you need a second unique test user in a test case. Using the same tag each time reduces
 the proliferation of test users.
 */
FBSDK_EXTERN NSString *kSecondTestUserTag;
/*!
 Consider using this tag to pass to sessionWithSharedUserWithPermissions:uniqueUserTag: when
 you need a third unique test user in a test case. Using the same tag each time reduces
 the proliferation of test users.
 */
FBSDK_EXTERN NSString *kThirdTestUserTag;

/*!
 @class FBTestSession

 @abstract
 Implements an FBSession subclass that knows about test users for a particular
 application. This should never be used from a real application, but may be useful
 for writing unit tests, etc.

 @discussion
 Facebook allows developers to create test accounts for testing their applications'
 Facebook integration (see https://developers.facebook.com/docs/test_users/). This class
 simplifies use of these accounts for writing unit tests. It is not designed for use in
 production application code.

 The main use case for this class is using sessionForUnitTestingWithPermissions:mode:
 to create a session for a test user. Two modes are supported. In "shared" mode, an attempt
 is made to find an existing test user that has the required permissions and, if it is not
 currently in use by another FBTestSession, j