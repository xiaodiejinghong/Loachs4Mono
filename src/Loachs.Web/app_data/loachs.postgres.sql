--
-- PostgreSQL database dump
--

SET statement_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = public, pg_catalog;

--
-- Name: seq_comments_commentid; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE seq_comments_commentid
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.seq_comments_commentid OWNER TO postgres;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: loachs_comments; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE loachs_comments (
    commentid integer DEFAULT nextval('seq_comments_commentid'::regclass) NOT NULL,
    postid integer,
    parentid integer,
    userid integer,
    name character varying(50),
    email character varying(50),
    siteurl character varying(200),
    content text,
    ipaddress character varying(50),
    emailnotify integer,
    approved integer,
    createdate timestamp without time zone,
    upsize_ts bytea
);


ALTER TABLE public.loachs_comments OWNER TO postgres;

--
-- Name: seq_links_linkid; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE seq_links_linkid
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.seq_links_linkid OWNER TO postgres;

--
-- Name: loachs_links; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE loachs_links (
    linkid integer DEFAULT nextval('seq_links_linkid'::regclass) NOT NULL,
    type integer,
    name character varying(255),
    href character varying(255),
    "position" integer,
    target character varying(50),
    description character varying(255),
    displayorder integer,
    status integer,
    createdate timestamp without time zone
);


ALTER TABLE public.loachs_links OWNER TO postgres;

--
-- Name: seq_posts_postid; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE seq_posts_postid
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.seq_posts_postid OWNER TO postgres;

--
-- Name: loachs_posts; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE loachs_posts (
    postid integer DEFAULT nextval('seq_posts_postid'::regclass) NOT NULL,
    categoryid integer NOT NULL,
    title character varying(255),
    slug character varying(255),
    imageurl character varying(255),
    summary text,
    content text,
    userid integer NOT NULL,
    commentstatus integer NOT NULL,
    commentcount integer NOT NULL,
    viewcount integer NOT NULL,
    tag character varying(255),
    urlformat integer,
    template character varying(50),
    recommend integer,
    status integer,
    topstatus integer,
    hidestatus integer,
    createdate timestamp without time zone,
    updatedate timestamp without time zone,
    upsize_ts bytea
);


ALTER TABLE public.loachs_posts OWNER TO postgres;

--
-- Name: seq_sites_siteid; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE seq_sites_siteid
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.seq_sites_siteid OWNER TO postgres;

--
-- Name: loachs_sites; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE loachs_sites (
    siteid integer DEFAULT nextval('seq_sites_siteid'::regclass) NOT NULL,
    postcount integer,
    commentcount integer,
    visitcount integer,
    tagcount integer,
    setting text,
    upsize_ts bytea
);


ALTER TABLE public.loachs_sites OWNER TO postgres;

--
-- Name: seq_terms_termid; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE seq_terms_termid
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.seq_terms_termid OWNER TO postgres;

--
-- Name: loachs_terms; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE loachs_terms (
    termid integer DEFAULT nextval('seq_terms_termid'::regclass) NOT NULL,
    type integer,
    name character varying(255),
    slug character varying(255),
    description character varying(255),
    displayorder integer,
    count integer,
    createdate timestamp without time zone
);


ALTER TABLE public.loachs_terms OWNER TO postgres;

--
-- Name: seq_users_userid; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE seq_users_userid
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.seq_users_userid OWNER TO postgres;

--
-- Name: loachs_users; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE loachs_users (
    userid integer DEFAULT nextval('seq_users_userid'::regclass) NOT NULL,
    type integer,
    username character varying(50),
    name character varying(50),
    password character varying(50),
    email character varying(50),
    siteurl character varying(255),
    avatarurl character varying(255),
    description character varying(255),
    displayorder integer,
    status integer,
    postcount integer,
    commentcount integer,
    createdate timestamp without time zone
);


ALTER TABLE public.loachs_users OWNER TO postgres;

