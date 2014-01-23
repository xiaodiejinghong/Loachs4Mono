-- MySQL dump 10.13  Distrib 5.1.71, for redhat-linux-gnu (x86_64)
--
-- Host: 127.0.0.1    Database: loachs
-- ------------------------------------------------------
-- Server version	5.1.71

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES UTF8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Loachs_Comments`
--

DROP TABLE IF EXISTS `Loachs_Comments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Loachs_Comments` (
  `commentid` int(11) NOT NULL AUTO_INCREMENT,
  `postid` int(11) DEFAULT NULL,
  `parentid` int(11) DEFAULT NULL,
  `userid` int(11) DEFAULT NULL,
  `name` varchar(50) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `siteurl` varchar(200) DEFAULT NULL,
  `content` longtext,
  `ipaddress` varchar(50) DEFAULT NULL,
  `emailnotify` int(11) DEFAULT NULL,
  `approved` int(11) DEFAULT NULL,
  `createdate` datetime DEFAULT NULL,
  `upsize_ts` longblob,
  PRIMARY KEY (`commentid`),
  KEY `ArticleID` (`postid`),
  KEY `CommentID` (`commentid`),
  KEY `createdate` (`createdate`),
  KEY `ParentID` (`parentid`),
  KEY `UserID` (`userid`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Loachs_Comments`
--

