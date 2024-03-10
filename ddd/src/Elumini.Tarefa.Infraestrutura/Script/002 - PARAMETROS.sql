use Elumini;

-- Ativar o IDENTITY_INSERT
SET IDENTITY_INSERT [dbo].[Parametros] ON;

insert [dbo].[Parametros] ([id], [Chave], [Valor], [Order]) values
(1, 'TarefaStatus', 'To do', 1),
(2, 'TarefaStatus', 'Doing', 2),
(3, 'TarefaStatus', 'Done', 3);

-- Desativar o IDENTITY_INSERT
SET IDENTITY_INSERT [dbo].[Parametros] OFF;

select * from Parametros