nce of the separator, while there's enough space.
        ArrayList<String> list = new ArrayList<String>();
        Matcher matcher = new Matcher(pattern, input);
        int begin = 0;
        while (list.size() + 1 != limit && matcher.find()) {
            list.add(input.substring(begin, matcher.start()));
            begin = matcher.end();
        }
        return finishSplit(list, input, begin, limit);
    }

    private static String[] finishSplit(List<String> list, String input, int begin, int limit) {
        // Add trailing text.
        if (begin < input.length()) {
            list.add(input.substring(begin));
        } else if (limit != 0) {
            list.add("");
        } else {
            // Remove all trailing empty matches in the limit == 0 case.
            int i = list.size() - 1;
            while (i >= 0 && list.get(i).isEmpty()) {
                list.remove(i);
                i--;
            }
        }
        // Convert to an array.
        return list.toArray(new String[list.size()]);
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               