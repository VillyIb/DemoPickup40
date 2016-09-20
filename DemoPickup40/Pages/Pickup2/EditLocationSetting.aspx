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

    <asp:UpdatePanel ID="XuUpdatePanel1" runat="server" UpdateMode="Always">

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
                                        <asp:DropDownList runat="server" ID="XuCustomerList" />
                                    </div>
                                </div>

                                <div class="css-tr">
                                    <div class="css-td col1">
                                        xxxx
                                    </div>
                                    <div class="css-td col2">
                                        <asp:TextBox runat="server" ID="DropDownList1" />
                                    </div>
                                </div>

                                <div class="css-tr">
                                    <div class="css-td col1">
                                        Forwarder
                                    </div>
                                    <div class="css-td col2">
                                        <asp:DropDownList runat="server" ID="XuForwarderList" />
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
                                        - Week Schedule
                                    </div>
                                    <div class="css-td col2">
                                        <asp:CheckBox runat="server" ID="CheckBox1" />
                                        <asp:CheckBox runat="server" ID="CheckBox2" />
                                        <asp:CheckBox runat="server" ID="CheckBox3" />
                                        <asp:CheckBox runat="server" ID="CheckBox4" />
                                        <asp:CheckBox runat="server" ID="CheckBox5" />
                                        <asp:CheckBox runat="server" ID="CheckBox6" />
                                        <asp:CheckBox runat="server" ID="CheckBox7" />
                                    </div>
                                </div>

                                <div class="css-tr">
                                    <div class="css-td col1">
                                        Loading Gear Description (to Pickup Operator)
                                    </div>
                                    <div class="css-td col2">
                                        <asp:TextBox runat="server" ID="TextBox2" />
                                    </div>
                                </div>

                                <div class="css-tr">
                                    <div class="css-td col1">
                                        Is feedback visible to Customer
                                    </div>
                                    <div class="css-td col2">
                                        <asp:CheckBox runat="server" ID="CheckBox8" />
                                    </div>
                                </div>

                                <div class="css-tr">
                                    <div class="css-td col1">
                                        Pickup Operator
                                    </div>
                                    <div class="css-td col2">
                                        <asp:DropDownList runat="server" ID="DropDownList2" />
                                    </div>
                                </div>

                                <div class="css-tr">
                                    <div class="css-td col1">
                                        Internal Note
                                    </div>
                                    <div class="css-td col2">
                                        <asp:TextBox runat="server" ID="TextBox1" />
                                    </div>
                                </div>

                                <%-- Location List --%>
                                <div class="css-tr">
                                    <div class="css-td col1A">
                                        X
                                    </div>
                                    <div class="css-td col2A">
                                        <asp:GridView
                                            ID="XuLocationList"
                                            runat="server"
                                            AutoGenerateColumns="false"
                                            ShowHeaderWhenEmpty="true"
                                            CssClass="XuForwarderPickup"
                                            OnSelectedIndexChanged="XuLocationList_SelectedIndexChanged"
                                            EnableViewState="False">

                                            <Columns>

                                                <asp:TemplateField HeaderText="X1">

                                                    <HeaderTemplate>xxx</HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label runat="server">some accounts </asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="X2">

                                                    <HeaderTemplate>yyy</HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="XuLocText" runat="server" Text='<%# Bind("Displaytext") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                            </Columns>

                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="css-td">

                            <div class="css-table">
                                <div class="css-tr">
                                    <div class="css-td">Location</div>
                                    <div class="css-td">
                                        <asp:Label ID="XuLocationAddress1" runat="server"></asp:Label>
                                        <asp:Label ID="XuLocationAddress2" runat="server"></asp:Label>
                                        <asp:Label ID="XuLocationAddress3" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="css-tr">
                                    <div class="css-td">AA</div>
                                    <div class="css-td">BB</div>
                                </div>
                                <div class="css-tr">
                                    <div class="css-td">AA</div>
                                    <div class="css-td">BB</div>
                                </div>
                                <div class="css-tr">
                                    <div class="css-td">AA</div>
                                    <div class="css-td">BB</div>
                                </div>
                            </div>

                        </div>

                    </div>

                    <div class="css-tr">

                        <div class="css-td col">
                            asfakjslfjæa
                        </div>

                        <div class="css-td col">
                            kjldfsjkaælkj
                        </div>

                    </div>
                </div>
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
