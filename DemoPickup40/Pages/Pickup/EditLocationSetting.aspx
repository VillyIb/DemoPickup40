<%@ Page
    Title=""
    Language="C#"
    MasterPageFile="~/Master.Master"
    AutoEventWireup="true"
    CodeBehind="EditLocationSetting.aspx.cs"
    Inherits="DemoPickup40.Pages.Pickup.EditLocationSetting" %>

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

        <ContentTemplate>

            <div id="XuTable1" class="css-table">

                <div class="css-tr">

                    <%-- Main Left Column --%>
                    <div class="css-td ">

                        <h2>Customers</h2>

                        <div id="XuTable11" class="css-table">

                            <%--Customer List--%>
                            <div class="css-tr">
                                <div class="css-td col1">
                                    Customers 
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
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                        </Columns>

                                        <SelectedRowStyle BackColor="Lime" BorderStyle="Solid" />

                                    </asp:GridView>
                                </div>
                            </div>

                        </div>

                    </div>

                    <%-- Main Right Column --%>
                    <div class="css-td">

                        <%--Location Information--%>
                        <span>
                            <h2>Location Settings</h2>
                        </span>

                        <div id="XuLocationDetails" runat="server" class="css-table">

                            <div class="css-tr">
                                <div class="css-td col1">
                                    Forwarder
                                </div>
                                <div class="css-td col2">
                                    <asp:DropDownList runat="server" ID="XuForwarderList" DataTextField="Text" DataValueField="Value" />
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

                            <%--TODO Bør muligvis flyttes til Permanent Collection--%>
                            <div class="css-tr">
                                <div class="css-td col1">
                                    Default Pickup Operator
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

                            <div class="css-tr">
                                <div class="css-td col1">
                                </div>
                                <div class="css-td col2">
                                    <asp:Button ID="XuSaveLocation" runat="server" Text="Update" OnClick="XuSaveLocation_Click" />
                                </div>
                            </div>

                        </div>

                        <%--Permanent Collection Information--%>
                        <span id="XuPermanentCollectionOuterTop" runat="server" class="">
                            <h2>Permanent Collection</h2>
                        </span>

                        <div id="XuPermanentCollection" runat="server" class="css-table">
                            <br />

                            <div class="css-tr">
                                <div class="css-td col1">
                                    Permanent Pickup Enabled
                                </div>
                                <div class="css-td col2">
                                    <asp:CheckBox ID="XuPermCollEnabled" runat="server" />
                                </div>
                            </div>

                            <div class="css-tr">
                                <div class="css-td col1">
                                    Pickup Schedule
                                </div>
                                <div class="css-td col2">
                                    <asp:CheckBox ID="XuScheduleMonday" runat="server" Checked="true" Text="monday" /><br />
                                    <asp:CheckBox ID="XuScheduleTuesday" runat="server" Checked="true" Text="tuesday" /><br />
                                    <asp:CheckBox ID="XuScheduleWedensday" runat="server" Checked="true" Text="wedensday" /><br />
                                    <asp:CheckBox ID="XuScheduleThursday" runat="server" Checked="true" Text="thursday" /><br />
                                    <asp:CheckBox ID="XuScheduleFruiday" runat="server" Checked="true" Text="friday" /><br />
                                    <asp:CheckBox ID="XuScheduleSaturday" runat="server" Checked="false" Text="saturday" /><br />
                                    <asp:CheckBox ID="XuScheduleSunday" runat="server" Checked="false" Text="sunday" /><br />
                                </div>
                            </div>

                            <div class="css-tr">
                                <div class="css-td col1">
                                    <asp:Button ID="XuPermCollDelete" runat="server" Text="Delete" OnClick="XuPermCollDelete_Click" />
                                </div>
                                <div class="css-td col2">
                                    <asp:Button ID="XuPermCollUpdate" runat="server" Text="Update" OnClick="XuPermCollUpdate_Click" />
                                </div>
                            </div>

                        </div>

                        <div id="XuPermanentCollectionOuterBottom" runat="server" class="">
                            <asp:Button ID="XuPermCollCreate" runat="server" Text="Create" OnClick="XuPermCollCreate_Click" />
                        </div>

                    </div>

                </div>

            </div>

        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
