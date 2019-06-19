CREATE TABLE enderecos
(
	id INT PRIMARY KEY IDENTITY(1,1),
	unidade_federativa VARCHAR(2),
	cidade VARCHAR(100),
	logradouro VARCHAR(100),
	cep VARCHAR(100),
	numero INT,
	complemento TEXT
);