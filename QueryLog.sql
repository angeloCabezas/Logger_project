USE [master]
GO

IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'BDLOGGER')
DROP DATABASE [BDLOGGER]
GO

CREATE DATABASE BDLOGGER
GO

USE BDLOGGER
GO

CREATE TABLE TBL_LOG (
	IdLoger int identity(1,1) primary Key,
	messageLog varchar(max),
	IdTipoMensaje int
)
GO

CREATE PROCEDURE usp_InsertTBL_LOG
@messageLog varchar(max), @IdTipoMensaje int
AS 
INSERT INTO TBL_LOG (messageLog,IdTipoMensaje) values (@messageLog,@IdTipoMensaje)
GO