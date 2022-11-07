using API.ConfigBD;

namespace API.DBObjects
{
    public class Utilitarios
    {
        #region TABELAS DO BANCO
        public string TabelaCliente()
        {
            return @"CREATE TABLE [dbo].[Cliente]
                        (

                            [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
                            [nome] NVARCHAR(100) NOT NULL,
                            [cpf] NVARCHAR(11) NULL UNIQUE,
                            [sexo] NVARCHAR(1) NULL,
                            [tipoCliente] int FOREIGN KEY REFERENCES TipoCliente(Id),
                            [situacaoCliente] int FOREIGN KEY REFERENCES SituacaoCliente(Id)
                        );";
        }

        public string TabelaSituacaoCliente()
        {
            return @"CREATE TABLE [dbo].[SituacaoCliente]
                        (
	                        [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
                            [situacaoCliente] NVARCHAR(100) NOT NULL
                        );";
        }

        public string TabelaTipoCliente()
        {
            return @"CREATE TABLE [dbo].[TipoCliente]
                        (
	                        [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
                            [tipoCliente] NVARCHAR(100) NOT NULL
                        );";
        }
        #endregion

        #region PROCEDURES DO BANCO
        public string ProcCliente()
        {
			return @"CREATE OR ALTER PROCEDURE [dbo].[PROCCLIENTES]
							@id int,
							@nome NVARCHAR(100),
							@cpf NVARCHAR(11),
							@sexo NVARCHAR(1),
							@TipoCliente int,
							@situacaoCliente int,
							@utilizacao NVARCHAR(1) AS
						BEGIN

							IF @utilizacao = 'L'
	
								SELECT T0.[Id]
									 , T0.[nome]
									 , REPLICATE('0', 11 - LEN(T0.[cpf])) + RTrim(T0.[cpf]) AS ""cpf""
									 , CASE WHEN T0.[sexo] = 'M' THEN 'Masculino' ELSE 'Feminino' END AS ""sexo""
									 , T1.situacaoCliente
									 , T2.TipoCliente
								FROM [dbo].[Cliente] AS T0
								LEFT JOIN [dbo].[SituacaoCliente] AS T1 ON T0.[situacaoCliente] = T1.Id
								LEFT JOIN [dbo].[TipoCliente] AS T2 ON T0.tipoCliente = T2.Id
								WHERE T0.[nome] LIKE '%' + ISNULL(@nome,T0.[nome]) + '%'
									AND T0.[cpf] LIKE '%' + ISNULL(@cpf,T0.[cpf]) + '%'
									AND T0.[sexo] = ISNULL(@sexo,T0.[sexo])
									AND T0.[Id] = ISNULL(@id,T0.[Id])

							IF @utilizacao = 'I' AND ISNULL(@id,0) = 0
								INSERT INTO [dbo].[Cliente] ([nome],[cpf],[sexo],[tipoCliente],[situacaoCliente]) VALUES (@nome,@cpf,@sexo,@TipoCliente,@situacaoCliente)

							IF @utilizacao = 'U' AND ISNULL(@id,0) > 0
								UPDATE [dbo].[Cliente]
								   SET [nome] = ISNULL(@nome,[nome])
									  ,[cpf] = ISNULL(@cpf,[cpf])
									  ,[sexo] = ISNULL(@sexo,[sexo])
									  ,[tipoCliente] = ISNULL(@TipoCliente,[tipoCliente])
									  ,[situacaoCliente] = ISNULL(@situacaoCliente,[situacaoCliente])
								 WHERE [id] = @id

							IF @utilizacao = 'D' AND ISNULL(@id,0) > 0
								DELETE FROM [dbo].[Cliente]
								WHERE [id] = @id
						END;";
        }

        public string ProcSituacaoCliente()
        {
            return @"CREATE OR ALTER PROCEDURE [dbo].[PROCSITUACAO]
	                    @id int,
	                    @situacaoCliente NVARCHAR(100),
	                    @utilizacao NVARCHAR(1) AS
                    BEGIN

	                    IF @utilizacao = 'L'	
		                    SELECT * FROM [dbo].[SituacaoCliente]
		                    WHERE [situacaoCliente] LIKE '%' + ISNULL(@situacaoCliente,[situacaoCliente]) + '%'
			                    AND [Id] = ISNULL(@id,[Id])

	                    IF @utilizacao = 'I' AND ISNULL(@id,0) = 0
		                    INSERT INTO [dbo].[SituacaoCliente] ([situacaoCliente]) VALUES (@situacaoCliente)

	                    IF @utilizacao = 'A' AND ISNULL(@id,0) > 0
		                    UPDATE [dbo].[SituacaoCliente]
		                       SET [situacaoCliente] = ISNULL(@situacaoCliente,[situacaoCliente])
		                     WHERE [Id] = @id

	                    IF @utilizacao = 'E' AND ISNULL(@id,0) > 0
		                    DELETE FROM [dbo].[SituacaoCliente]
		                    WHERE [Id] = @id
                    END";
        }

        public string ProTipoCliente()
        {
            return @"CREATE OR ALTER PROCEDURE [dbo].[PROCTIPO]
	                    @id int,
	                    @tipoCliente NVARCHAR(100),
	                    @utilizacao NVARCHAR(1) AS
                    BEGIN

	                    IF @utilizacao = 'L'	
		                    SELECT * FROM [dbo].[TipoCliente]
		                    WHERE [tipoCliente] LIKE '%' + ISNULL(@tipoCliente,[tipoCliente]) + '%'
			                    AND [Id] = ISNULL(@id,[id])

	                    IF @utilizacao = 'I' AND ISNULL(@id,0) = 0
		                    INSERT INTO [dbo].[TipoCliente] ([tipoCliente]) VALUES (@tipoCliente)

	                    IF @utilizacao = 'A' AND ISNULL(@id,0) > 0
		                    UPDATE [dbo].[TipoCliente]
		                       SET [tipoCliente] = ISNULL(@tipoCliente,[tipoCliente])
		                     WHERE [Id] = @id

	                    IF @utilizacao = 'E' AND ISNULL(@id,0) > 0
		                    DELETE FROM [dbo].[TipoCliente]
		                    WHERE [Id] = @id
                    END";
        }
        #endregion
    }
}
