package promotion_engine

// The interface to use for promotions
type PromotionApplier interface {
	IsApplicable(items []string) bool
	Apply(items []string) ([]string, int)
}

// Base promotion struct. This can be extended (embedded)
type Promotion struct {
	Price int
	SKUs  []string
}

// Combined promotion
type CombinedPromotion struct {
	Promotion
}

// IsApplicable tests if the promotion is applicable on the array of items
func (promotion CombinedPromotion) IsApplicable(items []string) bool {
	return IsSubset(items, promotion.SKUs)
}

// Apply will calculate the subtotal for the items after applying the promotion.
// Items on which the promotion was not applied will be returned.
func (promotion CombinedPromotion) Apply(items []string) ([]string, int) {
	// Test if we can apply promotion by checking whether all the required items are
	// present in the items list
	if !IsSubset(items, promotion.SKUs) {
		return items, 0
	}

	// Apply the promotion by consuming the items and returning the price
	return SubtractSlices(items, promotion.SKUs), promotion.Price
}

// GetTotal calculates and returns the total for items using the price_table and the promotions.
// It does so by testing for applicability of the available promotions, and recursively calling itself.
func GetTotal(price_table map[string]int, promotions []PromotionApplier, items []string) (int, error) {
	// Iterate over all promotions
	for i := 0; i < len(promotions); i++ {
		if promotions[i].IsApplicable(items) {
			// Apply promotion and recursively get the subtotal for the rest of the order
			sub_items, promo_total := promotions[i].Apply((items))
			sub_total, err := GetTotal(price_table, promotions, sub_items)

			// Test for and return errors
			if err != nil {
				return 0, err
			}

			// No error. Return the total
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
