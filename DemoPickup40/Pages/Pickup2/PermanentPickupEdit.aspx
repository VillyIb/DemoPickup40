﻿<%@ Page
    Title=""
    Language="C#"
    MasterPageFile="~/Master.Master"
    AutoEventWireup="true"
    CodeBehind="PermanentPickupEdit.aspx.cs"
    Inherits="DemoPickup40.Pages.Pickup2.PermanentPickupEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../assets/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/Site.css" rel="stylesheet" />
    <link href="../../assets/css/stylesheet-2016-02-23.css" rel="stylesheet" />
    <link href="PermanentPickup.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="XuUpdatePanel1" runat="server" UpdateMode="Always">

        <ContentTemplate>

            <h2>Permanent Pickup Settings</h2>

            <div id="XuPPEdit" class="css-table">
                <div class="css-tr">
                    <div class="css-th col1">Subject</div>
                    <div class="css-th col2">Value</div>
                    <div class="css-th col3">Note</div>
                </div>

                <div class="css-tr">
                    <div class="css-td">Permanent Pickup Enabled</div>
                    <div class="css-td">
                        <asp:CheckBox ID="XuEnabledMain" runat="server" Checked="True"></asp:CheckBox>
                    </div>
                    <div class="css-td">&nbsp;</div>
                </div>

                <div class="css-tr">
                    <div class="css-td">Weekday scheme</div>
                    <asp:CheckBox ID="XuEnabled1" runat="server" Text="1" Checked="True"></asp:CheckBox>
                    <asp:CheckBox ID="XuEnabled2" runat="server" Text="2" Checked="True"></asp:CheckBox>
                    <asp:CheckBox ID="XuEnabled3" runat="server" Text="3" Checked="True"></asp:CheckBox>
                    <asp:CheckBox ID="XuEnabled4" runat="server" Text="4" Checked="True"></asp:CheckBox>
                    <asp:CheckBox ID="XuEnabled5" runat="server" Text="5" Checked="True"></asp:CheckBox>
                    <asp:CheckBox ID="XuEnabled6" runat="server" Text="6"></asp:CheckBox>
                    <asp:CheckBox ID="XuEnabled7" runat="server" Text="7"></asp:CheckBox>
                    <div class="css-td">1: monday,...</div>
                </div>

                <div class="css-tr">
                    <div class="css-td">Pickup Operator</div>
                    <div class="css-td">
                        <asp:DropDownList runat="server" ID="XuPickupOperator" >
                            <asp:ListItem>DHL</asp:ListItem>
                            <asp:ListItem>GLS</asp:ListItem>
                            <asp:ListItem>FedEx</asp:ListItem>
                            <asp:ListItem>HH</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="css-td">&nbsp;</div>
                </div>


                <div class="css-tr">
                    <div class="css-td">Operation</div>
                    <div class="css-td">
                        <asp:Button ID="XuNew" runat="server" Text="New" ToolTip="Press new to enable selection of Customer and Account" OnClick="XuNew_Click"  CssClass="" Enabled="True"/>
                        <asp:Button ID="XuUpdate" runat="server" Text="Update" OnClick="XuUpdate_Click" CssClass="" Enabled="False"/>
                        <asp:Button ID="XuCreate" runat="server" Text="Create" OnClick="XuCreate_Click" CssClass="" Enabled="False"/>
                        <asp:Button ID="XuDelete" runat="server" Text="Delete" OnClick="XuDelete_Click" CssClass="" Enabled="False"/>
                        <asp:Button ID="XuCancel" runat="server" Text="Cancel" OnClick="XuCancel_Click" CssClass="" Enabled="True"/>
                    </div>
                    <div class="css-td">&nbsp;</div>
                </div>

            </div>

        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
