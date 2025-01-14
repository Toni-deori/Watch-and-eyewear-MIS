-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 27, 2024 at 06:58 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `eyeandwatchmis`
--

-- --------------------------------------------------------

--
-- Table structure for table `lowstockalerts`
--

CREATE TABLE `lowstockalerts` (
  `AlertID` int(11) NOT NULL,
  `ProductID` int(11) NOT NULL,
  `AlertDate` datetime NOT NULL,
  `Status` enum('Pending','Resolved') DEFAULT 'Pending'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `lowstockalerts`
--

INSERT INTO `lowstockalerts` (`AlertID`, `ProductID`, `AlertDate`, `Status`) VALUES
(1, 3, '2024-11-19 09:00:00', 'Resolved'),
(2, 4, '2024-11-20 10:00:00', 'Resolved'),
(3, 1, '2024-11-24 14:58:50', 'Resolved'),
(4, 4, '2024-11-24 16:01:36', 'Resolved'),
(5, 4, '2024-11-25 11:50:17', 'Resolved'),
(6, 18, '2024-11-25 12:32:30', 'Resolved');

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `ProductID` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Category` enum('Eyewear','Watches') NOT NULL,
  `SubCategory` varchar(50) DEFAULT NULL,
  `Price` decimal(10,2) NOT NULL,
  `Discount` decimal(5,2) DEFAULT 0.00,
  `StockQuantity` int(11) NOT NULL,
  `ImagePath` varchar(255) DEFAULT NULL,
  `Description` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`ProductID`, `Name`, `Category`, `SubCategory`, `Price`, `Discount`, `StockQuantity`, `ImagePath`, `Description`) VALUES
(1, 'Aviator Shades', 'Eyewear', 'Sunglasses', 99.99, 10.00, 48, 'C:\\Users\\hp\\Desktop\\eyeandwatchMIS\\imagerepo\\aviator.jpg', 'Aviator Shades feature a timeless, classic design with a thin metal frame and teardrop-shaped lenses. Known for their stylish and sophisticated look, these sunglasses offer excellent UV protection while ensuring comfort with their lightweight build. Perfect for any occasion, Aviator Shades add a cool, effortless touch to your outfit.'),
(2, 'wafer Shades', 'Eyewear', 'Sunglasses', 79.99, 5.00, 29, 'C:\\Users\\hp\\Desktop\\eyeandwatchMIS\\imagerepo\\wafer.jpg', 'Wafer Shades offer a sleek, minimalist design with a square frame and lightweight construction. Perfect for everyday wear, they provide excellent UV protection while adding a modern touch to your style. Simple yet stylish, these shades are the ideal accessory for any occasion.'),
(3, 'Sport Watch', 'Watches', 'Wristwatches', 199.99, 20.00, 19, 'C:\\Users\\hp\\Desktop\\eyeandwatchMIS\\imagerepo\\sports.jpg', 'Our Sports Watch combines durability and functionality in a sleek design. Featuring a stopwatch, heart rate monitor, and GPS tracking, it’s perfect for athletes and fitness enthusiasts. With a water-resistant build and easy-to-read display, this watch keeps you on track during any activity.'),
(4, 'Luxury Watch', 'Watches', 'Wristwatches', 499.99, 0.00, 14, 'C:\\Users\\hp\\Desktop\\eyeandwatchMIS\\imagerepo\\luxury.jpg', 'Introducing our Luxury Watch, a symbol of elegance and precision. Crafted with the finest materials, this timepiece features a sophisticated design with a sleek, polished finish. It combines timeless style with advanced functionality, offering exceptional durability and accuracy. Whether for formal occasions or everyday wear, this luxury watch adds a touch of refinement to any outfit.'),
(5, 'Reading Glasses', 'Eyewear', 'Reading Glasses', 49.99, 5.00, 100, 'C:\\Users\\hp\\Desktop\\eyeandwatchMIS\\imagerepo\\reading.jpg', 'Our Reading Glasses offer both comfort and style for those who need a little extra help with close-up tasks. Featuring a lightweight, durable frame and high-quality lenses, they provide clear, distortion-free vision for reading, writing, or working on your devices. With a sleek, modern design, these glasses are as fashionable as they are functional, making them the perfect accessory for everyday use.'),
(18, 'bikram tripathi', 'Eyewear', 'Contact Lenses', 0.00, 0.00, 0, 'C:\\Users\\hp\\Desktop\\Three Mile Island_1200x630.png', 'Gandu Insaan');

-- --------------------------------------------------------

--
-- Table structure for table `sales`
--

CREATE TABLE `sales` (
  `SaleID` int(11) NOT NULL,
  `Date` datetime NOT NULL,
  `ClerkID` int(11) NOT NULL,
  `CustomerName` varchar(100) DEFAULT NULL,
  `TotalAmount` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `sales`
--

INSERT INTO `sales` (`SaleID`, `Date`, `ClerkID`, `CustomerName`, `TotalAmount`) VALUES
(1, '2024-10-20 10:45:00', 2, 'John Doe', 249.97),
(2, '2024-11-20 11:30:00', 3, 'Jane Smith', 79.99),
(3, '2024-09-20 13:15:00', 2, 'Michael Carter', 499.99),
(16, '2024-11-24 14:25:38', 2, 'john', 88.15),
(17, '2024-11-24 14:42:07', 2, 'brian', 104.39),
(18, '2024-11-24 16:01:24', 2, 'emily', 1159.98),
(19, '2024-11-25 10:04:24', 2, 'john', 5405.07),
(20, '2024-11-25 11:49:45', 2, '', 5428.67),
(21, '2024-11-25 12:33:42', 2, 'moii', 0.00);

-- --------------------------------------------------------

--
-- Table structure for table `salesdetails`
--

CREATE TABLE `salesdetails` (
  `SaleDetailID` int(11) NOT NULL,
  `SaleID` int(11) NOT NULL,
  `ProductID` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `PriceAtSale` decimal(10,2) NOT NULL,
  `DiscountAtSale` decimal(5,2) DEFAULT 0.00
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `salesdetails`
--

INSERT INTO `salesdetails` (`SaleDetailID`, `SaleID`, `ProductID`, `Quantity`, `PriceAtSale`, `DiscountAtSale`) VALUES
(1, 1, 1, 2, 99.99, 10.00),
(2, 1, 5, 1, 49.99, 5.00),
(3, 2, 2, 1, 79.99, 5.00),
(4, 3, 4, 1, 499.99, 0.00),
(8, 16, 2, 1, 79.99, 5.00),
(9, 17, 1, 1, 99.99, 10.00),
(10, 18, 4, 2, 499.99, 0.00),
(11, 19, 1, 50, 99.99, 10.00),
(12, 19, 3, 1, 199.99, 20.00),
(13, 20, 1, 2, 99.99, 10.00),
(14, 20, 4, 9, 499.99, 0.00),
(15, 21, 18, 1, 0.00, 0.00);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `UserID` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `gender` varchar(12) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `address` varchar(255) NOT NULL,
  `contact_no` varchar(11) NOT NULL,
  `Role` enum('Admin','Clerk') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserID`, `name`, `gender`, `Username`, `Password`, `address`, `contact_no`, `Role`) VALUES
