onfig_overrideHasPermanentMenuKey">0</integer>

    <!-- default window inset isRound property -->
    <bool name="config_windowIsRound">false</bool>

    <!-- Defines the default set of global actions. Actions may still be disabled or hidden based
         on the current state of the device.
         Each item must be one of the following strings:
         "power" = Power off
         "settings" = An action to launch settings
         "airplane" = Airplane mode toggle
         "bugreport" = Take bug report, if available
         "silent" = silent mode
         "users" = list of users
         -->
    <string-array translatable="false" name="config_globalActionsList">
        <item>power</item>
        <item>airplane</item>
        <item>bugreport</item>
        <item>silent</item>
        <item>users</item>
    </string-array>

</resources>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          ﻿using System;
using System.Linq;
using System.IO;
using System.Xml;
using System