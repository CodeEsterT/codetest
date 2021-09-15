package promotion_engine

import (
	"reflect"
	"testing"
)

func TestIsSubsetTrue(t *testing.T) {
	a := []string{"A", "A", "B", "C", "C"}
	s := []string{"A", "C", "C"}
	if !IsSubset(a, s) {
		t.Fatalf("IsSubset returned false. Expected: true.")
	}

	// Slightly different ordering
	a = []string{"A", "A", "B", "C", "C"}
	s = []string{"C", "A", "C", "B"}
	if !IsSubset(a, s) {
		t.Fatalf("IsSubset returned false. Expected: true.")
	}

	// Empty subset
	a = []string{"A", "A", "B", "C", "C"}
	s = []string{}
	if !IsSubset(a, s) {
		t.Fatalf("IsSubset returned false. Expected: true.")
	}

	// Empty sets
	a = []string{}
	s = []string{}
	if !IsSubset(a, s) {
		t.Fatalf("IsSubset returned false. Expected: true.")
	}
}

func TestIsSubsetFalse(t *testing.T) {
	a := []string{"A", "A", "B", "C", "C"}
	s := []string{"A", "C", "C", "D"}
	if IsSubset(a, s) {
		t.Fatalf("IsSubset returned false. Expected: true.")
	}

	// Empty set
	a = []string{}
	s = []string{"C", "A", "C", "B"}
	if IsSubset(a, s) {
		t.Fatalf("IsSubset returned false. Expected: true.")
	}

	// Different sets
	a = []string{"A", "A", "B", "C", "C"}
	s = []string{"X", "Y", "Z"}
	if IsSubset(a, s) {
		t.Fatalf("IsSubset returned false. Expected: true.")
	}

	// Duplicate elements
	a = []string{"A", "A"}
	s = []string{"A", "A", "A"}
	if IsSubset(a, s) {
		t.Fatalf("IsSubset returned false. Expected: true.")
	}
}

func TestSubtractSlices(t *testing.T) {
	a := []string{"A", "A", "B", "C", "C"}
	s := []string{"A", "C", "C"}
	expected := []string{"A", "B"}

	subtracted := SubtractSlices(a, s)
	if !reflect.DeepEqual(subtracted, expected) {
		t.Fatalf("Subtracted set is not what is expected.")
	}

	// Same arrays
	a = []string{"A", "C", "C"}
	s = []string{"A", "C", "C"}
	expected = []string{}

	subtracted = SubtractSlices(a, s)
	if !reflect.DeepEqual(subtracted, expected) {
		t.Fatalf("Subtracted set is not what is expected.")
	}

	// Empty array
	a = []string{"A", "C", "C"}
	s = []string{}
	expected = []string{"A", "C", "C"}

	subtracted = SubtractSlices(a, s)
	if !reflect.DeepEqual(subtracted, expected) {
		t.Fatalf("Subtracted set is not what is expected.")
	}

	// Both empty
	a = []string{}
	s = []string{}
	expected = []string{}

	subtracted = SubtractSlices(a, s)
	if !reflect.DeepEqual(subtracted, expected) {
		t.Fatalf("Subtracted set is not what is expected.")
	}

}