LOCK TABLES `Loachs_Comments` WRITE;
/*!40000 ALTER TABLE `Loachs_Comments` DISABLE KEYS */;
INSERT INTO `Loachs_Comments` VALUES (1,1,0,0,'loachs','test@test.com','http://www.loachs.com','这是评论<br />可以删除','127.0.0.1',1,1,'2009-12-16 16:43:49','\0\0\0\0\0\0�');
/*!40000 ALTER TABLE `Loachs_Comments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Loachs_Links`
--

DROP TABLE IF EXISTS `Loachs_Links`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Loachs_Links` (
  `linkid` int(11) NOT NULL AUTO_INCREMENT,
  `type` int(11) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `href` varchar(255) DEFAULT NULL,
  `position` int(11) DEFAULT NULL,
  `target` varchar(50) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `displayorder` int(11) DEFAULT NULL,
  `status` int(11) DEFAULT NULL,
  `createdate` datetime DEFAULT NULL,
  PRIMARY KEY (`linkid`),
  KEY `LinkID` (`linkid`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Loachs_Links`
--

LOCK TABLES `Loachs_Links` WRITE;
/*!40000 ALTER TABLE `Loachs_Links` DISABLE KEYS */;
INSERT INTO `Loachs_Links` VALUES (1,0,'首页','${siteurl}',1,'_self','首页',1000,1,'2009-12-16 16:42:05'),(2,0,'管理','${siteurl}admin',1,'_self','后台管理',1000,1,'2009-12-16 16:42:23'),(3,0,'小泥鳅发源地','http://www.loachs.com/',2,'_blank','小泥鳅官方站',1000,1,'2009-12-16 16:42:45');
/*!40000 ALTER TABLE `Loachs_Links` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Loachs_Posts`
--

DROP TABLE IF EXISTS `Loachs_Posts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Loachs_Posts` (
  `postid` int(11) NOT NULL AUTO_INCREMENT,
  `categoryid` int(11) NOT NULL,
  `title` varchar(255) DEFAULT NULL,
  `slug` varchar(255) DEFAULT NULL,
  `imageurl` varchar(255) DEFAULT NULL,
  `summary` longtext,
  `content` longtext,
  `userid` int(11) NOT NULL,
  `commentstatus` int(11) NOT NULL,
  `commentcount` int(11) NOT NULL,
  `viewcount` int(11) NOT NULL,
  `tag` varchar(255) DEFAULT NULL,
  `urlformat` int(11) DEFAULT NULL,
  `template` varchar(50) DEFAULT NULL,
  `recommend` int(11) DEFAULT NULL,
  `status` int(11) DEFAULT NULL,
  `topstatus` int(11) DEFAULT NULL,
  `hidestatus` int(11) DEFAULT NULL,
  `createdate` datetime DEFAULT NULL,
  `updatedate` datetime DEFAULT NULL,
  `upsize_ts` longblob,
  PRIMARY KEY (`postid`),
  KEY `categoryid` (`categoryid`),
  KEY `createdate` (`createdate`),
  KEY `hidestatus` (`hidestatus`),
  KEY `recommend` (`recommend`),
  KEY `status` (`status`),
  KEY `topstatus` (`topstatus`),
  KEY `userid` (`userid`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Loachs_Posts`
--

LOCK TABLES `Loachs_Posts` WRITE;
/*!40000 ALTER TABLE `Loachs_Posts` DISABLE KEYS */;
INSERT INTO `Loachs_Posts` VALUES (1,1,'欢迎使用小泥鳅程序','welcome-use-loachs',NULL,'<p>\r\n	欢迎使用小泥鳅程序</p>\r\n<p>\r\n	如有问题或意见,欢迎与作者交流</p>\r\n\r\n<p>\r\n	<a href=\"http://www.loachs.com\">http://www.loachs.com</a></p>','<p>\r\n	欢迎使用小泥鳅程序</p>\r\n<p>\r\n	如有问题或意见,欢迎与作者交流</p>\r\n\r\n<p>\r\n	<a href=\"http://www.loachs.com\">http://www.loachs.com</a></p>',1,1,1,10,'{2}',2,'post.html',0,1,0,0,'2009-12-16 16:40:59','2009-12-16 16:44:31','\0\0\0\0\0\0*'),(3,0,'测试mysql','',NULL,'','<p>\r\n	<img src=\"/upfiles/201401/tx065x100.jpg\" /></p>\r\n<p>\r\n	&nbsp;</p>\r\n<p>\r\n	你知道什么是Linux.NET吗？不知道请见：http://www.cnblogs.com/xiaodiejinghong/</p>\r\n',1,1,0,6,'',1,'post.html',0,1,0,0,'2014-01-06 00:00:00','2014-01-24 00:00:00',NULL);
/*!40000 ALTER TABLE `Loachs_Posts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Loachs_Sites`
--

DROP TABLE IF EXISTS `Loachs_Sites`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Loachs_Sites` (
  `siteid` int(11) NOT NULL AUTO_INCREMENT,
  `postcount` int(11) DEFAULT NULL,
  `commentcount` int(11) DEFAULT NULL,
  `visitcount` int(11) DEFAULT NULL,
  `tagcount` int(11) DEFAULT NULL,
  `setting` longtext,
  `upsize_ts` longblob,
  PRIMARY KEY (`siteid`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Loachs_Sites`
--

LOCK TABLES `Loachs_Sites` WRITE;
/*!40000 ALTER TABLE `Loachs_Sites` DISABLE KEYS */;
INSERT INTO `Loachs_Sites` VALUES (1,2,1,153,1,'<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<SettingInfo xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <SendMailAuthorByPost>0</SendMailAuthorByPost>\r\n  <SendMailAuthorByComment>0</SendMailAuthorByComment>\r\n  <SendMailNotifyByComment>1</SendMailNotifyByComment>\r\n  <SmtpEmail />\r\n  <SmtpServer />\r\n  <SmtpServerPost>25</SmtpServerPost>\r\n  <SmtpUserName />\r\n  <SmtpPassword />\r\n  <SmtpEnableSsl>1</SmtpEnableSsl>\r\n  <SiteName>小泥鳅</SiteName>\r\n  <SiteDescription>感谢您选择小泥鳅</SiteDescription>\r\n  <MetaKeywords>这里填您的网站关键字,用逗号隔开</MetaKeywords>\r\n  <MetaDescription>这里填写您网站的简介</MetaDescription>\r\n  <SiteStatus>1</SiteStatus>\r\n  <SiteTotalType>1</SiteTotalType>\r\n  <EnableVerifyCode>1</EnableVerifyCode>\r\n  <CommentStatus>1</CommentStatus>\r\n  <CommentOrder>1</CommentOrder>\r\n  <CommentApproved>2</CommentApproved>\r\n  <CommentSpamwords>虚拟主机,域名注册,出租网,铃声下载,手机铃声,和弦铃声,手机游戏,免费铃声,彩铃,网站建设</CommentSpamwords>\r\n  <RssStatus>1</RssStatus>\r\n  <RssRowCount>20</RssRowCount>\r\n  <RssShowType>2</RssShowType>\r\n  <SidebarPostCount>10</SidebarPostCount>\r\n  <SidebarCommentCount>10</SidebarCommentCount>\r\n  <SidebarTagCount>10</SidebarTagCount>\r\n  <PageSizePostCount>10</PageSizePostCount>\r\n  <PageSizeCommentCount>50</PageSizeCommentCount>\r\n  <RewriteExtension>.aspx</RewriteExtension>\r\n  <FooterHtml>? 2008-2010 小泥鳅官方网站 版权所有</FooterHtml>\r\n  <Theme>lightword</Theme>\r\n  <MobileTheme>mobile</MobileTheme>\r\n  <WatermarkType>1</WatermarkType>\r\n  <WatermarkPosition>4</WatermarkPosition>\r\n  <WatermarkTransparency>8</WatermarkTransparency>\r\n  <WatermarkQuality>80</WatermarkQuality>\r\n  <WatermarkText>小泥鳅</WatermarkText>\r\n  <WatermarkFontSize>14</WatermarkFontSize>\r\n  <WatermarkFontName>Tahoma</WatermarkFontName>\r\n  <WatermarkImage>watermark.gif</WatermarkImage>\r\n  <PostRelatedCount>5</PostRelatedCount>\r\n  <PostShowType>2</PostShowType>\r\n</SettingInfo>','\0\0\0\0\0\0+');
/*!40000 ALTER TABLE `Loachs_Sites` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Loachs_Terms`
--

DROP TABLE IF EXISTS `Loachs_Terms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Loachs_Terms` (
  `termid` int(11) NOT NULL AUTO_INCREMENT,
  `type` int(11) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `slug` varchar(255) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `displayorder` int(11) DEFAULT NULL,
  `count` int(11) DEFAULT NULL,
  `createdate` datetime DEFAULT NULL,
  PRIMARY KEY (`termid`),
  KEY `TagID` (`termid`),
  KEY `type` (`type`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Loachs_Terms`
--

LOCK TABLES `Loachs_Terms` WRITE;
/*!40000 ALTER TABLE `Loachs_Terms` DISABLE KEYS */;
INSERT INTO `Loachs_Terms` VALUES (1,1,'默认分类','default','这是系统自动添加的默认分类',1000,1,'2009-12-16 00:00:00'),(2,2,'小泥鳅','小泥鳅','小泥鳅',1000,1,'2009-12-16 00:00:00');
/*!40000 ALTER TABLE `Loachs_Terms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Loachs_Users`
--

DROP TABLE IF EXISTS `Loachs_Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Loachs_Users` (
  `userid` int(11) NOT NULL AUTO_INCREMENT,
  `type` int(11) DEFAULT NULL,
  `username` varchar(50) DEFAULT NULL,
  `name` varchar(50) DEFAULT NULL,
  `password` varchar(50) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `siteurl` varchar(255) DEFAULT NULL,
  `avatarurl` varchar(255) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `displayorder` int(11) DEFAULT NULL,
  `status` int(11) DEFAULT NULL,
  `postcount` int(11) DEFAULT NULL,
  `commentcount` int(11) DEFAULT NULL,
  `createdate` datetime DEFAULT NULL,
  PRIMARY KEY (`userid`),
  KEY `UserID` (`userid`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Loachs_Users`
--

LOCK TABLES `Loachs_Users` WRITE;
/*!40000 ALTER TABLE `Loachs_Users` DISABLE KEYS */;
INSERT INTO `Loachs_Users` VALUES (1,1,'admin','admin','7FEF6171469E80D32C0559F88B377245','','','','',1000,1,2,1,'2009-12-16 00:00:00');
/*!40000 ALTER TABLE `Loachs_Users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-01-24  6:21:57
