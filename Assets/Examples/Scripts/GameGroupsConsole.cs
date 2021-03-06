 the number of mappings in this IdentityHashMap.
     */
    @Override
    public int size() {
        return size;
    }

    private void writeObject(ObjectOutputStream stream) throws IOException {
        stream.defaultWriteObject();
        stream.writeInt(size);
        Iterator<?> iterator = entrySet().iterator();
        while (iterator.hasNext()) {
            MapEntry<?, ?> entry = (MapEntry) iterator.next();
            stream.writeObject(entry.key);
            stream.writeObject(entry.value);
        }
    }

    @SuppressWarnings("unchecked")
    private void readObject(ObjectInputStream stream) throws IOException,
            ClassNotFoundException {
        stream.defaultReadObject();
        int savedSize = stream.readInt();
        threshold = getThreshold(DEFAULT_MAX_SIZE);
        elementData = newElementArray(computeElementArraySize());
        for (int i = savedSize; --i >= 0;) {
            K key = (K) stream.readObject();
            put(key, (V) stream.readObject());
        }
        size = savedSize;
    }

    private void putAllImpl(Map<? extends K, ? extends V> map) {
        if (map.entrySet() != null) {
            super.putAll(map);
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    