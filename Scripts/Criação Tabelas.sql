/*----------------------------------
Script Criação Tabelas
Autor: Daniel Brum
Data: 04/02/2023
------------------------------------*/


DROP TABLE IF EXISTS tbl_cartas;
CREATE TABLE tbl_cartas (
    Id int identity(1,1) primary key,
    Descricao varchar(20),
	Valor int
);

INSERT INTO tbl_cartas (Descricao, Valor) VALUES ('Ás', 1),
												 ('2', 2),
												 ('3', 3),
												 ('4', 4),
												 ('5', 5),
												 ('6', 6),
												 ('7', 7),
												 ('8', 8),
												 ('9', 9),
												 ('10', 10),
												 ('Valete', 10),
												 ('Dama', 10),
												 ('Rei', 10);

DROP TABLE IF EXISTS tbl_nipes;
CREATE TABLE tbl_nipes (
    Id int identity(1,1) primary key,
    Descricao varchar(20)
);

INSERT INTO tbl_nipes (Descricao) VALUES ('Paus'),
										 ('Copas'),
										 ('Ouros'),
										 ('Espadas');

DROP TABLE IF EXISTS tbl_jogos;
CREATE TABLE tbl_jogos (
    Id int identity(1,1) primary key,
    Descricao varchar(20)
);

DROP TABLE IF EXISTS tbl_jogadas;
CREATE TABLE tbl_jogadas (
    Id int identity(1,1) primary key,
	IdCarta int,
	IdNipe int,
    IdJogo int,
	FOREIGN KEY (IdCarta) REFERENCES tbl_cartas(Id),
	FOREIGN KEY (IdNipe) REFERENCES tbl_nipes(Id),
	FOREIGN KEY (IdJogo) REFERENCES tbl_jogos(Id)
);
