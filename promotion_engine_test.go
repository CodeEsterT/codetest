package promotion_engine

import (
	"testing"
)

func TestScenarioA(t *testing.T) {
	order := []string{"A", "B", "C"}
	expected_total := 100

	total, _ := GetTotal(order)

	if total != expected_total {
		t.Fatalf("Wrong total. Expected: %d, got: %d", expected_total, total)
	}
}

func TestScenarioB(t *testing.T) {
	order := []string{"A", "A", "A", "A", "A",
		"B", "B", "B", "B", "B",
		"C",
	}
	expected_total := 370

	total, _ := GetTotal(order)

	if total != expected_total {
		t.Fatalf("Wrong total. Expected: %d, got: %d", expected_total, total)
	}
}

func TestScenarioC(t *testing.T) {
	order := []string{"A", "A", "A",
		"B", "B", "B", "B", "B",
		"C",
		"D",
	}
	expected_total := 280

	total, _ := GetTotal(order)

	if total != expected_total {
		t.Fatalf("Wrong total. Expected: %d, got: %d", expected_total, total)
	}
}
