package promotion_engine

type Promotion interface {
	IsApplicable(items []string) bool
	Apply(items []string) ([]string, int)
}

func GetTotal(price_table map[string]int, promotions []Promotion, items []string) (int, error) {
	// Iterate over all promotions
	for i := 0; i < len(promotions); i++ {
		// Test if we can apply them
		if promotions[i].IsApplicable(items) {
			// Apply promotion
			sub_items, promo_total := promotions[i].Apply((items))

			// Recursively get the total for the rest of the order
			sub_total, err := GetTotal(price_table, promotions, sub_items)

			// Test for errors
			if err != nil {
				return 0, err
			}

			// Return
			return sub_total + promo_total, nil
		}
	}

	// If we reached here, there are no applicable promotions
	// Sum up the rest of the items and return
	total := 0
	for i := 0; i < len(items); i++ {
		total += price_table[items[i]]
	}
	return total, nil
}
