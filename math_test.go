package promotion_engine

import (
	"testing"
)

func TestIsSubset(t *testing.T) {
	a := []string{"A", "A", "B", "C", "C"}
	s := []string{"A", "C", "C"}

	if !IsSubset(a, s) {
		t.Fatalf("IsSubset returned false. Expected: true.")
	}
}