--
-- Data for Name: loachs_comments; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY loachs_comments (commentid, postid, parentid, userid, name, email, siteurl, content, ipaddress, emailnotify, approved, createdate, upsize_ts) FROM stdin;
1	1	0	0	loachs	test@test.com	http://www.loachs.com	这是评论<br />可以删除	127.0.0.1	1	1	2009-12-16 16:43:49	\\x00000000000007e0
\.


--
-- Data for Name: loachs_links; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY loachs_links (linkid, type, name, href, "position", target, description, displayorder, status, createdate) FROM stdin;
1	0	首页	${siteurl}	1	_self	首页	1000	1	2009-12-16 16:42:05
2	0	管理	${siteurl}admin	1	_self	后台管理	1000	1	2009-12-16 16:42:23
3	0	小泥鳅发源地	http://www.loachs.com/	2	_blank	小泥鳅官方站	1000	1	2009-12-16 16:42:45
\.


--
-- Data for Name: loachs_posts; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY loachs_posts (postid, categoryid, title, slug, imageurl, summary, content, userid, commentstatus, commentcount, viewcount, tag, urlformat, template, recommend, status, topstatus, hidestatus, createdate, updatedate, upsize_ts) FROM stdin;
1	1	欢迎使用小泥鳅程序	welcome-use-loachs	\N	<p>\r\n\t欢迎使用小泥鳅程序</p>\r\n<p>\r\n\t如有问题或意见,欢迎与作者交流</p>\r\n<p>\r\n\t<a href="http://www.loachs.com">http://www.loachs.com</a></p>\r\n	<p>\r\n\t欢迎使用小泥鳅程序</p>\r\n<p>\r\n\t如有问题或意见,欢迎与作者交流</p>\r\n<p>\r\n\t<a href="http://www.loachs.com">http://www.loachs.com</a></p>\r\n	1	1	1	9	{2}	2	post.html	0	1	0	0	2009-12-16 00:00:00	2014-01-18 00:00:00	\\x000000000000082a
6	0	Postgres测试		\N		<p>\r\n\t<img src="/upfiles/201401/tx065x100.jpg" /></p>\r\n<p>\r\n\t&nbsp;</p>\r\n<p>\r\n\t&nbsp;</p>\r\n<p>\r\n\t你知道LInux.NET吗？不知道请看：&nbsp;http://www.cnblogs.com/xiaodiejinghong/</p>\r\n	1	1	0	2		1	post.html	0	1	0	0	2014-01-24 00:00:00	2014-01-24 00:00:00	\N
\.


--
-- Data for Name: loachs_sites; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY loachs_sites (siteid, postcount, commentcount, visitcount, tagcount, setting, upsize_ts) FROM stdin;
1	2	1	112	3	<?xml version="1.0" encoding="utf-8"?>\r\n<SettingInfo xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">\r\n  <SendMailAuthorByPost>0</SendMailAuthorByPost>\r\n  <SendMailAuthorByComment>0</SendMailAuthorByComment>\r\n  <SendMailNotifyByComment>1</SendMailNotifyByComment>\r\n  <SmtpEmail />\r\n  <SmtpServer />\r\n  <SmtpServerPost>25</SmtpServerPost>\r\n  <SmtpUserName />\r\n  <SmtpPassword />\r\n  <SmtpEnableSsl>1</SmtpEnableSsl>\r\n  <SiteName>小泥鳅</SiteName>\r\n  <SiteDescription>感谢您选择小泥鳅</SiteDescription>\r\n  <MetaKeywords>这里填您的网站关键字,用逗号隔开</MetaKeywords>\r\n  <MetaDescription>这里填写您网站的简介</MetaDescription>\r\n  <SiteStatus>1</SiteStatus>\r\n  <SiteTotalType>1</SiteTotalType>\r\n  <EnableVerifyCode>1</EnableVerifyCode>\r\n  <CommentStatus>1</CommentStatus>\r\n  <CommentOrder>1</CommentOrder>\r\n  <CommentApproved>2</CommentApproved>\r\n  <CommentSpamwords>虚拟主机,域名注册,出租网,铃声下载,手机铃声,和弦铃声,手机游戏,免费铃声,彩铃,网站建设</CommentSpamwords>\r\n  <RssStatus>1</RssStatus>\r\n  <RssRowCount>20</RssRowCount>\r\n  <RssShowType>2</RssShowType>\r\n  <SidebarPostCount>10</SidebarPostCount>\r\n  <SidebarCommentCount>10</SidebarCommentCount>\r\n  <SidebarTagCount>10</SidebarTagCount>\r\n  <PageSizePostCount>10</PageSizePostCount>\r\n  <PageSizeCommentCount>50</PageSizeCommentCount>\r\n  <RewriteExtension>.aspx</RewriteExtension>\r\n  <FooterHtml>? 2008-2010 小泥鳅官方网站 版权所有</FooterHtml>\r\n  <Theme>lightword</Theme>\r\n  <MobileTheme>mobile</MobileTheme>\r\n  <WatermarkType>1</WatermarkType>\r\n  <WatermarkPosition>4</WatermarkPosition>\r\n  <WatermarkTransparency>8</WatermarkTransparency>\r\n  <WatermarkQuality>80</WatermarkQuality>\r\n  <WatermarkText>小泥鳅</WatermarkText>\r\n  <WatermarkFontSize>14</WatermarkFontSize>\r\n  <WatermarkFontName>Tahoma</WatermarkFontName>\r\n  <WatermarkImage>watermark.gif</WatermarkImage>\r\n  <PostRelatedCount>5</PostRelatedCount>\r\n  <PostShowType>2</PostShowType>\r\n</SettingInfo>	\\x000000000000082b
\.


