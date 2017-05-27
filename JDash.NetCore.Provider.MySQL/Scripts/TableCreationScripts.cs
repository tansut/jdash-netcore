using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDash.NetCore.Provider.MySQL.Scripts
{
    public class TableCreationScripts
    {
        // CREATE SCHEMA `jdash_mysql_demo` ;

        public static string DashboardCreateScript = @"CREATE TABLE `dashboard` (
                                                          `id` int(20) NOT NULL AUTO_INCREMENT,
                                                          `appId` varchar(45) NOT NULL,
                                                          `title` varchar(200) DEFAULT NULL,
                                                          `shareWith` varchar(200) DEFAULT NULL,
                                                          `description` varchar(500) DEFAULT NULL,
                                                          `user` varchar(200) NOT NULL,
                                                          `createdAt` datetime NOT NULL,
                                                          `config` varchar(2000) DEFAULT NULL,
                                                          `layout` varchar(2000) DEFAULT NULL,
                                                          PRIMARY KEY (`id`),
                                                          UNIQUE KEY `Id_UNIQUE` (`id`)
                                                        )
                                                    ";

        public static string DashletCreateScript = @"CREATE TABLE `dashlet` (
                                                          `id` int(20) NOT NULL AUTO_INCREMENT,
                                                          `moduleId` varchar(100) NOT NULL,
                                                          `dashboardId` bigint(20) NOT NULL,
                                                          `configuration` varchar(2000) DEFAULT NULL,
                                                          `title` varchar(200) DEFAULT NULL,
                                                          `description` varchar(2000) DEFAULT NULL,
                                                          `createdAt` datetime NOT NULL,
                                                          PRIMARY KEY (`id`)
                                                        )
                                                    ";

    }
}
