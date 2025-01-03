CREATE VIEW Relatorio AS
SELECT [l].[Codigo], [l].[AnoPublicacao], [l].[Edicao], [l].[Editora], [l].[Titulo], [s].[CodigoLivro], [s].[CodigoAutor], [s].[Codigo], [s].[Nome], [s0].[CodigoLivro], [s0].[CodigoAssunto], [s0].[Codigo], [s0].[Descricao]
FROM [Livro] AS [l]
LEFT JOIN (
    SELECT [l0].[CodigoLivro], [l0].[CodigoAutor], [a].[Codigo], [a].[Nome]
    FROM [Livro_Autor] AS [l0]
    INNER JOIN [Autor] AS [a] ON [l0].[CodigoAutor] = [a].[Codigo]
) AS [s] ON [l].[Codigo] = [s].[CodigoLivro]
LEFT JOIN (
    SELECT [l1].[CodigoLivro], [l1].[CodigoAssunto], [a0].[Codigo], [a0].[Descricao]
    FROM [Livro_Assunto] AS [l1]
    INNER JOIN [Assunto] AS [a0] ON [l1].[CodigoAssunto] = [a0].[Codigo]
) AS [s0] ON [l].[Codigo] = [s0].[CodigoLivro]
