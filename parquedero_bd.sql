-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: localhost    Database: parqueadero
-- ------------------------------------------------------
-- Server version	8.0.41

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
-- Table structure for table `configuraciontarifas`
--

DROP TABLE IF EXISTS `configuraciontarifas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `configuraciontarifas` (
  `TipoVehiculo` varchar(20) NOT NULL,
  `TarifaPorHora` decimal(10,2) NOT NULL,
  `TarifaPorDia` decimal(10,2) NOT NULL,
  `TarifaMensual` decimal(10,2) NOT NULL,
  `TarifaLavada` decimal(10,2) NOT NULL,
  PRIMARY KEY (`TipoVehiculo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `configuraciontarifas`
--

LOCK TABLES `configuraciontarifas` WRITE;
/*!40000 ALTER TABLE `configuraciontarifas` DISABLE KEYS */;
INSERT INTO `configuraciontarifas` VALUES ('Camioneta',5000.00,30000.00,80000.00,25000.00),('Carro',4000.00,15000.00,70000.00,20000.00),('Miscelaneo',30000.00,40000.00,50000.00,60000.00),('Moto',2000.00,15000.00,50000.00,15000.00);
/*!40000 ALTER TABLE `configuraciontarifas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ingresos`
--

DROP TABLE IF EXISTS `ingresos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ingresos` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Placa` varchar(15) NOT NULL,
  `TipoServicio` enum('Parqueadero','Mensualidad','Lavada') NOT NULL,
  `Costo` decimal(10,2) NOT NULL,
  `Fecha` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ingresos`
--

LOCK TABLES `ingresos` WRITE;
/*!40000 ALTER TABLE `ingresos` DISABLE KEYS */;
INSERT INTO `ingresos` VALUES (1,'ORT19D','Parqueadero',2000.00,'2025-05-28 18:58:51'),(2,'Chaza Azul','Mensualidad',70000.00,'2025-05-28 19:00:30'),(3,'FGU697','Mensualidad',70000.00,'2025-05-28 19:01:05'),(4,'ORT19D','Mensualidad',50000.00,'2025-05-28 19:01:23'),(5,'Chaza prueba','Mensualidad',30000.00,'2025-06-04 19:57:36'),(6,'Chaza Prueba 2','Mensualidad',40000.00,'2025-06-04 20:00:05'),(7,'Chaza 3','Mensualidad',50000.00,'2025-06-04 20:01:13'),(8,'chaxa4','Mensualidad',60000.00,'2025-06-04 20:02:08'),(9,'Chaza Azul','Mensualidad',50000.00,'2025-06-04 20:03:13'),(10,'mona','Mensualidad',30000.00,'2025-06-04 20:20:35'),(11,'mona','Mensualidad',30000.00,'2025-06-04 20:20:55'),(12,'dddd','Parqueadero',5000.00,'2025-06-04 20:25:41'),(13,'aasaas','Mensualidad',80000.00,'2025-06-04 20:26:05'),(14,'aasaas','Mensualidad',80000.00,'2025-06-04 20:26:31'),(15,'sdlpvmsd','Lavada',25000.00,'2025-06-04 20:28:41'),(16,'PTU89H','Parqueadero',105000.00,'2025-06-06 20:49:45'),(17,'e','Parqueadero',105000.00,'2025-06-06 20:49:48'),(18,'Prueba rata','Mensualidad',20000.00,'2025-06-06 21:05:56'),(19,'Prueba rata','Mensualidad',20000.00,'2025-06-06 21:06:39'),(20,'asa','Mensualidad',10000.00,'2025-06-06 21:07:23'),(21,'IHK88E','Parqueadero',2000.00,'2025-09-11 08:27:08'),(22,'Prueba rata','Mensualidad',20000.00,'2025-09-11 08:27:39'),(23,'ort19d','Parqueadero',2000.00,'2026-02-06 21:10:50'),(24,'dvsdvs','Parqueadero',4000.00,'2026-02-06 21:16:23'),(25,'Chaza Azul','Mensualidad',0.00,'2026-02-06 22:27:43');
/*!40000 ALTER TABLE `ingresos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mensualidades`
--

DROP TABLE IF EXISTS `mensualidades`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mensualidades` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Placa` varchar(15) NOT NULL,
  `Tipo` varchar(20) NOT NULL,
  `Propietario` varchar(100) NOT NULL,
  `Telefono` varchar(20) NOT NULL,
  `FechaInicio` date NOT NULL,
  `FechaFin` date NOT NULL,
  `EstadoPago` varchar(20) NOT NULL,
  `Costo` decimal(10,2) NOT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `mensualidades_chk_1` CHECK ((`EstadoPago` in (_utf8mb4'Pagado',_utf8mb4'Pendiente')))
) ENGINE=InnoDB AUTO_INCREMENT=67 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mensualidades`
--

LOCK TABLES `mensualidades` WRITE;
/*!40000 ALTER TABLE `mensualidades` DISABLE KEYS */;
INSERT INTO `mensualidades` VALUES (1,'Chaza Azul','Miscelaneo','Marleny Marin','2008007080','2026-02-06','2026-03-06','Pagado',0.00),(4,'Chaza prueba','Miscelaneo','Gato','2001003654','2025-06-04','2025-07-04','Pagado',0.00),(5,'Chaza Prueba 2','Miscelaneo','lala','5698569856','2025-06-04','2025-07-04','Pagado',0.00),(6,'Chaza 3','Miscelaneo','ñañañ','47854785','2025-06-04','2025-07-04','Pagado',0.00),(7,'chaxa4','Miscelaneo','skvos','242424','2025-06-04','2025-07-04','Pagado',0.00),(8,'mona','Miscelaneo','askdanao','6858646','2025-06-04','2025-07-04','Pagado',30000.00),(9,'aasaas','Camioneta','asasa','526416','2025-06-04','2025-07-04','Pagado',80000.00),(10,'GHY504','Carro','Victor Manuel Zapata','3007281849','2025-05-17','2025-06-17','Pagado',150000.00),(11,'MFY358','Carro','Luz Nelly Toro','3113543366','2025-05-17','2025-06-17','Pagado',150000.00),(12,'JCN760','Carro','Ali','3128356572','2025-05-30','2025-06-30','Pagado',150000.00),(13,'PBJ353','Carro','Oscar Guarin','3155295538','2025-05-22','2025-06-22','Pagado',150000.00),(14,'DLX402','Carro','Samuel Arredondo Marin','3004656240','2025-05-24','2025-06-24','Pagado',150000.00),(15,'GGG22E','Moto','Jaime Alberto Uribe Rico','3012821345','2025-05-16','2025-06-16','Pagado',50000.00),(16,'JLB73H','Moto','Samuel Arredondo Marin','3004656240','2025-05-24','2025-06-24','Pagado',50000.00),(17,'DEX28G','Moto','','','2025-05-30','2025-06-30','Pagado',50000.00),(18,'RJH49F','Moto','Juan Jose Giraldo R','3207089859','2025-05-11','2025-06-11','Pagado',50000.00),(19,'WTG55D','Moto','Jhonatan Galvis Castañeda','3054398062','2025-05-23','2025-06-23','Pagado',50000.00),(20,'QUS67G','Moto','Marisse Garcia','3217525669','2025-05-01','2025-06-01','Pendiente',50000.00),(21,'OWH86F','Moto','Elizabeth Arango','3143830460','2025-05-30','2025-06-30','Pagado',50000.00),(22,'IGQ41F','Moto','Esteban Montoya','','2025-05-07','2025-06-07','Pagado',50000.00),(23,'LG110D','Moto','Juan Pablo Betancur','3105038359','2025-05-30','2025-06-30','Pagado',50000.00),(24,'QXU29F','Moto','Kevin Andres Deossa','3184225847','2025-05-05','2025-06-05','Pagado',50000.00),(25,'VTJ57E','Moto','Jhon Alexander Uribe Rico','3146597552','2025-05-16','2025-06-16','Pagado',50000.00),(26,'NLZ39E','Moto','Robinson Serna Ortiz','3162876987','2025-05-03','2025-06-03','Pendiente',50000.00),(27,'ERK06F','Moto','Mauricio Quiroz Rico','3013243012','2025-05-30','2025-06-30','Pagado',50000.00),(28,'FB626F','Moto','Luis Enrique Giraldo','3105492221','2025-05-16','2025-06-16','Pagado',50000.00),(29,'BVT75E','Moto','Edward Cano','3044897108','2025-05-08','2025-06-08','Pagado',50000.00),(30,'SSP31E','Moto','Albeiro Roman','3213321512','2025-05-06','2025-06-06','Pendiente',50000.00),(31,'ZMY51C','Moto','','','2025-05-03','2025-06-03','Pendiente',50000.00),(32,'DCL11F','Moto','Alvaro Moreno H','3002809583','2025-05-03','2025-06-03','Pendiente',50000.00),(33,'ZMY51C','Moto','','','2025-05-03','2025-06-03','Pendiente',50000.00),(34,'HBR62G','Moto','','','2025-05-30','2025-06-30','Pagado',50000.00),(35,'PLB43B','Moto','Señor Bolsas','','2025-05-30','2025-06-30','Pagado',50000.00),(36,'AMF06H','Moto','','','2025-01-25','2025-01-25','Pendiente',50000.00),(37,'AMF06H','Moto','','','2025-01-25','2025-01-25','Pendiente',50000.00),(38,'AMF06H','Moto','','','2025-01-25','2025-01-25','Pagado',50000.00),(39,'AMF06H','Moto','','','2025-05-25','2025-06-25','Pagado',50000.00),(40,'QCG52G','Moto','','','2025-05-25','2025-06-25','Pagado',50000.00),(41,'YAG16F','Moto','','','2025-05-16','2025-05-16','Pagado',50000.00),(42,'RNJ32E','Moto','','3135159381','2025-05-14','2025-06-14','Pagado',50000.00),(43,'CSC22G','Moto','','','2025-05-20','2025-06-20','Pagado',50000.00),(44,'SAK54E','Moto','','','2025-05-22','2025-06-22','Pagado',50000.00),(45,'03A','Moto','','','2025-05-25','2025-05-25','Pagado',50000.00),(46,'Carro Tintero','Miscelaneo','Jose Rua','3225287565','2025-05-05','2025-06-05','Pagado',50000.00),(47,'Carro Fritos','Miscelaneo','Jhon Jairo Perez','3136139218','2025-05-30','2025-06-30','Pagado',50000.00),(48,'Fotocopias','Miscelaneo','Stella Jaramillo','3012367979','2025-05-06','2025-06-06','Pagado',50000.00),(49,'Carro Medias','Miscelaneo','Carlos Mario Caro','3163297423','2025-05-15','2025-06-15','Pagado',50000.00),(50,'Merkaprado','Miscelaneo','Mariano Orozco','3145956851','2025-05-18','2025-06-18','Pagado',50000.00),(51,'Carro de Dulces','Miscelaneo','Luz stella Caicedo','3046661866','2025-05-18','2025-06-18','Pagado',50000.00),(52,'las patricias','Miscelaneo','Adriana Jaramillo','3014494763','2025-05-18','2025-06-18','Pagado',50000.00),(53,'Aguacatero','Miscelaneo','Jesus Maria','3234367775','2025-05-07','2025-06-07','Pagado',50000.00),(54,'Aguacatero 2','Miscelaneo','Jesus Maria','3234367775','2025-05-14','2025-06-14','Pagado',50000.00),(55,'Aguacatero 3','Miscelaneo','Jesus Maria','3234367775','2025-05-21','2025-06-21','Pagado',50000.00),(56,'Aguacatero 4','Miscelaneo','Jesus Maria','3234367775','2025-05-28','2025-06-28','Pagado',50000.00),(57,'Carro Tintero','Miscelaneo','Jose Rua','3225287565','2025-05-30','2025-06-30','Pagado',50000.00),(58,'Hamburguesa','Miscelaneo','','','2025-05-30','2025-06-30','Pagado',50000.00),(59,'Carro Tintero','Miscelaneo','Jose Rua','3225287565','2025-05-30','2025-06-30','Pagado',50000.00),(60,'venezolana','Miscelaneo','Cora','3123919181','2025-05-28','2025-06-28','Pagado',50000.00),(61,'Chaza Azul','Miscelaneo','Marleny Marin','3023401561','2025-05-02','2025-06-02','Pendiente',50000.00),(62,'Carro de tintos','Miscelaneo','','3128463873','2025-05-17','2025-06-17','Pagado',50000.00),(63,'Comidas Grande','Miscelaneo','','','2025-05-17','2025-06-17','Pagado',50000.00),(64,'Artesanias','Miscelaneo','','3043804623','2025-05-07','2025-06-07','Pagado',50000.00),(65,'Prueba rata','Miscelaneo','alejo','12913929','2025-09-11','2025-10-11','Pagado',20000.00);
/*!40000 ALTER TABLE `mensualidades` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vehiculos`
--

DROP TABLE IF EXISTS `vehiculos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vehiculos` (
  `IdVehiculo` int NOT NULL AUTO_INCREMENT,
  `Placa` varchar(10) NOT NULL,
  `Tipo` varchar(20) NOT NULL,
  `Propietario` varchar(100) NOT NULL,
  `Telefono` varchar(20) NOT NULL,
  `HoraEntrada` datetime NOT NULL,
  `HoraSalida` datetime DEFAULT NULL,
  `TotalCobro` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`IdVehiculo`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vehiculos`
--

LOCK TABLES `vehiculos` WRITE;
/*!40000 ALTER TABLE `vehiculos` DISABLE KEYS */;
INSERT INTO `vehiculos` VALUES (1,'ORT19D','Moto','ALEJANDRO','3004021102','2025-05-26 12:03:30','2025-05-26 12:03:42',3000.00),(2,'KDG17P','Moto','Ñangara','2001006235','2025-05-26 19:34:54','2025-05-26 19:39:36',2000.00),(3,'XTW816','Carro','Aleja','3007083319','2025-05-26 22:53:01','2025-05-26 22:53:22',4000.00),(4,'ALK48P','Moto','Alex','2003004059','2025-05-28 14:13:58','2025-05-28 14:14:18',2000.00),(5,'ORT19D','Moto','aLEJO','3009004030','2025-05-28 18:58:39','2025-05-28 18:58:46',2000.00),(6,'PTU89H','Moto','aLEJP','2525555','2025-05-30 22:07:48','2025-06-06 20:49:28',105000.00),(7,'e','Carro','e','2','2025-05-30 22:33:43','2025-06-06 20:49:46',105000.00),(8,'dddd','Camioneta','dddd','546464','2025-06-04 20:25:27','2025-06-04 20:25:36',5000.00),(9,'IHK88E','Moto','Juanma','3002001232','2025-09-11 08:25:32','2025-09-11 08:26:55',2000.00),(10,'ort19d','Moto','alejo','3004021102','2026-02-06 21:08:26','2026-02-06 21:10:45',2000.00),(11,'dvsdvs','Carro','dsvds','23232ff','2026-02-06 21:16:12','2026-02-06 21:16:22',4000.00);
/*!40000 ALTER TABLE `vehiculos` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-02-26 19:11:09
