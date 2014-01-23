<%@ Page Language="C#" MasterPageFile="admin.master" AutoEventWireup="true" CodeFile="upfilelist.aspx.cs" Inherits="admin_upfilelist" Title="无标题页" %>
<%@ Register src="UserControls/upfilemanager.ascx" tagname="upfilemanager" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:upfilemanager ID="upfilemanager1" runat="server" />
</asp:Content>