--
-- Data for Name: loachs_terms; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY loachs_terms (termid, type, name, slug, description, displayorder, count, createdate) FROM stdin;
2	2	小泥鳅	小泥鳅	小泥鳅	1000	1	2009-12-16 00:00:00
1	1	默认分类	default	这是系统自动添加的默认分类	1000	1	2009-12-16 00:00:00
3	2	dd	dd	dd	1000	0	2014-01-19 00:00:00
4	2	aa	aa	aa	1000	0	2014-01-19 00:00:00
\.


--
-- Data for Name: loachs_users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY loachs_users (userid, type, username, name, password, email, siteurl, avatarurl, description, displayorder, status, postcount, commentcount, createdate) FROM stdin;
2	1	le	lele	D9180594744F870AEEFB086982E980BB					500	1	0	0	2014-01-19 00:00:00
3	5	a	a	0CC175B9C0F1B6A831C399E269772661					1	1	0	0	2014-01-19 00:00:00
1	1	admin	admin	7FEF6171469E80D32C0559F88B377245					1000	1	2	2	2009-12-16 00:00:00
\.


--
-- Name: seq_comments_commentid; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('seq_comments_commentid', 5, true);


--
-- Name: seq_links_linkid; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('seq_links_linkid', 4, true);


--
-- Name: seq_posts_postid; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('seq_posts_postid', 6, true);


--
-- Name: seq_sites_siteid; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('seq_sites_siteid', 1, true);


--
-- Name: seq_terms_termid; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('seq_terms_termid', 4, true);


--
-- Name: seq_users_userid; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('seq_users_userid', 3, true);


--
-- Name: loachs_comments_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY loachs_comments
    ADD CONSTRAINT loachs_comments_pkey PRIMARY KEY (commentid);


--
-- Name: loachs_links_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY loachs_links
    ADD CONSTRAINT loachs_links_pkey PRIMARY KEY (linkid);


--
-- Name: ArticleID; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX "ArticleID" ON loachs_comments USING btree (postid);


--
-- Name: CommentID; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX "CommentID" ON loachs_comments USING btree (commentid);


--
-- Name: LinkID; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX "LinkID" ON loachs_links USING btree (linkid);


--
-- Name: ParentID; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX "ParentID" ON loachs_comments USING btree (parentid);


--
-- Name: UserID; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX "UserID" ON loachs_comments USING btree (userid);


--
-- Name: categoryid; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX categoryid ON loachs_posts USING btree (categoryid);


--
-- Name: createdate; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX createdate ON loachs_comments USING btree (createdate);


--
-- Name: public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


--
-- PostgreSQL database dump complete
--

