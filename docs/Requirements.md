# Vending Machine Functional Specification #

## Purpose/Responsibilities
A .NET application that mimics the functionality of a coffee vending machine. Provides the means of ordering one or more customizable cups of coffee, providing payment, receiving change, and dispensing the correct product.

## Functionality/Tasks performed
- Order coffee in 3 sizes (small for $1.75, medium for $2.00, large for $2.25).
- Allow adding sugar and cream in discrete increments to each coffee order (from 1 – 3 sugars ($0.25 each) and from 1 – 3 creamers ($0.50 each)).
- “Dispense” coffee when an order is completed, only if adequate payment has been provided and display money remaining in payment transaction.
- Give warning if adequate payment has not been provided.
- Allow multiple coffee orders per payment transaction.
- Allow adding money in standard monetary increments (from $0.05 to $20).
- Allow user to specify the end of a payment transaction and dispense change.
- Developer should provide a minimum set of passing unit tests
