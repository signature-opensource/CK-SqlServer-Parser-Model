The abstractions of the T-SQL object definitions and of their parser: stored procedures, functions, views,
transformers and scripts.

`ISqlServerParser` is the parser contract; the parsed objects are `ISqlServerObject` (`ISqlServerStoredProcedure`,
`ISqlServerView`, `ISqlServerFunctionScalar`, `ISqlServerFunctionTable`, `ISqlServerFunctionInlineTable`),
`ISqlServerTransformer` and `ISqlServerScript`.
