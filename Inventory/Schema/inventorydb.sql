-- phpMyAdmin SQL Dump
-- version 5.1.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 03, 2022 at 02:48 PM
-- Server version: 10.4.19-MariaDB
-- PHP Version: 8.0.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `inventorydb`
--

-- --------------------------------------------------------

--
-- Table structure for table `audits`
--

CREATE TABLE `audits` (
  `id` bigint(20) NOT NULL,
  `type` varchar(500) COLLATE utf8mb4_unicode_ci NOT NULL,
  `table_name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `date_time` datetime NOT NULL,
  `old_values` varchar(500) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `new_values` varchar(500) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `affected_columns` varchar(500) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `primary_key` varchar(500) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `audits`
--

INSERT INTO `audits` (`id`, `type`, `table_name`, `date_time`, `old_values`, `new_values`, `affected_columns`, `primary_key`) VALUES
(1, 'None', 'Customer', '2022-01-02 17:01:53', NULL, NULL, NULL, '{\"CusId\":9}'),
(2, 'None', 'CustomerProxy', '2022-01-02 17:06:57', NULL, NULL, NULL, '{\"CusId\":9}'),
(3, 'Update', 'CustomerProxy', '2022-01-02 17:18:03', '{\"Address\":\"ktm_returns\",\"Email\":\"raju@gmail.com\",\"FullName\":\"Raju_don\",\"Gender\":\"Male\",\"PhoneNumber\":\"1288128912\"}', '{\"Address\":\"ktm\",\"Email\":\"raju@gmail.com\",\"FullName\":\"Raju\",\"Gender\":\"Male\",\"PhoneNumber\":\"1288128912\"}', '[\"Address\",\"Email\",\"FullName\",\"Gender\",\"PhoneNumber\"]', '{\"CusId\":9}');

-- --------------------------------------------------------

--
-- Table structure for table `customers`
--

CREATE TABLE `customers` (
  `CusId` int(10) NOT NULL,
  `FullName` varchar(50) NOT NULL,
  `Address` varchar(150) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `PhoneNumber` varchar(15) NOT NULL,
  `Gender` varchar(7) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `customers`
--

INSERT INTO `customers` (`CusId`, `FullName`, `Address`, `Email`, `PhoneNumber`, `Gender`) VALUES
(1, 'Common', 'BTM', 'commonkhadka@gmail.com', '1234567890', 'Male'),
(2, 'Deepak', 'btm', 'd@gmail.com', '8899889911', 'Male'),
(3, 'samir', 'btm', 'samir@gmail.com', '8989121290', 'Male'),
(4, 'sandip', 'btm', 'sandip@gmail.com', '7812781234', 'Male'),
(5, 'roshan', 'dhulabari', 'roshangrg@gmail.com', '9812120912', 'Male'),
(9, 'Raju', 'ktm', 'raju@gmail.com', '1288128912', 'Male');

-- --------------------------------------------------------

--
-- Table structure for table `customer_transaction`
--

CREATE TABLE `customer_transaction` (
  `id` bigint(20) NOT NULL,
  `customer_id` bigint(20) NOT NULL,
  `amount` decimal(18,2) NOT NULL,
  `amount_type` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `type` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `extraId` bigint(20) NOT NULL,
  `transaction_date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `customer_transaction`
--

INSERT INTO `customer_transaction` (`id`, `customer_id`, `amount`, `amount_type`, `type`, `extraId`, `transaction_date`) VALUES
(1, 1, '2800.00', 'DEBIT', 'SALES', 7, '2021-09-23 00:00:00'),
(2, 1, '2500.00', 'CREDIT', 'PAYMENT', 7, '2021-09-23 00:00:00'),
(3, 2, '950.00', 'DEBIT', 'SALES', 8, '2021-09-24 09:47:44'),
(4, 2, '950.00', 'CREDIT', 'PAYMENT', 8, '2021-09-24 09:47:44'),
(5, 1, '2000.00', 'DEBIT', 'SALES', 9, '2021-09-24 12:40:51'),
(6, 1, '2000.00', 'CREDIT', 'PAYMENT', 9, '2021-09-24 12:40:51'),
(7, 2, '2400.00', 'DEBIT', 'SALES', 10, '2021-09-24 15:27:33'),
(8, 2, '2400.00', 'CREDIT', 'PAYMENT', 10, '2021-09-24 15:27:33'),
(9, 1, '2200.00', 'DEBIT', 'SALES', 11, '2021-09-24 15:34:59'),
(10, 1, '2000.00', 'CREDIT', 'PAYMENT', 11, '2021-09-24 15:34:59'),
(11, 1, '1900.00', 'DEBIT', 'SALES', 12, '2021-09-24 15:38:56'),
(12, 1, '1900.00', 'CREDIT', 'PAYMENT', 12, '2021-09-24 15:38:56'),
(13, 1, '2200.00', 'DEBIT', 'SALES', 13, '2021-09-24 15:40:39'),
(14, 1, '2200.00', 'CREDIT', 'PAYMENT', 13, '2021-09-24 15:40:39'),
(15, 1, '2000.00', 'DEBIT', 'SALES', 14, '2021-09-24 15:49:06'),
(16, 1, '2000.00', 'CREDIT', 'PAYMENT', 14, '2021-09-24 15:49:06'),
(17, 1, '2000.00', 'DEBIT', 'SALES', 15, '2021-09-24 15:49:56'),
(18, 1, '2000.00', 'CREDIT', 'PAYMENT', 15, '2021-09-24 15:49:56'),
(19, 1, '220.00', 'DEBIT', 'SALES', 16, '2021-09-24 15:51:04'),
(20, 1, '220.00', 'CREDIT', 'PAYMENT', 16, '2021-09-24 15:51:04'),
(21, 1, '1900.00', 'DEBIT', 'SALES', 17, '2021-09-24 17:00:04'),
(22, 1, '1900.00', 'CREDIT', 'PAYMENT', 17, '2021-09-24 17:00:04'),
(23, 1, '2600.00', 'DEBIT', 'SALES', 18, '2021-09-25 00:07:08'),
(24, 1, '2500.00', 'CREDIT', 'PAYMENT', 18, '2021-09-25 00:07:08'),
(25, 1, '300.00', 'DEBIT', 'SALES', 19, '2021-09-25 11:16:01'),
(26, 1, '200.00', 'CREDIT', 'PAYMENT', 19, '2021-09-25 11:16:01'),
(27, 2, '220.00', 'DEBIT', 'SALES', 20, '2021-09-25 11:19:57');

-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `id` bigint(10) NOT NULL,
  `name` varchar(50) NOT NULL,
  `status` varchar(10) NOT NULL,
  `rate` decimal(18,2) NOT NULL,
  `unit_id` bigint(20) NOT NULL,
  `available_qty` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `items`
--

INSERT INTO `items` (`id`, `name`, `status`, `rate`, `unit_id`, `available_qty`) VALUES
(1, 'Apple', 'Active', '220.00', 1, 39),
(2, 'Floor', 'Active', '60.00', 1, 20),
(3, 'Rope', 'Active', '60.00', 3, 5);

-- --------------------------------------------------------

--
-- Table structure for table `purchases`
--

CREATE TABLE `purchases` (
  `id` bigint(20) NOT NULL,
  `supplier_id` bigint(20) NOT NULL,
  `total` decimal(20,0) NOT NULL,
  `discount` decimal(20,0) NOT NULL,
  `vat` decimal(20,0) NOT NULL,
  `remarks` varchar(150) DEFAULT NULL,
  `grand_total` decimal(10,0) NOT NULL,
  `date_time` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `purchases`
--

INSERT INTO `purchases` (`id`, `supplier_id`, `total`, `discount`, `vat`, `remarks`, `grand_total`, `date_time`) VALUES
(1, 1, '30000', '150', '0', NULL, '29850', '2021-09-08 02:19:33'),
(2, 2, '10000', '50', '0', NULL, '9950', '2021-09-24 03:59:43'),
(3, 1, '1800', '0', '0', NULL, '1800', '2021-09-24 05:46:43'),
(4, 1, '1800', '0', '0', NULL, '1800', '2021-09-24 06:54:57'),
(5, 2, '500', '0', '0', NULL, '500', '2021-09-25 05:29:19');

-- --------------------------------------------------------

--
-- Table structure for table `purchase_details`
--

CREATE TABLE `purchase_details` (
  `id` bigint(20) NOT NULL,
  `item_id` bigint(20) NOT NULL,
  `rate` decimal(18,2) NOT NULL,
  `amount` decimal(18,2) NOT NULL,
  `qty` bigint(20) NOT NULL,
  `purchase_id` bigint(20) NOT NULL,
  `sales_rate` decimal(18,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `purchase_details`
--

INSERT INTO `purchase_details` (`id`, `item_id`, `rate`, `amount`, `qty`, `purchase_id`, `sales_rate`) VALUES
(1, 1, '120.00', '15000.00', 125, 1, '0.00'),
(2, 2, '100.00', '3000.00', 30, 2, '0.00'),
(3, 1, '200.00', '2000.00', 10, 2, '0.00'),
(4, 1, '120.00', '1200.00', 10, 3, '0.00'),
(5, 2, '60.00', '600.00', 10, 3, '0.00'),
(6, 1, '180.00', '1800.00', 10, 4, '220.00'),
(7, 3, '50.00', '500.00', 10, 5, '60.00');

-- --------------------------------------------------------

--
-- Table structure for table `sales`
--

CREATE TABLE `sales` (
  `SaleId` bigint(10) NOT NULL,
  `CusId` bigint(10) NOT NULL,
  `timestamp` datetime DEFAULT NULL,
  `vat` decimal(10,2) NOT NULL,
  `total` bigint(20) NOT NULL,
  `netTotal` bigint(20) NOT NULL,
  `discount` bigint(20) NOT NULL,
  `paidAmount` bigint(20) NOT NULL,
  `returnAmount` bigint(20) NOT NULL,
  `dueAmount` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `sales`
--

INSERT INTO `sales` (`SaleId`, `CusId`, `timestamp`, `vat`, `total`, `netTotal`, `discount`, `paidAmount`, `returnAmount`, `dueAmount`) VALUES
(7, 1, '2021-09-23 11:20:55', '0.00', 2800, 2800, 200, 2500, 0, 300),
(8, 1, '2021-09-24 09:47:44', '0.00', 950, 950, 50, 1000, 50, 0),
(9, 1, '2021-09-24 12:40:50', '0.00', 2000, 2000, 200, 2000, 0, 0),
(10, 2, '2021-09-24 15:27:33', '0.00', 2400, 2400, 20, 2500, 100, 0),
(11, 1, '2021-09-24 15:34:59', '0.00', 2200, 2200, 0, 2000, 0, 200),
(12, 1, '2021-09-24 15:38:56', '0.00', 1900, 1900, 80, 2000, 100, 0),
(13, 1, '2021-09-24 15:40:39', '0.00', 2200, 2200, 0, 2220, 20, 0),
(14, 1, '2021-09-24 15:49:06', '0.00', 2000, 2000, 200, 2000, 0, 0),
(15, 1, '2021-09-24 15:49:56', '0.00', 2000, 2000, 200, 2000, 0, 0),
(16, 1, '2021-09-24 15:51:04', '0.00', 220, 220, 0, 220, 0, 0),
(17, 1, '2021-09-24 17:00:04', '0.00', 1900, 1900, 80, 2000, 100, 0),
(18, 1, '2021-09-25 00:07:07', '0.00', 2600, 2600, 200, 2500, 0, 100),
(19, 1, '2021-09-25 11:16:01', '0.00', 300, 300, 0, 200, 0, 100),
(20, 2, '2021-09-25 11:19:57', '0.00', 220, 220, 0, 0, 0, 220);

-- --------------------------------------------------------

--
-- Table structure for table `sale_details`
--

CREATE TABLE `sale_details` (
  `SaleDetailId` bigint(20) NOT NULL,
  `ItemName` varchar(50) NOT NULL,
  `Qty` int(11) NOT NULL,
  `Total` decimal(18,2) NOT NULL,
  `SaleId` bigint(20) NOT NULL,
  `Price` bigint(20) NOT NULL,
  `item_id` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `sale_details`
--

INSERT INTO `sale_details` (`SaleDetailId`, `ItemName`, `Qty`, `Total`, `SaleId`, `Price`, `item_id`) VALUES
(3, ' Apple ', 25, '3000.00', 7, 120, 0),
(4, ' Floor ', 10, '1000.00', 8, 100, 0),
(5, ' Apple ', 10, '2200.00', 9, 220, 0),
(6, ' Apple ', 11, '2420.00', 10, 220, 0),
(7, ' Apple ', 10, '2200.00', 11, 220, 0),
(8, ' Apple ', 9, '1980.00', 12, 220, 0),
(9, ' Apple ', 10, '2200.00', 13, 220, 0),
(10, ' Apple ', 10, '2200.00', 14, 220, 0),
(11, ' Apple ', 10, '2200.00', 15, 220, 0),
(12, ' Apple ', 1, '220.00', 16, 220, 0),
(13, ' Apple ', 9, '1980.00', 17, 220, 0),
(14, ' Apple ', 10, '2200.00', 18, 220, 0),
(15, ' Floor ', 10, '600.00', 18, 60, 0),
(16, ' Rope ', 5, '300.00', 19, 60, 0),
(17, ' Apple ', 1, '220.00', 20, 220, 0);

-- --------------------------------------------------------

--
-- Table structure for table `suppliers`
--

CREATE TABLE `suppliers` (
  `id` bigint(20) NOT NULL,
  `name` varchar(50) NOT NULL,
  `address` varchar(100) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `phone` varchar(20) NOT NULL,
  `status` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `suppliers`
--

INSERT INTO `suppliers` (`id`, `name`, `address`, `email`, `phone`, `status`) VALUES
(1, 'KK Supllier', 'brt', 'kk@gmail.com', '1234567890', 'Active'),
(2, 'Deepak', 'sdgh', 'd@gmail.com', '098765431', 'Active');

-- --------------------------------------------------------

--
-- Table structure for table `unit`
--

CREATE TABLE `unit` (
  `id` bigint(20) NOT NULL,
  `name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `status` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `unit`
--

INSERT INTO `unit` (`id`, `name`, `status`) VALUES
(1, 'Kg', 'Active'),
(2, 'gram', 'Active'),
(3, 'meter', 'Active');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `audits`
--
ALTER TABLE `audits`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `customers`
--
ALTER TABLE `customers`
  ADD PRIMARY KEY (`CusId`),
  ADD UNIQUE KEY `FullName` (`FullName`);

--
-- Indexes for table `customer_transaction`
--
ALTER TABLE `customer_transaction`
  ADD PRIMARY KEY (`id`),
  ADD KEY `customer` (`customer_id`);

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `purchases`
--
ALTER TABLE `purchases`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `purchase_details`
--
ALTER TABLE `purchase_details`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `sales`
--
ALTER TABLE `sales`
  ADD PRIMARY KEY (`SaleId`);

--
-- Indexes for table `sale_details`
--
ALTER TABLE `sale_details`
  ADD PRIMARY KEY (`SaleDetailId`);

--
-- Indexes for table `suppliers`
--
ALTER TABLE `suppliers`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `unit`
--
ALTER TABLE `unit`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `audits`
--
ALTER TABLE `audits`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `customers`
--
ALTER TABLE `customers`
  MODIFY `CusId` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `customer_transaction`
--
ALTER TABLE `customer_transaction`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT for table `items`
--
ALTER TABLE `items`
  MODIFY `id` bigint(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `purchases`
--
ALTER TABLE `purchases`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `purchase_details`
--
ALTER TABLE `purchase_details`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `sales`
--
ALTER TABLE `sales`
  MODIFY `SaleId` bigint(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `sale_details`
--
ALTER TABLE `sale_details`
  MODIFY `SaleDetailId` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `suppliers`
--
ALTER TABLE `suppliers`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `unit`
--
ALTER TABLE `unit`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
