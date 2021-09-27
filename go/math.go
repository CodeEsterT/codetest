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

func SubtractSlices(a []string, s []string) []string {
	a_dict := make(map[string]int)
	for _, v := range a {
		a_dict[v] += 1
	}

	for _, v := range s {
		a_dict[v] -= 1
	}

	result := []string{}

	for key, value := range a_dict {
		for i := 0; i < value; i++ {
			result = append(result, key)
		}
	}

	return result
}
