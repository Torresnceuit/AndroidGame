   final int persistedModeFlags;
        final long persistedCreateTime;

        private Snapshot(UriPermission perm) {
            this.targetUserId = perm.targetUserId;
            this.sourcePkg = perm.sourcePkg;
            this.targetPkg = perm.targetPkg;
            this.uri = perm.uri;
            this.persistedModeFlags = perm.persistedModeFlags;
            this.persistedCreateTime = perm.persistedCreateTime;
        }
    }

    public Snapshot snapshot() {
        return new Snapshot(this);
    }

    public android.content.UriPermission buildPersistedPublicApiObject() {
        return new android.content.UriPermission(uri.uri, persistedModeFlags, persistedCreateTime);
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              