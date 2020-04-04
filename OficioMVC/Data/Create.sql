-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: developdb
-- ------------------------------------------------------
-- Server version	8.0.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20200330213551_testes','2.1.14-servicing-32113'),('20200330225303_VarChar','2.1.14-servicing-32113'),('20200330225514_VarCharAll','2.1.14-servicing-32113');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `documento`
--

DROP TABLE IF EXISTS `documento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `documento` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Numeracao` int NOT NULL,
  `Ano` int NOT NULL,
  `Status` int NOT NULL,
  `Assunto` varchar(2500) NOT NULL,
  `Observacoes` varchar(2500) DEFAULT NULL,
  `Tipo` int NOT NULL,
  `CaminhoArq` longtext,
  `DataEnvio` datetime(6) NOT NULL,
  `DataAlteracao` datetime(6) NOT NULL,
  `UsuarioId` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Documento_Numeracao_Ano` (`Numeracao`,`Ano`),
  KEY `IX_Documento_UsuarioId` (`UsuarioId`),
  CONSTRAINT `FK_Documento_Siga_profs_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `siga_profs` (`ID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `documento`
--

LOCK TABLES `documento` WRITE;
/*!40000 ALTER TABLE `documento` DISABLE KEYS */;
INSERT INTO `documento` VALUES (1,1,2020,0,'Teste de desenvolvimento para criação de ambiente de homologação','Observação de teste',0,'1_2020.PNG','2020-03-30 18:38:02.000000','2020-03-30 18:53:55.046643',1),(2,2,2020,0,'Sou um Edital do Ambiente de desenvolvimento','Observação de Desenvolvimento',0,NULL,'2020-03-30 18:38:02.377071','0001-01-01 00:00:00.000000',1),(3,3,2020,1,'Sou um Edital Externo do Ambiente de desenvolvimento','Observação de Desenvolvimento',2,NULL,'2020-03-30 18:38:02.377071','0001-01-01 00:00:00.000000',1),(4,4,2020,1,'Sou um Edital Interno do Ambiente de desenvolvimento','Observação de Desenvolvimento',1,NULL,'2020-03-30 18:38:02.377071','0001-01-01 00:00:00.000000',1),(5,5,2020,1,'Sou um Oficio do Ambiente de desenvolvimento','Observação de Desenvolvimento',3,NULL,'2020-03-30 18:38:02.000000','2020-03-30 18:52:04.960725',1),(6,6,2020,1,'Sou um Memorando  do Ambiente de desenvolvimento','Observação de Desenvolvimento',4,NULL,'2020-03-30 18:38:02.377071','0001-01-01 00:00:00.000000',1),(7,7,2020,1,'Sou uma Portaria  do Ambiente de desenvolvimento','Observação de Desenvolvimento',5,NULL,'2020-03-30 18:38:02.377071','0001-01-01 00:00:00.000000',1);
/*!40000 ALTER TABLE `documento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `siga_profs`
--

DROP TABLE IF EXISTS `siga_profs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `siga_profs` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `user_login` longtext NOT NULL,
  `user_pass` longtext NOT NULL,
  `user_nicename` longtext,
  `ativo` longtext,
  `dpto` longtext,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `siga_profs`
--

LOCK TABLES `siga_profs` WRITE;
/*!40000 ALTER TABLE `siga_profs` DISABLE KEYS */;
INSERT INTO `siga_profs` VALUES (1,'teste','698dc19d489c4e4db73e28a713eab07b','Teste','ATIVO','Administracao');
/*!40000 ALTER TABLE `siga_profs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'developdb'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-03-30 19:57:33
