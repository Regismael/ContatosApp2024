﻿CREATE TABLE CONTATO(
ID                  UNIQUEIDENTIFIER              PRIMARY KEY,
NOME                VARCHAR(150)                  NOT NULL,
EMAIL               VARCHAR(50)                   NOT NULL,
TELEFONE            VARCHAR(20)                   NOT NULL,
DATAHORACADASTRO    DATETIME                      NOT NULL,
ATIVO               INT                           NOT NULL DEFAULT 1);
GO
