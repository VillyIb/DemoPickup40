<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Forwarder.aspx.cs" Inherits="DemoPickup40.Pages.Pickup.Forwarder" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../assets/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/Site.css" rel="stylesheet" />
    <link href="../../assets/css/stylesheet-2016-02-23.css" rel="stylesheet" />
    <link href="Forwarder.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="XuUpdatePanel1" runat="server">

        <ContentTemplate>


            <%--Forwarder Pickup Grid View--%>
            <asp:GridView
                ID="XuForwarderPickup"
                runat="server"
                AutoGenerateColumns="false"
                ShowHeaderWhenEmpty="true"
                CssClass=""
                OnRowCommand="XuGridForwarderPicup_RowCommand">

                <HeaderStyle CssClass="XuForwarderPickupHeader" />
                <RowStyle CssClass="XuForwarderPickupRow" />

                <Columns>

                    <%--Column Expand --%>
                    <asp:TemplateField HeaderText="XuFwExpand">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwExpandHeader" runat="server" Text="&nbsp;" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <%--Switch between glyphicon-triangle-bottom/top--%>
                            <%--<i class="glyphicon glyphicon-triangle-top big"></i>--%>
                            &nbsp;&nbsp;&nbsp;
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwExpand" />
                        <ItemStyle CssClass="XuFwExpand" />

                    </asp:TemplateField>


                    <%--Column Address --%>
                    <asp:TemplateField HeaderText="XuFwAddress">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwAddressHeader" runat="server" Text="XuFwAddressHeader" meta:resourcekey="XuFwAddressHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuFwAddressItem" runat="server" ToolTip='<%# Bind("Address.FullAddress") %>' Text='<%# Bind("Address.Street1") %>' />
                        </ItemTemplate>

                    </asp:TemplateField>

                    <%--Column Pickup Window --%>
                    <asp:TemplateField HeaderText="XuFwPickup">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwPickupHeader" runat="server" Text="XuFwPickupHeader" meta:resourcekey="XuFwPickupHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <%--<asp:Label ID="XuFwPickupItem" runat="server"  Text='<%# Bind("PickupDate") %>'></asp:Label>--%>
                            <asp:Label ID="Label1" runat="server" Text='<%# GetPickupText( (string)GetLocalResourceObject("XuPickupItem.Format"), Eval("PickupDate"), Eval("ReadyOpen"), Eval("ReadyClose")) %>'></asp:Label>
                            <asp:LinkButton
                                ID="XuFwdPickupX1"
                                runat="server"
                                CausesValidation="false"
                                CommandName="XcCmd03"
                                CommandArgument='<%# Eval("Id") %>'>
                                <span
                                    class="glyphicon glyphicon-refresh"
                                    title="Recalculate smallest window for Open/Close"
                                    />
                            </asp:LinkButton>
                        </ItemTemplate>

                    </asp:TemplateField>


                    <%--Column Carrier --%>
                    <asp:TemplateField HeaderText="XuFwCarrier">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwCarrierHeader" runat="server" Text="XuFwCarrierHeader" meta:resourcekey="XuFwCarrierHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuFwCarrierItem" runat="server" Text='<%# Bind("CarrierNameList") %>' />
                        </ItemTemplate>

                    </asp:TemplateField>


                    <%--Column Note --%>
                    <asp:TemplateField HeaderText="XuFwNote">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwNoteHeader" runat="server" Text="XuFwNoteHeader" meta:resourcekey="XuFwNoteHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuFwNoteItem" runat="server" Text='<%# Bind("Note") %>' />
                        </ItemTemplate>

                    </asp:TemplateField>



                    <%--Column Status --%>
                    <asp:TemplateField HeaderText="XuFwStatus">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwStatusHeader" runat="server" Text="XuFwStatusHeader" meta:resourcekey="XuFwStatusHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <%--
                                Status field contain 
                                * localized text (default to 
                                * Action
                            --%>
                            <asp:Label
                                ID="XuFwStatusItem"
                                runat="server"
                                Text='<%# GetLocalResourceObject("XuStatus-Text_StatusCode-" + Eval("PickupStatusForwarder") + ".Text") %>' />

                            <asp:LinkButton
                                ID="LinkButton12"
                                runat="server"
                                CausesValidation="false"
                                CommandName="XcCmd04"
                                CommandArgument='<%# Eval("Id") %>'>
                                <span 
                                    class ='<%# Eval("CssGlyphiconStatus") %>'
                                    title='<%# GetLocalResourceObject("XuStatus-Icon_StatusCode-" + Eval("PickupStatusForwarder") + ".ToolTip") %>' 
                                    />
                            </asp:LinkButton>

                        </ItemTemplate>

                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="XuFwEdit">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwEditHeader" runat="server" Text="XuFwEditHeader" meta:resourceKey="XuFwEditHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:LinkButton
                                ID="XuFwEditItemEvent"
                                runat="server"
                                target="_blank"
                                CausesValidation="false"
                                CommandName="XcCmd05"
                                CommandArgument='<%# Eval("Id") %>'>
                                <span 
                                    class="glyphicon glyphicon-edit" 
                                    title='<%# GetLocalResourceObject("XuFwEditEventItem.Text") %>' 
                                    />
                            </asp:LinkButton>
                        </ItemTemplate>

                    </asp:TemplateField>


                    <%--Move Customer Pickup--%>
                    <asp:TemplateField>

                        <HeaderTemplate>
                            <asp:Label ID="XuFwMoveHeader" runat="server" Text='<%# GetLocalResourceObject("XuFwMoveHeader.Text") %>' />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:LinkButton ID="XuFwMoveItem" CommandArgument='<%# Eval("Id") %>' runat="server" CausesValidation="false" CommandName="XcCmd06" ToolTip='<%# GetLocalResourceObject("XuFwMoveItem.ToolTip") %>   '>
                                <span 
                                    class='<%# Eval("CssGlyphiconMove") %>'
                                    title='<%# GetLocalResourceObject("XuMove-Icon_StatusCode-" + Eval("PickupStatusForwarder") + ".ToolTip") %>' 
                                />
                            </asp:LinkButton>
                        </ItemTemplate>

                    </asp:TemplateField>


                    <%--Column Customer  Details--%>
                    <asp:TemplateField HeaderText="XuCustomerDetails">
                        <HeaderTemplate>
                            <asp:Label runat="server" Text="&nbsp;" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--NOTE! this Column is mapped to a new row.--%>
                            <tr>
                                <td colspan="7">

                                    <%--Customer Pickup  GridView--%>
                                    <asp:GridView
                                        ID="XuCustomerPickup"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        ShowHeaderWhenEmpty="True"
                                        CssClass=""
                                        DataSource='<%# Bind("CustomerPickupList") %>'
                                        OnRowCommand="XuGridCustomerPickup_RowCommand">

                                        <HeaderStyle CssClass="XuCustomerPickupHeader" />
                                        <RowStyle CssClass="XuCustomerPickupRow" />

                                        <Columns>

                                            <%--Column Expand --%>
                                            <asp:TemplateField HeaderText="XuExpand">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuExpandHeader" runat="server" Text="&nbsp;" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <%--Switch between glyphicon-triangle-bottom/top--%>
                                                    <i class="glyphicon glyphicon-triangle-bottom big"></i>
                                                </ItemTemplate>

                                            </asp:TemplateField>


                                            <%--Column Address --%>
                                            <asp:TemplateField HeaderText="XuAddress">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuAddressHeader" runat="server" Text="XuAddressHeader" meta:resourcekey="XuAddressHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="XuAddressItem" runat="server" ToolTip='<%# Bind("Address.FullAddress") %>' Text='<%# Bind("Address.Street1") %>' />
                                                </ItemTemplate>

                                            </asp:TemplateField>


                                            <%--Column Pickup Window --%>
                                            <asp:TemplateField HeaderText="XuPickup">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuPickupHeader" runat="server" Text="XuPickupHeader" meta:resourcekey="XuPickupHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <%--<asp:Label ID="XuPickupItem" runat="server"  Text='<%# Bind("PickupDate") %>'></asp:Label>--%>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# GetPickupText( (string)GetLocalResourceObject("XuPickupItem.Format"), Eval("PickupDate"), Eval("ReadyOpen"), Eval("ReadyClose")) %>'></asp:Label>

                                                </ItemTemplate>

                                            </asp:TemplateField>


                                            <%--Column Carrier --%>
                                            <asp:TemplateField HeaderText="XuCarrier">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCarrierHeader" runat="server" Text="XuCarrierHeader" meta:resourcekey="XuCarrierHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="XuCarrierItem" runat="server" Text='<%# Bind("CarrierNameList") %>' />
                                                </ItemTemplate>

                                            </asp:TemplateField>


                                            <%--Column Note --%>
                                            <asp:TemplateField HeaderText="XuNote">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuNoteHeader" runat="server" Text="XuNoteHeader" meta:resourcekey="XuNoteHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="XuNoteItem" runat="server" Text='<%# Bind("Note") %>' />
                                                </ItemTemplate>

                                            </asp:TemplateField>


                                            <%--Column Status --%>
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
                                                        Text='<%# GetLocalResourceObject("XuStatus-Text_StatusCode-" + Eval("PickupStatusForwarder") + ".Text") %>' />

                                                    <%--Text='<%# GetLocalResourceObject("XuStatus-Text_StatusCode-" + Eval("PickupStatusText") + ".Text") %>'--%>
                                                    <%--ToolTip='<%# GetLocalResourceObject("XuStatus-Text_StatusCode-" + Eval("PickupStatusText") + ".ToolTip") %>'--%>

                                                    <asp:LinkButton
                                                        ID="LinkButton12"
                                                        runat="server"
                                                        CausesValidation="false"
                                                        CommandName="XcCmd01"
                                                        CommandArgument='<%# Eval("PickupStatusForwarder") + "." + Eval("Id") %>'>
                                <span 
                                    class='<%# "glyphicon " + GetLocalResourceObject("XuStatus-Icon_StatusCode-" + Eval("PickupStatusForwarder") + ".Glyphicon") %>' 
                                    title='<%# GetLocalResourceObject("XuStatus-Icon_StatusCode-" + Eval("PickupStatusForwarder") + ".ToolTip") %>' 
                                    />
                                                    </asp:LinkButton>

                                                </ItemTemplate>

                                            </asp:TemplateField>


                                            <%--Column Move --%>
                                            <asp:TemplateField HeaderText="XuMove">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuMoveHeader" runat="server" Text='<%# GetLocalResourceObject("XuMoveHeader.Text") %>' />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:LinkButton ID="XuMoveItem" CommandArgument='<%# Eval("Id") %>' runat="server" CausesValidation="false" CommandName="XcCmd02" ToolTip='<%# GetLocalResourceObject("XuMoveItem.ToolTip") %>   '>
                                                    <span 
                                                        class='<%# Eval("CssGlyphiconMove") %>' 
                                                        title='<%# GetLocalResourceObject("XuMove-Icon_StatusCode-" + Eval("PickupStatusForwarder") + ".ToolTip") %>' 
                                                        />
                                                    </asp:LinkButton>

                                                </ItemTemplate>
                                                <HeaderStyle CssClass="XuMove" />
                                                <ItemStyle CssClass="XuMove" />

                                            </asp:TemplateField>


                                            <%--Column Customer Pickup Select--%>
                                            <asp:TemplateField HeaderText="XuFwSelect">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuFwSelectHeader" runat="server" Text="&nbsp;" meta:resourcekey="XuFwSelectHeader" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <input id="XuFwSelectItem" type="checkbox" runat="server" disabled='<%# Eval("CssVisibleCheckbox") %>' value='<%# Eval("Id") %>' />
                                                </ItemTemplate>

                                            </asp:TemplateField>


                                            <%--Column Shipment Details--%>
                                            <asp:TemplateField HeaderText="XuShipmentDetails">
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="&nbsp;" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%--NOTE! this Column is mapped to a new row.--%>
                                                    <tr>
                                                        <td colspan="7">

                                                            <asp:GridView
                                                                ID="XuShipmentDetails"
                                                                runat="server"
                                                                AutoGenerateColumns="False"
                                                                DataSource='<%# Bind("Shipmentlist") %>'
                                                                CssClass="XuShipmentDetails hidden">

                                                                <HeaderStyle CssClass="XuShipmentDetailsHead" />
                                                                <RowStyle CssClass="XuShipmentDetailsRow" />

                                                                <Columns>
                                                                    <%--Column Waybill number --%>
                                                                    <asp:TemplateField HeaderText="XuWaybillnumber">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="XuWaybillnumberHead" runat="server" Text="Waybillnumber" meta:resourcekey="XuWaybillnumberHead" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="XuWaybillnumberItem" runat="server" Text='<%# Bind("Waybillnumber") %>' ToolTip='<%# Bind("Address.FullAddress") %>' />
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuWaybillnumber" />
                                                                        <ItemStyle CssClass="XuWaybillnumber" />

                                                                    </asp:TemplateField>


                                                                    <%--Column Carrier --%>
                                                                    <asp:TemplateField HeaderText="XuShCarrier">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="XuShCarrierHeader" runat="server" Text="XuShCarrierHeader" meta:resourcekey="XuShCarrierHeader" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="XuShCarrierItem" runat="server" Text='<%# Bind("CarrierName") %>' />
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuShCarrier" />
                                                                        <ItemStyle CssClass="XuShCarrier" />

                                                                    </asp:TemplateField>


                                                                    <%--Column Weight --%>
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


                                                                    <%--Column Dimension --%>
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


                                                                    <%--Column Select --%>
                                                                    <asp:TemplateField HeaderText="XuSelect">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="XuSelectHeader" runat="server" Text="&nbsp;" meta:resourcekey="XuSelectHeader" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <input id="XuSelectItem" type="checkbox" runat="server" disabled='<%# Eval("CssVisibleCheckbox") %>' value='<%# Eval("Id") %>' />
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

                                    <%--Customer Pickup  -- END --  GridView--%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>


        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
