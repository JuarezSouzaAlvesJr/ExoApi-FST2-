CREATE DATABASE ExoApi
GO

USE ExoApi
GO

CREATE TABLE Projetos(
	Id INT PRIMARY KEY IDENTITY,
	Titulo VARCHAR(255) UNIQUE NOT NULL,
	Situacao VARCHAR(255) NOT NULL,
	DataDeInicio VARCHAR(50) NOT NULL,
	Requisitos VARCHAR(255) NOT NULL
)
GO

CREATE TABLE Usuarios(
	Id INT PRIMARY KEY IDENTITY,
	Email VARCHAR(100) UNIQUE NOT NULL,
	Senha VARCHAR(255) NOT NULL,
	Tipo CHAR(1) NOT NULL
)
GO

INSERT INTO Projetos (Titulo, Situacao, DataDeInicio, Requisitos) 
VALUES ('Cria��o de banco de dados da empresa Dataq', 'Em desenvolvimento', '01/07/2022', 'Cada usu�rio do jogo poder� ter um personagem exclusivo; O personagem possuir� uma classe; Cada classe pode possuir uma ou mais habilidades.')
GO

INSERT INTO Projetos (Titulo, Situacao, DataDeInicio, Requisitos) 
VALUES ('Cria��o de API da biblioteca p�blica', 'A fazer', '31/07/2022', 'Disponibilizar os recursos para leitura, escrita, atualiza��o e dele��o dos livros do banco de dados; Restringir o acesso aos recursos da API aos usu�rios autenticados.')
GO

INSERT INTO Usuarios VALUES ('email_andre@email.com', '1234', 1)
GO

INSERT INTO Usuarios VALUES ('email_tiago@email.com','1234', 2)
GO

SELECT * FROM Usuarios