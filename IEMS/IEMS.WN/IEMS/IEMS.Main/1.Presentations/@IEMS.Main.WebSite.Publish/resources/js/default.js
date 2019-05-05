function endWith(s1, s2) {
    if (s1.length < s2.length)
        return false;
    if (s1 == s2)
        return true;
    if (s1.substring(s1.length - s2.length) == s2)
        return true;
    return false;
}