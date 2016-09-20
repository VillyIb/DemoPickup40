<%@ Page
    Title=""
    Language="C#"
    MasterPageFile="~/Master.Master"
    AutoEventWireup="true"
    CodeBehind="EditLocationSetting.aspx.cs"
    Inherits="DemoPickup40.Pages.Pickup2.EditLocationSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../assets/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/Site.css" rel="stylesheet" />
    <link href="../../assets/css/stylesheet-2016-02-23.css" rel="stylesheet" />
    <link href="EditLocationSetting.css" rel="stylesheet" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel
        ID="XuUpdatePanel1"
        runat="server">

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="XuLocationList" EventName="SelectedIndexChanged" />
        </Triggers>

        <ContentTemplate>

            <div id="XuTable1" class="css-table">
                <div class="css-tr">

                    <div class="css-tr">

                        <div class="css-td ">
                            <div id="XuTable11" class="css-table">

                                <div class="css-tr">
                                    <div class="css-td col1">
                                        Customer 
                                    </div>
                                    <div class="css-td col2">
                                        <asp:DropDownList runat="server" ID="XuCustomerList" DataTextField="Text" AutoPostBack="True" DataValueField="Value" CssClass="XuCustomerList" OnSelectedIndexChanged="XuCustomerList_SelectedIndexChanged" />
                                    </div>
                                </div>


                                <%-- Location List --%>
                                <div class="css-tr">
                                    <div class="css-td col1A">
                                        Locations
                                    </div>
                                    <div class="css-td col2A">
                                        <asp:GridView
                                            ID="XuLocationList"
                                            runat="server"
                                            AutoGenerateColumns="false"
                                            ShowHeaderWhenEmpty="true"
                                            CssClass="XuForwarderPickup"
                                            OnSelectedIndexChanged="XuLocationList_SelectedIndexChanged"
                                            EnableViewState="False" OnRowCommand="XuLocationList_RowCommand"
                                            AutoGenerateSelectButton="true">

                                            <Columns>

                                                <asp:TemplateField HeaderText="X2">

                                                    <HeaderTemplate>Address</HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="XuLocText" runat="server" Text='<%# Bind("Displaytext") %>'></asp:Label>
                                                        <%--<asp:LinkButton ID="XuLocText" runat="server" Text='<%# Bind("Displaytext") %>' />--%>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <%--<asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Edit" ShowEditButton="true" ShowInsertButton="true" CancelText="ct" EditText="et" HeaderText="ht" ShowHeader="true" />--%>
                                            </Columns>

                                            <SelectedRowStyle BackColor="Lime" BorderStyle="Solid" />

                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="css-td">

                            <div id="XuLocationDetails" runat="server" class="css-table hidden">

                                <div class="css-tr">
                                    <div class="css-td col1">
                                        Forwarder
                                    </div>
                                    <div class="css-td col2">
                                        <asp:DropDownList runat="server" ID="XuForwarderList" DataTextField="Text" DataValueField="Value"/>
                                    </div>
                                </div>

                                <div class="css-tr">
                                    <div class="css-td col1">
                                        Loading Gear
                                    </div>
                                    <div class="css-td col2">
                                        <asp:CheckBox runat="server" ID="XuHasLoadingGear" />
                                    </div>
                                </div>

                                <div class="css-tr">
                                    <div class="css-td col1">
                                        Loading Gear Description (to Pickup Operator)
                                    </div>
                                    <div class="css-td col2">
                                        <asp:TextBox runat="server" ID="XuLoadingGearDetails" />
                                    </div>
                                </div>

                                <div class="css-tr">
                                    <div class="css-td col1">
                                        Is feedback visible to Customer
                                    </div>
                                    <div class="css-td col2">
                                        <asp:CheckBox runat="server" ID="XuCustomerFeedback" />
                                    </div>
                                </div>

                                <div class="css-tr">
                                    <div class="css-td col1">
                                        Pickup Operator
                                    </div>
                                    <div class="css-td col2">
                                        <asp:DropDownList runat="server" ID="XuPickupOperatorList" />
                                    </div>
                                </div>

                                <div class="css-tr">
                                    <div class="css-td col1">
                                        Internal Note
                                    </div>
                                    <div class="css-td col2">
                                        <asp:TextBox runat="server" ID="XuNote" />
                                    </div>
                                </div>

                            </div>
                            <asp:Button ID="XuSaveLocation" runat="server" Text="Update" OnClick="XuSaveLocation_Click" />

                        </div>

                    </div>

                </div>
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
