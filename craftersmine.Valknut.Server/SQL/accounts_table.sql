CREATE TABLE `accounts` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'Unique user id in DB',
  `username` varchar(32) DEFAULT NULL COMMENT 'Username',
  `password` varchar(255) DEFAULT NULL COMMENT 'User password encrypted using BCrypt',
  `uuid` varchar(32) DEFAULT NULL COMMENT 'Unique ID for user',
  `email` varchar(255) DEFAULT NULL COMMENT 'User EMail',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb3