package promotion_engine

import (
	"sort"
)

func IsSubset(a []string, s []string) bool {
	// Sort arrays
	sort.Strings(a)
	sort.Strings(s)

	for len(s) > 0 {
		if len(a) == 0 {
			return false
		} else if a[0] == s[0] {
			a = a[1:]
			s = s[1:]
		} else {
			a = a[1:]
		}
	}

	return true
}

func RemoveSubset(a []string, s []string) []string {
	return nil
}
