CREATE TABLE clientes_pessoa_juridica 
(
	id INT PRIMARY KEY IDENTITY(1,1),
	cnpj VARCHAR(100),
	razao_social VARCHAR(100),
	inscricao_estadual VARCHAR(100)
);

SELECT * FROM clientes_pessoa_juridica;