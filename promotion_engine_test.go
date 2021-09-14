package promotion_engine

import (
	"testing"
)

func TestScenarioA(t *testing.T) {
	order := []string{"A", "B", "C"}
	expected_total := 100

	total, _ := GetTotal(order)

	if total != expected_total {
		t.Fatalf("Wrong total. Got: %d, expected: %d", total, expected_total)
	}
}
