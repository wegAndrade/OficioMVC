
DROP TABLE IF EXISTS `documento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `documento` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Numeracao` int NOT NULL,
  `Ano` int NOT NULL,
  `Status` int NOT NULL,
  `Assunto` varchar(2500) NOT NULL,
  `Observacoes` varchar(2500) NOT NULL,
  `Tipo` int NOT NULL,
  `CaminhoArq` longtext,
  `DataEnvio` datetime(6) NOT NULL,
  `DataAlteracao` datetime(6) DEFAULT NULL,
  `UsuarioId` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Documento_Numeracao_Ano` (`Numeracao`,`Ano`),
  KEY `IX_Documento_UsuarioId` (`UsuarioId`),
  CONSTRAINT `FK_Documento_Siga_profs_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `siga_profs` (`ID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


ALTER TABLE siga_profs add COLUMN `master`  bit(1) NOT NULL DEFAULT b'0';