<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="EditForwarderPickup.aspx.cs" Inherits="DemoPickup40.Pages.Pickup.EditForwarderPickup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="EditForwarderPickup.css" rel="stylesheet" />

    <script>
        function CloseWindow() {
            window.close();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Forwarder Pickup</h1>
    <div class="div-table">

        <div class="div-row">
            <span class="div-col-header col1">Name</span>
            <span class="div-col-header col2">Value</span>
            <div class="div-col-header" />
        </div>

        <div class="row">
            <asp:Label ID="Label1" class="div-col col1" runat="server" Text="Name"></asp:Label>
            <asp:TextBox ID="XuName" class="div-col col2" runat="server"></asp:TextBox>
            <div class="div-col-header" />
        </div>

        <div class="row">
            <asp:Label ID="Label2" class="div-col col1" runat="server" Text="Street1"></asp:Label>
            <asp:TextBox ID="XuStreet1" class="div-col col2" runat="server"></asp:TextBox>
            <div class="div-col-header" />
        </div>

        <div class="row">
            <asp:Label ID="Label3" class="div-col col1" runat="server" Text="Street2"></asp:Label>
            <asp:TextBox ID="XuStreet2" class="div-col col2" runat="server"></asp:TextBox>
            <div class="div-col-header" />
        </div>

        <div class="row state">
            <asp:Label ID="Label4" class="div-col col1" runat="server" Text="State"></asp:Label>
            <asp:TextBox ID="XuState" class="div-col col2" runat="server"></asp:TextBox>
            <div class="div-col-header" />
        </div>

        <div class="row">
            <asp:Label ID="Label5" class="div-col col1" runat="server" Text="Country"></asp:Label>
            <asp:TextBox ID="XuCountryCode" class="div-col col2" runat="server"></asp:TextBox>
            <div class="div-col-header" />
        </div>

        <div class="row">
            <asp:Label ID="Label6" class="div-col col1" runat="server" Text="Zip"></asp:Label>
            <asp:TextBox ID="XuZip" class="div-col col2" runat="server"></asp:TextBox>
            <div class="div-col-header" />
        </div>

        <div class="row">
            <asp:Label ID="Label7" class="div-col col1" runat="server" Text="Attention"></asp:Label>
            <asp:TextBox ID="XuAttention" class="div-col col2" runat="server"></asp:TextBox>
            <div class="div-col-header" />
        </div>

        <div class="row">
            <asp:Label ID="Label8" class="div-col col1" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="XuEmail" class="div-col col2" runat="server"></asp:TextBox>
            <div class="div-col-header" />
        </div>

        <div class="row">
            <asp:Label ID="Label9" class="div-col col1" runat="server" Text="Phone"></asp:Label>
            <asp:TextBox ID="XuPhone" class="div-col col2" runat="server"></asp:TextBox>
            <div class="div-col-header" />
        </div>

        <div class="row">
            <asp:Label ID="Label10" class="div-col col1" runat="server" Text="Note"></asp:Label>
            <asp:TextBox ID="XuNote" class="div-col col2" runat="server"></asp:TextBox>
            <div class="div-col-header" />
        </div>

        <div class="row">
            <asp:Label ID="Label11" class="div-col col1" runat="server" Text="PickupDate"></asp:Label>
            <asp:TextBox ID="XuPickupDate" class="div-col col2" runat="server"></asp:TextBox>
            <div class="div-col-header" />
        </div>

        <div class="row">
            <asp:Label ID="Label12" class="div-col col1" runat="server" Text="ReadyOpen"></asp:Label>
            <asp:TextBox ID="XuReadyOpen" class="div-col col2" runat="server"></asp:TextBox>
            <asp:Button ID="XuReadyOpenCalculated" class="div-col col3" runat="server" Text="12:00" OnClick="XuReadyOpenCalculated_Click"/>
        </div>

        <div class="row">
            <asp:Label ID="Label13" class="div-col col1" runat="server" Text="ReadyClose"></asp:Label>
            <asp:TextBox ID="XuReadyClose" class="div-col col2" runat="server"></asp:TextBox>
            <asp:Button ID="XuReadyCloseCalculated" class="div-col col3" runat="server" Text="15:00" OnClick="XuReadyCloseCalculated_Click"/>
        </div>

        <div class="row">
            <asp:Label ID="Label14" class="div-col col1" runat="server" Text="Status"></asp:Label>
            <asp:DropDownList ID="XuPickupStatus" runat="server"></asp:DropDownList>
            <div class="div-col-header" />
        </div>


    </div>

    <asp:Button ID="XuSubmit" runat="server" Text="Save" OnClick="XuSubmit_Click" />

    <asp:Label ID="XuError" runat="server" CssClass="error hidden" Text="no error"></asp:Label>

</asp:Content>