(1, 'Emma Carter', 'Female', 'admin', 'root', '45 Maple Street, Springfield, IL 62701, USA', '9366844098', 'Admin'),
(2, 'Lucas Bennett', 'Male', 'clerk', 'clerk', '210 Ocean View Drive, Miami Beach, FL 33139, USA', '9366844043', 'Clerk'),
(3, 'Sophia Morgan', 'Female', 'clerk_002', 'clerk', '88 Elmwood Avenue, Brooklyn, NY 11201, USA', '876844098', 'Clerk'),
(4, 'johhny', 'Male', 'johnny', 'root', '456 Oakwood Drive, Springfield, IL 62704, USA', '9436152673', 'Admin');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `lowstockalerts`
--
ALTER TABLE `lowstockalerts`
  ADD PRIMARY KEY (`AlertID`),
  ADD KEY `ProductID` (`ProductID`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`ProductID`);

--
-- Indexes for table `sales`
--
ALTER TABLE `sales`
  ADD PRIMARY KEY (`SaleID`),
  ADD KEY `ClerkID` (`ClerkID`);

--
-- Indexes for table `salesdetails`
--
ALTER TABLE `salesdetails`
  ADD PRIMARY KEY (`SaleDetailID`),
  ADD KEY `SaleID` (`SaleID`),
  ADD KEY `ProductID` (`ProductID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserID`),
  ADD UNIQUE KEY `Username` (`Username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `lowstockalerts`
--
ALTER TABLE `lowstockalerts`
  MODIFY `AlertID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `ProductID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `sales`
--
ALTER TABLE `sales`
  MODIFY `SaleID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT for table `salesdetails`
--
ALTER TABLE `salesdetails`
  MODIFY `SaleDetailID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `UserID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `lowstockalerts`
--
ALTER TABLE `lowstockalerts`
  ADD CONSTRAINT `lowstockalerts_ibfk_1` FOREIGN KEY (`ProductID`) REFERENCES `products` (`ProductID`);

--
-- Constraints for table `sales`
--
ALTER TABLE `sales`
  ADD CONSTRAINT `sales_ibfk_1` FOREIGN KEY (`ClerkID`) REFERENCES `users` (`UserID`);

--
-- Constraints for table `salesdetails`
--
ALTER TABLE `salesdetails`
  ADD CONSTRAINT `salesdetails_ibfk_1` FOREIGN KEY (`SaleID`) REFERENCES `sales` (`SaleID`),
  ADD CONSTRAINT `salesdetails_ibfk_2` FOREIGN KEY (`ProductID`) REFERENCES `products` (`ProductID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
