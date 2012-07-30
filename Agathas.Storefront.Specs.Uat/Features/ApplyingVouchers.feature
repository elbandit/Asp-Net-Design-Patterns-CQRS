Feature: Applying Promotion Vouchers
	In order to recieve a discount to my basket
	As a customer 
	I want to be able to apply a promotional voucher to my basket 	

Background:
	Given the following promotions
		| VoucherCode | Discount | Quota     | Scope    | BasketTotalMustMeet |
		| XXX-XXX     | 5.00     | Unlimited | HeadGear | 50                  |
		| YYY-YYY     | 10.00    | SingleUse | Clothing | 100                 |
		| ZZZ-ZZZ     | 10%      | SingleUse | All      | 0                   |
	And the following products
		|ProductId	|Price			|Name	|Category		|
		|1			|10.50			|Hat	|HeadGear		|
		|2			|44.65			|Jumper	|Clothing		|			


Scenario: Apply a promotional voucher to a valid basket
	Given I have added the following items to my basket
		| ProductId | 
		| 1			| 
		| 2			| 
	When I apply the promotional voucher 
		| VoucherCode	| 
		| XXX-XXX		| 
	Then the total amount payable on the basket should be £50.15


Scenario: Apply a promotional voucher to a valid basket, then make the basket total come under the voucher threshold
	Given I have added the following items to my basket
		| ProductId | 
		| 1			| 
		| 2			| 
	When I apply the promotional voucher 
		| VoucherCode	| 
		| XXX-XXX		| 
	Then the total amount payable on the basket should be £50.15
	When I remove product id '1'
	Then the total amount payable on the basket should be £44.65
#	And I should be told that "Your basket is below the threshold in order to apply the voucher XXX-XXX."


Scenario: Apply a promotional voucher to an invalid basket
	Given I have added the following items to my basket
		| ProductId | 
		| 1			| 
	When I apply the promotional voucher 
		| VoucherCode	| 
		| XXX-XXX		| 
	Then the total amount payable on the basket should be £10.50
#	And I should be told that "Your basket is below the threshold in order to apply the voucher XXX-XXX."


Scenario: Applying a promotional voucher to a valid basket, which has already had a voucher applied
	Given I have added the following items to my basket
		| ProductId | 
		| 1			| 
		| 2			| 
	When I apply the promotional voucher 
		| VoucherCode	| 
		| XXX-XXX		| 
	Then the total amount payable on the basket should be £50.15
	When I apply the promotional voucher 
		| VoucherCode	| 
		| YYY-YYY		| 
	Then the total amount payable on the basket should be £50.15
#	And I should be told that "Voucher YYY-YYY cannot be used in conjunction with any other voucher."


Scenario: Removing a promotional voucher after applying it to a valid basket
	Given I have added the following items to my basket
		| ProductId | 
		| 1			| 
		| 2			| 
	When I apply the promotional voucher 
		| VoucherCode	| 
		| XXX-XXX		| 
	Then the total amount payable on the basket should be £50.15
	When I remove the promotional voucher 
		| VoucherCode	| 
		| XXX-XXX		| 
	Then the total amount payable on the basket should be £55.15

