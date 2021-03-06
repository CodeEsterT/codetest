# Coding test: Promotion Engine
![Tests](https://github.com/CodeEsterT/codetest/actions/workflows/go.yml/badge.svg)

This repository contains the code for the promotion engine coding test.

## Description
This test is written in Go. I have never used this language before, and I thought this was a great opportunity to try and learn the language.

The main logic resides in the `promotion_engine.go`. It contains the interface for the promotions, as well as their implementations.

It also contains the `GetTotal()` function, which recursively calculates the total by applying the available promotions, and removing the items for which a promotion has already been used.


## Running the tests
This project is tested using GitHub actions, but it can also run locally.
With Go installed on the system, the tests can be run by executing:
```
$ go test -v
```

## Author
Jan Meznik

[@JanmanX](https://github.com/JanmanX)

jan@meznik.dk
