CREATE TABLE `sessions` (
  `uuid` varchar(36) NOT NULL COMMENT 'User Unique ID identical to uuid from accounts table',
  `username` varchar(32) DEFAULT NULL COMMENT 'User username identical to username from accounts table',
  `serverId` varchar(48) DEFAULT NULL COMMENT 'User last server ID',
  `sessionId` varchar(255) DEFAULT NULL COMMENT 'User current session ID',
  `clientToken` varchar(36) DEFAULT NULL COMMENT 'User current client token',
  PRIMARY KEY (`uuid`),
  UNIQUE KEY `uuid_UNIQUE` (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3