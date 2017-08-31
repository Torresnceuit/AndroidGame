
            score = network.rssiCurve.lookupScore(result.level, isActiveNetwork);
            if (DBG) {
                Log.e(TAG, "getNetworkScore found scored network " + network.networkKey
                        + " score " + Integer.toString(score)
                        + " RSSI " + result.level
                        + " isActiveNetwork " + isActiveNetwork);
            }
        }
        return score;
    }

    private ScoredNetwork getScoredNetwork(ScanResult result) {
        String key = buildNetworkKey(result);
        if (key == null) return null;

        //find it
        synchronized(mNetworkCache) {
            ScoredNetwork network = mNetworkCache.get(key);
            return network;
        }
    }

     private String buildNetworkKey(ScoredNetwork network) {
        if (network.networkKey == null) return null;
        if (network.networkKey.wifiKey == null) return null;
        if (network.networkKey.type == NetworkKey.TYPE_WIFI) {
            String key = network.networkKey.wifiKey.ssid;
            if (key == null) return null;
            if (network.networkKey.wifiKey.bssid != null) {
                key = key + network.networkKey.wifiKey.bssid;
            }
            return key;
        }
        return null;
    }

    private String buildNetworkKey(ScanResult result) {
        if (result.SSID == null) {
            return null;
        }
        StringBuilder key = new StringBuilder("\"");
        key.append(result.SSID);
        key.append("\"");
        if (result.BSSID != null) {
            key.append(result.BSSID);
        }
        return key.toString();
    }

    @Override protected final void dump(FileDescriptor fd, PrintWriter writer, String[] args) {
        mContext.enforceCallingOrSelfPermission(permission.DUMP, TAG);
        writer.println("WifiNetworkScoreCache");
        writer.println("  All score curves:");
        for (Map.Entry<String, ScoredNetwork> entry : mNetworkCache.entrySet()) {
            writer.println("    " + entry.getKey() + ": " + entry.getValue().rssiCurve);
        }
        writer.println("  Current network scores:");
        WifiManager wifiManager = (WifiManager) mContext.getSystemService(Context.WIFI_SERVICE);
        for (ScanResult scanResult : wifiMan