package promotion_engine

import (
	"testing"
)

var price_table = map[string]int{
	"A": 50,
	"B": 30,
	"C": 20,
	"D": 15,
}

var active_promotions = []PromotionApplier{
	BulkPromotion{
		Promotion{
			Price: 130,
			SKUs:  []string{"A", "A", "A"},
		},
	},
	BulkPromotion{
		Promotion{
			Price: 45,
			SKUs:  []string{"B", "B"},
		},
	},
	BulkPromotion{
		Promotion{
			Price: 30,
			SKUs:  []string{"C", "D"},
		},
	},
}

func TestScenarioA(t *testing.T) {
	order := []string{"A", "B", "C"}
	expected_total := 100

	total, _ := GetTotal(price_table, active_promotions, order)

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

	total, _ := GetTotal(price_table, active_promotions, order)

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

	total, _ := GetTotal(price_table, active_promotions, order)

	if total != expected_total {
		t.Fatalf("Wrong total. Expected: %d, got: %d", expected_total, total)
	}
}

func TestNoPromotions(t *testing.T) {
	order := []string{"A", "B", "C"}
	expected_total := 100

	total, _ := GetTotal(price_table, active_promotions, order)

	if total != expected_total {
		t.Fatalf("Wrong total. Expected: %d, got: %d", expected_total, total)
	}
}

func TestEmptyCart(t *testing.T) {
	order := []string{}
	expected_total := 0

	total, _ := GetTotal(price_table, active_promotions, order)

	if total != expected_total {
		t.Fatalf("Wrong total. Expected: %d, got: %d", expected_total, total)
	}
}
