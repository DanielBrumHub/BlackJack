/*----------------------------------
Script Criação Tabelas
Autor: Daniel Brum
Data: 04/02/2023
------------------------------------*/
USE blackjack;

DROP TABLE IF EXISTS tbl_jogadas;
DROP TABLE IF EXISTS tbl_jogos;
DROP TABLE IF EXISTS tbl_nipes;
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

CREATE TABLE tbl_nipes (
    Id int identity(1,1) primary key,
    Descricao varchar(20)
);

INSERT INTO tbl_nipes (Descricao) VALUES ('Paus'),
										 ('Copas'),
										 ('Ouros'),
										 ('Espadas');

CREATE TABLE tbl_jogos (
    Id int identity(1,1) primary key,
    Descricao varchar(20),
	IdtEncerrado int
);

CREATE TABLE tbl_jogadas (
    Id int identity(1,1) primary key,
	IdtDealer int,
	IdCarta int,
	IdNipe int,
    IdJogo int
);


