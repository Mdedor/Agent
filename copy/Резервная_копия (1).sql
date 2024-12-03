CREATE DATABASE  IF NOT EXISTS `agent` /*!40100 DEFAULT CHARACTER SET utf8 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `agent`;
-- MySQL dump 10.13  Distrib 8.0.28, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: agent
-- ------------------------------------------------------
-- Server version	8.0.28

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
-- Table structure for table `applicant`
--

DROP TABLE IF EXISTS `applicant`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `applicant` (
  `applicant_id` int NOT NULL AUTO_INCREMENT,
  `applicant_surname` varchar(45) NOT NULL,
  `applicant_name` varchar(45) NOT NULL,
  `applicant_patronymic` varchar(45) NOT NULL,
  `applicant_phone_number` varchar(18) NOT NULL,
  `applicant_address` varchar(45) NOT NULL,
  `applicant_date_of_birth` date NOT NULL,
  `applicant_image` varchar(100) DEFAULT NULL,
  `applicant_gender` int NOT NULL,
  `applicant_delete_status` int DEFAULT NULL,
  PRIMARY KEY (`applicant_id`),
  KEY `fk_applicant_gender1_idx` (`applicant_gender`),
  KEY `applicant_delete_status_idx` (`applicant_delete_status`),
  CONSTRAINT `applicant_delete_status` FOREIGN KEY (`applicant_delete_status`) REFERENCES `delete` (`id`),
  CONSTRAINT `fk_applicant_gender1` FOREIGN KEY (`applicant_gender`) REFERENCES `gender` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `company`
--

DROP TABLE IF EXISTS `company`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `company` (
  `id` int NOT NULL AUTO_INCREMENT,
  `company_name` varchar(100) NOT NULL,
  `company_desceiption` varchar(1000) NOT NULL,
  `company_phone_number` varchar(18) NOT NULL,
  `company_address` varchar(45) NOT NULL,
  `companyc_linq` varchar(200) DEFAULT NULL,
  `company_delete_status` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `company_delete_status_idx` (`company_delete_status`),
  CONSTRAINT `company_delete_status` FOREIGN KEY (`company_delete_status`) REFERENCES `delete` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `delete`
--

DROP TABLE IF EXISTS `delete`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `delete` (
  `id` int NOT NULL AUTO_INCREMENT,
  `status` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `direction`
--

DROP TABLE IF EXISTS `direction`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `direction` (
  `id` int NOT NULL AUTO_INCREMENT,
  `direction_aplicant` int NOT NULL,
  `direction_vacancy` int NOT NULL,
  `direction_employee` int NOT NULL,
  `direction_date` date NOT NULL,
  `direction_status` varchar(45) NOT NULL,
  `direction_delete_status` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_direction_vacancy1_idx` (`direction_vacancy`),
  KEY `fk_direction_applicant1_idx` (`direction_aplicant`),
  KEY `direction_delete_status_idx` (`direction_delete_status`),
  KEY `direction_employee_idx` (`direction_employee`),
  CONSTRAINT `direction_delete_status` FOREIGN KEY (`direction_delete_status`) REFERENCES `delete` (`id`),
  CONSTRAINT `direction_employee` FOREIGN KEY (`direction_employee`) REFERENCES `employe` (`id`),
  CONSTRAINT `fk_direction_applicant1` FOREIGN KEY (`direction_aplicant`) REFERENCES `applicant` (`applicant_id`),
  CONSTRAINT `fk_direction_vacancy1` FOREIGN KEY (`direction_vacancy`) REFERENCES `vacancy` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `employe`
--

DROP TABLE IF EXISTS `employe`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employe` (
  `id` int NOT NULL AUTO_INCREMENT,
  `employe_surname` varchar(45) NOT NULL,
  `employe_name` varchar(45) NOT NULL,
  `employe_partronymic` varchar(45) NOT NULL,
  `employe_phone_number` varchar(18) NOT NULL,
  `employe_adress` varchar(45) NOT NULL,
  `employe_login` varchar(45) NOT NULL,
  `employe_pwd` varchar(100) NOT NULL,
  `employe_post` int NOT NULL,
  `employe_delete_status` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_employee_post1_idx` (`employe_post`),
  KEY `employe_delete_status_idx` (`employe_delete_status`),
  CONSTRAINT `employe_delete_status` FOREIGN KEY (`employe_delete_status`) REFERENCES `delete` (`id`),
  CONSTRAINT `fk_employee_post1` FOREIGN KEY (`employe_post`) REFERENCES `post` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `gender`
--

DROP TABLE IF EXISTS `gender`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `gender` (
  `id` int NOT NULL,
  `genders` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `post`
--

DROP TABLE IF EXISTS `post`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `post` (
  `id` int NOT NULL AUTO_INCREMENT,
  `posts` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `profession`
--

DROP TABLE IF EXISTS `profession`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `profession` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `description` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `resume`
--

DROP TABLE IF EXISTS `resume`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `resume` (
  `id` int NOT NULL AUTO_INCREMENT,
  `resume_applicant` int NOT NULL,
  `resume_profession` int NOT NULL,
  `salary` decimal(7,0) NOT NULL,
  `resume_education` varchar(1000) NOT NULL,
  `resume_work_experience` varchar(1000) NOT NULL,
  `resume_knowledge_of_languages` varchar(150) NOT NULL,
  `resume_personal_qualities` varchar(100) NOT NULL,
  `resume_delete_status` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_resume_applicant1_idx` (`resume_applicant`),
  KEY `resume_profession_idx` (`resume_profession`),
  KEY `applicant_delete_status_idx` (`resume_delete_status`),
  CONSTRAINT `fk_resume_applicant1` FOREIGN KEY (`resume_applicant`) REFERENCES `applicant` (`applicant_id`),
  CONSTRAINT `resume_delete_status` FOREIGN KEY (`resume_delete_status`) REFERENCES `delete` (`id`),
  CONSTRAINT `resume_profession` FOREIGN KEY (`resume_profession`) REFERENCES `profession` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `vacancy`
--

DROP TABLE IF EXISTS `vacancy`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vacancy` (
  `id` int NOT NULL AUTO_INCREMENT,
  `vacancy_company` int NOT NULL,
  `vacancy_profession` int NOT NULL,
  `vacancy_responsibilities` varchar(500) NOT NULL,
  `vacancy_requirements` varchar(500) NOT NULL,
  `vacancy_conditions` varchar(500) NOT NULL,
  `vacancy_address` varchar(200) NOT NULL,
  `vacancy_salary_by` decimal(7,0) NOT NULL,
  `vacancy_salary_before` decimal(7,0) NOT NULL,
  `vacancy_delete_status` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_vacancy_company1_idx` (`vacancy_company`),
  KEY `vacancy_profession_idx` (`vacancy_profession`),
  KEY `vacancy_delete_status_idx` (`vacancy_delete_status`),
  CONSTRAINT `fk_vacancy_company1` FOREIGN KEY (`vacancy_company`) REFERENCES `company` (`id`),
  CONSTRAINT `vacancy_delete_status` FOREIGN KEY (`vacancy_delete_status`) REFERENCES `delete` (`id`),
  CONSTRAINT `vacancy_profession` FOREIGN KEY (`vacancy_profession`) REFERENCES `profession` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-12-03 12:53:56
