# Storekey - campaign price calculator

## Overview

The algorithm can be found in the `StorePrice.cs` file.

The steps of the algorithm is annotated with comments.

## Limitations

The solution is sub optimal.
It will not figure out the best possible combination since that is a fairly complicated problem, that can be resource intensive in the case of big amount of input.
Limitations:

- Volume campaigns are prioritized over the combo campaigns
- One product can only be part of one combo campaign

## Test

Simple test cases are provided to demonstrate the usage of the library.

Run the test with the following command

```bash
dotnet test
```