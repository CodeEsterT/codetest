# Coding test: Promotion Engine
![Tests](https://github.com/CodeEsterT/CodingTest/actions/workflows/dotnet.yml/badge.svg)

This repository contains the code for the promotion engine coding test.

## Description
This test is written in C# with .NET 5.0. The main business logic is contained in the `PromotionEngine.cs`,
which contains the interface for promotions `IPromotion`, an implementation of this promotion `BulkPromotion`,
and the `PromoEngine`, which contains the logic to calculate the price of items in a cart using the pricing-list
and the promotions.

Tests are written in `PromotionEngineTests.cs`.

## Running the tests
This project is tested using GitHub actions, but it can also run locally.
With `dotnet` version 5.0 installed on the system, the tests can be run by executing:
```
$ dotnet test
```

## Author
Jan Meznik

[@JanmanX](https://github.com/JanmanX)

jan@meznik.dk
