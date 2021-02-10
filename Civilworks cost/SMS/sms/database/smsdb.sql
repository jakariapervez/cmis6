-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Nov 10, 2018 at 05:26 AM
-- Server version: 10.1.36-MariaDB
-- PHP Version: 7.2.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `smsdb`
--
CREATE DATABASE IF NOT EXISTS `smsdb` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `smsdb`;

-- --------------------------------------------------------

--
-- Table structure for table `sms_data`
--

CREATE TABLE `sms_data` (
  `id` int(11) NOT NULL,
  `station_id` int(11) NOT NULL,
  `sender_no` varchar(45) NOT NULL,
  `value` float NOT NULL,
  `date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `sms_data`
--

INSERT INTO `sms_data` (`id`, `station_id`, `sender_no`, `value`, `date`) VALUES
(1, 1, '01788577733', 2.42196, '2018-10-20 00:00:00'),
(2, 1, '01788577733', 5.24566, '2018-10-20 03:00:00'),
(3, 1, '01788577733', 2.92497, '2018-10-20 06:00:00'),
(4, 1, '01788577733', 1.39909, '2018-10-20 09:00:00'),
(5, 1, '01788577733', 7.69996, '2018-10-20 12:00:00'),
(6, 1, '01788577733', 9.1535, '2018-10-20 15:00:00'),
(7, 1, '01788577733', 2.22911, '2018-10-20 18:00:00'),
(8, 1, '01788577733', 4.58389, '2018-10-20 21:00:00'),
(9, 1, '01788577733', 1.30744, '2018-10-21 00:00:00'),
(10, 1, '01788577733', 9.69168, '2018-10-21 03:00:00'),
(11, 1, '01788577733', 6.04624, '2018-10-21 06:00:00'),
(12, 1, '01788577733', 5.2649, '2018-10-21 09:00:00'),
(13, 1, '01788577733', 1.10643, '2018-10-21 12:00:00'),
(14, 1, '01788577733', 2.62624, '2018-10-21 15:00:00'),
(15, 1, '01788577733', 1.75076, '2018-10-21 18:00:00'),
(16, 1, '01788577733', 3.3626, '2018-10-21 21:00:00'),
(17, 1, '01788577733', 9.6804, '2018-10-22 00:00:00'),
(18, 1, '01788577733', 5.48572, '2018-10-22 03:00:00'),
(19, 1, '01788577733', 1.48737, '2018-10-22 06:00:00'),
(20, 1, '01788577733', 1.72648, '2018-10-22 09:00:00'),
(21, 1, '01788577733', 1.5749, '2018-10-22 12:00:00'),
(22, 1, '01788577733', 0.745507, '2018-10-22 15:00:00'),
(23, 1, '01788577733', 0.395575, '2018-10-22 18:00:00'),
(24, 1, '01788577733', 1.28857, '2018-10-22 21:00:00'),
(25, 1, '01788577733', 9.3108, '2018-10-23 00:00:00'),
(26, 1, '01788577733', 7.97139, '2018-10-23 03:00:00'),
(27, 1, '01788577733', 0.918924, '2018-10-23 06:00:00'),
(28, 1, '01788577733', 2.09395, '2018-10-23 09:00:00'),
(29, 1, '01788577733', 1.625, '2018-10-23 12:00:00'),
(30, 1, '01788577733', 2.21738, '2018-10-23 15:00:00'),
(31, 1, '01788577733', 4.97913, '2018-10-23 18:00:00'),
(32, 1, '01788577733', 4.14903, '2018-10-23 21:00:00'),
(33, 1, '01788577733', 5.99707, '2018-10-24 00:00:00'),
(34, 1, '01788577733', 4.3423, '2018-10-24 03:00:00'),
(35, 1, '01788577733', 9.55486, '2018-10-24 06:00:00'),
(36, 1, '01788577733', 1.80374, '2018-10-24 09:00:00'),
(37, 1, '01788577733', 8.93617, '2018-10-24 12:00:00'),
(38, 1, '01788577733', 2.59112, '2018-10-24 15:00:00'),
(39, 1, '01788577733', 2.77736, '2018-10-24 18:00:00'),
(40, 1, '01788577733', 5.47147, '2018-10-24 21:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `sms_log`
--

CREATE TABLE `sms_log` (
  `id` int(11) NOT NULL,
  `sender_no` varchar(45) NOT NULL,
  `text` varchar(500) NOT NULL,
  `time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `station_info`
--

CREATE TABLE `station_info` (
  `id` int(11) NOT NULL,
  `station_name` varchar(45) NOT NULL,
  `station_code` varchar(45) NOT NULL,
  `lat` float NOT NULL,
  `lon` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `station_info`
--

INSERT INTO `station_info` (`id`, `station_name`, `station_code`, `lat`, `lon`) VALUES
(1, 'Gazipur', 'bd001', 23.99, 90.41),
(2, 'Comilla', 'bd002', 23.47, 91.16),
(3, 'Barisal', 'bd003', 22.68, 90.36),
(4, 'Rajshahi', 'bd004', 24.37, 88.66),
(5, 'Rangpur', 'bd005', 25.7, 89.27);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `sms_data`
--
ALTER TABLE `sms_data`
  ADD PRIMARY KEY (`id`,`station_id`),
  ADD KEY `fk_sms_data_station_info_idx` (`station_id`);

--
-- Indexes for table `sms_log`
--
ALTER TABLE `sms_log`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `station_info`
--
ALTER TABLE `station_info`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `sms_data`
--
ALTER TABLE `sms_data`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=41;

--
-- AUTO_INCREMENT for table `station_info`
--
ALTER TABLE `station_info`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `sms_data`
--
ALTER TABLE `sms_data`
  ADD CONSTRAINT `fk_sms_data_station_info` FOREIGN KEY (`station_id`) REFERENCES `station_info` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
