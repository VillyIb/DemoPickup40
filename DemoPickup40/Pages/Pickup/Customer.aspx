<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="DemoPickup40.Pages.Pickup.Customer" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../assets/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/Site.css" rel="stylesheet" />
    <link href="../../assets/css/stylesheet-2016-02-23.css" rel="stylesheet" />
    <link href="Customer.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="XuUpdatePanel1" runat="server">

        <ContentTemplate>


            <%--First level GridView--%>
            <asp:GridView
                ID="XuCustomerPickup"
                runat="server"
                AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True"
                CssClass=""
                OnRowCommand="XuGridCustomerPickup_RowCommand">

                <HeaderStyle CssClass="XuCustomerPickupHeader" />
                <RowStyle CssClass="XuCustomerPickupRow" />

                <Columns>

                    <%--Column #0--%>
                    <asp:TemplateField HeaderText="XuExpand">

                        <HeaderTemplate>
                            <asp:Label ID="XuExpandHeader" runat="server" Text="&nbsp;" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <%--Switch between glyphicon-triangle-bottom/top--%>
                            <i class="glyphicon glyphicon-triangle-top big"></i>
                        </ItemTemplate>

                    </asp:TemplateField>


                    <%--Column #1--%>
                    <asp:TemplateField HeaderText="XuAddress">

                        <HeaderTemplate>
                            <asp:Label ID="XuAddressHeader" runat="server" Text="XuAddressHeader" meta:resourcekey="XuAddressHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuAddressItem" runat="server" ToolTip='<%# Bind("FullAddress") %>' Text='<%# Bind("Address") %>' />
                        </ItemTemplate>

                    </asp:TemplateField>


                    <%--Column #2 Pickup Window --%>
                    <asp:TemplateField HeaderText="XuPickup">

                        <HeaderTemplate>
                            <asp:Label ID="XuPickupHeader" runat="server" Text="XuPickupHeader" meta:resourcekey="XuPickupHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuPickupItem" runat="server" Text='<%# Bind("Pickup") %>' />
                        </ItemTemplate>

                    </asp:TemplateField>


                    <%--Column #3 Note --%>
                    <asp:TemplateField HeaderText="XuNote">

                        <HeaderTemplate>
                            <asp:Label ID="XuNoteHeader" runat="server" Text="XuNoteHeader" meta:resourcekey="XuNoteHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuNoteItem" runat="server" Text='<%# Bind("Note") %>' />
                        </ItemTemplate>

                    </asp:TemplateField>


                    <%--Column #4 'Status'--%>
                    <asp:TemplateField HeaderText="XuStatus">

                        <HeaderTemplate>
                            <asp:Label ID="XuStatusHeader" runat="server" Text="XuStatusHeader" meta:resourcekey="XuStatusHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <%--
                                Status field contain 
                                * localized text (default to 
                                * Action
                            --%>
                            <asp:Label
                                ID="XuStatusItem"
                                runat="server"
                                Text='<%# GetLocalResourceObject("XuStatus-Text_StatusCode-" + Eval("StatusCodeText") + ".Text") %>' />

                            <%--Text='<%# GetLocalResourceObject("XuStatus-Text_StatusCode-" + Eval("StatusCodeText") + ".Text") %>'--%>
                            <%--ToolTip='<%# GetLocalResourceObject("XuStatus-Text_StatusCode-" + Eval("StatusCodeText") + ".ToolTip") %>'--%>

                            <asp:LinkButton
                                ID="LinkButton12"
                                runat="server"
                                CausesValidation="false"
                                CommandName="XcCmd01"
                                CommandArgument='<%# Eval("StatusCodeText") + "." + Eval("PickupId") %>'>
                                <span 
                                    class='<%# "glyphicon " + GetLocalResourceObject("XuStatus-Icon_StatusCode-" + Eval("StatusCodeText") + ".Glyphicon") %>' 
                                    title='<%# GetLocalResourceObject("XuStatus-Icon_StatusCode-" + Eval("StatusCodeText") + ".ToolTip") %>' 
                                    />
                            </asp:LinkButton>

                        </ItemTemplate>

                    </asp:TemplateField>


                    <%--Column #5 Move  --%>
                    <asp:TemplateField HeaderText="XuMove">

                        <HeaderTemplate>
                            <asp:Label ID="XuMoveHeader" runat="server" Text='<%# GetLocalResourceObject("XuMoveHeader.Text") %>' />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:LinkButton ID="XuMoveItem" CommandArgument='<%# Eval("PickupId") %>' runat="server" CausesValidation="false" CommandName="XcCmd02" ToolTip='<%# GetLocalResourceObject("XuMoveItem.ToolTip") %>   '>
                                <span 
                                    class='<%# "glyphicon " + GetLocalResourceObject("XuMove-Icon_StatusCode-" + Eval("StatusCodeText") + ".Glyphicon") %>' 
                                    title='<%# GetLocalResourceObject("XuMove-Icon_StatusCode-" + Eval("StatusCodeText") + ".ToolTip") %>' 
                                    />
                            </asp:LinkButton>
                            
                           
                        </ItemTemplate>
                        <HeaderStyle CssClass="XuMove" />
                        <ItemStyle CssClass="XuMove" />

                    </asp:TemplateField>



                    <%--Column #N--%>
                    <asp:TemplateField HeaderText="XuShipmentDetails">
                        <HeaderTemplate>
                            <asp:Label runat="server" Text="&nbsp;" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--NOTE! this Column is mapped to a new row.--%>
                            <tr >
                                <td colspan="7">
                                    
                                    <asp:GridView 
                                        ID="XuShipmentDetails" 
                                        runat="server" 
                                        AutoGenerateColumns="False" 
                                        DataSource='<%# Bind("Shipmentlist") %>'
                                        CssClass="XuShipmentDetails"
                                        >
                                        
                                        <HeaderStyle CssClass="XuShipmentDetailsHead" />
                                        <RowStyle CssClass="XuShipmentDetailsRow" />

                                        <Columns>
                                            <%--Column #1--%>
                                            <asp:TemplateField HeaderText="XuWaybillnumber">
                                                <HeaderTemplate>
                                                    <asp:Label ID="XuWaybillnumberHead" runat="server" Text="Waybillnumber"  meta:resourcekey="XuWaybillnumberHead"/>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="XuWaybillnumberItem" runat="server" Text='<%# Bind("Waybillnumber") %>' ToolTip='<%# Bind("FullAddress") %>'/>
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuWaybillnumber" />
                                                <ItemStyle CssClass="XuWaybillnumber" />

                                            </asp:TemplateField>

                                            <%--Column #2--%>
                                            <asp:TemplateField HeaderText="XuWeight">
                                                <HeaderTemplate>
                                                    <asp:Label ID="XuWeightHeader" runat="server" Text="XuWeightHeader" meta:resourcekey="XuWeightHeader" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="XuWeightItem" runat="server" Text='<%# Bind("Weight") %>' />
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuWeight" />
                                                <ItemStyle CssClass="XuWeight" />

                                            </asp:TemplateField>

                                            <%--Column #2--%>
                                            <asp:TemplateField HeaderText="XuDimension">
                                                <HeaderTemplate>
                                                    <asp:Label ID="XuDimensionHead" runat="server" Text="XuDimensionHead" meta:resourcekey="XuDimensionHead" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="XuDimensionItem" runat="server" Text='<%# Bind("Dimensions") %>' />
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuDimension" />
                                                <ItemStyle CssClass="XuDimension" />

                                            </asp:TemplateField>

                                            <%--Column #0--%>
                                            <asp:TemplateField HeaderText="XuSelect" >
                                                <HeaderTemplate>
                                                    <asp:Label ID="XuSelectHeader" runat="server" Text="&nbsp;" meta:resourcekey="XuSelectHeader" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <input id="XuSelectItem" type="checkbox" runat="server"  disabled='<%# "ForwSched".Equals( Eval("StatusCodeText")) %>' value='<%# Eval("ShipmentId") %>' />
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuSelect" />
                                                <ItemStyle CssClass="XuSelect" />

                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </ItemTemplate>
                        
                        <%--Do not show this as the last Column--%>
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden" />

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>




        </ContentTemplate>

    </asp:UpdatePanel>



</asp:Content>
