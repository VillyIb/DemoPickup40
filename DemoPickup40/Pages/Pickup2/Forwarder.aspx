<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Forwarder.aspx.cs" Inherits="DemoPickup40.Pages.Pickup2.Forwarder" %>


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

    <asp:UpdatePanel ID="XuUpdatePanel1" runat="server" UpdateMode="Always">

        <ContentTemplate>

            <%----------- Settings start -----------%>

            <div id="XuSettings" class="css-table">

                <div class="css-tr header">
                    <div class="css-td header">
                        <div id="XuSettingsHeader" class="css-table">

                            <div class="css-tr">
                                <div class="css-td ColWidth01">
                                    <asp:LinkButton
                                        ID="LinkButton1"
                                        runat="server"
                                        CausesValidation="false"
                                        OnClick="XuSettingsExpand_Click">
                                        <div id="XuSettingsIcon" runat="server"
                                            class="glyphicon glyphicon-triangle-bottom big"
                                            title='<%# GetLocalResourceObject("XuSettingsIcon.Text") %>' />
                                    </asp:LinkButton>
                                </div>
                                <div class="css-td">
                                    <h2>Settings  
                                        <asp:Label ID="XuTimeOfDay" runat="server"></asp:Label>
                                    </h2>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div id="XuSettingsRow" runat="server" class="css-tr body hidden">
                    <div class="css-td body">
                        <div id="XuSettingsBody" class="css-table">

                            <div class="css-tr">
                                <div class="css-th col1">Subject</div>
                                <div class="css-th col2">Values</div>
                                <div class="css-th col3">Notes</div>
                            </div>

                            <div class="css-tr">
                                <div class="css-td">Forwarder Pickup Status</div>
                                <div class="css-td">
                                    <asp:CheckBoxList runat="server" ID="XuStFilterPickupStatus" CssClass="XuFilterPickupStatus">
                                        <asp:ListItem Value="CustCan">Cancelled by Customer</asp:ListItem>
                                        <asp:ListItem Value="CustHand">NoPickup</asp:ListItem>
                                        <asp:ListItem Value="CustWait" Selected="true">Pending on customer</asp:ListItem>
                                        <asp:ListItem Value="ForwWait" Selected="true">Pending on Forwarder</asp:ListItem>
                                        <asp:ListItem Value="ForwSched">Scheduled by Forwarder</asp:ListItem>
                                        <asp:ListItem Value="PermColl">Permanent Collection</asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                                <div class="css-td">Select none (no filter), one or multiple</div>
                            </div>

                            <div class="css-tr">
                                <div class="css-td">Looking forward</div>
                                <div class="css-td">
                                    <asp:TextBox ID="XuStLookForward" runat="server">180</asp:TextBox><asp:CheckBox ID="XuStLookForwardEnabled" runat="server" Text="Enabled" />
                                </div>
                                <div class="css-td">Specify minutes to look forward from current time</div>
                            </div>

                            <div class="css-tr">
                                <div class="css-td">Shipment date window</div>
                                <div class="css-td">
                                    <asp:TextBox ID="XuStPickupDateFrom" runat="server">2016-08-30</asp:TextBox><asp:TextBox ID="XuStPickupDateUntil" runat="server">2016-09-01</asp:TextBox>
                                </div>
                                <div class="css-td">&nbsp;</div>
                            </div>

                            <div class="css-tr">
                                <div class="css-td">Number of shipments</div>
                                <div class="css-td">
                                    <asp:CheckBox ID="XuStNumberOfShipments0" runat="server" Text="0 (zero)" Checked="false" />
                                    <asp:CheckBox ID="XuStNumberOfShipments1" runat="server" Text="1+ (one or more)" Checked="true" />
                                </div>
                                <div class="css-td">Only valid for for Permanent Pickup</div>
                            </div>

                            <div class="css-tr">
                                <div class="css-td">Specific customer</div>
                                <div class="css-td">
                                    <asp:DropDownList ID="XuStSpecificCustomerB" runat="server" DataTextField="Name" DataValueField="Id" CssClass="XuStSpecificCustomerB"></asp:DropDownList>
                                </div>
                                <div class="css-td">blank: all, customerId for single customer</div>
                            </div>

                            <div class="css-tr">
                                <div class="css-td">Specific Website</div>
                                <div class="css-td">
                                    <asp:TextBox ID="XuStWebsite" runat="server"></asp:TextBox>
                                </div>
                                <div class="css-td">blank: all, Guid for specific website</div>
                            </div>

                            <div class="css-tr">
                                <div class="css-td">Sorting</div>
                                <div class="css-td">
                                    <asp:RadioButtonList ID="XuSorting" runat="server">
                                        <asp:ListItem Value="1" Selected="True">Sort into groups, timeClose, Address</asp:ListItem>
                                        <asp:ListItem Value="2">Sort on Address</asp:ListItem>
                                        <asp:ListItem Value="3">#3</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="css-td">blank: all, Guid for specific website</div>
                            </div>

                        </div>

                    </div>
                </div>

            </div>

            <div id="XuExpandAll" class="css-table">

                <div class="css-tr">
                    <div class="css-td ColWidth01">
                        <asp:LinkButton
                            ID="XuContainerCol1"
                            runat="server"
                            CausesValidation="false"
                            OnClick="XuExpandAll_Click">
                            <div id="XuExpandAllIcon" runat="server"
                                class="glyphicon glyphicon-triangle-bottom big"
                                title="Expand/Collapse all Customer Pickups" />
                            <%-- Cant get title to execute inline code --%>
                            <%--title='<%# GetLocalResourceObject("XuExpandAllIcon.Text") %>' --%>
                        </asp:LinkButton>
                    </div>

                    <div class="css-td ColWidth01">
                        <asp:LinkButton
                            ID="XuExpandAllRefresh"
                            runat="server"
                            CausesValidation="false"
                            OnClick="XuExpandAllRefresh_Click"
                            Text="Refresh"
                            ToolTip="Reloads everything from the database">
                        </asp:LinkButton>
                    </div>

                </div>
            </div>

            <%----------- Settings end ------------%>

            <%--Forwarder Pickup Grid View--%>
            <asp:GridView
                ID="XuForwarderPickup"
                runat="server"
                AutoGenerateColumns="false"
                ShowHeaderWhenEmpty="true"
                CssClass="XuForwarderPickup"
                OnRowCommand="XuGridForwarderPicup_RowCommand">

                <HeaderStyle CssClass="XuForwarderPickupHeader" />
                <RowStyle CssClass="XuForwarderPickupRow" />

                <Columns>

                    <%------------ Forwarder ------------%>

                    <%--Column Expand Customer Pickups below Forwarder Pickup --%>
                    <asp:TemplateField HeaderText="XuFwExpandCustomer">
                        <HeaderTemplate>
                            &nbsp;
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:LinkButton
                                ID="XuFwExpandCustomerItem"
                                runat="server"
                                CausesValidation="false"
                                CommandName="XcCmd09"
                                CommandArgument='<%# Eval("Id") %>'>
                                <span
                                    class ='<%# Eval("CssGlyphiconExpandCustomer") %>'
                                    title='<%# GetLocalResourceObject("XuFwExpandCustomerItem.Text") %>' 
                                    />
                            </asp:LinkButton>
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwExpandCustomer" />
                        <ItemStyle CssClass="XuFwExpandCustomer" />

                    </asp:TemplateField>


                    <%--Column Expand all Shipments below Customer Pickups below Forwarder Pickup --%>
                    <asp:TemplateField HeaderText="XuFwExpandShipment">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwExpandShipmentHeader" runat="server" Text="&nbsp;" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:LinkButton
                                ID="XuFwExpandShipmentItem"
                                runat="server"
                                CausesValidation="false"
                                CommandName="XcCmd08"
                                CommandArgument='<%# Eval("Id") %>'>
                                <span 
                                    class ='<%# Eval("CssGlyphiconExpandGroup") %>'
                                    title='<%# GetLocalResourceObject("XuFwExpandShipmentItem.Text") %>' 
                                    />
                            </asp:LinkButton>

                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwExpand" />
                        <ItemStyle CssClass="XuFwExpand" />

                    </asp:TemplateField>


                    <%--Column Address --%>
                    <asp:TemplateField HeaderText="XuFwAddress1">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwAddress1Header" runat="server" Text="XuFwAddress1Header" meta:resourcekey="XuFwAddress1Header" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuFwAddress1Item" runat="server"
                                ToolTip='<%# Bind("Address.CompareInfo") %>'
                                Text='<%#  Bind("Address.Address1") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwAddress1" />
                        <ItemStyle CssClass="XuFwAddress1" />

                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="XuFwAddress2">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwAddress2Header" runat="server" Text="XuFwAddress2Header" meta:resourcekey="XuFwAddress2Header" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuFwAddress2Item" runat="server"
                                Text='<%# Bind("Address.Address2") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwAddress2" />
                        <ItemStyle CssClass="XuFwAddress2" />

                    </asp:TemplateField>


                    <%--Column Pickup Window --%>
                    <asp:TemplateField HeaderText="XuFwPickup">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwPickupHeader" runat="server" Text="XuFwPickupHeader" meta:resourcekey="XuFwPickupHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <%--<asp:Label ID="XuFwPickupItem" runat="server"  Text='<%# Bind("PickupDate") %>'></asp:Label>--%>
                            <asp:Label ID="Label1" runat="server" Text='<%# XmGetPickupText( (string)GetLocalResourceObject("XuFwPickupItem.Format"), Eval("PickupDate"), Eval("TimeReady"), Eval("TimeClose")) %>'></asp:Label>
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

                        <HeaderStyle CssClass="XuFwPickup" />
                        <ItemStyle CssClass="XuFwPickup" />

                    </asp:TemplateField>


                    <%--Column Carrier --%>
                    <asp:TemplateField HeaderText="XuFwCarrier">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwCarrierHeader" runat="server" Text="XuFwCarrierHeader" meta:resourcekey="XuFwCarrierHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuFwCarrierItem" runat="server" Text='<%# Bind("CarrierNameList") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwCarrier" />
                        <ItemStyle CssClass="XuFwCarrier" />

                    </asp:TemplateField>


                    <%--Column Counters--%>
                    <asp:TemplateField HeaderText="XuFwCounter">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwCounterHeader" runat="server" Text="XuFwCounterHeader" meta:resourcekey="XuFwCounterHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuFwCounterItem" runat="server" Text='<%# string.Format("{0} ({1})", Eval("CountCustomers") , Eval("CountShipments") ) %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwCounter" />
                        <ItemStyle CssClass="XuFwCounter" />

                    </asp:TemplateField>


                    <%--Column Note --%>
                    <asp:TemplateField HeaderText="XuFwNote">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwNoteHeader" runat="server" Text="XuFwNoteHeader" meta:resourcekey="XuFwNoteHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuFwNoteItem" runat="server" Text='<%# Bind("Note") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwNote" />
                        <ItemStyle CssClass="XuFwNote" />

                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="XuFwPickupOperator">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwPickupOperatorHeader" runat="server" Text="XuFwPickupOperatorHeader" meta:resourcekey="XuFwPickupOperatorHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuFwPickupOperatorItem" runat="server" Text='<%# Bind("PickupOperator") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwPickupOperator" />
                        <ItemStyle CssClass="XuFwPickupOperator" />


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
                                Text='<%# GetLocalResourceObject("XuStatus-Text_StatusCode-" + Eval("PickupStatusForwarder") + ".Text") %>' 
                                ToolTip='<%# Bind("PickupStatusForwarder") %>'
                                />

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

                        <HeaderStyle CssClass="XuFwStatus" />
                        <ItemStyle CssClass="XuFwStatus" />

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

                        <HeaderStyle CssClass="XuFwEdit" />
                        <ItemStyle CssClass="XuFwEdit" />

                    </asp:TemplateField>


                    <%--Move Customer Pickup--%>
                    <asp:TemplateField HeaderText="XuFwMove">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwMoveHeader" runat="server" Text='<%# GetLocalResourceObject("XuFwMoveHeader.Text") %>' />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:LinkButton ID="XuFwMoveItem" CommandArgument='<%# Eval("Id") %>' runat="server" CausesValidation="false" CommandName="XcCmd06">
                                <span 
                                    class='<%# Eval("CssGlyphiconMove") %>'
                                    title='<%# GetLocalResourceObject("XuFwMove-Icon.Title") %>' 
                                />
                            </asp:LinkButton>
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwMove" />
                        <ItemStyle CssClass="XuFwMove" />

                    </asp:TemplateField>


                    <%------------ Customer ------------%>

                    <%--Column Customer  Details--%>
                    <asp:TemplateField HeaderText="XuCustomerDetails">
                        <HeaderTemplate>
                            <asp:Label runat="server" Text="&nbsp;" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--NOTE! this Column is mapped to a new row.--%>
                            <tr>
                                <td colspan="99">

                                    <%--Customer Pickup  GridView--%>
                                    <asp:GridView
                                        ID="XuCustomerPickup"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        ShowHeaderWhenEmpty="True"
                                        CssClass='<%# "XuCustomerPickup" + Eval("CssVisibleCustomer") %>'
                                        DataSource='<%# Bind("CustomerPickupList") %>'
                                        OnRowCommand="XuGridCustomerPickup_RowCommand">

                                        <HeaderStyle CssClass="XuCustomerPickupHeader" />
                                        <RowStyle CssClass="XuCustomerPickupRow" />

                                        <Columns>

                                            <%--Column filler column to allign with parent table--%>
                                            <asp:TemplateField HeaderText="XuCuFiller">
                                                <HeaderTemplate>
                                                    &nbsp;
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    &nbsp;&nbsp;&nbsp;
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuFiller" />
                                                <ItemStyle CssClass="XuCuFiller" />

                                            </asp:TemplateField>


                                            <%--Column Expand --%>
                                            <asp:TemplateField HeaderText="XuCuExpand">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuExpandHeader" runat="server" Text="&nbsp;" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:LinkButton ID="XuCuExpandItem" CommandArgument='<%# Eval("Id") %>' runat="server" CausesValidation="false" CommandName="XcCmd07">
                                                        <span 
                                                            class='<%# Eval("CssGlyphiconExpand") %>'
                                                            title='<%# GetLocalResourceObject("XuCuExpand-Icon.Title") %>' 
                                                        />
                                                    </asp:LinkButton>

                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuExpand" />
                                                <ItemStyle CssClass="XuCuExpand" />

                                            </asp:TemplateField>


                                            <%--Column Address --%>
                                            <asp:TemplateField HeaderText="XuCuAddress">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuAddressHeader" runat="server" Text="XuCuAddressHeader" meta:resourcekey="XuCuAddressHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="XuCuAddressItem" runat="server" Text='<%# Eval("Address.CompareInfo") %>' />
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuAddress" />
                                                <ItemStyle CssClass="XuCuAddress" />

                                            </asp:TemplateField>


                                            <%--Column Pickup Window --%>
                                            <asp:TemplateField HeaderText="XuCuPickup">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuPickupHeader" runat="server" Text="XuCuPickupHeader" meta:resourcekey="XuCuPickupHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <%--<asp:Label ID="XuCuPickupItem" runat="server"  Text='<%# Bind("PickupDate") %>'></asp:Label>--%>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# XmGetPickupText( (string)GetLocalResourceObject("XuCuPickupItem.Format"), Eval("PickupDate"), Eval("TimeReady"), Eval("TimeClose")) %>'></asp:Label>

                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuPickup ColPickup" />
                                                <ItemStyle CssClass="XuCuPickup ColPickup" />

                                            </asp:TemplateField>


                                            <%--Column Carrier --%>
                                            <asp:TemplateField HeaderText="XuCuCarrier">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuCarrierHeader" runat="server" Text="XuCuCarrierHeader" meta:resourcekey="XuCuCarrierHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="XuCuCarrierItem" runat="server" Text='<%# Bind("CarrierNameList") %>' />
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuCarrier ColCarrier" />
                                                <ItemStyle CssClass="XuCuCarrier ColCarrier" />

                                            </asp:TemplateField>


                                            <%--Column Counters--%>
                                            <asp:TemplateField HeaderText="XuCuCounter">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuCounterHeader" runat="server" Text="XuCuCounterHeader" meta:resourcekey="XuCuCounterHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="XuCuCounterItem" runat="server" Text='<%# Bind("CountShipments")  %>' />
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuCounter ColCounter" />
                                                <ItemStyle CssClass="XuCuCounter ColCounter" />

                                            </asp:TemplateField>


                                            <%--Column Note --%>
                                            <asp:TemplateField HeaderText="XuCuNote">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuNoteHeader" runat="server" Text="XuCuNoteHeader" meta:resourcekey="XuCuNoteHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="XuCuNoteItem" runat="server" Text='<%# Bind("Note") %>' />
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuNote" />
                                                <ItemStyle CssClass="XuCuNote" />

                                            </asp:TemplateField>


                                            <%--Column Status --%>
                                            <asp:TemplateField HeaderText="XuCuStatus">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuStatusHeader" runat="server" Text="XuCuStatusHeader" meta:resourcekey="XuCuStatusHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <%--
                                                        Status field contain 
                                                        * localized text (default to 
                                                        * Action
                                                    --%>
                                                    <asp:Label
                                                        ID="XuCuStatusItem"
                                                        runat="server"
                                                        Text='<%# GetLocalResourceObject("XuStatus-Text_StatusCode-" + Eval("PickupStatusCustomer") + ".Text") %>' />

                                                    <asp:LinkButton
                                                        ID="LinkButton12"
                                                        runat="server"
                                                        CausesValidation="false"
                                                        CommandName="XcCmd01"
                                                        CommandArgument='<%# Eval("PickupStatusForwarder") + "." + Eval("Id") %>'>
                                                        <span 
                                                            class='<%# "glyphicon " + GetLocalResourceObject("XuStatus-Icon_StatusCode-" + Eval("PickupStatusCustomer") + ".Glyphicon") %>' 
                                                            title='<%# GetLocalResourceObject("XuStatus-Icon_StatusCode-" + Eval("PickupStatusCustomer") + ".ToolTip") %>' 
                                                            />
                                                    </asp:LinkButton>

                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuStatus" />
                                                <ItemStyle CssClass="XuCuStatus" />

                                            </asp:TemplateField>


                                            <%--Column Move --%>
                                            <asp:TemplateField HeaderText="XuCuMove">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuMoveHeader" runat="server" meta:resourcekey="XuCuMoveHeader" Text="XuCuMoveHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:LinkButton ID="XuCuMoveItem" CommandArgument='<%# Eval("Id") %>' runat="server" CausesValidation="false" CommandName="XcCmd02">
                                                    <span 
                                                        class='<%# Eval("CssGlyphiconMove") %>' 
                                                        title='<%# GetLocalResourceObject("XuCuMove-Icon.Title") %>' 
                                                        />
                                                    </asp:LinkButton>

                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuMove" />
                                                <ItemStyle CssClass="XuCuMove" />

                                            </asp:TemplateField>


                                            <%--Column Customer Pickup Select--%>
                                            <asp:TemplateField HeaderText="XuCuSelect">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuSelectHeader" runat="server" Text="XuCuSelectHeader" meta:resourcekey="XuCuSelectHeader" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <input id="XuCuSelectItem" type="checkbox" runat="server" disabled='<%# Eval("CssDisabledCheckbox") %>' value='<%# Eval("Id") %>' />
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuSelect" />
                                                <ItemStyle CssClass="XuCuSelect" />

                                            </asp:TemplateField>

                                            <%------------ Shipment Details ------------%>

                                            <%--Column Shipment Details--%>
                                            <asp:TemplateField HeaderText="XuShipmentDetails">
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="&nbsp;" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%--NOTE! this Column is mapped to a new row.--%>
                                                    <tr>
                                                        <td colspan="10">

                                                            <asp:GridView
                                                                ID="XuShipmentDetails"
                                                                runat="server"
                                                                AutoGenerateColumns="False"
                                                                DataSource='<%# Bind("Shipmentlist") %>'
                                                                CssClass='<%# "XuShipmentDetails" + Eval("CssVisibleShipments") %>'>

                                                                <HeaderStyle CssClass="XuShipmentDetailsHead" />
                                                                <RowStyle CssClass="XuShipmentDetailsRow" />

                                                                <Columns>

                                                                    <%--filler column to allign with parent table--%>
                                                                    <asp:TemplateField HeaderText="XuCuFillerA">
                                                                        <HeaderTemplate>
                                                                            &nbsp;
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            &nbsp;
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuCuFillerA" />
                                                                        <ItemStyle CssClass="XuCuFillerA" />

                                                                    </asp:TemplateField>


                                                                    <%--filler column to allign with parent table--%>
                                                                    <asp:TemplateField HeaderText="XuCuFillerB">
                                                                        <HeaderTemplate>
                                                                            &nbsp;
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            &nbsp;
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuCuFillerB" />
                                                                        <ItemStyle CssClass="XuCuFillerB" />

                                                                    </asp:TemplateField>


                                                                    <%--Column Waybill number --%>
                                                                    <asp:TemplateField HeaderText="XuShWaybillnumber">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="XuShWaybillnumberHead" runat="server" Text="XuShWaybillnumberHead" meta:resourcekey="XuShWaybillnumberHead" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="XuShWaybillnumberItem" runat="server" Text='<%# Bind("Waybillnumber") %>' ToolTip='<%# Bind("Address.CompareInfo") %>' />
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuShWaybillnumber" />
                                                                        <ItemStyle CssClass="XuShWaybillnumber" />

                                                                    </asp:TemplateField>


                                                                    <%--Column Carrier --%>
                                                                    <asp:TemplateField HeaderText="XuShCarrier">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="XuShCarrierHeader" runat="server" Text="XuShCarrierHeader" meta:resourcekey="XuShCarrierHeader" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="XuShCarrierItem" runat="server" Text='<%# Bind("CarrierName") %>' ToolTip='<%# "Customer/Account" + Eval("CustomerId") + "/" + Eval("CustomerAccountId") %>' />
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuShCarrier" />
                                                                        <ItemStyle CssClass="XuShCarrier" />

                                                                    </asp:TemplateField>


                                                                    <%--Column Weight --%>
                                                                    <asp:TemplateField HeaderText="XuShWeight">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="XuShWeightHeader" runat="server" Text="XuShWeightHeader" meta:resourcekey="XuShWeightHeader" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="XuShWeightItem" runat="server" Text='<%# Bind("Weight") %>' />
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuShWeight" />
                                                                        <ItemStyle CssClass="XuShWeight" />

                                                                    </asp:TemplateField>


                                                                    <%--Column Dimension --%>
                                                                    <asp:TemplateField HeaderText="XuShDimension">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="XuShDimensionHeader" runat="server" Text="XuShDimensionHeader" meta:resourcekey="XuShDimensionHeader" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="XuShDimensionItem" runat="server" Text='<%# Bind("Dimensions") %>' />
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuShDimension" />
                                                                        <ItemStyle CssClass="XuShDimension" />

                                                                    </asp:TemplateField>


                                                                    <%--Column Select --%>
                                                                    <asp:TemplateField HeaderText="XuShSelect">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="XuShSelectHeader" runat="server" Text="XuShSelectHeader" meta:resourcekey="XuShSelectHeader" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <input id="XuShSelectItem" type="checkbox" runat="server" disabled='<%# Eval("CssDisabledCheckbox") %>' value='<%# Eval("Id") %>' />
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuShSelect" />
                                                                        <ItemStyle CssClass="XuShSelect" />

                                                                    </asp:TemplateField>

                                                                    <%--Column Shipment filler column to allign with parent table --%>
                                                                    <asp:TemplateField HeaderText="XuShFiller">
                                                                        <HeaderTemplate>
                                                                            &nbsp;&nbsp;&nbsp;
                                                                        </HeaderTemplate>
                                                                        <InsertItemTemplate>
                                                                            &nbsp;&nbsp;&nbsp;
                                                                        </InsertItemTemplate>

                                                                        <HeaderStyle CssClass="XuShFiller" />
                                                                        <ItemStyle CssClass="XuShFiller" />

                                                                    </asp:TemplateField>

                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>

                                                <%--Do not show this as the last Column of Customer Pickup row--%>
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />

                                            </asp:TemplateField>

                                        </Columns>

                                    </asp:GridView>

                                    <%--Customer Pickup  -- END --  GridView--%>
                                </td>
                            </tr>
                        </ItemTemplate>

                        <%--Do not show this as the last Column of Forwarder Pickup row --%>
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden" />

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>


        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
