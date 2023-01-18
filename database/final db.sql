-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: coureproject
-- ------------------------------------------------------
-- Server version	8.0.31

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
-- Table structure for table `answeroption`
--

DROP TABLE IF EXISTS `answeroption`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `answeroption` (
  `idAnswerOption` int NOT NULL AUTO_INCREMENT,
  `title` varchar(45) NOT NULL,
  `isRight` tinyint NOT NULL,
  `task_idtask` int NOT NULL,
  PRIMARY KEY (`idAnswerOption`),
  KEY `fk_answeroption_task1_idx` (`task_idtask`),
  CONSTRAINT `fk_answeroption_task1` FOREIGN KEY (`task_idtask`) REFERENCES `task` (`idtask`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `answeroption`
--

LOCK TABLES `answeroption` WRITE;
/*!40000 ALTER TABLE `answeroption` DISABLE KEYS */;
INSERT INTO `answeroption` VALUES (1,'язык программирования',1,1),(2,'си решетка',0,1),(3,'Model-View-Controller',1,2),(4,'хз',0,2),(6,'dadaw',0,4),(7,'awdwad',1,8),(8,'wadwadwa',1,8);
/*!40000 ALTER TABLE `answeroption` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `discipline`
--

DROP TABLE IF EXISTS `discipline`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `discipline` (
  `iddiscipline` int NOT NULL AUTO_INCREMENT,
  `title` varchar(80) NOT NULL,
  `lector_User_idUser` int NOT NULL,
  PRIMARY KEY (`iddiscipline`),
  KEY `fk_discipline_lector1_idx` (`lector_User_idUser`),
  CONSTRAINT `fk_discipline_lector1` FOREIGN KEY (`lector_User_idUser`) REFERENCES `lector` (`User_idUser`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `discipline`
--

LOCK TABLES `discipline` WRITE;
/*!40000 ALTER TABLE `discipline` DISABLE KEYS */;
INSERT INTO `discipline` VALUES (1,'MMGO',2),(4,'TP',2),(5,'RPS',4);
/*!40000 ALTER TABLE `discipline` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `discipline_has_group`
--

DROP TABLE IF EXISTS `discipline_has_group`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `discipline_has_group` (
  `discipline_iddiscipline` int NOT NULL,
  `group_idGroup` int NOT NULL,
  PRIMARY KEY (`discipline_iddiscipline`,`group_idGroup`),
  KEY `fk_discipline_has_group_group1_idx` (`group_idGroup`),
  KEY `fk_discipline_has_group_discipline1_idx` (`discipline_iddiscipline`),
  CONSTRAINT `fk_discipline_has_group_discipline1` FOREIGN KEY (`discipline_iddiscipline`) REFERENCES `discipline` (`iddiscipline`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_discipline_has_group_group1` FOREIGN KEY (`group_idGroup`) REFERENCES `group` (`idGroup`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `discipline_has_group`
--

LOCK TABLES `discipline_has_group` WRITE;
/*!40000 ALTER TABLE `discipline_has_group` DISABLE KEYS */;
INSERT INTO `discipline_has_group` VALUES (1,1),(4,1),(5,1),(1,2),(4,2),(1,3);
/*!40000 ALTER TABLE `discipline_has_group` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `file`
--

DROP TABLE IF EXISTS `file`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `file` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `path` varchar(255) NOT NULL,
  `idstudent` int NOT NULL,
  `idlabwork` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `file`
--

LOCK TABLES `file` WRITE;
/*!40000 ALTER TABLE `file` DISABLE KEYS */;
INSERT INTO `file` VALUES (27,'IGS_Ekzamen.docx','/Files/IGS_Ekzamen.docx',1,1),(29,'1638980586_13-almode-ru-p-ded-moroz-13.jpg','/Files/1638980586_13-almode-ru-p-ded-moroz-13.jpg',5,1);
/*!40000 ALTER TABLE `file` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `group`
--

DROP TABLE IF EXISTS `group`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `group` (
  `idGroup` int NOT NULL AUTO_INCREMENT,
  `title` varchar(80) NOT NULL,
  `speciality` varchar(80) NOT NULL,
  PRIMARY KEY (`idGroup`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `group`
--

LOCK TABLES `group` WRITE;
/*!40000 ALTER TABLE `group` DISABLE KEYS */;
INSERT INTO `group` VALUES (1,'PRI-120','Software Engenering'),(2,'IST-120','Information Systems'),(3,'VT-120','Vichislitelnie Technology'),(5,'PRI-220','Software Engenering');
/*!40000 ALTER TABLE `group` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `labwork`
--

DROP TABLE IF EXISTS `labwork`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `labwork` (
  `idlabwork` int NOT NULL AUTO_INCREMENT,
  `title` varchar(80) NOT NULL,
  `deadline` datetime NOT NULL,
  `manual` varchar(255) NOT NULL,
  `status` int NOT NULL,
  `iddiscipline` int NOT NULL,
  PRIMARY KEY (`idlabwork`),
  KEY `fk_labwork_discipline_idx` (`iddiscipline`),
  CONSTRAINT `fk_labwork_discipline` FOREIGN KEY (`iddiscipline`) REFERENCES `discipline` (`iddiscipline`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `labwork`
--

LOCK TABLES `labwork` WRITE;
/*!40000 ALTER TABLE `labwork` DISABLE KEYS */;
INSERT INTO `labwork` VALUES (1,'Лабораторная работа №1. Построение прямых','2023-02-12 00:00:00','http://op.vlsu.ru/fileadmin/Programmy/Bacalavr_academ/09.03.02/R_prog/09.03.02_TIPiS_RP_2019_01.pdf',0,1),(3,'Лабораторная работа №1. Изучение ASP.NET CORE','2023-02-12 00:00:00','#',1,4),(4,'Лабораторная работа №2. Изучение ASP.NET CORE','2023-03-12 00:00:00','#',1,4),(10,'Лабораторная работа №2. Построение касательных','2023-01-18 00:00:00','https://ispi.cdo.vlsu.ru/pluginfile.php/17782/mod_resource/content/4/TP-1sem-lab01-09.pdf',1,1);
/*!40000 ALTER TABLE `labwork` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `labwork_has_student`
--

DROP TABLE IF EXISTS `labwork_has_student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `labwork_has_student` (
  `labwork_idlabwork` int NOT NULL,
  `student_User_idUser` int NOT NULL,
  `discipline_iddiscipline` int NOT NULL,
  `scorelab` int NOT NULL,
  `passeddate` datetime NOT NULL,
  `idlector` int NOT NULL,
  PRIMARY KEY (`labwork_idlabwork`,`student_User_idUser`),
  KEY `fk_labwork_has_student_student1_idx` (`student_User_idUser`),
  KEY `fk_labwork_has_student_labwork1_idx` (`labwork_idlabwork`),
  KEY `fk_labwork_has_student_discipline1_idx` (`discipline_iddiscipline`),
  KEY `fk_labwork_has_student_lector1_idx` (`idlector`),
  CONSTRAINT `fk_labwork_has_student_discipline1` FOREIGN KEY (`discipline_iddiscipline`) REFERENCES `discipline` (`iddiscipline`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_labwork_has_student_labwork1` FOREIGN KEY (`labwork_idlabwork`) REFERENCES `labwork` (`idlabwork`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_labwork_has_student_lector1` FOREIGN KEY (`idlector`) REFERENCES `lector` (`User_idUser`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_labwork_has_student_student1` FOREIGN KEY (`student_User_idUser`) REFERENCES `student` (`User_idUser`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `labwork_has_student`
--

LOCK TABLES `labwork_has_student` WRITE;
/*!40000 ALTER TABLE `labwork_has_student` DISABLE KEYS */;
INSERT INTO `labwork_has_student` VALUES (1,1,1,10,'2023-01-18 20:03:29',2),(1,5,1,0,'2023-01-18 23:15:27',2),(3,1,4,0,'2023-01-17 21:05:23',2),(4,1,4,0,'2023-01-17 21:25:06',2);
/*!40000 ALTER TABLE `labwork_has_student` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lector`
--

DROP TABLE IF EXISTS `lector`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `lector` (
  `User_idUser` int NOT NULL,
  PRIMARY KEY (`User_idUser`),
  KEY `fk_lector_User1_idx` (`User_idUser`),
  CONSTRAINT `fk_lector_User1` FOREIGN KEY (`User_idUser`) REFERENCES `user` (`idUser`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lector`
--

LOCK TABLES `lector` WRITE;
/*!40000 ALTER TABLE `lector` DISABLE KEYS */;
INSERT INTO `lector` VALUES (2),(4);
/*!40000 ALTER TABLE `lector` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role` (
  `id` int NOT NULL AUTO_INCREMENT,
  `roleName` varchar(40) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'student'),(2,'lector'),(3,'admin');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `score`
--

DROP TABLE IF EXISTS `score`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `score` (
  `idscore` int NOT NULL AUTO_INCREMENT,
  `student_User_idUser` int NOT NULL,
  `discipline_iddiscipline` int NOT NULL,
  `score` int NOT NULL,
  PRIMARY KEY (`idscore`),
  KEY `fk_score_student1_idx` (`student_User_idUser`),
  KEY `fk_score_discipline1_idx` (`discipline_iddiscipline`),
  CONSTRAINT `fk_score_discipline1` FOREIGN KEY (`discipline_iddiscipline`) REFERENCES `discipline` (`iddiscipline`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_score_student1` FOREIGN KEY (`student_User_idUser`) REFERENCES `student` (`User_idUser`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `score`
--

LOCK TABLES `score` WRITE;
/*!40000 ALTER TABLE `score` DISABLE KEYS */;
INSERT INTO `score` VALUES (30,5,1,13),(31,7,1,2),(32,1,4,3),(34,6,4,0),(35,6,5,0),(36,1,1,0),(37,6,1,0);
/*!40000 ALTER TABLE `score` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `student`
--

DROP TABLE IF EXISTS `student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `student` (
  `User_idUser` int NOT NULL,
  `group_idGroup` int NOT NULL,
  PRIMARY KEY (`User_idUser`),
  KEY `fk_student_User1_idx` (`User_idUser`),
  KEY `fk_student_group1_idx` (`group_idGroup`),
  CONSTRAINT `fk_student_group1` FOREIGN KEY (`group_idGroup`) REFERENCES `group` (`idGroup`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_student_User1` FOREIGN KEY (`User_idUser`) REFERENCES `user` (`idUser`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student`
--

LOCK TABLES `student` WRITE;
/*!40000 ALTER TABLE `student` DISABLE KEYS */;
INSERT INTO `student` VALUES (1,1),(6,1),(5,2),(7,3);
/*!40000 ALTER TABLE `student` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `student_has_test`
--

DROP TABLE IF EXISTS `student_has_test`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `student_has_test` (
  `student_User_idUser` int NOT NULL,
  `test_idtest` int NOT NULL,
  `discipline_iddiscipline` int NOT NULL,
  `scoretest` int NOT NULL,
  `passeddate` datetime NOT NULL,
  PRIMARY KEY (`student_User_idUser`,`test_idtest`),
  KEY `fk_student_has_test_test1_idx` (`test_idtest`),
  KEY `fk_student_has_test_student1_idx` (`student_User_idUser`),
  KEY `fk_student_has_test_discipline1_idx` (`discipline_iddiscipline`),
  CONSTRAINT `fk_student_has_test_discipline1` FOREIGN KEY (`discipline_iddiscipline`) REFERENCES `discipline` (`iddiscipline`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_student_has_test_student1` FOREIGN KEY (`student_User_idUser`) REFERENCES `student` (`User_idUser`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_student_has_test_test1` FOREIGN KEY (`test_idtest`) REFERENCES `test` (`idtest`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student_has_test`
--

LOCK TABLES `student_has_test` WRITE;
/*!40000 ALTER TABLE `student_has_test` DISABLE KEYS */;
INSERT INTO `student_has_test` VALUES (1,7,1,1,'2023-01-18 00:26:07'),(1,20,4,0,'2023-01-19 01:15:12'),(5,7,1,1,'2023-01-18 00:14:51'),(6,7,1,2,'2023-01-18 22:28:42'),(7,7,1,2,'2023-01-18 22:27:16');
/*!40000 ALTER TABLE `student_has_test` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `task`
--

DROP TABLE IF EXISTS `task`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `task` (
  `idtask` int NOT NULL AUTO_INCREMENT,
  `title` varchar(80) NOT NULL,
  `test_idtest` int NOT NULL,
  PRIMARY KEY (`idtask`),
  KEY `fk_task_test1_idx` (`test_idtest`),
  CONSTRAINT `fk_task_test1` FOREIGN KEY (`test_idtest`) REFERENCES `test` (`idtest`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `task`
--

LOCK TABLES `task` WRITE;
/*!40000 ALTER TABLE `task` DISABLE KEYS */;
INSERT INTO `task` VALUES (1,'что такое с#',7),(2,'расшифруйте MVC',7),(4,'jhgyugi',18),(6,'dawdaw',18),(8,'awawfwaf',4);
/*!40000 ALTER TABLE `task` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `test`
--

DROP TABLE IF EXISTS `test`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `test` (
  `idtest` int NOT NULL AUTO_INCREMENT,
  `title` varchar(80) NOT NULL,
  `deadLine` datetime NOT NULL,
  `status` int NOT NULL,
  `iddiscipline` int NOT NULL,
  PRIMARY KEY (`idtest`),
  KEY `iddiscipline_idx` (`iddiscipline`),
  CONSTRAINT `fk_test_discipline` FOREIGN KEY (`iddiscipline`) REFERENCES `discipline` (`iddiscipline`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `test`
--

LOCK TABLES `test` WRITE;
/*!40000 ALTER TABLE `test` DISABLE KEYS */;
INSERT INTO `test` VALUES (1,'Тест №1. Двухмерные фракталы','2023-01-18 05:00:00',1,1),(4,'Тест №1. WindowsForms','2023-01-12 00:00:00',0,4),(7,'Тест №2. Криволинейные поверхности','2023-03-12 01:51:00',0,1),(18,'Тест №3. Модели объёмных графических объектов','2023-04-12 00:00:00',1,1),(20,'Тест №2. Диаграммы','2023-04-12 00:00:00',0,4);
/*!40000 ALTER TABLE `test` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `idUser` int NOT NULL AUTO_INCREMENT,
  `fullname` varchar(80) NOT NULL,
  `login` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `birthdate` date NOT NULL,
  `Role_id` int NOT NULL,
  PRIMARY KEY (`idUser`),
  KEY `fk_User_Role_idx` (`Role_id`),
  CONSTRAINT `fk_User_Role` FOREIGN KEY (`Role_id`) REFERENCES `role` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'Krokodil Gena','genakrokodile','1111','gena@krokodile.com','2001-12-11',1),(2,'Kirill Cheburashkin','cheba','1231','cheba@gmail.com','2003-01-26',2),(3,'Vlad Jubrailka','admin','admin11','admin@admin.com','1999-02-12',3),(4,'Kirillova Svetlana','teacher','123','teacher@example.com','1982-01-01',2),(5,'Abram Linkoln','linkoln','4321q','abram@gmail.com','2002-11-12',1),(6,'Alexandro Potato','potato123','4512','potato@gmail.com','2002-01-10',1),(7,'David Tractor','david123','5124','david123@gmail.com','2003-01-10',1),(10,'Irina Vafelkina','ira521','62123','ira@gmail.com','2002-01-14',1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-01-19  1:52:36
